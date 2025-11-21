terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=4.1.0"
    }
  }
  backend "azurerm" {
    key                  = "kubernetes.terraform.tfstate"
    storage_account_name = "sttbasics001"
    resource_group_name  = "rg-global"
    container_name       = "tfstate"
  }
}

provider "azurerm" {
  features {}
  subscription_id = var.subscription
}

resource "azurerm_resource_group" "rg-kubernetes-001" {
  name     = "rg-kubernetes-${var.environment}-001"
  location = var.region
}