resource "azurerm_sql_server_firewall_rule" "example" {
  name = "firewallRule"
  resource_group_name = azurerm_resource_group.example.name
  server_name = azurerm_sql_server.example.name
  start_ip_address = "10.0.0.1"
  end_ip_address = "10.0.0.1"
  
}