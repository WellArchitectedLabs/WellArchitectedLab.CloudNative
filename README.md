# WellArchitectedLab.CloudNative

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
    - Terraform command line tool installed
    - dotnet 8 or above
    - npm 10.9.3 or above
    - Helm command (using Winget utility for windows)

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

4. **Cluster configuration**

**Azure Container Registry attachment**

Since this repo is only for demo purposes, and due to azure budget optimization, we have not configured private endpoint access from AKS cluster to the Azure Container Registry, which require Premium tier for private endpoints integration.
For the AKS cluster to access the docker image, we have allowed public access to the ACR registry (again, this is not the recommanded best practise), which direct ACR attachment to the AKS cluster using the az aks --attach-acr command option.
Following is a sample command:
    ```
    az aks update -n aks-dev-001 -g rg-kubernetes-dev-001 --attach-acr <acr-name-or-resource-id>
    ```

This configuration, coupled with the terraform's role based access (AcrPull access) will allow the worker nodes to pull the images that are pushed into the ACR container registry.

**Argo CD installation and usage**

- Please install argocd using helm command line (```helm repo add argocd <argocd-url>``` followed by ```helm update repo argocd```).
- Once installed, a service called argocd-server will run under argocd namespace. Please run the application manifest under gitops folder.

Once installed, you can apply the application file located under gitops folder (named gitops_argocd-application.yaml).
This will ensure your application will appear in your argocd-server.

In order for the kustomization logic to execute correctly on argocd server level, you can appply the kustomizations executing ```kubectl apply -k .``` command under the overloay/dev folder. The command will read the kustomization.yaml file under this folder then apply the otehr kustomizations in cascade for the referenced resource folders.

5. **Continous Integration / Continous Delivery**

I didn't include a CI/CD pipeline for the moment (Since I needed to explore kubernetes as part of this repo rather than github actions).

In order to integrate with the kubernetes cluster, I push docker images into a docker registry that is created via the iac sub-project.

In order to push and update the docker images, please configure you local docker client (I personally use podman as a replacement of docker which is a bit hard to configure on my windows machine).
You will to use two commands:

- Place you command line prompt under the DockerFile folder location and run ```docker build -t <registry-url>/<image-name>:vx.x .``` in order to create an image taged with version x.x.
- When you package is ready to deploy, push it in the created registry as part of terraform using ```docker push <registry-url>/<image-name>:vx.x```.

## Running solution locally

- Place command line under backend API src folder then run ```dotnet run```. Once backend is ran, in another command line, run ```npm run dev``` which will automatically point to the local backend's address. Please verify correct backend url in frontend's yaml configuration file.

## Contributing

Please follow standard Git practices and ensure all deployments are tested before submitting pull requests.

## License

This project is for training purposes only.
