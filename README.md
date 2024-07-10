# iPractice Technical Assignment Project 📚

This repository hosts the project for the technical assignment, which is a simple ASP.NET Core API utilizing SQLite as the database.

The project features:
- An initial migration to establish the database and populate it with predefined data. 🧱
- Additional migrations to facilitate booking and availability tables. 📅
- Two controllers with the requested endpoints. 🚀
- Models representing ORM mapped entities. 📦
- A service managing availability and booking related operations. 👩‍💻
- Documentation and failure mode handling. 📖

The project was upgraded to .NET 8.0 and a test project structure was created, but the quality assurance framework was not fully implemented. The project was developed on M1 Silicon, necessitating a launch configuration change. 🍎

The project is not optimized and is not suitable for production use. Potential areas for enhancement include optimizing queries, entities, and representations, such as storing availability slices instead of computing them on demand. 🚧

The project has been refactored to adhere to Domain-Driven Design (DDD) principles, ensuring a more structured and maintainable architecture. Key changes include:

1. **Domain Modeling**: Entities, Value Objects, and Repositories have been introduced to encapsulate business logic and ensure data integrity. This includes the `Psychologist`, `TimeSlot`, `Availability`, and `Booking` entities, as well as the `IPsychologistRepository` interface.
2. **Service Layer**: The `AvailabilityService` and `AppointmentService` have been created to manage complex business operations, such as creating and updating availability, and booking appointments. These services encapsulate the logic for interacting with the domain model and repositories.
3. **Repository Pattern**: The `IPsychologistRepository` interface and its implementation ensure data access and manipulation are decoupled from the business logic, promoting flexibility and testability.
5. **Value Objects**: Value objects like `TimeSlot` and `Availability` have been introduced to encapsulate immutable data and ensure consistency in the domain model.
6. **Aggregate Roots**: The `Psychologist` entity has been identified as an aggregate root, ensuring that all changes to the psychologist's state are managed through this entity, promoting data consistency and integrity.

These changes have resulted in a more maintainable, scalable, and flexible architecture that adheres to DDD principles, enabling the system to better model the complexities of the problem domain.



