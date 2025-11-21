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
  subscription_id = local.subscription
}

module "global" {
  source       = "../../global"
  region       = local.region
  subscription = local.subscription
  environment  = local.environment
}

module "compute" {
  source              = "../../compute"
  region              = local.region
  subscription        = local.subscription
  resource_group_name = module.global.resource_group_name
  environment         = local.environment
}