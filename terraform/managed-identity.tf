
# Create a managed identity

resource "azurerm_user_assigned_identity" "app-identity" {
  resource_group_name = azurerm_resource_group.wimc-net-rg.name
  location            = azurerm_resource_group.wimc-net-rg.location

  name = join("-", ["mi", var.namespace, var.environment])
}

resource "azurerm_key_vault_access_policy" "app-identity-vault-access" {
  key_vault_id = azurerm_key_vault.winc-net-app-vault.id
  tenant_id    = var.azure-tenant-id
  object_id    = var.managed-identity-object-id # TODO:  Determine how to get this after creating the identity

  secret_permissions = ["Get", "List"]
}
