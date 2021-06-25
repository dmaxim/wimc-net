
# Create a managed identity

resource "azurerm_user_assigned_identity" "app-identity" {
  resource_group_name = azurerm_resource_group.wimc-net-rg.name
  location            = azurerm_resource_group.wimc-net-rg.location

  name = join("-", ["mi", var.namespace, var.environment])
}

resource "azurerm_key_vault_access_policy" "app-identity-vault-access" {
  key_vault_id = azurerm_key_vault.winc-net-app-vault.id
  tenant_id    = var.azure-tenant-id
  object_id    = azurerm_user_assigned_identity.app-identity.principal_id # var.managed-identity-object-id # TODO:  Determine how to get this after creating the identity az identity show -g rg-mxinfo-wimc-net-dev -n mx-mxinfo-wimc-net-dev

  secret_permissions = ["Get", "List"]
}



data "azurerm_subscription" "primary" {

}

/*

The SP for Terraform does not have permission to add the role - need to determine how to grant this permission
az identity list --resource-group rg-mxinfo-wimc-net-dev
az role assignment create --role "Managed Identity Operator" --assignee 4ea83a06-0e6a-4b34-bdb0-e03d7a720bec --scope /subscriptions/bb0c99b7-d44d-413a-b294-564466712637/resourcegroups/rg-mxinfo-wimc-net-dev/providers/Microsoft.ManagedIdentity/userAssignedIdentities/mi-mxinfo-wimc-net-dev
manually added via 
resource "azurerm_role_assignment" "app-identity-role" {
  scope                = data.azurerm_subscription.primary.id
  role_definition_name = "Managed Identity Operator"
  principal_id         = azurerm_user_assigned_identity.app-identity.principal_id
}
*/



