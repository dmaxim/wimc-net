resource "azurerm_notification_hub" "example" {
  name                = "mynotificationhub"
  namespace_name      = azurerm_notification_hub_namespace.example.name
  resource_group_name = azurerm_resource_group.example.name
  location            = azurerm_resource_group.example.location
}