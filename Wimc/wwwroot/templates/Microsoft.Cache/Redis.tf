
resource "azurerm_redis_cache" "redis_cach" {

  capacity = 0
  family = ""
  location = ""
  name = ""
  resource_group_name = ""
  sku_name = ""
  minimum_tls_version = "1.2"
  enable_non_ssl_port = false
  
  redis_configuration {
    
  }
}
