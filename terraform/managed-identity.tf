# Create a managed identity

resource "azurerm_user_assigned_identity" "app-identity" {
  resource_group_name = azurerm_resource_group.wimc-net-rg.name
  location            = azurerm_resource_group.wimc-net-rg.location

  name = join("-", ["mi", var.namespace, var.environment])
}