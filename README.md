# Cach Machine System

This project is an implementation of an ATM system using a hexagonal (port-adapter) architecture. The main focus is on multilayer architecture, anemic data model and separation of business logic from infrastructure code.

The application supports both user and administrator modes and implements a console interface for interaction.

# Technologies

- Language: C#
- Database: PostgreSQL (Npgsql)
- Containerization: docker-compose
- Testing and mocks: XUnit, NSubstitute
- Console UI: Spectre.Console

# Functionality

- Create an account
- View balance
- Withdrawal and replenishment of funds
- View transaction history
- Enter PIN-code for users
- Enter system password for administrator
- Operations validation and error handling
- Data persistence with PostgreSQL
