terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=4.1.0"
    }
    random = {
      source = "hashicorp/random"
      version = "~> 3.1"
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

resource "random_string" "acr_suffix" {
  length = 6
  special =  false
  lower = true
  numeric = true
}

resource "azurerm_container_registry" "acr-kubernetes-001" {
  name = "acr${var.environment}${random_string.acr_suffix.result}"
  sku = "Basic"
  resource_group_name = var.resource_group_name
  location = var.region
  admin_enabled = true
}

resource "azurerm_kubernetes_cluster" "aks-kubernetes-001" {
  name                = "aks-${var.environment}-001"
  location            = var.region
  resource_group_name = var.resource_group_name
  default_node_pool {
    name    = "npkube001"
    vm_size = "standard_a2_v2"
    node_count = 1
  }
  dns_prefix = "kubernetes"
  identity {
    type = "SystemAssigned"
  }
  depends_on = [ azurerm_container_registry.acr-kubernetes-001 ]
}

resource "azurerm_kubernetes_cluster_node_pool" "npuser001" {
  kubernetes_cluster_id = azurerm_kubernetes_cluster.aks-kubernetes-001.id
  name = "npuser001"
  node_taints = [ "workload=backend:NoSchedule" ]
  node_count = 2
  depends_on = [ azurerm_kubernetes_cluster.aks-kubernetes-001 ]
  vm_size = "Standard_D4ds_v5"
}
resource "azurerm_kubernetes_cluster_node_pool" "npuser002" {
  kubernetes_cluster_id = azurerm_kubernetes_cluster.aks-kubernetes-001.id
  name = "npuser002"
  node_taints = [ "workload=frontend:NoSchedule" ]
  node_count = 2
  depends_on = [ azurerm_kubernetes_cluster.aks-kubernetes-001, azurerm_kubernetes_cluster_node_pool.npuser001 ]
  vm_size = "Standard_D4ds_v5"
}

resource "azurerm_role_assignment" "cluster-registry-access" {
  principal_id = azurerm_kubernetes_cluster.aks-kubernetes-001.kubelet_identity[0].object_id
  scope = azurerm_container_registry.acr-kubernetes-001.id
  role_definition_name = "AcrPull"
  depends_on = [ azurerm_container_registry.acr-kubernetes-001, azurerm_kubernetes_cluster.aks-kubernetes-001 ]
  skip_service_principal_aad_check = true
}