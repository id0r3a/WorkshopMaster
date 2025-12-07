WorkshopMaster – Backend API 🚗🔧
Ett komplett backend-API för ett modernt boknings- och verkstadsadministrationssystem. Byggt med .NET 8, SQL Server och tydlig lagerindelning. API:et används av frontenden för att hantera kunder, fordon, bokningar och verkstadsstatus.

🌟 Vad systemet gör

WorkshopMaster är kärnan i ett digitalt verkstadsflöde:

👤 Kundhantering

Skapa, uppdatera och lista kunder

Unik e-postvalidering

🚗 Fordon

Flera fordon per kund

Validering av registreringsnummer + modell/brand

📅 Bokningar

Skapa bokningar med datum, starttid och automatisk end-time

Hantera status: Pending, Confirmed, Completed, Cancelled

Filtrering på status, registreringsnummer och datum

📊 Dashboard

Öppna ordrar

Slutförda denna vecka

Omsättning 30 dagar

Antal kunder

All logik följer Clean Architecture-principer, så varje lager är isolerat och testbart.

🧱 Arkitektur

src/

Domain – entiteter & regler

Application – tjänster, DTOs, validering

Infrastructure – EF Core, DbContext, repos

Api – controllers, DI, middleware

SQL Server med EF Core 8 migrationer.
Relationer:

Customer → Vehicles (1-many)

Vehicle → Bookings (1-many)

Booking ↔ ServiceType (many-many)

🚀 Kom igång

Klona projektet
git clone https://github.com/
<your-backend-repo>.git

Installera
dotnet restore

Skapa databas
dotnet ef database update

Starta API
dotnet run

✔ API: http://localhost:5222

✔ Swagger: /swagger

📌 Endpoints (kort version)

Customers: CRUD
Vehicles: CRUD + by-customer
Bookings: CRUD + status-PATCH + filtrering
Dashboard: statistik-endpoint

🧪 Tester

Ligger under /tests
Innehåller:

Enhetstester

Logiktester

Integrationstester (VG-krav)

Kör tester:
dotnet test

⚙️ GitHub Actions (CI)

Pipeline kör:

restore

build

test

Workflow: .github/workflows/dotnet-ci.yml
