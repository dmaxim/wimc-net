# Resource group
resource "azurerm_resource_group" "wimc-net-rg" {
  name     = join("-", ["rg", var.namespace, var.environment])
  location = var.location
}


# Create Azure Storage for DAPI
resource "azurerm_storage_account" "wimc-net-storage" {
  name                     = join("", [var.application-storage-account, var.environment])
  resource_group_name      = azurerm_resource_group.wimc-net-rg.name
  location                 = azurerm_resource_group.wimc-net-rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
  min_tls_version          = "TLS1_2"

  tags = {
    environment = var.environment
  }
}

# Create Azure AD App Registration for the application

resource "azuread_application" "winc-net-app" {
  display_name               = join("-", [var.namespace, var.environment])
  homepage                   = var.azure-ad-app-url
  reply_urls                 = [var.azure-ad-app-url]
  oauth2_allow_implicit_flow = true
  type                       = "webapp/api"
  group_membership_claims    = "All"
}


resource "azuread_service_principal" "winc-net-app" {
  application_id = azuread_application.winc-net-app.application_id
}

/*
resource "azuread_application_password" "app-service-test" {
    application_object_id   = azuread_application.app-service-test.id
    description             = "application-secret"
    value                   = var.azure-ad-app-client-secret
    end_date                = "2099-01-01T01:01:01Z"
}

resource "azuread_service_principal_password" "app-service-test" {
    service_principal_id = azuread_service_principal.app-service-test.id
    value = var.azure-ad-app-client-secret
    end_date = "2022-09-15T01:02:03Z"
}

*/
# Create Key Vault

resource "azurerm_key_vault" "winc-net-app-vault" {
  name                        = join("-", ["kv", var.namespace, var.environment])
  resource_group_name         = azurerm_resource_group.wimc-net-rg.name
  location                    = azurerm_resource_group.wimc-net-rg.location
  enabled_for_disk_encryption = false
  tenant_id                   = var.azure-tenant-id

  sku_name = "standard"

  access_policy {
    tenant_id = var.azure-tenant-id
    object_id = azuread_service_principal.winc-net-app.id

    secret_permissions = [
      "get",
      "list"
    ]

    key_permissions = [
      "Get",
      "List",
      "WrapKey",
      "UnwrapKey",
      "Sign",
      "Verify"
    ]
  }

  tags = {
    environment = var.environment
  }
}


# Create DAPI Key


resource "azurerm_key_vault_key" "wimc-dapi" {
  name         = "wimc-dapi"
  key_vault_id = azurerm_key_vault.winc-net-app-vault.id
  key_type     = "RSA"
  key_size     = 4096
  key_opts = [
    "decrypt",
    "encrypt",
    "sign",
    "unwrapKey",
    "verify",
    "wrapKey"
  ]
}


