# Resource group
resource "azurerm_resource_group" "wimc-net-rg" {
  name     = join("-", ["rg", var.namespace, var.environment])
  location = var.location
}


# Create Azure Storage for DAPI


# Create Key Vault


# Create DAPI Key
