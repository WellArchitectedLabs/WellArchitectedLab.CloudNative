variable "environment" {
  type        = string
  description = "Environment in which resources are deployed"
}

variable "region" {
  type        = string
  description = "Region in which resource is deployed"
}

variable "resource_group_name" {
  type        = string
  description = "Resource Group in which we store the compute components"
}

variable "subscription" {
  type        = string
  description = "Azure subscription in which resources will be billed"
}