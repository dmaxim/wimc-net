resource "azurerm_maps_account" "example" {
  name                = "example-maps-account"
  resource_group_name = azurerm_resource_group.example.name
  sku_name            = "S1"

  tags = {
    environment = "Test"
  }
}