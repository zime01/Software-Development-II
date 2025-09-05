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
            new City { Id = 2, Name = "Mostar", CountryId = 1 }
        );

        // Hotels
        modelBuilder.Entity<Hotel>().HasData(
            new Hotel { Id = 1, Name = "Hotel Hills", CountryId = 1, CityId = 1, ManagerId = 1, Address = "Some Address" },
            new Hotel { Id = 2, Name = "Hotel Europe", CountryId = 1, CityId = 1, ManagerId = 1, Address = "Some Address" },
            new Hotel { Id = 3, Name = "Swissotel Sarajevo", CountryId = 1, CityId = 1, ManagerId = 1, StateMachine = "active", Address = "Some Address" }
        );

        // RoomTypes
        modelBuilder.Entity<RoomType>().HasData(
            new RoomType { Id = 1, Name = "Deluxe", Description = "Triple Room" },
            new RoomType { Id = 2, Name = "Deluxe", Description = "Double Room" }
        );

        // Rooms
        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, HotelId = 1, RoomTypeId = 1, Name = "Deluxe Triple Room", Capacity = 3, PricePerNight = 150 },
            new Room { Id = 2, HotelId = 1, RoomTypeId = 2, Name = "Deluxe Double Room", Capacity = 2, PricePerNight = 100, Description = "string", IsAvailable = true }
        );

        // Assets
        modelBuilder.Entity<Asset>().HasData(
            new Asset { Id = 1, FileName = "hills1.jpg", MimeType = "image.jpeg", CreatedAt = DateTime.Parse("2025-08-06T10:04:04.297"), HotelId = 1, RoomId = 1 },
            new Asset { Id = 2, FileName = "hills2.jpg", MimeType = "image.jpeg", CreatedAt = DateTime.Parse("2025-08-06T10:44:01.087"), HotelId = 1, RoomId = 1 },
            new Asset { Id = 3, FileName = "hills1.2.jpg", MimeType = "image.jpeg", CreatedAt = DateTime.Parse("2025-08-06T11:09:30.427"), HotelId = 1, RoomId = 2 }
        );

        // Roles
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Manager" }
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
            new User { Id = 6, FirstName = "Hazim", LastName = "Zimić", Email = "zime1921@gmail.com", Username = "zime_01", PasswordHash = "2eM1YIrZQAVh/jGfdoyUdOMdrEQ=", PasswordSalt = "s0Q/8KH8VbNeaMtkT18CAA==", PhoneNumber = "+38762404557", IsActive = true, CreatedAt = DateTime.Parse("2025-08-08T18:26:28.5"), LastLoginAt = DateTime.Parse("2025-08-08T18:26:28.5") }
        );

        // Reservations
        modelBuilder.Entity<Reservation>().HasData(
            new Reservation { Id = 1, UserId = 1, RoomId = 1, CheckInDate = DateTime.Parse("2025-08-07T10:00:00.1"), CheckOutDate = DateTime.Parse("2025-08-17T10:00:00.1"), TotalPrice = 1500, CreatedAt = DateTime.Parse("2025-08-06T18:05:59.1") },
            new Reservation { Id = 2, UserId = 1, RoomId = 2, CheckInDate = DateTime.Parse("2025-08-07T10:00:00.1"), CheckOutDate = DateTime.Parse("2025-08-17T10:00:00.1"), TotalPrice = 1500, CreatedAt = DateTime.Parse("2025-08-06T18:05:59.1") },
            new Reservation { Id = 3, UserId = 2, RoomId = 1, CheckInDate = DateTime.Parse("2025-08-20T10:00:00.68"), CheckOutDate = DateTime.Parse("2025-08-22T10:00:00.68"), TotalPrice = 300, Status = "Completed", CreatedAt = DateTime.Parse("2025-08-07T06:30:15.68") },
            new Reservation { Id = 4, UserId = 6, RoomId = 1, CheckInDate = DateTime.Parse("2025-08-09T10:28:01.71"), CheckOutDate = DateTime.Parse("2025-08-14T10:28:01.71"), TotalPrice = 750, Status = "Completed", CreatedAt = DateTime.Parse("2025-08-08T18:28:01.71") }
        );

        // Services
        modelBuilder.Entity<Service>().HasData(
            new Service { Id = 1, Name = "Breakfast", Description = "Buffet breakfast included", Price = 15, HotelId = 1 },
            new Service { Id = 2, Name = "Parking", Description = "Underground parking", Price = 10, HotelId = 2 }
        );

        // Reviews
        modelBuilder.Entity<Review>().HasData(
            new Review { Id = 1, UserId = 2, HotelId = 1, ReservationId = 3, Rating = 4, Comment = "Very good", ReviewDate = DateTime.Parse("2025-08-07T06:40:56.07") }
        );

        // Notifications
        modelBuilder.Entity<Notification>().HasData(
            new Notification { Id = 1, UserId = 2, Title = "Hello world", Message = "Hello world", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-08T20:14:18.847") },
            new Notification { Id = 2, UserId = 2, Title = "Reservation status updated", Message = "Your reservation #3 status changed to Completed", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-08T20:24:24.237") },
            new Notification { Id = 3, UserId = 2, Title = "Reservation status updated", Message = "Your reservation #3 status changed to Completed", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-08T20:29:23.607") },
            new Notification { Id = 4, UserId = 2, Title = "Reservation status updated", Message = "Your reservation #3 status changed to Completed", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-08T20:30:10.677") },
            new Notification { Id = 5, UserId = 6, Title = "Reservation status updated", Message = "Your reservation #4 status changed to Completed", Type = "email", IsRead = false, SentAt = DateTime.Parse("2025-08-08T20:31:04.463") }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
