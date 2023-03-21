# Checkout Service
This service is built using the following approaches:
* Onion Architecture
* Doman Driven Design - Tactical design to create the Domain Model
* CQRS implemented using MediatR

## Domain
The domain layer Contains a domain model which is designed using DDD tactical design patterns (entities, value objects, aggregates, domain events...).
This layer is independent and does not have reference to any other layer.

![Image](img/domain_class_diagram.png "Class Diagram") 

## Application
The application layer contains an implementation of commands,  queries, and as well as corresponding handlers for commands and queries. Also, it contains abstraction (interfaces) for repositories.
This layer has a reference only to the Domain.

## Infrastructure
The infrastructure layer contains an implementation of Repositories.
This layer can have references to the Domain and the Application layers.


## Sequence diagram

### Create Draft Order - Command 

```mermaid
sequenceDiagram;
    participant C as Client;
    participant Api as Api
    participant M as MediatR
    participant CH as CommandHandler
    participant R as Repository

    C->>Api: Create Draft Order
    Api->>Api: Create CreateDraftOrderCommand
    Api->>M: Send CreateDraftOrderCommand
    M->>CH: Call CreateDraftOrderCommand Handler
    CH->>R: Save Draft Order
    Api->>C: 201:Created
```



### Get Order - Query

```mermaid
sequenceDiagram;
    participant C as Client;
    participant Api as Api
    participant M as MediatR
    participant QH as QueryHandler
    participant R as Repository

    C->>Api: Create Draft Order
    Api->>Api: Create GetOrderQuery
    Api->>M: Send GetOrderQuery
    M->>QH: Call GetOrderQuery Handler
    QH->>R: Get Order
    Api->>C: 200:Order
```
