
resource "azurerm_relay_hybrid_connection" "hc" {
  name = "name"
  resource_group_name = "resource_group"
  relay_namespace_name = azurerm_relay_namespace.example.name
  user_metadata = "metadata"
}


resource "azurerm_app_service_hybrid_connection" "hc_app" {
  app_service_name = azurerm_app_service.example.name
  resource_group_name = "resource_group"
  relay_id = azurerm_relay_hybrid_connection.hc.id
  hostname = "hostname"
  port = 8080
  send_key_name = "exampleSharedAccessKey"
}