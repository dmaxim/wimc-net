resource "azurerm_app_service_plan" "app-service-test" {
  name                = join("-", ["plan", var.namespace, var.environment])
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location


  sku {
    tier = "Standard"
    size = "S1"

  }

  tags = {
    environment = var.environment
  }
}