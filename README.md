# WorkshopMaster – .NET 8 Backend API

Backend-API för **WorkshopMaster**, ett boknings- och verkstadsystem för bilverkstad (likt GLS Verkstad).  
Byggt för att visa upp en modern .NET-backend med tydlig lagerindelning, SQL Server, validering, tester och CI via GitHub Actions.

> Frontend (React/Vite) ligger i ett separat repo och pratar med detta API.

---

## Innehåll

- [Översikt](#översikt)
- [Teknikstack](#teknikstack)
- [Arkitektur](#arkitektur)
- [Domänmodell](#domänmodell)
- [Funktionalitet](#funktionalitet)
- [Komma igång](#komma-igång)
- [API-endpoints](#api-endpoints)
- [Validering & felhantering](#validering--felhantering)
- [Loggning](#loggning)
- [Tester](#tester)
- [CI / GitHub Actions](#ci--github-actions)
- [Kända begränsningar & förbättringar](#kända-begränsningar--förbättringar)

---

## Översikt

WorkshopMaster-backenden är ett **RESTful .NET 8 API** som hanterar:

- Kunder och deras fordon  
- Tjänster (service-typer) som verkstaden erbjuder  
- Bokningar kopplade till fordon  
- Dashboard-statistik (antal bokningar, kunder, intäkter m.m.)

Applikationen är byggd för att efterlikna hur en juniorutvecklare skulle strukturera ett **skarpt backendprojekt**:  
tydliga lager, separerad domänlogik, validering, global felhantering, tester och CI.

---

## Teknikstack

**Backend**

- .NET 8 Web API
- C#
- ASP.NET Core MVC Controllers

**Data**

- SQL Server
- Entity Framework Core (code-first, migrationer)
- Relationsmodell:
  - One-to-many: `Customer → Vehicle`, `Vehicle → Booking`
  - Many-to-many: `Booking ↔ ServiceType` via `BookingServiceType`

**Övrigt**

- FluentValidation (inputvalidering)
- AutoMapper (DTO ↔ entities)
- Global error handler & custom exceptions
- xUnit / liknande för enhetstester
- GitHub Actions för CI (restore, build, test)

---

## Arkitektur

Projektet följer en **Clean-ish Architecture / Service-Repository** struktur.

```text
src/
  WorkshopMaster.Domain/        # Domänentiteter, affärslogik nära modellen
  WorkshopMaster.Application/   # DTOs, services, interfaces, validators, mapping
  WorkshopMaster.Infrastructure/# EF Core, DbContext, repository-implementationer, seed
  WorkshopMaster.Api/           # Web API, controllers, DI, middleware, startup

tests/
  WorkshopMaster.Application.Tests/  # Enhetstester för services m.m.
