# Project: Saga with MassTransit and RabbitMQ

This project demonstrates the use of the Saga pattern with MassTransit and RabbitMQ in an event-driven architecture for orchestrating distributed processes.

## 📌 Prerequisites

Before running the project, ensure you have installed:

- .NET 9 SDK
- Docker

## 🚀 Setup and Execution

### 1️⃣ Start RabbitMQ and PostgreSQL with Docker

Run the following commands to start RabbitMQ and PostgreSQL in containers:

```sh
# RabbitMQ with web management on port 15672
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 \
  -e RABBITMQ_DEFAULT_USER=guest \
  -e RABBITMQ_DEFAULT_PASS=guest \
  rabbitmq:3-management

# PostgreSQL on port 5432 with default user and database
docker run -d --name postgres -p 5432:5432 \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  -e POSTGRES_DB=saga_db \
  postgres:latest
```


## 🔍 Verification

- RabbitMQ Dashboard: [http://localhost:15672](http://localhost:15672) (User: `guest`, Password: `guest`).
- PostgreSQL: You can connect using a client like DBeaver or `psql`.

## 📌 Additional Notes

This project is a demonstration of the Saga pattern and MassTransit, focusing on functionality rather than optimization.

You can customize the implementation according to your needs, adjusting persistence, event patterns, and scalability.

