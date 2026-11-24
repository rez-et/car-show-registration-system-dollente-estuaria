namespace Infrastructure.Persistence;

using Domain.Entities;
using Domain.Enums;

public static class RegistrationSeeder
{
    public static void Seed(AppDbContext dbContext)
    {
        if (dbContext.Registrations.Any())
        {
            return;
        }

        var baseDate = new DateTime(2026, 11, 1, 8, 0, 0, DateTimeKind.Utc);

        var registrations = new List<Registration>
        {
            Build("CS27-0001", baseDate.AddHours(2), "Juan Dela Cruz", "09171234501", "juan.delacruz@example.com", "Rizal St., Sorsogon City", "Toyota Supra MK4", "NCS 2701", RegistrationType.VIP, RegistrationStatus.Confirmed, 5000m),
            Build("CS27-0002", baseDate.AddHours(6), "Maria Clara Santos", "09182345602", "maria.santos@example.com", "Magsaysay Ave., Naga City", "Honda Civic Type R FK8", "EAB 1402", RegistrationType.Competitor, RegistrationStatus.CheckedIn, 2500m),
            Build("CS27-0003", baseDate.AddHours(11), "Carlos Mendoza", "09193456703", "carlos.mendoza@example.com", "P. Burgos St., Legazpi City", "Mazda RX-7 FD3S", "EAC 3903", RegistrationType.Exhibitor, RegistrationStatus.Pending, 0m),
            Build("CS27-0004", baseDate.AddDays(1).AddHours(3), "Ana Reyes", "09204567804", "ana.reyes@example.com", "Ayala Ave., Makati City", "Nissan Skyline GT-R R34", "NAA 8804", RegistrationType.VIP, RegistrationStatus.Pending, 2500m),
            Build("CS27-0005", baseDate.AddDays(1).AddHours(9), "Miguel Villanueva", "09215678905", "miguel.v@example.com", "Ortigas Ave., Pasig City", "Mitsubishi Lancer Evo IX", "NBB 4505", RegistrationType.Competitor, RegistrationStatus.Confirmed, 1500m),
            Build("CS27-0006", baseDate.AddDays(2).AddHours(1), "Patricia Lim", "09226789006", "patricia.lim@example.com", "Katipunan Ave., Quezon City", "Subaru WRX STI", "NCC 6606", RegistrationType.Exhibitor, RegistrationStatus.CheckedIn, 1500m),
            Build("CS27-0007", baseDate.AddDays(2).AddHours(7), "Rafael Navarro", "09237890107", "rafael.navarro@example.com", "Bonifacio Global City, Taguig", "Porsche 911 GT3", "NDD 9117", RegistrationType.VIP, RegistrationStatus.CheckedIn, 5000m),
            Build("CS27-0008", baseDate.AddDays(3).AddHours(4), "Sofia Bautista", "09248901208", "sofia.bautista@example.com", "Session Rd., Baguio City", "Toyota GR86", "CAA 2008", RegistrationType.Exhibitor, RegistrationStatus.Cancelled, 0m),
            Build("CS27-0009", baseDate.AddDays(3).AddHours(10), "Gabriel Torres", "09259012309", "gabriel.torres@example.com", "J.P. Laurel Ave., Davao City", "Honda S2000", "LAA 3109", RegistrationType.Competitor, RegistrationStatus.Pending, 0m),
            Build("CS27-0010", baseDate.AddDays(4).AddHours(2), "Camille Aquino", "09260123410", "camille.aquino@example.com", "Gorordo Ave., Cebu City", "Mazda MX-5 Miata ND", "GAA 4210", RegistrationType.Exhibitor, RegistrationStatus.Confirmed, 750m),
            Build("CS27-0011", baseDate.AddDays(4).AddHours(8), "Juancho Mercado", "09271234511", "juancho.mercado@example.com", "Gubat, Sorsogon", "Nissan 350Z", "EAD 5311", RegistrationType.Competitor, RegistrationStatus.Confirmed, 2500m),
            Build("CS27-0012", baseDate.AddDays(5).AddHours(5), "Analyn Castillo", "09282345612", "analyn.castillo@example.com", "Irosin, Sorsogon", "Toyota GR Yaris", "EAE 6412", RegistrationType.VIP, RegistrationStatus.Pending, 0m),
            Build("CS27-0013", baseDate.AddDays(5).AddHours(12), "Lorenzo Garcia", "09293456713", "lorenzo.garcia@example.com", "Daraga, Albay", "BMW M3 E46", "EAF 7513", RegistrationType.VIP, RegistrationStatus.Cancelled, 1000m),
            Build("CS27-0014", baseDate.AddDays(6).AddHours(3), "Bea Alonzo-Cruz", "09354567814", "bea.cruz@example.com", "Alabang, Muntinlupa", "Honda NSX NA1", "NEE 8614", RegistrationType.VIP, RegistrationStatus.CheckedIn, 5000m),
            Build("CS27-0015", baseDate.AddDays(6).AddHours(9), "Enzo Ramos", "09365678915", "enzo.ramos@example.com", "Magallanes, Sorsogon", "Nissan Silvia S15", "EAG 9715", RegistrationType.Competitor, RegistrationStatus.CheckedIn, 2500m),
            Build("CS27-0016", baseDate.AddDays(7).AddHours(1), "Isabella Ocampo", "09376789016", "isabella.ocampo@example.com", "Shaw Blvd., Mandaluyong", "Volkswagen Golf GTI", "NFF 1816", RegistrationType.Exhibitor, RegistrationStatus.Confirmed, 1500m),
            Build("CS27-0017", baseDate.AddDays(7).AddHours(8), "Paolo Fernandez", "09387890117", "paolo.fernandez@example.com", "MacArthur Hwy., Pampanga", "Ford Mustang GT", "CAA 2917", RegistrationType.Competitor, RegistrationStatus.Cancelled, 0m),
            Build("CS27-0018", baseDate.AddDays(8).AddHours(4), "Katrina Soriano", "09398901218", "katrina.soriano@example.com", "Bulan, Sorsogon", "Toyota AE86 Sprinter Trueno", "EAH 3018", RegistrationType.Exhibitor, RegistrationStatus.Pending, 500m),
            Build("CS27-0019", baseDate.AddDays(8).AddHours(11), "Marco Evangelista", "09459012319", "marco.evangelista@example.com", "Greenhills, San Juan", "Audi R8 V10", "NGG 4119", RegistrationType.VIP, RegistrationStatus.Confirmed, 3000m),
            Build("CS27-0020", baseDate.AddDays(9).AddHours(2), "Daniella Flores", "09460123420", "daniella.flores@example.com", "Taft Ave., Manila", "Honda Integra Type R DC2", "NHH 5220", RegistrationType.Competitor, RegistrationStatus.Pending, 1000m),
            Build("CS27-0021", baseDate.AddDays(9).AddHours(9), "Adrian Pangilinan", "09471234521", "adrian.p@example.com", "Matnog, Sorsogon", "Mitsubishi Eclipse GSX", "EAJ 6321", RegistrationType.Exhibitor, RegistrationStatus.CheckedIn, 1500m),
            Build("CS27-0022", baseDate.AddDays(10).AddHours(5), "Bianca Rosales", "09482345622", "bianca.rosales@example.com", "Tabaco City, Albay", "Subaru BRZ tS", "EAK 7422", RegistrationType.Exhibitor, RegistrationStatus.Confirmed, 1500m),
            Build("CS27-0023", baseDate.AddDays(10).AddHours(10), "Victor Manuel", "09493456723", "victor.manuel@example.com", "Bacolod City, Negros Occidental", "Toyota Celica GT-Four", "FAA 8523", RegistrationType.Competitor, RegistrationStatus.CheckedIn, 2500m),
            Build("CS27-0024", baseDate.AddDays(11).AddHours(3), "Anastacia Gomez", "09954567824", "anastacia.gomez@example.com", "Roxas Blvd., Pasay City", "Lexus LC 500", "NJJ 9624", RegistrationType.VIP, RegistrationStatus.Pending, 1500m),
            Build("CS27-0025", baseDate.AddDays(11).AddHours(9), "Kenneth De Leon", "09965678925", "kenneth.deleon@example.com", "Ligao City, Albay", "Honda Civic SiR EK4", "EAL 1725", RegistrationType.Competitor, RegistrationStatus.Confirmed, 1200m),
            Build("CS27-0026", baseDate.AddDays(12).AddHours(6), "Jerome Padilla", "09976789026", "jerome.padilla@example.com", "Casiguran, Sorsogon", "Nissan 370Z Nismo", "EAM 2826", RegistrationType.Exhibitor, RegistrationStatus.Pending, 0m)
        };

        dbContext.Registrations.AddRange(registrations);
        dbContext.SaveChanges();
    }

    private static Registration Build(
        string registrationNumber,
        DateTime registrationDate,
        string participantName,
        string contactNumber,
        string email,
        string address,
        string carMakeModel,
        string plateNumber,
        RegistrationType registrationType,
        RegistrationStatus status,
        decimal amountPaid)
    {
        return new Registration
        {
            Id = Guid.NewGuid(),
            RegistrationNumber = registrationNumber,
            RegistrationDate = registrationDate,
            ParticipantName = participantName,
            ContactNumber = contactNumber,
            Email = email,
            Address = address,
            CarMakeModel = carMakeModel,
            PlateNumber = plateNumber,
            RegistrationType = registrationType,
            Status = status,
            RegistrationFee = Registration.GetFeeForType(registrationType),
            AmountPaid = amountPaid
        };
    }
}
