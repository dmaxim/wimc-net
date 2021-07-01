resource "azurerm_app_service_certificate" "example" {
  name                = "example-cert"
  resource_group_name = azurerm_resource_group.example.name
  location            = azurerm_resource_group.example.location
  pfx_blob            = filebase64("certificate.pfx")
  password            = "terraform"
}