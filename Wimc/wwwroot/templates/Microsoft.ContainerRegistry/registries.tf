
resource "azurerm_container_registry" "acr" {
  name = "containerRegistry"

  location = ""
  resource_group_name = ""
  sku = "Basic"
  admin_enabled = false
}