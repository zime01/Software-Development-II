using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace hotelEase.Services.Database;

public partial class HotelEaseContext : DbContext
{
    public HotelEaseContext()
    {
    }

    public HotelEaseContext(DbContextOptions<HotelEaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<ReservationService> ReservationServices { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomAvailability> RoomAvailabilities { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=HotelEase;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assets__3214EC07E249B58C");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(200);
            entity.Property(e => e.MimeType).HasMaxLength(100);

            entity.HasOne(d => d.Hotel).WithMany(p => p.Assets)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Assets__HotelId__5DCAEF64");

            entity.HasOne(d => d.Room).WithMany(p => p.Assets)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Assets__RoomId__5EBF139D");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3214EC0763B2364D");

            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cities__CountryI__3B75D760");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Countrie__3214EC07C5B2033D");

            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hotels__3214EC078B43B21F");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Spa).HasColumnName("SPA");
            entity.Property(e => e.StateMachine).HasMaxLength(100);

            entity.HasOne(d => d.City).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Hotels__CityId__3E52440B");

            entity.HasOne(d => d.Country).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Hotels__CountryI__3F466844");

            entity.HasOne(d => d.Manager).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Hotels__ManagerI__403A8C7D");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC07D414C746");

            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.SentAt).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__5AEE82B9");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC07F0F41DE0");

            entity.ToTable("Payment");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Currency)
                .HasMaxLength(10)
                .HasDefaultValue("USD");
            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.Provider)
                .HasMaxLength(50)
                .HasDefaultValue("stripe");
            entity.Property(e => e.ProviderPaymentId).HasMaxLength(200);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("processing");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Reservation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__Reserva__18EBB532");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservat__3214EC072F6279EE");

            entity.Property(e => e.CheckInDate).HasColumnType("datetime");
            entity.Property(e => e.CheckOutDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__RoomI__4F7CD00D");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__UserI__4E88ABD4");
        });

        modelBuilder.Entity<ReservationService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reservat__3214EC075ED819B9");

            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Reservation).WithMany(p => p.ReservationServices)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__Reser__52593CB8");

            entity.HasOne(d => d.Service).WithMany(p => p.ReservationServices)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__Servi__534D60F1");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC07077D883D");

            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.ReviewDate).HasColumnType("datetime");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__HotelId__571DF1D5");

            entity.HasOne(d => d.Reservation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__Reserva__5812160E");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__UserId__5629CD9C");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07AE0B5CEE");

            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rooms__3214EC0713FFA2B0");

            entity.Property(e => e.Ac).HasColumnName("AC");
            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PricePerNight).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rooms__HotelId__44FF419A");

            entity.HasOne(d => d.RoomType).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.RoomTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rooms__RoomTypeI__45F365D3");
        });

        modelBuilder.Entity<RoomAvailability>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoomAvai__3214EC07C7110479");

            entity.ToTable("RoomAvailability");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.DeletedTime).HasColumnType("datetime");

            entity.HasOne(d => d.Room).WithMany(p => p.RoomAvailabilities)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoomAvail__RoomI__48CFD27E");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoomType__3214EC07AD3DD3A0");

            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Services__3214EC0720B89D5A");

            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Services)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Services__HotelI__4BAC3F29");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07E3061A65");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedTime).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastLoginAt).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRoles__RoleI__6477ECF3"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserRoles__UserI__6383C8BA"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF2760AD81661037");
                        j.ToTable("UserRoles");
                    });
        });

        base.OnModelCreating(modelBuilder);

        // Countries
        modelBuilder.Entity<Country>().HasData(
            new Country { Id = 1, Name = "Bosnia and Herzegovina" },
            new Country { Id = 2, Name = "Croatia" }
        );

        // Cities
        modelBuilder.Entity<City>().HasData(
            new City { Id = 1, Name = "Sarajevo", CountryId = 1 },
            new City { Id = 2, Name = "Mostar", CountryId = 1 },
            new City { Id = 4, Name = "Banja Luka", CountryId = 1 }
        );

        // Hotels
        modelBuilder.Entity<Hotel>().HasData(
    new Hotel
    {
        Id = 1,
        Name = "Hotel Hills",
        Description = "Hotel Hills, Thermal & Spa Resort Sarajevo is located in the center of the green oasis of Sarajevo Hotel Hills, Thermal & Spa Resort Sarajevo includes 330 rooms and suites, multipurpose Congress center with modern conference technology, Wellness, Spa & Fitness Health center, indoor and outdoor swimming pools, Adrenalin park for children and adults on impressive 2200 square meters, Wedding halls, restaurants with international and national cuisine, and attractive Panoramic restaurant and lounge bar on the rooftop of the Hotel with beautiful panoramic view to the green surroundings of the hotel and the city. The Sky bar is open only for organised corporate party, weddings, organisation of birthdays, etc.",
        Address = "Butmirska cesta 18, 71000 Sarajevo",
        CityId = 1,
        CountryId = 1,
        ManagerId = 2,
        StarRating = 5,
        IsActive = true,
        Pool = true,
        CreatedAt = DateTime.Parse("2025-02-08T00:00:00")
    },
    new Hotel
    {
        Id = 2,
        Name = "Hotel Europe",
        Description = "Luxury hotel with a spa, modern amenities, and a central location.",
        Address = "Mussalla 1, 71000 Sarajevo",
        CityId = 1,
        CountryId = 1,
        ManagerId = 2,
        StarRating = 5,
        IsActive = true,
        CreatedAt = DateTime.Parse("2025-08-03T19:00:56.207")
    },
    new Hotel
    {
        Id = 3,
        Name = "Swissotel Sarajevo",
        Description = "Premium luxury hotel offering top-tier amenities and stunning views.",
        Address = "Kardinala Stepinca 31, 71000 Sarajevo",
        CityId = 1,
        CountryId = 1,
        ManagerId = 2,
        StarRating = 5,
        IsActive = true,
        CreatedAt = DateTime.Parse("2025-08-04T12:15:19.743")
    },
    new Hotel
    {
        Id = 5,
        Name = "Hotel Hollywood",
        Description = "Well-known hotel with a rich history, offering a large conference center and leisure facilities.",
        Address = "Sarajevska 29, 71000 Sarajevo",
        CityId = 1,
        CountryId = 1,
        ManagerId = 6,
        StarRating = 4,
        IsActive = true,
        CreatedAt = DateTime.Parse("2025-08-15T08:45:56.95")
    },
    new Hotel
    {
        Id = 7,
        Name = "Courtyard by Marriott Banja Luka",
        Description = "The 4-star Courtyard by Marriott Banja Luka offers modern-style accommodations and a number of amenities including a state-of-the-art lobby and an on-site restaurant. Located 1640 feet from the National Theater and within 1 mi of Kastel Fortress, it provides a 24-hour front desk and free WiFi.",
        Address = "Prvog krajiškog korpusa 33, Banja Luka 78000",
        CityId = 4,
        CountryId = 1,
        ManagerId = 5,
        StarRating = 4,
        IsActive = true,
        Parking = true,
        WiFi = true,
        Bar = true,
        CreatedAt = DateTime.Parse("2025-09-05T12:16:28.803")
    },
    new Hotel
    {
        Id = 8,
        Name = "Hotel Integra Banja Luka",
        Description = "Located 14 mi from Banja Luka International Airport, the hotel is a 19-minute walk from Kastel Fortress. Nearby attractions include the Banja Luka Museum and the Banja Luka Cathedral.",
        Address = "Kralja Petra I Karađorđevića 129, Banja Luka 78000",
        CityId = 4,
        CountryId = 1,
        ManagerId = 5,
        StarRating = 4,
        IsActive = true,
        Parking = true,
        WiFi = true,
        CreatedAt = DateTime.Parse("2025-09-05T12:16:28.803")
    },
    new Hotel
    {
        Id = 9,
        Name = "Hotel Cezar Banja Luka",
        Description = "Set in the center of Banja Luka, Hotel Cezar is located only 164 feet from the main bus and train station. The City Park is 328 feet away. The on-site restaurant serves local and international cuisine.  Free Wi-Fi and free private parking are available. All rooms come with an LCD TV and a private bathroom with shower, hairdryer and toiletries. The breakfast is served each morning.",
        Address = "Dr. Mladena Stojanovića 123, Banja Luka 78000",
        CityId = 4,
        CountryId = 1,
        ManagerId = 5,
        StarRating = 4,
        IsActive = true,
        WiFi = true,
        CreatedAt = DateTime.Parse("2025-09-05T12:16:28.803")
    },
    new Hotel
    {
        Id = 10,
        Name = "Hotel Mepas",
        Description = "Offering free access to an indoor pool and a spa and wellness center, Hotel Mepas is located in Mostar, in the very center of town. A shopping center with various entertainment and shopping options is located right below the hotel. Free WiFi access is available as well as free parking. Each room here will provide you with a TV, air conditioning and a mini-bar. Featuring both a shower and a bathtub, the bathrooms also come with a hairdryer and bathrobes. Extras in all rooms include a seating area, satellite channels and pay-per-view channels.",
        Address = "Kneza Višeslava, Mostar 88000",
        CityId = 2,
        CountryId = 1,
        ManagerId = 5,
        StarRating = 5,
        IsActive = true,
        Parking = true,
        WiFi = true,
        Pool = true,
        Bar = true,
        Fitness = true,
        CreatedAt = DateTime.Parse("2025-09-05T12:16:28.803")
    },
    new Hotel
    {
        Id = 11,
        Name = "City Hotel Mostar",
        Description = "Located in a residential area of Mostar, City Hotel Mostar offers modernly furnished rooms that overlook the town. There is a restaurant in the same building, as well as shops, bars and rent-a-car service. Free Wi-Fi and free garage parking are available. All rooms are air-conditioned and feature a flat-screen TV, a mini-bar and a safe. Private bathroom offers a shower and hairdryer.",
        Address = "Vukovarska 7, Mostar 88000",
        CityId = 2,
        CountryId = 1,
        ManagerId = 5,
        StarRating = 5,
        IsActive = true,
        Parking = true,
        WiFi = true,
        CreatedAt = DateTime.Parse("2025-09-05T12:16:28.803")
    },
    new Hotel
    {
        Id = 12,
        Name = "Hotel Amicus",
        Description = "Comfortable Accommodations: Hotel Amicus in Mostar offers 4-star comfort with air-conditioned rooms featuring private bathrooms, balconies, and garden or pool views. Each room includes a TV, soundproofing, and free WiFi.",
        Address = "29. hercegovačke udarne divizije 3, Mostar 88000",
        CityId = 2,
        CountryId = 1,
        ManagerId = 5,
        StarRating = 5,
        IsActive = true,
        Parking = true,
        WiFi = true,
        Pool = true,
        Bar = true,
        CreatedAt = DateTime.Parse("2025-09-05T12:16:28.803")
    }
);

        // RoomTypes
        modelBuilder.Entity<RoomType>().HasData(
    new RoomType
    {
        Id = 1,
        Name = "Deluxe",
        Description = "Triple Room",
    },
    new RoomType
    {
        Id = 2,
        Name = "Deluxe",
        Description = "Double Room",
    },
    new RoomType
    {
        Id = 3,
        Name = "Superior",
        Description = "Double Room",
    },
    new RoomType
    {
        Id = 4,
        Name = "Standard Double Room",
        Description = "Standard Double or Twin Room",
    },
    new RoomType
    {
        Id = 5,
        Name = "Standard King Room",
        Description = "Standard King Room",
    },
    new RoomType
    {
        Id = 6,
        Name = "Double Superior Room",
        Description = "Double or Twin Superior Room",
    },
    new RoomType
    {
        Id = 7,
        Name = "Single Superior Room",
        Description = "Single Superior Room",
    },
    new RoomType
    {
        Id = 10,
        Name = "Standard, Guest room",
        Description = "Standard, Guest room, 1 King, City view",
    },
    new RoomType
    {
        Id = 11,
        Name = "Superior, Guest room",
        Description = "Superior, Guest room, 2 Twin/Single Bed(s), City view",
    },
    new RoomType
    {
        Id = 12,
        Name = "Triple Room",
        Description = "Triple Room",
    },
    new RoomType
    {
        Id = 13,
        Name = "Premium Quadruple Room",
        Description = "Premium Quadruple Room",
    },
    new RoomType
    {
        Id = 14,
        Name = "Double Room with Balcony",
        Description = "Double Room with Balcony",
    },
    new RoomType
    {
        Id = 15,
        Name = "Standard Family Room",
        Description = "1 twin bed and 1 queen bed",
        
    },
    new RoomType
    {
        Id = 16,
        Name = "Superior Suite",
        Description = "1 sofa bed and 1 queen bed",
        
    },
    new RoomType
    {
        Id = 17,
        Name = "Family Suite",
        Description = "1 sofa bed and 1 queen bed",
        IsDeleted = false
    },
    new RoomType
    {
        Id = 18,
        Name = "Presidential Suite",
        Description = "1 king bed and 1 sofa bed",
        
    },
    new RoomType
    {
        Id = 19,
        Name = "Comfort Single Room",
        Description = "Comfort Single Room",
        
    },
    new RoomType
    {
        Id = 20,
        Name = "Deluxe Single Room",
        Description = "Deluxe Single Room",
        
    }
);


        // Rooms
        modelBuilder.Entity<Room>().HasData(
    new Room { Id = 1, HotelId = 1, RoomTypeId = 1, Name = "Deluxe Triple Room", Capacity = 3, PricePerNight = 150, QueenBed = true },
    new Room { Id = 2, HotelId = 1, RoomTypeId = 2, Name = "Deluxe Double Room", Capacity = 2, PricePerNight = 100, QueenBed = true, WiFi = true, IsAvailable = true, Description = "string" },
    new Room { Id = 3, HotelId = 2, RoomTypeId = 3, Name = "Superior Double Room", Capacity = 2, PricePerNight = 125, QueenBed = true, WiFi = true, CityView = true, IsAvailable = true, Description = "string" },
    new Room { Id = 4, HotelId = 5, RoomTypeId = 2, Name = "Deluxe Double Room", Capacity = 2, PricePerNight = 100, IsAvailable = true, Description = "Providing free toiletries, this double room includes a private bathroom with a bath or a shower and a hairdryer. The spacious double room provides air conditioning, soundproof walls, a mini-bar, a wardrobe, as well as a flat-screen TV with cable channels. The unit offers 1 bed." },
    new Room { Id = 5, HotelId = 3, RoomTypeId = 3, Name = "Superior Double Room", Capacity = 2, PricePerNight = 140, IsAvailable = true, Description = "The spacious triple room offers air conditioning, soundproof walls, as well as a private bathroom boasting a walk-in shower and a hairdryer. The triple room has carpeted floors, a seating area with a flat-screen TV, a mini-bar, a tea and coffee maker as well as mountain views. The unit has 2 beds." },
    new Room { Id = 6, HotelId = 3, RoomTypeId = 4, Name = "Standard Double Room", Capacity = 2, PricePerNight = 100, IsAvailable = true, Description = "Standard Double Room" },
    new Room { Id = 7, HotelId = 3, RoomTypeId = 5, Name = "Standard King Room", Capacity = 3, PricePerNight = 180, IsAvailable = true, Description = "Standard King Room" },
    new Room { Id = 8, HotelId = 5, RoomTypeId = 7, Name = "Single Superior Room", Capacity = 1, PricePerNight = 80, IsAvailable = true, Description = "Single Superior Room" },
    new Room { Id = 9, HotelId = 5, RoomTypeId = 6, Name = "Twin Superior Room", Capacity = 1, PricePerNight = 130, IsAvailable = true, Description = "Twin Superior Room" },
    new Room { Id = 10, HotelId = 1, RoomTypeId = 7, Name = "room", Capacity = 1, PricePerNight = 120, Description = "description" },
    new Room { Id = 11, HotelId = 1, RoomTypeId = 7, Name = "1", Capacity = 1, PricePerNight = 111, Description = "desc" },
    new Room { Id = 12, HotelId = 1, RoomTypeId = 5, Name = "rrrr", Capacity = 111, PricePerNight = 1111, Description = "1111" },
    new Room { Id = 13, HotelId = 5, RoomTypeId = 4, Name = "rrrr", Capacity = 1, PricePerNight = 111, Description = "dassa" },
    new Room { Id = 14, HotelId = 5, RoomTypeId = 3, Name = "room", Capacity = 2, PricePerNight = 110, Description = "description" },
    new Room { Id = 15, HotelId = 7, RoomTypeId = 10, Name = "Standard, Guest room", Capacity = 1, PricePerNight = 89, IsAvailable = true, Description = "Standard, Guest room, 1 King, City view" },
    new Room { Id = 16, HotelId = 7, RoomTypeId = 10, Name = "Superior, Guest room", Capacity = 2, PricePerNight = 109, IsAvailable = true, Description = "Superior, Guest room, 2 Twin/Single Bed(s), City view" },
    new Room { Id = 17, HotelId = 8, RoomTypeId = 12, Name = "Triple Room", Capacity = 3, PricePerNight = 130, IsAvailable = true, Description = "Triple Room" },
    new Room { Id = 18, HotelId = 8, RoomTypeId = 13, Name = "Premium Quadruple Room", Capacity = 4, PricePerNight = 150, IsAvailable = true, Description = "Premium Quadruple Room" },
    new Room { Id = 19, HotelId = 9, RoomTypeId = 14, Name = "Double Room with Balcony", Capacity = 2, PricePerNight = 69, IsAvailable = true, Description = "Double Room with Balcony" },
    new Room { Id = 20, HotelId = 9, RoomTypeId = 15, Name = "Standard Family Room", Capacity = 4, PricePerNight = 119, IsAvailable = true, Description = "1 twin bed and 1 queen bed" },
    new Room { Id = 21, HotelId = 10, RoomTypeId = 16, Name = "Superior Suite", Capacity = 3, PricePerNight = 145, IsAvailable = true, Description = "1 sofa bed and 1 queen bed" },
    new Room { Id = 22, HotelId = 10, RoomTypeId = 17, Name = "Family Suite", Capacity = 3, PricePerNight = 125, IsAvailable = true, Description = "1 sofa bed and 1 queen bed" },
    new Room { Id = 23, HotelId = 11, RoomTypeId = 18, Name = "Presidential Suite", Capacity = 3, PricePerNight = 150, IsAvailable = true, Description = "1 king bed and 1 sofa bed" },
    new Room { Id = 24, HotelId = 12, RoomTypeId = 19, Name = "Comfort Single Room", Capacity = 1, PricePerNight = 100, IsAvailable = true, Description = "Comfort Single Room" },
    new Room { Id = 25, HotelId = 12, RoomTypeId = 20, Name = "Deluxe Single Room", Capacity = 1, PricePerNight = 89, IsAvailable = true, Description = "Deluxe Single Room" }
);


        // Assets


        // Roles
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Manager" },
            new Role { Id = 4, Name = "User" }


        );

        //RoomsAvailability
        modelBuilder.Entity<RoomAvailability>().HasData(
        new RoomAvailability { Id = 1, RoomId = 1, Date = DateTime.Parse("2025-08-10T16:11:34.197"), Status = 1 },
        new RoomAvailability { Id = 2, RoomId = 1, Date = DateTime.Parse("2025-08-11T16:11:34.197"), Status = 0 },
        new RoomAvailability { Id = 3, RoomId = 2, Date = DateTime.Parse("2025-08-11T16:11:34.197"), Status = 1 },
        new RoomAvailability { Id = 4, RoomId = 2, Date = DateTime.Parse("2025-08-10T16:11:34.197"), Status = 1 }
    );

        // Users
        modelBuilder.Entity<User>().HasData(
    new User { Id = 1, FirstName = "meho", LastName = "mehic", Email = "user@gmail.com", Username = "user", PasswordHash = "OypiMGzMHp0o9DYe5yWSnkky54A=", PasswordSalt = "o5hAjrnYH7NRqp9OBA6J9Q==", PhoneNumber = "061111111", IsActive = true, CreatedAt = DateTime.Parse("2025-08-01T16:30:26.593"), LastLoginAt = DateTime.Parse("2025-08-01T16:30:26.593") },
    new User { Id = 2, FirstName = "hazim", LastName = "hazim", Email = "hazim@gmail.com", Username = "hazim", PasswordHash = "/o8nmsH5Dbd76SDP//tH/GAvlxU=", PasswordSalt = "NOaVnvJ5ycPcKCjCCy8OdQ==", PhoneNumber = "061234567", IsActive = true, CreatedAt = DateTime.Parse("2025-08-01T16:19:45.617"), LastLoginAt = DateTime.Parse("2025-08-01T16:19:45.617") },
    new User { Id = 4, FirstName = "ado", LastName = "ado", Email = "ado", Username = "ado", PasswordHash = "2fjG4q1LtucR2lA018pK6nWkyOc=", PasswordSalt = "1uIRFG7ijKSRdLtw5a74dw==", PhoneNumber = "063333333" },
    new User { Id = 5, FirstName = "test", LastName = "test", Email = "test@gmail.com", Username = "test", PasswordHash = "s/BuGRf7UYcqxjZMRoXq3Lu30YA=", PasswordSalt = "pToIaH5hKO0heIRZvyCouA==", PhoneNumber = "06000000", IsActive = true, CreatedAt = DateTime.Parse("2025-08-04T17:30:15.713"), LastLoginAt = DateTime.Parse("2025-08-04T17:30:15.713") },
    new User { Id = 6, FirstName = "Hazim", LastName = "Zimić", Email = "zime1921@gmail.com", Username = "zime_01", PasswordHash = "2eM1YIrZQAVh/jGfdoyUdOMdrEQ=", PasswordSalt = "s0Q/8KH8VbNeaMtkT18CAA==", PhoneNumber = "+38762404557", IsActive = true, CreatedAt = DateTime.Parse("2025-08-08T18:26:28.5"), LastLoginAt = DateTime.Parse("2025-08-08T18:26:28.5") },
    new User { Id = 7, FirstName = "test", LastName = "test", Email = "zime1921@gmail.com", Username = "test", PasswordHash = "4pOLn3oczBkoe6ryRO3roYb70c4=", PasswordSalt = "tBf5M34DNdsz/42SdTmJHQ==", IsActive = true, CreatedAt = DateTime.Parse("2025-08-20T10:37:21.807") },
    new User { Id = 8, FirstName = "test", LastName = "test", Email = "zime1921@gmail.com", Username = "test", PasswordHash = "DUdL3t0xvuewcsxxsSrW23x948I=", PasswordSalt = "Rfk+5T78WxlZGOiYrl2j9A==", IsActive = true, CreatedAt = DateTime.Parse("2025-08-20T10:40:04.5") },
    new User { Id = 9, FirstName = "abcd", LastName = "abcd", Email = "abcd", Username = "abcd", PasswordHash = "9ciStnRAT8sF6RjAI4dy5L4FCO8=", PasswordSalt = "C5DJk5nT7x+gkzuYTcZ5fA==", PhoneNumber = "string", IsActive = true, CreatedAt = DateTime.Parse("2025-08-20T08:51:11.363") },
    new User { Id = 10, FirstName = "", LastName = "", Email = "", Username = "", PasswordHash = "G6FEm+iqUeqJr7qE2wKhCo6xzqk=", PasswordSalt = "Aa8kPMrOl2Y0xdJNLrUErw==", IsActive = true, CreatedAt = DateTime.Parse("2025-08-20T19:20:20.337") },
    new User { Id = 13, FirstName = "mujo", LastName = "mujo", Email = "eaglehl022@gmail.com", Username = "mujo", PasswordHash = "rtNMo9LEpchW1xPiej9xshhp/Js=", PasswordSalt = "b5mgx/KFt/oB/LFCFX/Dag==", PhoneNumber = "061111111" }
);

        // Reservations
        modelBuilder.Entity<Reservation>().HasData(
    new Reservation
    {
        Id = 1,
        UserId = 1,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-08-07T10:00:00.1"),
        CheckOutDate = DateTime.Parse("2025-08-17T10:00:00.1"),
        TotalPrice = 1500,
        Status = null,
        CreatedAt = DateTime.Parse("2025-08-06T18:05:59.1")
    },
    new Reservation
    {
        Id = 2,
        UserId = 1,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-08-07T10:00:00.1"),
        CheckOutDate = DateTime.Parse("2025-08-17T10:00:00.1"),
        TotalPrice = 1500,
        Status = null,
        CreatedAt = DateTime.Parse("2025-08-06T18:05:59.1")
    },
    new Reservation
    {
        Id = 3,
        UserId = 2,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-08-20T10:00:00.68"),
        CheckOutDate = DateTime.Parse("2025-08-22T10:00:00.68"),
        TotalPrice = 300,
        Status = "Completed",
        CreatedAt = DateTime.Parse("2025-08-07T06:30:15.68")
    },
    new Reservation
    {
        Id = 4,
        UserId = 6,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-08-09T10:28:01.71"),
        CheckOutDate = DateTime.Parse("2025-08-14T10:28:01.71"),
        TotalPrice = 750,
        Status = "Completed",
        CreatedAt = DateTime.Parse("2025-08-08T18:28:01.71")
    },
    new Reservation
    {
        Id = 6,
        UserId = 6,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-08-24T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-28T00:00:00"),
        TotalPrice = 650,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 7,
        UserId = 6,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-08-28T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-31T00:00:00"),
        TotalPrice = 500,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 8,
        UserId = 6,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-08-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-03T00:00:00"),
        TotalPrice = 250,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 9,
        UserId = 6,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-08-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-03T00:00:00"),
        TotalPrice = 250,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 10,
        UserId = 6,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-08-17T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-18T00:00:00"),
        TotalPrice = 150,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 11,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-08-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-05T00:00:00"),
        TotalPrice = 520,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 12,
        UserId = 6,
        RoomId = 5,
        CheckInDate = DateTime.Parse("2025-08-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-05T00:00:00"),
        TotalPrice = 560,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 13,
        UserId = 6,
        RoomId = 5,
        CheckInDate = DateTime.Parse("2025-08-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-05T00:00:00"),
        TotalPrice = 560,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 14,
        UserId = 6,
        RoomId = 5,
        CheckInDate = DateTime.Parse("2025-08-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-05T00:00:00"),
        TotalPrice = 560,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 15,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-08-16T21:59:11.45"),
        CheckOutDate = DateTime.Parse("2025-08-16T21:59:11.45"),
        TotalPrice = 500,
        Status = "booked",
        CreatedAt = DateTime.Parse("2025-08-16T21:59:11.45")
    },
    new Reservation
    {
        Id = 16,
        UserId = 6,
        RoomId = 5,
        CheckInDate = DateTime.Parse("2025-08-05T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-08T00:00:00"),
        TotalPrice = 420,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 17,
        UserId = 6,
        RoomId = 5,
        CheckInDate = DateTime.Parse("2025-08-05T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-08T00:00:00"),
        TotalPrice = 420,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 18,
        UserId = 6,
        RoomId = 5,
        CheckInDate = DateTime.Parse("2025-08-05T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-08T00:00:00"),
        TotalPrice = 420,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 19,
        UserId = 6,
        RoomId = 4,
        CheckInDate = DateTime.Parse("2025-08-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-04T00:00:00"),
        TotalPrice = 300,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 20,
        UserId = 6,
        RoomId = 4,
        CheckInDate = DateTime.Parse("2025-08-04T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-07T00:00:00"),
        TotalPrice = 300,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 21,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-08-05T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-09T00:00:00"),
        TotalPrice = 520,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 22,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-08-09T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-12T00:00:00"),
        TotalPrice = 395,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 23,
        UserId = 6,
        RoomId = 5,
        CheckInDate = DateTime.Parse("2025-08-08T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-12T00:00:00"),
        TotalPrice = 560,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 24,
        UserId = 6,
        RoomId = 4,
        CheckInDate = DateTime.Parse("2025-08-07T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-12T00:00:00"),
        TotalPrice = 500,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 25,
        UserId = 6,
        RoomId = 5,
        CheckInDate = DateTime.Parse("2025-08-12T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-16T00:00:00"),
        TotalPrice = 560,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 26,
        UserId = 6,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-08-03T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-06T00:00:00"),
        TotalPrice = 300,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 27,
        UserId = 6,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-08-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-03T00:00:00"),
        TotalPrice = 350,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 28,
        UserId = 6,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-08-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-03T00:00:00"),
        TotalPrice = 350,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 29,
        UserId = 6,
        RoomId = 4,
        CheckInDate = DateTime.Parse("2025-08-12T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-16T00:00:00"),
        TotalPrice = 400,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 30,
        UserId = 6,
        RoomId = 4,
        CheckInDate = DateTime.Parse("2025-08-12T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-16T00:00:00"),
        TotalPrice = 400,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 31,
        UserId = 6,
        RoomId = 4,
        CheckInDate = DateTime.Parse("2025-08-12T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-16T00:00:00"),
        TotalPrice = 400,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 32,
        UserId = 6,
        RoomId = 4,
        CheckInDate = DateTime.Parse("2025-08-12T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-08-16T00:00:00"),
        TotalPrice = 400,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 33,
        UserId = 6,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-07-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-05T00:00:00"),
        TotalPrice = 650,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 34,
        UserId = 6,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-07-05T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-10T00:00:00"),
        TotalPrice = 800,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 35,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-07-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-06T00:00:00"),
        TotalPrice = 645,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 36,
        UserId = 6,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-07-10T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-17T00:00:00"),
        TotalPrice = 1100,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 37,
        UserId = 6,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-07-10T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-17T00:00:00"),
        TotalPrice = 1100,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 38,
        UserId = 6,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-07-10T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-17T00:00:00"),
        TotalPrice = 1100,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 39,
        UserId = 6,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-07-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-05T00:00:00"),
        TotalPrice = 450,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 40,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-07-06T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-12T00:00:00"),
        TotalPrice = 770,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 41,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-07-06T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-12T00:00:00"),
        TotalPrice = 770,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 42,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-07-06T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-12T00:00:00"),
        TotalPrice = 770,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 43,
        UserId = 6,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-07-23T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-28T00:00:00"),
        TotalPrice = 950,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 44,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-07-12T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-17T00:00:00"),
        TotalPrice = 645,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 45,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-07-17T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-19T00:00:00"),
        TotalPrice = 250,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 46,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-07-17T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-19T00:00:00"),
        TotalPrice = 270,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 47,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-07-17T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-19T00:00:00"),
        TotalPrice = 270,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 48,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-07-19T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-21T00:00:00"),
        TotalPrice = 250,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 49,
        UserId = 6,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-07-05T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-10T00:00:00"),
        TotalPrice = 550,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 50,
        UserId = 6,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-07-05T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-10T00:00:00"),
        TotalPrice = 550,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 51,
        UserId = 6,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-07-05T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-10T00:00:00"),
        TotalPrice = 550,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 52,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-07-21T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-26T00:00:00"),
        TotalPrice = 645,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 53,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-07-21T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-26T00:00:00"),
        TotalPrice = 645,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 54,
        UserId = 6,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-07-10T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-12T00:00:00"),
        TotalPrice = 250,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 55,
        UserId = 6,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-07-12T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-17T00:00:00"),
        TotalPrice = 550,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 56,
        UserId = 6,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-07-12T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-17T00:00:00"),
        TotalPrice = 550,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 57,
        UserId = 6,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-07-12T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-17T00:00:00"),
        TotalPrice = 550,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 58,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-07-26T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-31T00:00:00"),
        TotalPrice = 645,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 59,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-07-26T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-31T00:00:00"),
        TotalPrice = 645,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 60,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-06-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-05T00:00:00"),
        TotalPrice = 520,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 61,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-06-05T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-10T00:00:00"),
        TotalPrice = 645,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 62,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-06-05T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-10T00:00:00"),
        TotalPrice = 645,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 63,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-06-10T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-14T00:00:00"),
        TotalPrice = 520,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 64,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-06-10T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-14T00:00:00"),
        TotalPrice = 520,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 65,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-06-10T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-14T00:00:00"),
        TotalPrice = 520,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 66,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-06-10T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-14T00:00:00"),
        TotalPrice = 520,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 67,
        UserId = 6,
        RoomId = 5,
        CheckInDate = DateTime.Parse("2025-07-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-05T00:00:00"),
        TotalPrice = 560,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 68,
        UserId = 6,
        RoomId = 4,
        CheckInDate = DateTime.Parse("2025-07-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-05T00:00:00"),
        TotalPrice = 400,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 69,
        UserId = 6,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-06-14T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-19T00:00:00"),
        TotalPrice = 645,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 70,
        UserId = 6,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-07-23T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-28T00:00:00"),
        TotalPrice = 800,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 71,
        UserId = 13,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-07-17T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-23T00:00:00"),
        TotalPrice = 650,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 72,
        UserId = 13,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-06-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-07T00:00:00"),
        TotalPrice = 900,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 73,
        UserId = 13,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-06-07T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-14T00:00:00"),
        TotalPrice = 1050,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 74,
        UserId = 13,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-06-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-07T00:00:00"),
        TotalPrice = 650,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 75,
        UserId = 13,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-06-07T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-17T00:00:00"),
        TotalPrice = 1000,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 76,
        UserId = 13,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-06-19T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-21T00:00:00"),
        TotalPrice = 270,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 77,
        UserId = 13,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-06-21T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-25T00:00:00"),
        TotalPrice = 520,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 78,
        UserId = 13,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-06-25T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-30T00:00:00"),
        TotalPrice = 625,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 79,
        UserId = 13,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-06-25T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-30T00:00:00"),
        TotalPrice = 625,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 80,
        UserId = 13,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-05-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-05-15T00:00:00"),
        TotalPrice = 1770,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 81,
        UserId = 13,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-05-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-05-15T00:00:00"),
        TotalPrice = 1770,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 82,
        UserId = 13,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-06-17T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-22T00:00:00"),
        TotalPrice = 550,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 83,
        UserId = 13,
        RoomId = 2,
        CheckInDate = DateTime.Parse("2025-06-17T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-22T00:00:00"),
        TotalPrice = 550,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 84,
        UserId = 13,
        RoomId = 6,
        CheckInDate = DateTime.Parse("2025-07-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-07T00:00:00"),
        TotalPrice = 600,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 85,
        UserId = 13,
        RoomId = 8,
        CheckInDate = DateTime.Parse("2025-07-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-05T00:00:00"),
        TotalPrice = 320,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 86,
        UserId = 13,
        RoomId = 3,
        CheckInDate = DateTime.Parse("2025-05-15T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-05-21T00:00:00"),
        TotalPrice = 770,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 87,
        UserId = 13,
        RoomId = 8,
        CheckInDate = DateTime.Parse("2025-07-06T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-07-11T00:00:00"),
        TotalPrice = 400,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 88,
        UserId = 13,
        RoomId = 9,
        CheckInDate = DateTime.Parse("2025-06-01T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-07T00:00:00"),
        TotalPrice = 780,
        Status = "Confirmed",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 89,
        UserId = 13,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-06-14T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-21T00:00:00"),
        TotalPrice = 1100,
        Status = "Pending",
        CreatedAt = null
    },
    new Reservation
    {
        Id = 90,
        UserId = 13,
        RoomId = 1,
        CheckInDate = DateTime.Parse("2025-06-14T00:00:00"),
        CheckOutDate = DateTime.Parse("2025-06-21T00:00:00"),
        TotalPrice = 1100,
        Status = "Pending",
        CreatedAt = null
    }

);

        // Services
        modelBuilder.Entity<Service>().HasData(
            new Service { Id = 1, Name = "Breakfast", Description = "Buffet breakfast included", Price = 15, HotelId = 1 },
            new Service { Id = 2, Name = "Parking", Description = "Underground parking", Price = 10, HotelId = 2 }
        );

        // Reviews
        modelBuilder.Entity<Review>().HasData(
    new Review { Id = 1, UserId = 2, HotelId = 1, ReservationId = 3, Rating = 4, Comment = "Very good", ReviewDate = DateTime.Parse("2025-08-07T06:40:56.07") },
    new Review { Id = 2, UserId = 1, HotelId = 1, ReservationId = 1, Rating = 5, Comment = "Very good", ReviewDate = DateTime.Parse("2025-08-22T18:15:14.967") },
    new Review { Id = 3, UserId = 1, HotelId = 1, ReservationId = 1, Rating = 5, Comment = "Super", ReviewDate = DateTime.Parse("2025-08-22T18:29:08.153") }
);

        // Notifications
        modelBuilder.Entity<Notification>().HasData(
    new Notification { Id = 1, UserId = 2, Title = "Hello world", Message = "Hello world", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-08T20:14:18.847") },
    new Notification { Id = 2, UserId = 2, Title = "Reservation status updated", Message = "Your reservation #3 status changed to Completed", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-08T20:24:24.237") },
    new Notification { Id = 3, UserId = 2, Title = "Reservation status updated", Message = "Your reservation #3 status changed to Completed", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-08T20:29:23.607") },
    new Notification { Id = 4, UserId = 2, Title = "Reservation status updated", Message = "Your reservation #3 status changed to Completed", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-08T20:30:10.677") },
    new Notification { Id = 5, UserId = 6, Title = "Reservation status updated", Message = "Your reservation #4 status changed to Completed", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-08T20:31:04.463") },
    new Notification { Id = 7, UserId = 6, Title = "Reservation Created", Message = "Your reservation #7 for 28.08.2025 - 31.08.2025 is created. Total: 500,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-16T23:37:02.967") },
    new Notification { Id = 8, UserId = 6, Title = "Reservation Created", Message = "Your reservation #8 for 01.08.2025 - 03.08.2025 is created. Total: 250,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-16T23:42:54.683") },
    new Notification { Id = 9, UserId = 6, Title = "Reservation Created", Message = "Your reservation #9 for 01.08.2025 - 03.08.2025 is created. Total: 250,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-16T23:45:22.73") },
    new Notification { Id = 10, UserId = 6, Title = "Reservation Created", Message = "Your reservation #10 for 17.08.2025 - 18.08.2025 is created. Total: 150,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-16T23:46:37.26") },
    new Notification { Id = 11, UserId = 6, Title = "Reservation Created", Message = "Your reservation #11 for 01.08.2025 - 05.08.2025 is created. Total: 520,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-16T23:52:32.633") },
    new Notification { Id = 12, UserId = 6, Title = "Reservation Created", Message = "Your reservation #12 for 01.08.2025 - 05.08.2025 is created. Total: 560,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-16T23:56:27.64") },
    new Notification { Id = 13, UserId = 6, Title = "Reservation Created", Message = "Your reservation #13 for 01.08.2025 - 05.08.2025 is created. Total: 560,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-16T23:57:31.307") },
    new Notification { Id = 14, UserId = 6, Title = "Reservation Created", Message = "Your reservation #14 for 01.08.2025 - 05.08.2025 is created. Total: 560,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-16T23:57:40.73") },
    new Notification { Id = 15, UserId = 6, Title = "Reservation Created", Message = "Your reservation #15 for 16.08.2025 - 16.08.2025 is created. Total: 500,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-16T23:59:34.067") },
    new Notification { Id = 16, UserId = 6, Title = "Reservation Created", Message = "Your reservation #16 for 05.08.2025 - 08.08.2025 is created. Total: 420,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T08:06:39.97") },
    new Notification { Id = 17, UserId = 6, Title = "Reservation Created", Message = "Your reservation #17 for 05.08.2025 - 08.08.2025 is created. Total: 420,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T08:07:18.94") },
    new Notification { Id = 18, UserId = 6, Title = "Reservation Created", Message = "Your reservation #18 for 05.08.2025 - 08.08.2025 is created. Total: 420,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T08:10:31.163") },
    new Notification { Id = 19, UserId = 6, Title = "Reservation Created", Message = "Your reservation #19 for 01.08.2025 - 04.08.2025 is created. Total: 300,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T08:11:26.45") },
    new Notification { Id = 20, UserId = 6, Title = "Reservation Created", Message = "Your reservation #20 for 04.08.2025 - 07.08.2025 is created. Total: 300,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T09:11:53.177") },
    new Notification { Id = 21, UserId = 6, Title = "Reservation Created", Message = "Your reservation #21 for 05.08.2025 - 09.08.2025 is created. Total: 520,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T09:16:11.31") },
    new Notification { Id = 22, UserId = 6, Title = "Reservation Created", Message = "Your reservation #22 for 09.08.2025 - 12.08.2025 is created. Total: 395,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T09:19:32.817") },
    new Notification { Id = 23, UserId = 6, Title = "Reservation Created", Message = "Your reservation #23 for 08.08.2025 - 12.08.2025 is created. Total: 560,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T15:18:19.567") },
    new Notification { Id = 24, UserId = 6, Title = "Reservation Created", Message = "Your reservation #24 for 07.08.2025 - 12.08.2025 is created. Total: 500,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T15:58:18.583") },
    new Notification { Id = 25, UserId = 6, Title = "Reservation Created", Message = "Your reservation #25 for 12.08.2025 - 16.08.2025 is created. Total: 560,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T15:59:58.07") },
    new Notification { Id = 26, UserId = 6, Title = "Reservation Created", Message = "Your reservation #26 for 03.08.2025 - 06.08.2025 is created. Total: 300,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T16:09:38.747") },
    new Notification { Id = 28, UserId = 6, Title = "Reservation Created", Message = "Your reservation #28 for 01.08.2025 - 03.08.2025 is created. Total: 350,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T16:17:55.037") },
    new Notification { Id = 29, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T16:17:55.233") },
    new Notification { Id = 30, UserId = 6, Title = "Reservation Created", Message = "Your reservation #29 for 12.08.2025 - 16.08.2025 is created. Total: 400,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T16:19:06.56") },
    new Notification { Id = 31, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T16:19:06.703") },
    new Notification { Id = 32, UserId = 6, Title = "Reservation Created", Message = "Your reservation #30 for 12.08.2025 - 16.08.2025 is created. Total: 400,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T16:19:49.727") },
    new Notification { Id = 33, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T16:19:49.947") },
    new Notification { Id = 34, UserId = 6, Title = "Reservation Created", Message = "Your reservation #31 for 12.08.2025 - 16.08.2025 is created. Total: 400,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T16:20:00.587") },
    new Notification { Id = 35, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T16:20:00.76") },
    new Notification { Id = 37, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T16:20:12.697") },
    new Notification { Id = 38, UserId = 6, Title = "Reservation Created", Message = "Your reservation #33 for 01.07.2025 - 05.07.2025 is created. Total: 650,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T16:56:09.017") },
    new Notification { Id = 39, UserId = 6, Title = "Reservation Created", Message = "Your reservation #34 for 05.07.2025 - 10.07.2025 is created. Total: 800,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T17:07:13.937") },
    new Notification { Id = 40, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T17:07:14.143") },
    new Notification { Id = 41, UserId = 6, Title = "Reservation Created", Message = "Your reservation #35 for 01.07.2025 - 06.07.2025 is created. Total: 645,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T20:00:47.18") },
    new Notification { Id = 42, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 01.07.2025\nCheck-out: 06.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-17T20:00:47.407") },
    new Notification { Id = 43, UserId = 6, Title = "Reservation Created", Message = "Your reservation #36 for 10.07.2025 - 17.07.2025 is created. Total: 1.100,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T13:36:44.51") },
    new Notification { Id = 44, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 10.07.2025\nCheck-out: 17.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T13:36:45.037") },
    new Notification { Id = 45, UserId = 6, Title = "Reservation Created", Message = "Your reservation #37 for 10.07.2025 - 17.07.2025 is created. Total: 1.100,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T13:41:53.747") },
    new Notification { Id = 46, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 10.07.2025\nCheck-out: 17.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T13:41:54.013") },
    new Notification { Id = 47, UserId = 6, Title = "Reservation Created", Message = "Your reservation #38 for 10.07.2025 - 17.07.2025 is created. Total: 1.100,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T13:42:08.683") },
    new Notification { Id = 48, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 10.07.2025\nCheck-out: 17.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T13:42:08.94") },
    new Notification { Id = 50, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 01.07.2025\nCheck-out: 05.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T13:44:48.733") },
    new Notification { Id = 51, UserId = 6, Title = "Reservation Created", Message = "Your reservation #40 for 06.07.2025 - 12.07.2025 is created. Total: 770,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T13:48:56.94") },
    new Notification { Id = 52, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 06.07.2025\nCheck-out: 12.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T13:48:57.163") },
    new Notification { Id = 53, UserId = 6, Title = "Reservation Created", Message = "Your reservation #41 for 06.07.2025 - 12.07.2025 is created. Total: 770,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T13:51:39.043") },
    new Notification { Id = 54, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 06.07.2025\nCheck-out: 12.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T13:51:39.53") },
    new Notification { Id = 55, UserId = 6, Title = "Reservation Created", Message = "Your reservation #42 for 06.07.2025 - 12.07.2025 is created. Total: 770,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T13:53:39.473") },
    new Notification { Id = 56, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 06.07.2025\nCheck-out: 12.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T13:53:39.81") },
    new Notification { Id = 57, UserId = 6, Title = "Reservation Created", Message = "Your reservation #43 for 17.07.2025 - 23.07.2025 is created. Total: 950,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T15:26:46.147") },
    new Notification { Id = 58, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 17.07.2025\nCheck-out: 23.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T15:26:46.47") },
    new Notification { Id = 59, UserId = 6, Title = "Reservation Created", Message = "Your reservation #44 for 12.07.2025 - 17.07.2025 is created. Total: 645,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T15:56:22.687") },
    new Notification { Id = 60, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 12.07.2025\nCheck-out: 17.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T15:56:22.907") },
    new Notification { Id = 61, UserId = 6, Title = "Reservation Created", Message = "Your reservation #45 for 17.07.2025 - 19.07.2025 is created. Total: 250,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T16:00:01.533") },
    new Notification { Id = 62, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 17.07.2025\nCheck-out: 19.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T16:00:01.81") },
    new Notification { Id = 63, UserId = 6, Title = "Reservation Created", Message = "Your reservation #46 for 17.07.2025 - 19.07.2025 is created. Total: 270,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T16:03:23.53") },
    new Notification { Id = 64, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 17.07.2025\nCheck-out: 19.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T16:03:23.693") },
    new Notification { Id = 65, UserId = 6, Title = "Reservation Created", Message = "Your reservation #47 for 17.07.2025 - 19.07.2025 is created. Total: 270,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T16:59:43.73") },
    new Notification { Id = 66, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 17.07.2025\nCheck-out: 19.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T16:59:43.907") },
    new Notification { Id = 67, UserId = 6, Title = "Reservation Created", Message = "Your reservation #48 for 19.07.2025 - 21.07.2025 is created. Total: 250,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T17:05:32.08") },
    new Notification { Id = 68, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 19.07.2025\nCheck-out: 21.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T17:05:32.243") },
    new Notification { Id = 69, UserId = 6, Title = "Reservation Created", Message = "Your reservation #49 for 05.07.2025 - 10.07.2025 is created. Total: 550,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T17:50:12.673") },
    new Notification { Id = 70, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 05.07.2025\nCheck-out: 10.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T17:50:12.983") },
    new Notification { Id = 71, UserId = 6, Title = "Reservation Created", Message = "Your reservation #50 for 05.07.2025 - 10.07.2025 is created. Total: 550,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T17:55:55.47") },
    new Notification { Id = 72, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 05.07.2025\nCheck-out: 10.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T17:55:55.667") },
    new Notification { Id = 73, UserId = 6, Title = "Reservation Created", Message = "Your reservation #51 for 05.07.2025 - 10.07.2025 is created. Total: 550,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T17:56:06.747") },
    new Notification { Id = 74, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 05.07.2025\nCheck-out: 10.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T17:56:06.927") },
    new Notification { Id = 75, UserId = 6, Title = "Reservation Created", Message = "Your reservation #52 for 21.07.2025 - 26.07.2025 is created. Total: 645,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T17:58:47.173") },
    new Notification { Id = 76, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 21.07.2025\nCheck-out: 26.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T17:58:47.343") },
    new Notification { Id = 77, UserId = 6, Title = "Reservation Created", Message = "Your reservation #53 for 21.07.2025 - 26.07.2025 is created. Total: 645,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T18:06:42.65") },
    new Notification { Id = 78, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 21.07.2025\nCheck-out: 26.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T18:06:42.827") },
    new Notification { Id = 79, UserId = 6, Title = "Reservation Created", Message = "Your reservation #54 for 10.07.2025 - 12.07.2025 is created. Total: 250,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T20:23:45.387") },
    new Notification { Id = 80, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 10.07.2025\nCheck-out: 12.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T20:23:45.683") },
    new Notification { Id = 81, UserId = 6, Title = "Reservation Created", Message = "Your reservation #55 for 12.07.2025 - 17.07.2025 is created. Total: 550,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T21:43:30.577") },
    new Notification { Id = 82, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 12.07.2025\nCheck-out: 17.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T21:43:30.907") },
    new Notification { Id = 83, UserId = 6, Title = "Reservation Created", Message = "Your reservation #56 for 12.07.2025 - 17.07.2025 is created. Total: 550,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T21:47:37.74") },
    new Notification { Id = 84, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 12.07.2025\nCheck-out: 17.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T21:47:37.92") },
    new Notification { Id = 85, UserId = 6, Title = "Reservation Created", Message = "Your reservation #57 for 12.07.2025 - 17.07.2025 is created. Total: 550,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T21:51:00.533") },
    new Notification { Id = 86, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 12.07.2025\nCheck-out: 17.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T21:51:00.71") },
    new Notification { Id = 87, UserId = 6, Title = "Reservation Created", Message = "Your reservation #58 for 26.07.2025 - 31.07.2025 is created. Total: 645,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T21:53:02.073") },
    new Notification { Id = 88, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 26.07.2025\nCheck-out: 31.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T21:53:02.317") },
    new Notification { Id = 89, UserId = 6, Title = "Reservation Created", Message = "Your reservation #59 for 26.07.2025 - 31.07.2025 is created. Total: 645,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T22:00:02.743") },
    new Notification { Id = 90, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 26.07.2025\nCheck-out: 31.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T22:00:02.963") },
    new Notification { Id = 92, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 01.06.2025\nCheck-out: 05.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-18T22:05:55.603") },
    new Notification { Id = 93, UserId = 6, Title = "Reservation Created", Message = "Your reservation #61 for 05.06.2025 - 10.06.2025 is created. Total: 645,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-19T09:22:45.93") },
    new Notification { Id = 94, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 05.06.2025\nCheck-out: 10.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-19T09:22:46.283") },
    new Notification { Id = 95, UserId = 6, Title = "Reservation Created", Message = "Your reservation #62 for 05.06.2025 - 10.06.2025 is created. Total: 645,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-19T10:00:04.22") },
    new Notification { Id = 96, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 05.06.2025\nCheck-out: 10.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-19T10:00:04.427") },
      new Notification { Id = 108, UserId = 6, Title = "Reservation Created", Message = "Your reservation #68 for 01.07.2025 - 05.07.2025 is created. Total: 400,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-19T12:13:54.887") },
    new Notification { Id = 109, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hollywood\nRoom: Deluxe Double Room\nCheck-in: 01.07.2025\nCheck-out: 05.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-19T12:13:55.18") },
    new Notification { Id = 110, UserId = 6, Title = "Payment succeeded", Message = "✅ Vaše plaćanje za rezervaciju #68 u iznosu 400,00 USD je uspješno. Status rezervacije: Confirmed.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-19T12:14:17.83") },
    new Notification { Id = 111, UserId = 13, Title = "Welcome to HotelEase 🎉", Message = "Hello mujo,<br/><br/>Your registration was successful!<br/>Welcome to HotelEase 🏨.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-20T22:21:56.063") },
    new Notification { Id = 112, UserId = 6, Title = "Reservation Created", Message = "Your reservation #69 for 14.06.2025 - 19.06.2025 is created. Total: 645,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-21T10:18:30.517") },
    new Notification { Id = 113, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 14.06.2025\nCheck-out: 19.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-21T10:18:30.71") },
    new Notification { Id = 114, UserId = 6, Title = "Payment succeeded", Message = "✅ Vaše plaćanje za rezervaciju #69 u iznosu 645,00 USD je uspješno. Status rezervacije: Confirmed.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-21T10:18:57.48") },
    new Notification { Id = 115, UserId = 6, Title = "Reservation Created", Message = "Your reservation #70 for 23.07.2025 - 28.07.2025 is created. Total: 800,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-21T10:26:34.837") },
    new Notification { Id = 116, UserId = 6, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 23.07.2025\nCheck-out: 28.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-21T10:26:34.98") },
    new Notification { Id = 117, UserId = 6, Title = "Payment succeeded", Message = "✅ Vaše plaćanje za rezervaciju #70 u iznosu 800,00 USD je uspješno. Status rezervacije: Confirmed.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-21T10:26:55.447") },
    new Notification { Id = 118, UserId = 13, Title = "Reservation Created", Message = "Your reservation #71 for 17.07.2025 - 23.07.2025 is created. Total: 650,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:30:03.407") },
    new Notification { Id = 119, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 17.07.2025\nCheck-out: 23.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:30:03.82") },
    new Notification { Id = 120, UserId = 13, Title = "Payment succeeded", Message = "✅ Vaše plaćanje za rezervaciju #71 u iznosu 650,00 USD je uspješno. Status rezervacije: Confirmed.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:30:26.347") },
    new Notification { Id = 121, UserId = 13, Title = "Reservation Created", Message = "Your reservation #72 for 01.06.2025 - 07.06.2025 is created. Total: 900,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:30:38.637") },
    new Notification { Id = 122, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 01.06.2025\nCheck-out: 07.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:30:38.86") },
    new Notification { Id = 123, UserId = 13, Title = "Payment succeeded", Message = "✅ Vaše plaćanje za rezervaciju #72 u iznosu 900,00 USD je uspješno. Status rezervacije: Confirmed.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:30:57.72") },
    new Notification { Id = 124, UserId = 13, Title = "Reservation Created", Message = "Your reservation #73 for 07.06.2025 - 14.06.2025 is created. Total: 1.050,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:31:08.653") },
    new Notification { Id = 125, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 07.06.2025\nCheck-out: 14.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:31:08.843") },
    new Notification { Id = 126, UserId = 13, Title = "Payment succeeded", Message = "✅ Vaše plaćanje za rezervaciju #73 u iznosu 1050,00 USD je uspješno. Status rezervacije: Confirmed.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:31:25.113") },
    new Notification { Id = 127, UserId = 13, Title = "Reservation Created", Message = "Your reservation #74 for 01.06.2025 - 07.06.2025 is created. Total: 650,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:31:42.447") },
    new Notification { Id = 128, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 01.06.2025\nCheck-out: 07.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:31:42.65") },
    new Notification { Id = 129, UserId = 13, Title = "Payment succeeded", Message = "✅ Vaše plaćanje za rezervaciju #74 u iznosu 650,00 USD je uspješno. Status rezervacije: Confirmed.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:31:57.48") },
    new Notification { Id = 130, UserId = 13, Title = "Reservation Created", Message = "Your reservation #75 for 07.06.2025 - 17.06.2025 is created. Total: 1.000,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:32:11.103") },
    new Notification { Id = 131, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 07.06.2025\nCheck-out: 17.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:32:11.33") },
    new Notification { Id = 132, UserId = 13, Title = "Payment succeeded", Message = "✅ Vaše plaćanje za rezervaciju #75 u iznosu 1000,00 USD je uspješno. Status rezervacije: Confirmed.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:32:26.407") },
    new Notification { Id = 133, UserId = 13, Title = "Reservation Created", Message = "Your reservation #76 for 19.06.2025 - 21.06.2025 is created. Total: 270,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:32:39.733") },
    new Notification { Id = 134, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 19.06.2025\nCheck-out: 21.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:32:39.913") },
    new Notification { Id = 135, UserId = 13, Title = "Payment succeeded", Message = "✅ Vaše plaćanje za rezervaciju #76 u iznosu 270,00 USD je uspješno. Status rezervacije: Confirmed.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:32:57.203") },
    new Notification { Id = 136, UserId = 13, Title = "Reservation Created", Message = "Your reservation #77 for 21.06.2025 - 25.06.2025 is created. Total: 520,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:33:14.683") },
    new Notification { Id = 137, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 21.06.2025\nCheck-out: 25.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:33:14.873") },
    new Notification { Id = 138, UserId = 13, Title = "Payment succeeded", Message = "✅ Vaše plaćanje za rezervaciju #77 u iznosu 520,00 USD je uspješno. Status rezervacije: Confirmed.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:33:36.877") },
    new Notification { Id = 139, UserId = 13, Title = "Reservation Created", Message = "Your reservation #78 for 25.06.2025 - 30.06.2025 is created. Total: 625,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:33:47.11") },
    new Notification { Id = 140, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 25.06.2025\nCheck-out: 30.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:33:47.3") },
    new Notification { Id = 141, UserId = 13, Title = "Payment succeeded", Message = "✅ Vaše plaćanje za rezervaciju #78 u iznosu 625,00 USD je uspješno. Status rezervacije: Confirmed.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:34:02.237") },
    new Notification { Id = 142, UserId = 13, Title = "Reservation Created", Message = "Your reservation #79 for 25.06.2025 - 30.06.2025 is created. Total: 625,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:34:04.597") },
    new Notification { Id = 143, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 25.06.2025\nCheck-out: 30.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:34:04.78") },
    new Notification { Id = 144, UserId = 13, Title = "Payment succeeded", Message = "✅ Vaše plaćanje za rezervaciju #79 u iznosu 625,00 USD je uspješno. Status rezervacije: Confirmed.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:34:23.357") },
    new Notification { Id = 145, UserId = 13, Title = "Reservation Created", Message = "Your reservation #80 for 01.05.2025 - 15.05.2025 is created. Total: 1.770,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:34:36.507") },
    new Notification { Id = 146, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 01.05.2025\nCheck-out: 15.05.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:34:36.707") },
    new Notification { Id = 147, UserId = 13, Title = "Payment succeeded", Message = "✅ Vaše plaćanje za rezervaciju #80 u iznosu 1770,00 USD je uspješno. Status rezervacije: Confirmed.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:34:51.967") },
    new Notification { Id = 148, UserId = 13, Title = "Reservation Created", Message = "Your reservation #81 for 01.05.2025 - 15.05.2025 is created. Total: 1.770,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:34:54.497") },
    new Notification { Id = 149, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 01.05.2025\nCheck-out: 15.05.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-22T18:34:54.677") },
    new Notification { Id = 150, UserId = 13, Title = "Reservation Created", Message = "Your reservation #82 for 17.06.2025 - 22.06.2025 is created. Total: 550,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-25T08:28:00.8") },
    new Notification { Id = 151, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 17.06.2025\nCheck-out: 22.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-25T08:28:01.163") },
    new Notification { Id = 152, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 17.06.2025\nCheck-out: 22.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-25T08:33:59.43") },
    new Notification { Id = 153, UserId = 13, Title = "Reservation Created", Message = "Your reservation #84 for 01.07.2025 - 07.07.2025 is created. Total: 600,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-25T08:39:37.883") },
    new Notification { Id = 154, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Swissotel Sarajevo\nRoom: Standard Double Room\nCheck-in: 01.07.2025\nCheck-out: 07.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-25T08:39:38.18") },
    new Notification { Id = 155, UserId = 13, Title = "Reservation Created", Message = "Your reservation #85 for 01.07.2025 - 05.07.2025 is created. Total: 320,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-25T08:41:19.193") },
    new Notification { Id = 156, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hollywood\nRoom: Single Superior Room\nCheck-in: 01.07.2025\nCheck-out: 05.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-25T08:41:19.49") },
    new Notification { Id = 157, UserId = 13, Title = "Reservation Created", Message = "Your reservation #86 for 15.05.2025 - 21.05.2025 is created. Total: 770,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-25T08:43:14.13") },
    new Notification { Id = 158, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 15.05.2025\nCheck-out: 21.05.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-25T08:43:14.377") },
    new Notification { Id = 159, UserId = 13, Title = "Reservation Created", Message = "Your reservation #87 for 06.07.2025 - 11.07.2025 is created. Total: 400,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-25T08:48:22.507") },
    new Notification { Id = 160, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hollywood\nRoom: Single Superior Room\nCheck-in: 06.07.2025\nCheck-out: 11.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-25T08:48:22.797") },
    new Notification { Id = 161, UserId = 13, Title = "Reservation Created", Message = "Your reservation #88 for 01.06.2025 - 07.06.2025 is created. Total: 780,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-25T08:56:29.197") },
    new Notification { Id = 162, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hollywood\nRoom: Twin Superior Room\nCheck-in: 01.06.2025\nCheck-out: 07.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-25T08:56:29.497") },
    new Notification { Id = 163, UserId = 13, Title = "Payment succeeded", Message = "✅ Vaše plaćanje za rezervaciju #88 u iznosu 780,00 USD je uspješno. Status rezervacije: Confirmed.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-25T08:56:55.41") },
    new Notification { Id = 164, UserId = 13, Title = "Reservation Created", Message = "Your reservation #89 for 14.06.2025 - 21.06.2025 is created. Total: 1.100,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T13:56:02.563") },
    new Notification { Id = 165, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 14.06.2025\nCheck-out: 21.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T13:56:02.74") },
    new Notification { Id = 166, UserId = 13, Title = "Reservation Created", Message = "Your reservation #90 for 14.06.2025 - 21.06.2025 is created. Total: 1.100,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T13:56:12.44") },
    new Notification { Id = 167, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 14.06.2025\nCheck-out: 21.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T13:56:12.56") },
    new Notification { Id = 168, UserId = 13, Title = "Reservation Created", Message = "Your reservation #91 for 21.06.2025 - 28.06.2025 is created. Total: 1.100,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T13:59:31.723") },
    new Notification { Id = 169, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 21.06.2025\nCheck-out: 28.06.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T13:59:31.963") },
    new Notification { Id = 170, UserId = 13, Title = "Reservation Created", Message = "Your reservation #92 for 01.08.2025 - 08.08.2025 is created. Total: 560,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T14:03:11.38") },
    new Notification { Id = 171, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hollywood\nRoom: Single Superior Room\nCheck-in: 01.08.2025\nCheck-out: 08.08.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T14:03:11.543") },
    new Notification { Id = 172, UserId = 13, Title = "Reservation Created", Message = "Your reservation #93 for 01.07.2025 - 08.07.2025 is created. Total: 1.260,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T14:04:57.32") },
    new Notification { Id = 173, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Swissotel Sarajevo\nRoom: Standard King Room\nCheck-in: 01.07.2025\nCheck-out: 08.07.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T14:04:57.407") },
    new Notification { Id = 174, UserId = 13, Title = "Reservation Created", Message = "Your reservation #94 for 01.08.2025 - 08.08.2025 is created. Total: 910,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T14:10:38.88") },
    new Notification { Id = 175, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hollywood\nRoom: Twin Superior Room\nCheck-in: 01.08.2025\nCheck-out: 08.08.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T14:10:38.997") },
    new Notification { Id = 176, UserId = 13, Title = "Reservation Created", Message = "Your reservation #95 for 01.08.2025 - 08.08.2025 is created. Total: 910,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T14:22:34.257") },
    new Notification { Id = 177, UserId = 13, Title = "Reservation Confirmed", Message = "Your reservation has been successfully created.\n\nHotel: Hotel Hollywood\nRoom: Twin Superior Room\nCheck-in: 01.08.2025\nCheck-out: 08.08.2025", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T14:22:34.567") },
    new Notification { Id = 178, UserId = 13, Title = "Reservation Created", Message = "Your reservation #96 for 08.08.2025 - 15.08.2025 is created. Total: 910,00 KM.", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-09-01T14:29:40.453") }


    );
        AssetSeeder.SeedAssets(modelBuilder);

        modelBuilder.Entity<Payment>().HasData(
    new Payment { Id = 1, ReservationId = 53, Provider = "stripe", ProviderPaymentId = "pi_3RxVaKKH5av5GhJI0TtbCxav", Amount = 645, Currency = "USD", Status = "requires_payment_method", CreatedAt = DateTime.Parse("2025-08-18T16:07:00.347"), UpdatedAt = null, IsDeleted = null, DeletedTime = null },
    new Payment { Id = 2, ReservationId = 56, Provider = "stripe", ProviderPaymentId = "pi_3RxZ28KH5av5GhJI0CUJ63XF", Amount = 550, Currency = "USD", Status = "requires_payment_method", CreatedAt = DateTime.Parse("2025-08-18T19:47:56.53"), UpdatedAt = null, IsDeleted = null, DeletedTime = null },
    new Payment { Id = 3, ReservationId = 56, Provider = "stripe", ProviderPaymentId = "pi_3RxZ2uKH5av5GhJI1j745cw1", Amount = 550, Currency = "USD", Status = "requires_payment_method", CreatedAt = DateTime.Parse("2025-08-18T19:48:44.65"), UpdatedAt = null, IsDeleted = null, DeletedTime = null },
    new Payment { Id = 4, ReservationId = 57, Provider = "stripe", ProviderPaymentId = "pi_3RxZ5WKH5av5GhJI1RVq9UBb", Amount = 550, Currency = "USD", Status = "requires_payment_method", CreatedAt = DateTime.Parse("2025-08-18T19:51:26.887"), UpdatedAt = null, IsDeleted = null, DeletedTime = null },
    new Payment { Id = 5, ReservationId = 59, Provider = "stripe", ProviderPaymentId = "pi_3RxZEAKH5av5GhJI1w9aQe3w", Amount = 645, Currency = "USD", Status = "requires_payment_method", CreatedAt = DateTime.Parse("2025-08-18T20:00:22.053"), UpdatedAt = null, IsDeleted = null, DeletedTime = null },
    new Payment { Id = 6, ReservationId = 59, Provider = "stripe", ProviderPaymentId = "pi_3RxZFUKH5av5GhJI01TZhB1z", Amount = 645, Currency = "USD", Status = "requires_payment_method", CreatedAt = DateTime.Parse("2025-08-18T20:01:44.41"), UpdatedAt = null, IsDeleted = null, DeletedTime = null },
    new Payment { Id = 7, ReservationId = 60, Provider = "stripe", ProviderPaymentId = "pi_3RxZJpKH5av5GhJI0nOZjyEP", Amount = 520, Currency = "USD", Status = "requires_payment_method", CreatedAt = DateTime.Parse("2025-08-18T20:06:13.14"), UpdatedAt = null, IsDeleted = null, DeletedTime = null },
    new Payment { Id = 8, ReservationId = 61, Provider = "stripe", ProviderPaymentId = "pi_3RxjthKH5av5GhJI02tmdHJd", Amount = 645, Currency = "USD", Status = "requires_payment_method", CreatedAt = DateTime.Parse("2025-08-19T07:23:57.447"), UpdatedAt = null, IsDeleted = null, DeletedTime = null },
    new Payment { Id = 9, ReservationId = 62, Provider = "stripe", ProviderPaymentId = "pi_3RxkSvKH5av5GhJI1AhEKH1y", Amount = 645, Currency = "USD", Status = "requires_payment_method", CreatedAt = DateTime.Parse("2025-08-19T08:00:21.243"), UpdatedAt = null, IsDeleted = null, DeletedTime = null },
    new Payment { Id = 10, ReservationId = 63, Provider = "stripe", ProviderPaymentId = "pi_3RxkVPKH5av5GhJI1a1s20y8", Amount = 520, Currency = "USD", Status = "requires_payment_method", CreatedAt = DateTime.Parse("2025-08-19T08:02:54.91"), UpdatedAt = null, IsDeleted = null, DeletedTime = null },
    new Payment { Id = 11, ReservationId = 63, Provider = "stripe", ProviderPaymentId = "pi_3RxkVRKH5av5GhJI0cBEouxn", Amount = 520, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-19T08:02:56.97"), UpdatedAt = DateTime.Parse("2025-08-19T08:03:34.327"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 12, ReservationId = 64, Provider = "stripe", ProviderPaymentId = "pi_3RxkgKKH5av5GhJI0IghVNgj", Amount = 520, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-19T08:14:11.967"), UpdatedAt = DateTime.Parse("2025-08-19T08:14:44.967"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 13, ReservationId = 65, Provider = "stripe", ProviderPaymentId = "pi_3RxkhmKH5av5GhJI1VvZgCc7", Amount = 520, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-19T08:15:41.46"), UpdatedAt = DateTime.Parse("2025-08-19T08:15:59.103"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 14, ReservationId = 66, Provider = "stripe", ProviderPaymentId = "pi_3RxkiBKH5av5GhJI0FDlMPWT", Amount = 520, Currency = "USD", Status = "requires_payment_method", CreatedAt = DateTime.Parse("2025-08-19T08:16:06.87"), UpdatedAt = null, IsDeleted = null, DeletedTime = null },
    new Payment { Id = 15, ReservationId = 66, Provider = "stripe", ProviderPaymentId = "pi_3RxkmMKH5av5GhJI1HXaZSZc", Amount = 520, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-19T08:20:26.013"), UpdatedAt = DateTime.Parse("2025-08-19T08:20:49.23"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 16, ReservationId = 67, Provider = "stripe", ProviderPaymentId = "pi_3Rxlb2KH5av5GhJI1DiObtS8", Amount = 560, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-19T09:12:47.587"), UpdatedAt = DateTime.Parse("2025-08-19T09:13:06.757"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 17, ReservationId = 68, Provider = "stripe", ProviderPaymentId = "pi_3RxmYFKH5av5GhJI1iksnFs6", Amount = 400, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-19T10:13:58.47"), UpdatedAt = DateTime.Parse("2025-08-19T10:14:17.83"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 18, ReservationId = 69, Provider = "stripe", ProviderPaymentId = "pi_3RyThdKH5av5GhJI0ZsV59V5", Amount = 645, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-21T08:18:34.1"), UpdatedAt = DateTime.Parse("2025-08-21T08:18:57.48"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 19, ReservationId = 70, Provider = "stripe", ProviderPaymentId = "pi_3RyTpRKH5av5GhJI1bWbc61O", Amount = 800, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-21T08:26:37.363"), UpdatedAt = DateTime.Parse("2025-08-21T08:26:55.447"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 20, ReservationId = 71, Provider = "stripe", ProviderPaymentId = "pi_3RyxqsKH5av5GhJI1UpyMP6h", Amount = 650, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-22T16:30:06.5"), UpdatedAt = DateTime.Parse("2025-08-22T16:30:26.347"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 21, ReservationId = 72, Provider = "stripe", ProviderPaymentId = "pi_3RyxrQKH5av5GhJI1B65tf0S", Amount = 900, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-22T16:30:40.337"), UpdatedAt = DateTime.Parse("2025-08-22T16:30:57.72"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 22, ReservationId = 73, Provider = "stripe", ProviderPaymentId = "pi_3RyxruKH5av5GhJI1ud55iEj", Amount = 1050, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-22T16:31:10.283"), UpdatedAt = DateTime.Parse("2025-08-22T16:31:25.113"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 23, ReservationId = 74, Provider = "stripe", ProviderPaymentId = "pi_3RyxsSKH5av5GhJI1wYe3PAa", Amount = 650, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-22T16:31:44.053"), UpdatedAt = DateTime.Parse("2025-08-22T16:31:57.48"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 24, ReservationId = 75, Provider = "stripe", ProviderPaymentId = "pi_3RyxsvKH5av5GhJI1uSbokVs", Amount = 1000, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-22T16:32:12.943"), UpdatedAt = DateTime.Parse("2025-08-22T16:32:26.407"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 25, ReservationId = 76, Provider = "stripe", ProviderPaymentId = "pi_3RyxtNKH5av5GhJI1MFgRmlY", Amount = 270, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-22T16:32:41.37"), UpdatedAt = DateTime.Parse("2025-08-22T16:32:57.203"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 26, ReservationId = 77, Provider = "stripe", ProviderPaymentId = "pi_3RyxtwKH5av5GhJI05yLJEBT", Amount = 520, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-22T16:33:16.237"), UpdatedAt = DateTime.Parse("2025-08-22T16:33:36.877"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 27, ReservationId = 78, Provider = "stripe", ProviderPaymentId = "pi_3RyxuSKH5av5GhJI04plVd0n", Amount = 625, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-22T16:33:48.72"), UpdatedAt = DateTime.Parse("2025-08-22T16:34:02.237"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 28, ReservationId = 79, Provider = "stripe", ProviderPaymentId = "pi_3RyxukKH5av5GhJI1K0DqUVl", Amount = 625, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-22T16:34:06.373"), UpdatedAt = DateTime.Parse("2025-08-22T16:34:23.357"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 29, ReservationId = 80, Provider = "stripe", ProviderPaymentId = "pi_3RyxvGKH5av5GhJI12RQJgwG", Amount = 1770, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-22T16:34:38.027"), UpdatedAt = DateTime.Parse("2025-08-22T16:34:51.967"), IsDeleted = null, DeletedTime = null },
    new Payment { Id = 30, ReservationId = 88, Provider = "stripe", ProviderPaymentId = "pi_3RzuKQKH5av5GhJI0XCCT0Qn", Amount = 780, Currency = "USD", Status = "succeeded", CreatedAt = DateTime.Parse("2025-08-25T06:56:32.093"), UpdatedAt = DateTime.Parse("2025-08-25T06:56:55.41"), IsDeleted = null, DeletedTime = null }
   );


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
