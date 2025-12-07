🚗 WorkshopMaster – Backend API (.NET 8)

Backend-API för WorkshopMaster, ett komplett boknings- och verkstadssystem för bilservice.
API:t hanterar all affärslogik, datalagring, validering, statistik och integration mot SQL Server.

Byggt för att demonstrera ren arkitektur, professionell API-design, EF Core, enhetstester och CI/CD.

🧱 Arkitekturöversikt
Domain – entiteter och kärnregler

Application – tjänster, DTO:er, validering

Infrastructure – EF Core, databasåtkomst

API – endpoints, swagger, global error handler

⚙️ Komma igång
1️⃣ Klona repo
git clone <REPO_URL>
cd WorkshopMaster

2️⃣ Installera beroenden
dotnet restore

3️⃣ Databasinställning

API:t använder SQL Server.
Redigera connection string i:
WorkshopMaster.Api/appsettings.Development.json

🗄️ Skapa databasen
Alternativ A – via EF Core migrations
cd WorkshopMaster.Api
dotnet ef database update

Alternativ B – SQL-script

I root-mappen ligger:
database.sql
Öppna i SSMS → Kör.

▶️ Starta API
cd WorkshopMaster.Api
dotnet run
API:n körs på:
http://localhost:5222
Swagger: http://localhost:5222/swagger

📡 API Endpoints (översikt)
👤 Customers

GET /api/Customers

GET /api/Customers/{id}

POST /api/Customers

PUT /api/Customers/{id}

DELETE /api/Customers/{id}

🚘 Vehicles

GET /api/Vehicles

GET /api/Vehicles/{id}

GET /api/Vehicles/by-customer/{customerId}

POST /api/Vehicles

PUT /api/Vehicles/{id}

DELETE /api/Vehicles/{id}

🔧 Service Types

GET /api/ServiceTypes

POST /api/ServiceTypes

PUT /api/ServiceTypes/{id}

DELETE /api/ServiceTypes/{id}

📅 Bookings

GET /api/Bookings

GET /api/Bookings/{id}

POST /api/Bookings

PATCH /api/Bookings/{id}/status

DELETE /api/Bookings/{id}

📊 Dashboard

GET /api/Dashboard/booking-stats

🧪 Tester

Kör alla tester lokalt:
dotnet test
GitHub Actions-pipeline kör:

Restore

Build

Test

På varje push till master.

🐞 Kända buggar / Begränsningar

🚫 Ingen autentisering – API:t är öppet (endast utvecklingsmiljö).

⏱️ Tidzonslogik enkel – frontend skickar lokal tid, backend konverterar till UTC.

🔁 Dubbelbokningslogiken är enkel och saknar avancerade regler.

🛠️ Uppdatering av ServiceTypes påverkar inte historiska bokningar.
