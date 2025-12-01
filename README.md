# Trainings.Kubernetes

A comprehensive training repository demonstrating front-to-back application deployment on Azure Kubernetes Service (AKS) infrastructure.

## Overview

This repository provides practical examples and hands-on training for deploying applications to AKS, covering infrastructure provisioning, application development, and Kubernetes orchestration.

## Repository Structure

### 📁 k8s/
Contains Kubernetes manifests and deployment configurations:
- Deployment configurations
- Service definitions
- ConfigMaps and Secrets
- Ingress controllers / gateway apis
- Http routes
- Monitoring and logging setup

### 📁 iac/
Infrastructure as Code (IaC) templates:
- Terraform/ARM templates for AKS cluster provisioning
- Network configuration
- Security policies and RBAC
- Azure resource definitions

### 📁 src/
Application source code:
- Frontend applications
- Backend services
- API implementations
- Configuration files

## Getting Started

1. **Prerequisites**
    - Azure CLI installed and configured
    - kubectl configured for AKS access
    - Docker installed for container builds
    - dotnet 8 or above

2. **Infrastructure Setup**

    ```bash
    cd iac/
    # Follow infrastructure deployment instructions
    ```

3. **Application Deployment**

    ```bash
    cd k8s/
    kubectl apply -f .
    ```
4. **Azure Container Registry X Kubernetes attachment**

Since this repo is only for demo purposes, and due to azure budget optimization, we have not configured private endpoint access from AKS cluster to the Azure Container Registry, which require Premium tier for private endpoints integration.
For the AKS cluster to access the docker image, we have allowed public access to the ACR registry (again, this is not the recommanded best practise), which direct ACR attachment to the AKS cluster using the az aks --attach-acr command option.
Following is a sample command:
    ```bash
    az aks update -n aks-dev-001 -g rg-kubernetes-dev-001 --attach-acr <acr-name-or-resource-id>
    ```

This configuration, coupled with the terraform's role based access (AcrPull access) will allow the worker nodes to pull the images that are pushed into the ACR container registry.

4. **Continous Integration / Continous Delivery**

I didn't include a CI/CD pipeline for the moment (Since I needed to explore kubernetes as part of this repo rather than github actions).

In order to integrate with the kubernetes cluster, I push docker images into a docker registry that is created via the iac sub-project.

In order to push and update the docker images, please configure you local docker client (I personally use podman as a replacement of docker which is a bit hard to configure on my windows machine).
You will to use two commands:

- Place you command line prompt under the DockerFile folder location and run ```docker build -t <registry-url>/<image-name>:vx.x .``` in order to create an image taged with version x.x.
- When you package is ready to deploy, push it in the created registry as part of terraform using ```docker push <registry-url>/<image-name>:vx.x```.

## Contributing

Please follow standard Git practices and ensure all deployments are tested before submitting pull requests.

## License

This project is for training purposes only.