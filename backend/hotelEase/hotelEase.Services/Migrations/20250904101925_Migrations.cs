using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace hotelEase.Services.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomAvailability",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomAvailability",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomAvailability",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoomAvailability",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Users",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Services",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "RoomTypes",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Rooms",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AC",
                table: "Rooms",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CityView",
                table: "Rooms",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "QueenBed",
                table: "Rooms",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WiFi",
                table: "Rooms",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "RoomAvailability",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Roles",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Reviews",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "ReservationServices",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Reservations",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Notifications",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StateMachine",
                table: "Hotels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Hotels",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Bar",
                table: "Hotels",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Fitness",
                table: "Hotels",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Parking",
                table: "Hotels",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Pool",
                table: "Hotels",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SPA",
                table: "Hotels",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WiFi",
                table: "Hotels",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Countries",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Cities",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Assets",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "stripe"),
                    ProviderPaymentId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "USD"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "processing"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__3214EC07F0F41DE0", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Payment__Reserva__18EBB532",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ReservationId",
                table: "Payment",
                column: "ReservationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropColumn(
                name: "AC",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CityView",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "QueenBed",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "WiFi",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Bar",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Fitness",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Parking",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "Pool",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "SPA",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "WiFi",
                table: "Hotels");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Services",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "RoomTypes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Rooms",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "RoomAvailability",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Roles",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Reviews",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "ReservationServices",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Reservations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Notifications",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Notifications",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StateMachine",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Hotels",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Countries",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Cities",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedTime",
                table: "Assets",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "DeletedTime", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, null, null, "Bosnia and Herzegovina" },
                    { 2, null, null, "Croatia" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DeletedTime", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, null, null, "Admin" },
                    { 2, null, null, "Manager" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "DeletedTime", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, null, "Triple Room", null, "Deluxe" },
                    { 2, null, "Double Room", null, "Deluxe" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedTime", "Email", "FirstName", "IsActive", "IsDeleted", "LastLoginAt", "LastName", "PasswordHash", "PasswordSalt", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 1, 16, 30, 26, 593, DateTimeKind.Unspecified), null, "user@gmail.com", "meho", true, null, new DateTime(2025, 8, 1, 16, 30, 26, 593, DateTimeKind.Unspecified), "mehic", "OypiMGzMHp0o9DYe5yWSnkky54A=", "o5hAjrnYH7NRqp9OBA6J9Q==", "061111111", "user" },
                    { 2, new DateTime(2025, 8, 1, 16, 19, 45, 617, DateTimeKind.Unspecified), null, "hazim@gmail.com", "hazim", true, null, new DateTime(2025, 8, 1, 16, 19, 45, 617, DateTimeKind.Unspecified), "hazim", "/o8nmsH5Dbd76SDP//tH/GAvlxU=", "NOaVnvJ5ycPcKCjCCy8OdQ==", "061234567", "hazim" },
                    { 4, null, null, "ado", "ado", null, null, null, "ado", "2fjG4q1LtucR2lA018pK6nWkyOc=", "1uIRFG7ijKSRdLtw5a74dw==", "063333333", "ado" },
                    { 5, new DateTime(2025, 8, 4, 17, 30, 15, 713, DateTimeKind.Unspecified), null, "test@gmail.com", "test", true, null, new DateTime(2025, 8, 4, 17, 30, 15, 713, DateTimeKind.Unspecified), "test", "s/BuGRf7UYcqxjZMRoXq3Lu30YA=", "pToIaH5hKO0heIRZvyCouA==", "06000000", "test" },
                    { 6, new DateTime(2025, 8, 8, 18, 26, 28, 500, DateTimeKind.Unspecified), null, "zime1921@gmail.com", "Hazim", true, null, new DateTime(2025, 8, 8, 18, 26, 28, 500, DateTimeKind.Unspecified), "Zimić", "2eM1YIrZQAVh/jGfdoyUdOMdrEQ=", "s0Q/8KH8VbNeaMtkT18CAA==", "+38762404557", "zime_01" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "DeletedTime", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, 1, null, null, "Sarajevo" },
                    { 2, 1, null, null, "Mostar" }
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "DeletedTime", "IsDeleted", "IsRead", "Message", "SentAt", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, null, null, false, "Hello world", new DateTime(2025, 8, 8, 20, 14, 18, 847, DateTimeKind.Unspecified), "Hello world", "email", 2 },
                    { 2, null, null, false, "Your reservation #3 status changed to Completed", new DateTime(2025, 8, 8, 20, 24, 24, 237, DateTimeKind.Unspecified), "Reservation status updated", "email", 2 },
                    { 3, null, null, false, "Your reservation #3 status changed to Completed", new DateTime(2025, 8, 8, 20, 29, 23, 607, DateTimeKind.Unspecified), "Reservation status updated", "email", 2 },
                    { 4, null, null, false, "Your reservation #3 status changed to Completed", new DateTime(2025, 8, 8, 20, 30, 10, 677, DateTimeKind.Unspecified), "Reservation status updated", "email", 2 },
                    { 5, null, null, false, "Your reservation #4 status changed to Completed", new DateTime(2025, 8, 8, 20, 31, 4, 463, DateTimeKind.Unspecified), "Reservation status updated", "email", 6 }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "CityId", "CountryId", "CreatedAt", "DeletedTime", "Description", "IsActive", "IsDeleted", "ManagerId", "Name", "StarRating", "StateMachine" },
                values: new object[,]
                {
                    { 1, "Some Address", 1, 1, null, null, null, null, null, 1, "Hotel Hills", 0, null },
                    { 2, "Some Address", 1, 1, null, null, null, null, null, 1, "Hotel Europe", 0, null },
                    { 3, "Some Address", 1, 1, null, null, null, null, null, 1, "Swissotel Sarajevo", 0, "active" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "DeletedTime", "Description", "HotelId", "IsAvailable", "IsDeleted", "Name", "PricePerNight", "RoomTypeId" },
                values: new object[,]
                {
                    { 1, 3, null, null, 1, null, null, "Deluxe Triple Room", 150m, 1 },
                    { 2, 2, null, "string", 1, true, null, "Deluxe Double Room", 100m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "DeletedTime", "Description", "HotelId", "IsDeleted", "Name", "Price" },
                values: new object[,]
                {
                    { 1, null, "Buffet breakfast included", 1, null, "Breakfast", 15m },
                    { 2, null, "Underground parking", 2, null, "Parking", 10m }
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "CreatedAt", "DeletedTime", "FileName", "HotelId", "Image", "ImageThumb", "IsDeleted", "MimeType", "RoomId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 6, 10, 4, 4, 297, DateTimeKind.Unspecified), null, "hills1.jpg", 1, null, null, null, "image.jpeg", 1 },
                    { 2, new DateTime(2025, 8, 6, 10, 44, 1, 87, DateTimeKind.Unspecified), null, "hills2.jpg", 1, null, null, null, "image.jpeg", 1 },
                    { 3, new DateTime(2025, 8, 6, 11, 9, 30, 427, DateTimeKind.Unspecified), null, "hills1.2.jpg", 1, null, null, null, "image.jpeg", 2 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CheckInDate", "CheckOutDate", "CreatedAt", "DeletedTime", "IsDeleted", "RoomId", "Status", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 7, 10, 0, 0, 100, DateTimeKind.Unspecified), new DateTime(2025, 8, 17, 10, 0, 0, 100, DateTimeKind.Unspecified), new DateTime(2025, 8, 6, 18, 5, 59, 100, DateTimeKind.Unspecified), null, null, 1, null, 1500m, 1 },
                    { 2, new DateTime(2025, 8, 7, 10, 0, 0, 100, DateTimeKind.Unspecified), new DateTime(2025, 8, 17, 10, 0, 0, 100, DateTimeKind.Unspecified), new DateTime(2025, 8, 6, 18, 5, 59, 100, DateTimeKind.Unspecified), null, null, 2, null, 1500m, 1 },
                    { 3, new DateTime(2025, 8, 20, 10, 0, 0, 680, DateTimeKind.Unspecified), new DateTime(2025, 8, 22, 10, 0, 0, 680, DateTimeKind.Unspecified), new DateTime(2025, 8, 7, 6, 30, 15, 680, DateTimeKind.Unspecified), null, null, 1, "Completed", 300m, 2 },
                    { 4, new DateTime(2025, 8, 9, 10, 28, 1, 710, DateTimeKind.Unspecified), new DateTime(2025, 8, 14, 10, 28, 1, 710, DateTimeKind.Unspecified), new DateTime(2025, 8, 8, 18, 28, 1, 710, DateTimeKind.Unspecified), null, null, 1, "Completed", 750m, 6 }
                });

            migrationBuilder.InsertData(
                table: "RoomAvailability",
                columns: new[] { "Id", "Date", "DeletedTime", "IsDeleted", "RoomId", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 10, 16, 11, 34, 197, DateTimeKind.Unspecified), null, null, 1, 1 },
                    { 2, new DateTime(2025, 8, 11, 16, 11, 34, 197, DateTimeKind.Unspecified), null, null, 1, 0 },
                    { 3, new DateTime(2025, 8, 11, 16, 11, 34, 197, DateTimeKind.Unspecified), null, null, 2, 1 },
                    { 4, new DateTime(2025, 8, 10, 16, 11, 34, 197, DateTimeKind.Unspecified), null, null, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "DeletedTime", "HotelId", "IsDeleted", "Rating", "ReservationId", "ReviewDate", "UserId" },
                values: new object[] { 1, "Very good", null, 1, null, 4, 3, new DateTime(2025, 8, 7, 6, 40, 56, 70, DateTimeKind.Unspecified), 2 });
        }
    }
}
