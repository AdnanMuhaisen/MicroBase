# MicroBase
MicroBase is a high-performance system built using a microservices architecture. It enables users to navigate and manage a set of platforms and their commands. The system incorporates a range of modern technologies to ensure scalability, reliability, and performance.

## Features
- Microservices Architecture: Each service is independently developed, deployed, and maintained.
- Asynchronous Messaging: Uses RabbitMQ as a message bus for asynchronous communication between services.
- Fast Data Communication: gRPC is employed for efficient and performant communication between services.
- Containerization & Orchestration: All services are containerized using Docker and orchestrated using Kubernetes for scalability and resource management.
- RESTful API: Services follow the REST architectural style for communication.
- Database Management: SQL Server is used as the database storage solution.

## Technologies Used
- .NET: Core framework for developing services.
- ASP.NET: Web API for the RESTful communication.
- RabbitMQ: Message bus for asynchronous messaging.
- gRPC: Fast, efficient protocol for service-to-service communication.
- Docker: Containerization platform for consistent deployments.
- Kubernetes: Orchestration platform for managing Docker containers at scale.
- SQL Server: Relational database for data storage.
- NGINX: Configured as an ingress controller and API gateway for routing and load balancing.

## Architecture Overview
MicroBase is designed as a distributed system where each service is deployed in its own container, allowing for independent scaling and updating. The communication between services is handled via RabbitMQ and gRPC, ensuring fast and asynchronous messaging. With Kubernetes managing the deployment of these containers, the system is robust, scalable, and easy to maintain.

## Key Components:
- RabbitMQ: Acts as the message broker to decouple services and enable asynchronous communication.
- gRPC: Used for lightweight and fast service-to-service communication.
- Docker: Each service is containerized, ensuring a consistent environment across development and production.
- Kubernetes: Provides orchestration, including service discovery, load balancing, and self-healing of services.
- Ingress NGINX: Acts as an API Gateway, managing incoming traffic and ensuring smooth service navigation.

## Getting Started
Prerequisites
To run this project, you'll need the following installed:
.NET SDK
Docker
Kubernetes
SQL Server

## Installation
Clone the repository:
> git clone https://github.com/your-username/microbase.git
> 
Navigate to the project directory:
Build and run the services using Docker and Kubernetes:
navigate the K8S folder and run the following command for each yaml file
> kubectl apply -f [file.yaml]
> 
Contributing
Feel free to fork this repository and create pull requests to improve the project. Contributions are welcome!
