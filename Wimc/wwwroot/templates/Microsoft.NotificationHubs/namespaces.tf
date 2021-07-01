resource "azurerm_notification_hub_namespace" "example" {
name                = "myappnamespace"
resource_group_name = azurerm_resource_group.example.name
location            = azurerm_resource_group.example.location
namespace_type      = "NotificationHub"

sku_name = "Free"
}