using Microsoft.EntityFrameworkCore;
using WorkshopMaster.Domain.Entities;

namespace WorkshopMaster.Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            db.Database.Migrate();

            SeedCustomers(db);
            SeedServiceTypes(db);
            SeedVehicles(db);
            SeedBookings(db);
        }

        private static void SeedCustomers(AppDbContext db)
        {
            if (db.Customers.Any()) return;

            var customers = new List<Customer>
            {
                new() { FullName = "Anna Andersson",   PhoneNumber = "070-1112233", Email = "anna.andersson@example.com" },
                new() { FullName = "Erik Eriksson",    PhoneNumber = "070-2223344", Email = "erik.eriksson@example.com" },
                new() { FullName = "Sara Svensson",    PhoneNumber = "070-3334455", Email = "sara.svensson@example.com" },
                new() { FullName = "Johan Johansson",  PhoneNumber = "070-4445566", Email = "johan.johansson@example.com" },
                new() { FullName = "Maria Martinsson", PhoneNumber = "070-5556677", Email = "maria.martinsson@example.com" },
                new() { FullName = "David Dahl",       PhoneNumber = "070-6667788", Email = "david.dahl@example.com" },
                new() { FullName = "Lina Larsson",     PhoneNumber = "070-7778899", Email = "lina.larsson@example.com" },
                new() { FullName = "Peter Pettersson", PhoneNumber = "070-8889900", Email = "peter.pettersson@example.com" },
                new() { FullName = "Nina Nilsson",     PhoneNumber = "070-9990011", Email = "nina.nilsson@example.com" },
                new() { FullName = "Oskar Olsson",     PhoneNumber = "070-0001122", Email = "oskar.olsson@example.com" }
            };

            db.Customers.AddRange(customers);
            db.SaveChanges();
        }

        private static void SeedServiceTypes(AppDbContext db)
        {
            if (db.ServiceTypes.Any()) return;

            var serviceTypes = new List<ServiceType>
            {
                new() { Name = "Lackering",               BasePrice = 4500m, Description = "Karosslackering och finish." },
                new() { Name = "Byte bromsar",            BasePrice = 2500m, Description = "Byte av bromsbelägg och kontroll." },
                new() { Name = "Oljebyte",                BasePrice =  900m, Description = "Byte av motorolja och filter." },
                new() { Name = "Däckskifte",              BasePrice =  600m, Description = "Säsongsbyte av däck." },
                new() { Name = "Hjulinställning",         BasePrice = 1500m, Description = "Justering av hjulvinklar." },
                new() { Name = "AC-service",              BasePrice = 1800m, Description = "Kontroll och påfyllning av AC." },
                new() { Name = "Kamremsbyte",             BasePrice = 6500m, Description = "Byte av kamrem och kringkomponenter." },
                new() { Name = "Motorfelsdiagnos",        BasePrice = 1200m, Description = "Felsökning med diagnosutrustning." },
                new() { Name = "Besiktningskontroll",     BasePrice =  800m, Description = "Genomgång inför besiktning." },
                new() { Name = "In- och utvändig tvätt",  BasePrice =  700m, Description = "Tvätt av kaross och interiör." }
            };

            db.ServiceTypes.AddRange(serviceTypes);
            db.SaveChanges();
        }

        private static void SeedVehicles(AppDbContext db)
        {
            if (db.Vehicles.Any()) return;

            var customers = db.Customers.OrderBy(c => c.Id).ToList();
            if (!customers.Any()) return; 

            var vehicles = new List<Vehicle>
            {
                new() { CustomerId = customers[0].Id, Brand = "Volvo",      Model = "V60",     RegistrationNumber = "ABC123", Year = 2018 },
                new() { CustomerId = customers[1].Id, Brand = "BMW",        Model = "320i",    RegistrationNumber = "DEF456", Year = 2016 },
                new() { CustomerId = customers[2].Id, Brand = "Audi",       Model = "A4",      RegistrationNumber = "GHI789", Year = 2019 },
                new() { CustomerId = customers[3].Id, Brand = "Saab",       Model = "9-3",     RegistrationNumber = "JKL012", Year = 2010 },
                new() { CustomerId = customers[4].Id, Brand = "Volkswagen", Model = "Golf",    RegistrationNumber = "MNO345", Year = 2015 },
                new() { CustomerId = customers[5].Id, Brand = "Volvo",      Model = "XC60",    RegistrationNumber = "PQR678", Year = 2020 },
                new() { CustomerId = customers[6].Id, Brand = "Toyota",     Model = "Yaris",   RegistrationNumber = "STU901", Year = 2014 },
                new() { CustomerId = customers[7].Id, Brand = "Kia",        Model = "Ceed",    RegistrationNumber = "VWX234", Year = 2017 },
                new() { CustomerId = customers[8].Id, Brand = "Hyundai",    Model = "i30",     RegistrationNumber = "YZA567", Year = 2013 },
                new() { CustomerId = customers[9].Id, Brand = "Skoda",      Model = "Octavia", RegistrationNumber = "BCE890", Year = 2021 }
            };

            db.Vehicles.AddRange(vehicles);
            db.SaveChanges();
        }

        private static void SeedBookings(AppDbContext db)
        {
            if (db.Bookings.Any()) return;

            var vehicles = db.Vehicles.OrderBy(v => v.Id).ToList();
            var serviceTypes = db.ServiceTypes.OrderBy(s => s.Id).ToList();

            if (!vehicles.Any() || !serviceTypes.Any()) return;

            var random = new Random();
            var today = DateTime.Today;

            var bookings = new List<Booking>();

            for (int i = 0; i < 12; i++)
            {
                var vehicle = vehicles[i % vehicles.Count];

                var start = today
                    .AddDays(-4 + i)           
                    .AddHours(8 + (i % 4) * 2); 

                var durationHours = 1 + (i % 3); 
                var end = start.AddHours(durationHours);

                var status = i switch
                {
                    < 4 => "Completed",
                    < 8 => "Confirmed",
                    < 10 => "Cancelled",
                    _ => "Pending"
                };

                var booking = new Booking
                {
                    VehicleId = vehicle.Id,
                    StartTime = start,
                    EndTime = end,
                    Status = status,
                    Notes = $"Seedad bokning #{i + 1} för {vehicle.RegistrationNumber}"
                };

                var serviceCount = 1 + (i % 3);
                var usedIndexes = new HashSet<int>();

                for (int j = 0; j < serviceCount; j++)
                {
                    int idx;
                    do
                    {
                        idx = random.Next(serviceTypes.Count);
                    } while (!usedIndexes.Add(idx));

                    var st = serviceTypes[idx];

                    booking.BookingServiceTypes.Add(new BookingServiceType
                    {
                        ServiceTypeId = st.Id
                    });
                }

                bookings.Add(booking);
            }

            db.Bookings.AddRange(bookings);
            db.SaveChanges();
        }
    }
}
