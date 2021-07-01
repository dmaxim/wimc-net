resource "azurerm_network_watcher" "example" {
  name                = "production-nwwatcher"
  location            = azurerm_resource_group.example.location
  resource_group_name = azurerm_resource_group.example.name
}