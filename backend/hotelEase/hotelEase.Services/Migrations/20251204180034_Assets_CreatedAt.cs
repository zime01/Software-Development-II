using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace hotelEase.Services.Migrations
{
    /// <inheritdoc />
    public partial class Assets_CreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__UserRoles__RoleI__6477ECF3",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK__UserRoles__UserI__6383C8BA",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK__UserRole__AF2760AD81661037",
                table: "UserRoles");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Assets",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Assets",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

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
                    { 2, null, null, "Manager" },
                    { 4, null, null, "User" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "DeletedTime", "Description", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, null, "Triple Room", null, "Deluxe" },
                    { 2, null, "Double Room", null, "Deluxe" },
                    { 3, null, "Double Room", null, "Superior" },
                    { 4, null, "Standard Double or Twin Room", null, "Standard Double Room" },
                    { 5, null, "Standard King Room", null, "Standard King Room" },
                    { 6, null, "Double or Twin Superior Room", null, "Double Superior Room" },
                    { 7, null, "Single Superior Room", null, "Single Superior Room" },
                    { 10, null, "Standard, Guest room, 1 King, City view", null, "Standard, Guest room" },
                    { 11, null, "Superior, Guest room, 2 Twin/Single Bed(s), City view", null, "Superior, Guest room" },
                    { 12, null, "Triple Room", null, "Triple Room" },
                    { 13, null, "Premium Quadruple Room", null, "Premium Quadruple Room" },
                    { 14, null, "Double Room with Balcony", null, "Double Room with Balcony" },
                    { 15, null, "1 twin bed and 1 queen bed", null, "Standard Family Room" },
                    { 16, null, "1 sofa bed and 1 queen bed", null, "Superior Suite" },
                    { 17, null, "1 sofa bed and 1 queen bed", null, "Family Suite" },
                    { 18, null, "1 king bed and 1 sofa bed", null, "Presidential Suite" },
                    { 19, null, "Comfort Single Room", null, "Comfort Single Room" },
                    { 20, null, "Deluxe Single Room", null, "Deluxe Single Room" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedTime", "Email", "FirstName", "IsActive", "IsDeleted", "LastLoginAt", "LastName", "PasswordHash", "PasswordSalt", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 1, 16, 30, 26, 593, DateTimeKind.Unspecified), null, "user@gmail.com", "meho", true, null, new DateTime(2025, 8, 1, 16, 30, 26, 593, DateTimeKind.Unspecified), "mehic", "OypiMGzMHp0o9DYe5yWSnkky54A=", "o5hAjrnYH7NRqp9OBA6J9Q==", "061111111", "user" },
                    { 2, new DateTime(2025, 8, 1, 16, 19, 45, 617, DateTimeKind.Unspecified), null, "hazim@gmail.com", "hazim", true, null, new DateTime(2025, 8, 1, 16, 19, 45, 617, DateTimeKind.Unspecified), "hazim", "/o8nmsH5Dbd76SDP//tH/GAvlxU=", "NOaVnvJ5ycPcKCjCCy8OdQ==", "061234567", "hazim" },
                    { 5, new DateTime(2025, 8, 4, 17, 30, 15, 713, DateTimeKind.Unspecified), null, "test@gmail.com", "test", true, null, new DateTime(2025, 8, 4, 17, 30, 15, 713, DateTimeKind.Unspecified), "test", "s/BuGRf7UYcqxjZMRoXq3Lu30YA=", "pToIaH5hKO0heIRZvyCouA==", "06000000", "test" },
                    { 6, new DateTime(2025, 8, 8, 18, 26, 28, 500, DateTimeKind.Unspecified), null, "zime1921@gmail.com", "Hazim", true, null, new DateTime(2025, 8, 8, 18, 26, 28, 500, DateTimeKind.Unspecified), "Zimić", "2eM1YIrZQAVh/jGfdoyUdOMdrEQ=", "s0Q/8KH8VbNeaMtkT18CAA==", "+38762404557", "zime_01" },
                    { 7, new DateTime(2025, 8, 20, 10, 37, 21, 807, DateTimeKind.Unspecified), null, "zime1921@gmail.com", "test", true, null, null, "test", "4pOLn3oczBkoe6ryRO3roYb70c4=", "tBf5M34DNdsz/42SdTmJHQ==", null, "test" },
                    { 8, new DateTime(2025, 8, 20, 10, 40, 4, 500, DateTimeKind.Unspecified), null, "zime1921@gmail.com", "test", true, null, null, "test", "DUdL3t0xvuewcsxxsSrW23x948I=", "Rfk+5T78WxlZGOiYrl2j9A==", null, "test" },
                    { 9, new DateTime(2025, 8, 20, 8, 51, 11, 363, DateTimeKind.Unspecified), null, "abcd", "abcd", true, null, null, "abcd", "9ciStnRAT8sF6RjAI4dy5L4FCO8=", "C5DJk5nT7x+gkzuYTcZ5fA==", "string", "abcd" },
                    { 13, null, null, "eaglehl022@gmail.com", "mujo", null, null, null, "mujo", "rtNMo9LEpchW1xPiej9xshhp/Js=", "b5mgx/KFt/oB/LFCFX/Dag==", "061111111", "mujo" },
                    { 14, new DateTime(2025, 9, 7, 8, 41, 35, 897, DateTimeKind.Unspecified), null, "zime1921@gmail.com", "desktop", true, null, new DateTime(2025, 9, 7, 8, 41, 35, 897, DateTimeKind.Unspecified), "desktop", "Y4Janfy1qua8GMku6qDTz9Jgz4E=", "NGNxtZEDNXRYCIZ4BmpfCA==", "060000000", "desktop" },
                    { 15, new DateTime(2025, 9, 7, 8, 41, 35, 897, DateTimeKind.Unspecified), null, "zime1921@gmail.com", "mobile", true, null, new DateTime(2025, 9, 7, 8, 41, 35, 897, DateTimeKind.Unspecified), "mobile", "n2fzGycplgIHEO4s8nkTI02+h9s=", "l0mdchhOcVIle5zMGBzNsw==", "060000000", "mobile" },
                    { 16, new DateTime(2025, 9, 7, 8, 41, 35, 897, DateTimeKind.Unspecified), null, "zime1921@gmail.com", "manager", true, null, new DateTime(2025, 9, 7, 8, 41, 35, 897, DateTimeKind.Unspecified), "manager", "SlQMpOoWgdEk/XraKqgzsgAoSQk=", "4CfVppyqsb2hDrDrtIH75Q==", "060000000", "manager" },
                    { 17, new DateTime(2025, 9, 7, 8, 41, 35, 897, DateTimeKind.Unspecified), null, "zime1921@gmail.com", "user", true, null, new DateTime(2025, 9, 7, 8, 41, 35, 897, DateTimeKind.Unspecified), "user", "ZLlQ1tOvhGD5nfnBCXq4IDpWMcA=", "iI4aRztVcPsWPBh7kLNY8w==", "060000000", "user" },
                    { 18, new DateTime(2025, 9, 7, 8, 41, 35, 897, DateTimeKind.Unspecified), null, "zime1921@gmail.com", "admin", true, null, new DateTime(2025, 9, 7, 8, 41, 35, 897, DateTimeKind.Unspecified), "admin", "4eAriAdT1M44UGPF39XdOq3xFmI=", "zwqMc1j7EAsIqYvuztBaOA==", "060000000", "admin" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "DeletedTime", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, 1, null, null, "Sarajevo" },
                    { 2, 1, null, null, "Mostar" },
                    { 4, 1, null, null, "Banja Luka" }
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
                    { 5, null, null, false, "Your reservation #4 status changed to Completed", new DateTime(2025, 8, 8, 20, 31, 4, 463, DateTimeKind.Unspecified), "Reservation status updated", "email", 6 },
                    { 7, null, null, false, "Your reservation #7 for 28.08.2025 - 31.08.2025 is created. Total: 500,00 KM.", new DateTime(2025, 8, 16, 23, 37, 2, 967, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 8, null, null, false, "Your reservation #8 for 01.08.2025 - 03.08.2025 is created. Total: 250,00 KM.", new DateTime(2025, 8, 16, 23, 42, 54, 683, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 9, null, null, false, "Your reservation #9 for 01.08.2025 - 03.08.2025 is created. Total: 250,00 KM.", new DateTime(2025, 8, 16, 23, 45, 22, 730, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 10, null, null, false, "Your reservation #10 for 17.08.2025 - 18.08.2025 is created. Total: 150,00 KM.", new DateTime(2025, 8, 16, 23, 46, 37, 260, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 11, null, null, false, "Your reservation #11 for 01.08.2025 - 05.08.2025 is created. Total: 520,00 KM.", new DateTime(2025, 8, 16, 23, 52, 32, 633, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 12, null, null, false, "Your reservation #12 for 01.08.2025 - 05.08.2025 is created. Total: 560,00 KM.", new DateTime(2025, 8, 16, 23, 56, 27, 640, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 13, null, null, false, "Your reservation #13 for 01.08.2025 - 05.08.2025 is created. Total: 560,00 KM.", new DateTime(2025, 8, 16, 23, 57, 31, 307, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 14, null, null, false, "Your reservation #14 for 01.08.2025 - 05.08.2025 is created. Total: 560,00 KM.", new DateTime(2025, 8, 16, 23, 57, 40, 730, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 15, null, null, false, "Your reservation #15 for 16.08.2025 - 16.08.2025 is created. Total: 500,00 KM.", new DateTime(2025, 8, 16, 23, 59, 34, 67, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 16, null, null, false, "Your reservation #16 for 05.08.2025 - 08.08.2025 is created. Total: 420,00 KM.", new DateTime(2025, 8, 17, 8, 6, 39, 970, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 17, null, null, false, "Your reservation #17 for 05.08.2025 - 08.08.2025 is created. Total: 420,00 KM.", new DateTime(2025, 8, 17, 8, 7, 18, 940, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 18, null, null, false, "Your reservation #18 for 05.08.2025 - 08.08.2025 is created. Total: 420,00 KM.", new DateTime(2025, 8, 17, 8, 10, 31, 163, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 19, null, null, false, "Your reservation #19 for 01.08.2025 - 04.08.2025 is created. Total: 300,00 KM.", new DateTime(2025, 8, 17, 8, 11, 26, 450, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 20, null, null, false, "Your reservation #20 for 04.08.2025 - 07.08.2025 is created. Total: 300,00 KM.", new DateTime(2025, 8, 17, 9, 11, 53, 177, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 21, null, null, false, "Your reservation #21 for 05.08.2025 - 09.08.2025 is created. Total: 520,00 KM.", new DateTime(2025, 8, 17, 9, 16, 11, 310, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 22, null, null, false, "Your reservation #22 for 09.08.2025 - 12.08.2025 is created. Total: 395,00 KM.", new DateTime(2025, 8, 17, 9, 19, 32, 817, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 23, null, null, false, "Your reservation #23 for 08.08.2025 - 12.08.2025 is created. Total: 560,00 KM.", new DateTime(2025, 8, 17, 15, 18, 19, 567, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 24, null, null, false, "Your reservation #24 for 07.08.2025 - 12.08.2025 is created. Total: 500,00 KM.", new DateTime(2025, 8, 17, 15, 58, 18, 583, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 25, null, null, false, "Your reservation #25 for 12.08.2025 - 16.08.2025 is created. Total: 560,00 KM.", new DateTime(2025, 8, 17, 15, 59, 58, 70, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 26, null, null, false, "Your reservation #26 for 03.08.2025 - 06.08.2025 is created. Total: 300,00 KM.", new DateTime(2025, 8, 17, 16, 9, 38, 747, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 28, null, null, false, "Your reservation #28 for 01.08.2025 - 03.08.2025 is created. Total: 350,00 KM.", new DateTime(2025, 8, 17, 16, 17, 55, 37, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 29, null, null, false, "Your reservation has been successfully created.", new DateTime(2025, 8, 17, 16, 17, 55, 233, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 30, null, null, false, "Your reservation #29 for 12.08.2025 - 16.08.2025 is created. Total: 400,00 KM.", new DateTime(2025, 8, 17, 16, 19, 6, 560, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 31, null, null, false, "Your reservation has been successfully created.", new DateTime(2025, 8, 17, 16, 19, 6, 703, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 32, null, null, false, "Your reservation #30 for 12.08.2025 - 16.08.2025 is created. Total: 400,00 KM.", new DateTime(2025, 8, 17, 16, 19, 49, 727, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 33, null, null, false, "Your reservation has been successfully created.", new DateTime(2025, 8, 17, 16, 19, 49, 947, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 34, null, null, false, "Your reservation #31 for 12.08.2025 - 16.08.2025 is created. Total: 400,00 KM.", new DateTime(2025, 8, 17, 16, 20, 0, 587, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 35, null, null, false, "Your reservation has been successfully created.", new DateTime(2025, 8, 17, 16, 20, 0, 760, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 37, null, null, false, "Your reservation has been successfully created.", new DateTime(2025, 8, 17, 16, 20, 12, 697, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 38, null, null, false, "Your reservation #33 for 01.07.2025 - 05.07.2025 is created. Total: 650,00 KM.", new DateTime(2025, 8, 17, 16, 56, 9, 17, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 39, null, null, false, "Your reservation #34 for 05.07.2025 - 10.07.2025 is created. Total: 800,00 KM.", new DateTime(2025, 8, 17, 17, 7, 13, 937, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 40, null, null, false, "Your reservation has been successfully created.", new DateTime(2025, 8, 17, 17, 7, 14, 143, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 41, null, null, false, "Your reservation #35 for 01.07.2025 - 06.07.2025 is created. Total: 645,00 KM.", new DateTime(2025, 8, 17, 20, 0, 47, 180, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 42, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 01.07.2025\nCheck-out: 06.07.2025", new DateTime(2025, 8, 17, 20, 0, 47, 407, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 43, null, null, false, "Your reservation #36 for 10.07.2025 - 17.07.2025 is created. Total: 1.100,00 KM.", new DateTime(2025, 8, 18, 13, 36, 44, 510, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 44, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 10.07.2025\nCheck-out: 17.07.2025", new DateTime(2025, 8, 18, 13, 36, 45, 37, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 45, null, null, false, "Your reservation #37 for 10.07.2025 - 17.07.2025 is created. Total: 1.100,00 KM.", new DateTime(2025, 8, 18, 13, 41, 53, 747, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 46, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 10.07.2025\nCheck-out: 17.07.2025", new DateTime(2025, 8, 18, 13, 41, 54, 13, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 47, null, null, false, "Your reservation #38 for 10.07.2025 - 17.07.2025 is created. Total: 1.100,00 KM.", new DateTime(2025, 8, 18, 13, 42, 8, 683, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 48, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 10.07.2025\nCheck-out: 17.07.2025", new DateTime(2025, 8, 18, 13, 42, 8, 940, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 50, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 01.07.2025\nCheck-out: 05.07.2025", new DateTime(2025, 8, 18, 13, 44, 48, 733, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 51, null, null, false, "Your reservation #40 for 06.07.2025 - 12.07.2025 is created. Total: 770,00 KM.", new DateTime(2025, 8, 18, 13, 48, 56, 940, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 52, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 06.07.2025\nCheck-out: 12.07.2025", new DateTime(2025, 8, 18, 13, 48, 57, 163, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 53, null, null, false, "Your reservation #41 for 06.07.2025 - 12.07.2025 is created. Total: 770,00 KM.", new DateTime(2025, 8, 18, 13, 51, 39, 43, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 54, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 06.07.2025\nCheck-out: 12.07.2025", new DateTime(2025, 8, 18, 13, 51, 39, 530, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 55, null, null, false, "Your reservation #42 for 06.07.2025 - 12.07.2025 is created. Total: 770,00 KM.", new DateTime(2025, 8, 18, 13, 53, 39, 473, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 56, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 06.07.2025\nCheck-out: 12.07.2025", new DateTime(2025, 8, 18, 13, 53, 39, 810, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 57, null, null, false, "Your reservation #43 for 17.07.2025 - 23.07.2025 is created. Total: 950,00 KM.", new DateTime(2025, 8, 18, 15, 26, 46, 147, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 58, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 17.07.2025\nCheck-out: 23.07.2025", new DateTime(2025, 8, 18, 15, 26, 46, 470, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 59, null, null, false, "Your reservation #44 for 12.07.2025 - 17.07.2025 is created. Total: 645,00 KM.", new DateTime(2025, 8, 18, 15, 56, 22, 687, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 60, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 12.07.2025\nCheck-out: 17.07.2025", new DateTime(2025, 8, 18, 15, 56, 22, 907, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 61, null, null, false, "Your reservation #45 for 17.07.2025 - 19.07.2025 is created. Total: 250,00 KM.", new DateTime(2025, 8, 18, 16, 0, 1, 533, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 62, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 17.07.2025\nCheck-out: 19.07.2025", new DateTime(2025, 8, 18, 16, 0, 1, 810, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 63, null, null, false, "Your reservation #46 for 17.07.2025 - 19.07.2025 is created. Total: 270,00 KM.", new DateTime(2025, 8, 18, 16, 3, 23, 530, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 64, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 17.07.2025\nCheck-out: 19.07.2025", new DateTime(2025, 8, 18, 16, 3, 23, 693, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 65, null, null, false, "Your reservation #47 for 17.07.2025 - 19.07.2025 is created. Total: 270,00 KM.", new DateTime(2025, 8, 18, 16, 59, 43, 730, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 66, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 17.07.2025\nCheck-out: 19.07.2025", new DateTime(2025, 8, 18, 16, 59, 43, 907, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 67, null, null, false, "Your reservation #48 for 19.07.2025 - 21.07.2025 is created. Total: 250,00 KM.", new DateTime(2025, 8, 18, 17, 5, 32, 80, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 68, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 19.07.2025\nCheck-out: 21.07.2025", new DateTime(2025, 8, 18, 17, 5, 32, 243, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 69, null, null, false, "Your reservation #49 for 05.07.2025 - 10.07.2025 is created. Total: 550,00 KM.", new DateTime(2025, 8, 18, 17, 50, 12, 673, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 70, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 05.07.2025\nCheck-out: 10.07.2025", new DateTime(2025, 8, 18, 17, 50, 12, 983, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 71, null, null, false, "Your reservation #50 for 05.07.2025 - 10.07.2025 is created. Total: 550,00 KM.", new DateTime(2025, 8, 18, 17, 55, 55, 470, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 72, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 05.07.2025\nCheck-out: 10.07.2025", new DateTime(2025, 8, 18, 17, 55, 55, 667, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 73, null, null, false, "Your reservation #51 for 05.07.2025 - 10.07.2025 is created. Total: 550,00 KM.", new DateTime(2025, 8, 18, 17, 56, 6, 747, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 74, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 05.07.2025\nCheck-out: 10.07.2025", new DateTime(2025, 8, 18, 17, 56, 6, 927, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 75, null, null, false, "Your reservation #52 for 21.07.2025 - 26.07.2025 is created. Total: 645,00 KM.", new DateTime(2025, 8, 18, 17, 58, 47, 173, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 76, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 21.07.2025\nCheck-out: 26.07.2025", new DateTime(2025, 8, 18, 17, 58, 47, 343, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 77, null, null, false, "Your reservation #53 for 21.07.2025 - 26.07.2025 is created. Total: 645,00 KM.", new DateTime(2025, 8, 18, 18, 6, 42, 650, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 78, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 21.07.2025\nCheck-out: 26.07.2025", new DateTime(2025, 8, 18, 18, 6, 42, 827, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 79, null, null, false, "Your reservation #54 for 10.07.2025 - 12.07.2025 is created. Total: 250,00 KM.", new DateTime(2025, 8, 18, 20, 23, 45, 387, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 80, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 10.07.2025\nCheck-out: 12.07.2025", new DateTime(2025, 8, 18, 20, 23, 45, 683, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 81, null, null, false, "Your reservation #55 for 12.07.2025 - 17.07.2025 is created. Total: 550,00 KM.", new DateTime(2025, 8, 18, 21, 43, 30, 577, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 82, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 12.07.2025\nCheck-out: 17.07.2025", new DateTime(2025, 8, 18, 21, 43, 30, 907, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 83, null, null, false, "Your reservation #56 for 12.07.2025 - 17.07.2025 is created. Total: 550,00 KM.", new DateTime(2025, 8, 18, 21, 47, 37, 740, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 84, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 12.07.2025\nCheck-out: 17.07.2025", new DateTime(2025, 8, 18, 21, 47, 37, 920, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 85, null, null, false, "Your reservation #57 for 12.07.2025 - 17.07.2025 is created. Total: 550,00 KM.", new DateTime(2025, 8, 18, 21, 51, 0, 533, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 86, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 12.07.2025\nCheck-out: 17.07.2025", new DateTime(2025, 8, 18, 21, 51, 0, 710, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 87, null, null, false, "Your reservation #58 for 26.07.2025 - 31.07.2025 is created. Total: 645,00 KM.", new DateTime(2025, 8, 18, 21, 53, 2, 73, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 88, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 26.07.2025\nCheck-out: 31.07.2025", new DateTime(2025, 8, 18, 21, 53, 2, 317, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 89, null, null, false, "Your reservation #59 for 26.07.2025 - 31.07.2025 is created. Total: 645,00 KM.", new DateTime(2025, 8, 18, 22, 0, 2, 743, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 90, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 26.07.2025\nCheck-out: 31.07.2025", new DateTime(2025, 8, 18, 22, 0, 2, 963, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 92, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 01.06.2025\nCheck-out: 05.06.2025", new DateTime(2025, 8, 18, 22, 5, 55, 603, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 93, null, null, false, "Your reservation #61 for 05.06.2025 - 10.06.2025 is created. Total: 645,00 KM.", new DateTime(2025, 8, 19, 9, 22, 45, 930, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 94, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 05.06.2025\nCheck-out: 10.06.2025", new DateTime(2025, 8, 19, 9, 22, 46, 283, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 95, null, null, false, "Your reservation #62 for 05.06.2025 - 10.06.2025 is created. Total: 645,00 KM.", new DateTime(2025, 8, 19, 10, 0, 4, 220, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 96, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 05.06.2025\nCheck-out: 10.06.2025", new DateTime(2025, 8, 19, 10, 0, 4, 427, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 108, null, null, false, "Your reservation #68 for 01.07.2025 - 05.07.2025 is created. Total: 400,00 KM.", new DateTime(2025, 8, 19, 12, 13, 54, 887, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 109, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hollywood\nRoom: Deluxe Double Room\nCheck-in: 01.07.2025\nCheck-out: 05.07.2025", new DateTime(2025, 8, 19, 12, 13, 55, 180, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 110, null, null, false, "✅ Vaše plaćanje za rezervaciju #68 u iznosu 400,00 USD je uspješno. Status rezervacije: Confirmed.", new DateTime(2025, 8, 19, 12, 14, 17, 830, DateTimeKind.Unspecified), "Payment succeeded", "email", 6 },
                    { 111, null, null, false, "Hello mujo,<br/><br/>Your registration was successful!<br/>Welcome to HotelEase 🏨.", new DateTime(2025, 8, 20, 22, 21, 56, 63, DateTimeKind.Unspecified), "Welcome to HotelEase 🎉", "email", 13 },
                    { 112, null, null, false, "Your reservation #69 for 14.06.2025 - 19.06.2025 is created. Total: 645,00 KM.", new DateTime(2025, 8, 21, 10, 18, 30, 517, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 113, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 14.06.2025\nCheck-out: 19.06.2025", new DateTime(2025, 8, 21, 10, 18, 30, 710, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 114, null, null, false, "✅ Vaše plaćanje za rezervaciju #69 u iznosu 645,00 USD je uspješno. Status rezervacije: Confirmed.", new DateTime(2025, 8, 21, 10, 18, 57, 480, DateTimeKind.Unspecified), "Payment succeeded", "email", 6 },
                    { 115, null, null, false, "Your reservation #70 for 23.07.2025 - 28.07.2025 is created. Total: 800,00 KM.", new DateTime(2025, 8, 21, 10, 26, 34, 837, DateTimeKind.Unspecified), "Reservation Created", "email", 6 },
                    { 116, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 23.07.2025\nCheck-out: 28.07.2025", new DateTime(2025, 8, 21, 10, 26, 34, 980, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 6 },
                    { 117, null, null, false, "✅ Vaše plaćanje za rezervaciju #70 u iznosu 800,00 USD je uspješno. Status rezervacije: Confirmed.", new DateTime(2025, 8, 21, 10, 26, 55, 447, DateTimeKind.Unspecified), "Payment succeeded", "email", 6 },
                    { 118, null, null, false, "Your reservation #71 for 17.07.2025 - 23.07.2025 is created. Total: 650,00 KM.", new DateTime(2025, 8, 22, 18, 30, 3, 407, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 119, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 17.07.2025\nCheck-out: 23.07.2025", new DateTime(2025, 8, 22, 18, 30, 3, 820, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 120, null, null, false, "✅ Vaše plaćanje za rezervaciju #71 u iznosu 650,00 USD je uspješno. Status rezervacije: Confirmed.", new DateTime(2025, 8, 22, 18, 30, 26, 347, DateTimeKind.Unspecified), "Payment succeeded", "email", 13 },
                    { 121, null, null, false, "Your reservation #72 for 01.06.2025 - 07.06.2025 is created. Total: 900,00 KM.", new DateTime(2025, 8, 22, 18, 30, 38, 637, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 122, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 01.06.2025\nCheck-out: 07.06.2025", new DateTime(2025, 8, 22, 18, 30, 38, 860, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 123, null, null, false, "✅ Vaše plaćanje za rezervaciju #72 u iznosu 900,00 USD je uspješno. Status rezervacije: Confirmed.", new DateTime(2025, 8, 22, 18, 30, 57, 720, DateTimeKind.Unspecified), "Payment succeeded", "email", 13 },
                    { 124, null, null, false, "Your reservation #73 for 07.06.2025 - 14.06.2025 is created. Total: 1.050,00 KM.", new DateTime(2025, 8, 22, 18, 31, 8, 653, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 125, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 07.06.2025\nCheck-out: 14.06.2025", new DateTime(2025, 8, 22, 18, 31, 8, 843, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 126, null, null, false, "✅ Vaše plaćanje za rezervaciju #73 u iznosu 1050,00 USD je uspješno. Status rezervacije: Confirmed.", new DateTime(2025, 8, 22, 18, 31, 25, 113, DateTimeKind.Unspecified), "Payment succeeded", "email", 13 },
                    { 127, null, null, false, "Your reservation #74 for 01.06.2025 - 07.06.2025 is created. Total: 650,00 KM.", new DateTime(2025, 8, 22, 18, 31, 42, 447, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 128, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 01.06.2025\nCheck-out: 07.06.2025", new DateTime(2025, 8, 22, 18, 31, 42, 650, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 129, null, null, false, "✅ Vaše plaćanje za rezervaciju #74 u iznosu 650,00 USD je uspješno. Status rezervacije: Confirmed.", new DateTime(2025, 8, 22, 18, 31, 57, 480, DateTimeKind.Unspecified), "Payment succeeded", "email", 13 },
                    { 130, null, null, false, "Your reservation #75 for 07.06.2025 - 17.06.2025 is created. Total: 1.000,00 KM.", new DateTime(2025, 8, 22, 18, 32, 11, 103, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 131, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 07.06.2025\nCheck-out: 17.06.2025", new DateTime(2025, 8, 22, 18, 32, 11, 330, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 132, null, null, false, "✅ Vaše plaćanje za rezervaciju #75 u iznosu 1000,00 USD je uspješno. Status rezervacije: Confirmed.", new DateTime(2025, 8, 22, 18, 32, 26, 407, DateTimeKind.Unspecified), "Payment succeeded", "email", 13 },
                    { 133, null, null, false, "Your reservation #76 for 19.06.2025 - 21.06.2025 is created. Total: 270,00 KM.", new DateTime(2025, 8, 22, 18, 32, 39, 733, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 134, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 19.06.2025\nCheck-out: 21.06.2025", new DateTime(2025, 8, 22, 18, 32, 39, 913, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 135, null, null, false, "✅ Vaše plaćanje za rezervaciju #76 u iznosu 270,00 USD je uspješno. Status rezervacije: Confirmed.", new DateTime(2025, 8, 22, 18, 32, 57, 203, DateTimeKind.Unspecified), "Payment succeeded", "email", 13 },
                    { 136, null, null, false, "Your reservation #77 for 21.06.2025 - 25.06.2025 is created. Total: 520,00 KM.", new DateTime(2025, 8, 22, 18, 33, 14, 683, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 137, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 21.06.2025\nCheck-out: 25.06.2025", new DateTime(2025, 8, 22, 18, 33, 14, 873, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 138, null, null, false, "✅ Vaše plaćanje za rezervaciju #77 u iznosu 520,00 USD je uspješno. Status rezervacije: Confirmed.", new DateTime(2025, 8, 22, 18, 33, 36, 877, DateTimeKind.Unspecified), "Payment succeeded", "email", 13 },
                    { 139, null, null, false, "Your reservation #78 for 25.06.2025 - 30.06.2025 is created. Total: 625,00 KM.", new DateTime(2025, 8, 22, 18, 33, 47, 110, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 140, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 25.06.2025\nCheck-out: 30.06.2025", new DateTime(2025, 8, 22, 18, 33, 47, 300, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 141, null, null, false, "✅ Vaše plaćanje za rezervaciju #78 u iznosu 625,00 USD je uspješno. Status rezervacije: Confirmed.", new DateTime(2025, 8, 22, 18, 34, 2, 237, DateTimeKind.Unspecified), "Payment succeeded", "email", 13 },
                    { 142, null, null, false, "Your reservation #79 for 25.06.2025 - 30.06.2025 is created. Total: 625,00 KM.", new DateTime(2025, 8, 22, 18, 34, 4, 597, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 143, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 25.06.2025\nCheck-out: 30.06.2025", new DateTime(2025, 8, 22, 18, 34, 4, 780, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 144, null, null, false, "✅ Vaše plaćanje za rezervaciju #79 u iznosu 625,00 USD je uspješno. Status rezervacije: Confirmed.", new DateTime(2025, 8, 22, 18, 34, 23, 357, DateTimeKind.Unspecified), "Payment succeeded", "email", 13 },
                    { 145, null, null, false, "Your reservation #80 for 01.05.2025 - 15.05.2025 is created. Total: 1.770,00 KM.", new DateTime(2025, 8, 22, 18, 34, 36, 507, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 146, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 01.05.2025\nCheck-out: 15.05.2025", new DateTime(2025, 8, 22, 18, 34, 36, 707, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 147, null, null, false, "✅ Vaše plaćanje za rezervaciju #80 u iznosu 1770,00 USD je uspješno. Status rezervacije: Confirmed.", new DateTime(2025, 8, 22, 18, 34, 51, 967, DateTimeKind.Unspecified), "Payment succeeded", "email", 13 },
                    { 148, null, null, false, "Your reservation #81 for 01.05.2025 - 15.05.2025 is created. Total: 1.770,00 KM.", new DateTime(2025, 8, 22, 18, 34, 54, 497, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 149, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 01.05.2025\nCheck-out: 15.05.2025", new DateTime(2025, 8, 22, 18, 34, 54, 677, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 150, null, null, false, "Your reservation #82 for 17.06.2025 - 22.06.2025 is created. Total: 550,00 KM.", new DateTime(2025, 8, 25, 8, 28, 0, 800, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 151, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 17.06.2025\nCheck-out: 22.06.2025", new DateTime(2025, 8, 25, 8, 28, 1, 163, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 152, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Double Room\nCheck-in: 17.06.2025\nCheck-out: 22.06.2025", new DateTime(2025, 8, 25, 8, 33, 59, 430, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 153, null, null, false, "Your reservation #84 for 01.07.2025 - 07.07.2025 is created. Total: 600,00 KM.", new DateTime(2025, 8, 25, 8, 39, 37, 883, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 154, null, null, false, "Your reservation has been successfully created.\n\nHotel: Swissotel Sarajevo\nRoom: Standard Double Room\nCheck-in: 01.07.2025\nCheck-out: 07.07.2025", new DateTime(2025, 8, 25, 8, 39, 38, 180, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 155, null, null, false, "Your reservation #85 for 01.07.2025 - 05.07.2025 is created. Total: 320,00 KM.", new DateTime(2025, 8, 25, 8, 41, 19, 193, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 156, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hollywood\nRoom: Single Superior Room\nCheck-in: 01.07.2025\nCheck-out: 05.07.2025", new DateTime(2025, 8, 25, 8, 41, 19, 490, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 157, null, null, false, "Your reservation #86 for 15.05.2025 - 21.05.2025 is created. Total: 770,00 KM.", new DateTime(2025, 8, 25, 8, 43, 14, 130, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 158, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Europe\nRoom: Superior Double Room\nCheck-in: 15.05.2025\nCheck-out: 21.05.2025", new DateTime(2025, 8, 25, 8, 43, 14, 377, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 159, null, null, false, "Your reservation #87 for 06.07.2025 - 11.07.2025 is created. Total: 400,00 KM.", new DateTime(2025, 8, 25, 8, 48, 22, 507, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 160, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hollywood\nRoom: Single Superior Room\nCheck-in: 06.07.2025\nCheck-out: 11.07.2025", new DateTime(2025, 8, 25, 8, 48, 22, 797, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 161, null, null, false, "Your reservation #88 for 01.06.2025 - 07.06.2025 is created. Total: 780,00 KM.", new DateTime(2025, 8, 25, 8, 56, 29, 197, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 162, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hollywood\nRoom: Twin Superior Room\nCheck-in: 01.06.2025\nCheck-out: 07.06.2025", new DateTime(2025, 8, 25, 8, 56, 29, 497, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 163, null, null, false, "✅ Vaše plaćanje za rezervaciju #88 u iznosu 780,00 USD je uspješno. Status rezervacije: Confirmed.", new DateTime(2025, 8, 25, 8, 56, 55, 410, DateTimeKind.Unspecified), "Payment succeeded", "email", 13 },
                    { 164, null, null, false, "Your reservation #89 for 14.06.2025 - 21.06.2025 is created. Total: 1.100,00 KM.", new DateTime(2025, 9, 1, 13, 56, 2, 563, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 165, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 14.06.2025\nCheck-out: 21.06.2025", new DateTime(2025, 9, 1, 13, 56, 2, 740, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 166, null, null, false, "Your reservation #90 for 14.06.2025 - 21.06.2025 is created. Total: 1.100,00 KM.", new DateTime(2025, 9, 1, 13, 56, 12, 440, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 167, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 14.06.2025\nCheck-out: 21.06.2025", new DateTime(2025, 9, 1, 13, 56, 12, 560, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 168, null, null, false, "Your reservation #91 for 21.06.2025 - 28.06.2025 is created. Total: 1.100,00 KM.", new DateTime(2025, 9, 1, 13, 59, 31, 723, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 169, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hills\nRoom: Deluxe Triple Room\nCheck-in: 21.06.2025\nCheck-out: 28.06.2025", new DateTime(2025, 9, 1, 13, 59, 31, 963, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 170, null, null, false, "Your reservation #92 for 01.08.2025 - 08.08.2025 is created. Total: 560,00 KM.", new DateTime(2025, 9, 1, 14, 3, 11, 380, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 171, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hollywood\nRoom: Single Superior Room\nCheck-in: 01.08.2025\nCheck-out: 08.08.2025", new DateTime(2025, 9, 1, 14, 3, 11, 543, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 172, null, null, false, "Your reservation #93 for 01.07.2025 - 08.07.2025 is created. Total: 1.260,00 KM.", new DateTime(2025, 9, 1, 14, 4, 57, 320, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 173, null, null, false, "Your reservation has been successfully created.\n\nHotel: Swissotel Sarajevo\nRoom: Standard King Room\nCheck-in: 01.07.2025\nCheck-out: 08.07.2025", new DateTime(2025, 9, 1, 14, 4, 57, 407, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 174, null, null, false, "Your reservation #94 for 01.08.2025 - 08.08.2025 is created. Total: 910,00 KM.", new DateTime(2025, 9, 1, 14, 10, 38, 880, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 175, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hollywood\nRoom: Twin Superior Room\nCheck-in: 01.08.2025\nCheck-out: 08.08.2025", new DateTime(2025, 9, 1, 14, 10, 38, 997, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 176, null, null, false, "Your reservation #95 for 01.08.2025 - 08.08.2025 is created. Total: 910,00 KM.", new DateTime(2025, 9, 1, 14, 22, 34, 257, DateTimeKind.Unspecified), "Reservation Created", "email", 13 },
                    { 177, null, null, false, "Your reservation has been successfully created.\n\nHotel: Hotel Hollywood\nRoom: Twin Superior Room\nCheck-in: 01.08.2025\nCheck-out: 08.08.2025", new DateTime(2025, 9, 1, 14, 22, 34, 567, DateTimeKind.Unspecified), "Reservation Confirmed", "email", 13 },
                    { 178, null, null, false, "Your reservation #96 for 08.08.2025 - 15.08.2025 is created. Total: 910,00 KM.", new DateTime(2025, 9, 1, 14, 29, 40, 453, DateTimeKind.Unspecified), "Reservation Created", "email", 13 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 14 },
                    { 4, 15 },
                    { 2, 16 },
                    { 4, 17 },
                    { 1, 18 }
                });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Address", "Bar", "CityId", "CountryId", "CreatedAt", "DeletedTime", "Description", "Fitness", "IsActive", "IsDeleted", "ManagerId", "Name", "Parking", "Pool", "SPA", "StarRating", "StateMachine", "WiFi" },
                values: new object[,]
                {
                    { 1, "Butmirska cesta 18, 71000 Sarajevo", null, 1, 1, new DateTime(2025, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hotel Hills, Thermal & Spa Resort Sarajevo is located in the center of the green oasis of Sarajevo Hotel Hills, Thermal & Spa Resort Sarajevo includes 330 rooms and suites, multipurpose Congress center with modern conference technology, Wellness, Spa & Fitness Health center, indoor and outdoor swimming pools, Adrenalin park for children and adults on impressive 2200 square meters, Wedding halls, restaurants with international and national cuisine, and attractive Panoramic restaurant and lounge bar on the rooftop of the Hotel with beautiful panoramic view to the green surroundings of the hotel and the city. The Sky bar is open only for organised corporate party, weddings, organisation of birthdays, etc.", null, true, null, 2, "Hotel Hills", null, true, null, 5, null, null },
                    { 2, "Mussalla 1, 71000 Sarajevo", null, 1, 1, new DateTime(2025, 8, 3, 19, 0, 56, 207, DateTimeKind.Unspecified), null, "Luxury hotel with a spa, modern amenities, and a central location.", null, true, null, 2, "Hotel Europe", null, null, null, 5, null, null },
                    { 3, "Kardinala Stepinca 31, 71000 Sarajevo", null, 1, 1, new DateTime(2025, 8, 4, 12, 15, 19, 743, DateTimeKind.Unspecified), null, "Premium luxury hotel offering top-tier amenities and stunning views.", null, true, null, 2, "Swissotel Sarajevo", null, null, null, 5, null, null },
                    { 5, "Sarajevska 29, 71000 Sarajevo", null, 1, 1, new DateTime(2025, 8, 15, 8, 45, 56, 950, DateTimeKind.Unspecified), null, "Well-known hotel with a rich history, offering a large conference center and leisure facilities.", null, true, null, 6, "Hotel Hollywood", null, null, null, 4, null, null },
                    { 7, "Prvog krajiškog korpusa 33, Banja Luka 78000", true, 4, 1, new DateTime(2025, 9, 5, 12, 16, 28, 803, DateTimeKind.Unspecified), null, "The 4-star Courtyard by Marriott Banja Luka offers modern-style accommodations and a number of amenities including a state-of-the-art lobby and an on-site restaurant. Located 1640 feet from the National Theater and within 1 mi of Kastel Fortress, it provides a 24-hour front desk and free WiFi.", null, true, null, 5, "Courtyard by Marriott", true, null, null, 4, null, true },
                    { 8, "Kralja Petra I Karađorđevića 129, Banja Luka 78000", null, 4, 1, new DateTime(2025, 9, 5, 12, 16, 28, 803, DateTimeKind.Unspecified), null, "Located 14 mi from Banja Luka International Airport, the hotel is a 19-minute walk from Kastel Fortress. Nearby attractions include the Banja Luka Museum and the Banja Luka Cathedral.", null, true, null, 5, "Hotel Integra Banja Luka", true, null, null, 4, null, true },
                    { 9, "Dr. Mladena Stojanovića 123, Banja Luka 78000", null, 4, 1, new DateTime(2025, 9, 5, 12, 16, 28, 803, DateTimeKind.Unspecified), null, "Set in the center of Banja Luka, Hotel Cezar is located only 164 feet from the main bus and train station. The City Park is 328 feet away. The on-site restaurant serves local and international cuisine.  Free Wi-Fi and free private parking are available. All rooms come with an LCD TV and a private bathroom with shower, hairdryer and toiletries. The breakfast is served each morning.", null, true, null, 5, "Hotel Cezar Banja Luka", null, null, null, 4, null, true },
                    { 10, "Kneza Višeslava, Mostar 88000", true, 2, 1, new DateTime(2025, 9, 5, 12, 16, 28, 803, DateTimeKind.Unspecified), null, "Offering free access to an indoor pool and a spa and wellness center, Hotel Mepas is located in Mostar, in the very center of town. A shopping center with various entertainment and shopping options is located right below the hotel. Free WiFi access is available as well as free parking. Each room here will provide you with a TV, air conditioning and a mini-bar. Featuring both a shower and a bathtub, the bathrooms also come with a hairdryer and bathrobes. Extras in all rooms include a seating area, satellite channels and pay-per-view channels.", true, true, null, 5, "Hotel Mepas", true, true, null, 5, null, true },
                    { 11, "Vukovarska 7, Mostar 88000", null, 2, 1, new DateTime(2025, 9, 5, 12, 16, 28, 803, DateTimeKind.Unspecified), null, "Located in a residential area of Mostar, City Hotel Mostar offers modernly furnished rooms that overlook the town. There is a restaurant in the same building, as well as shops, bars and rent-a-car service. Free Wi-Fi and free garage parking are available. All rooms are air-conditioned and feature a flat-screen TV, a mini-bar and a safe. Private bathroom offers a shower and hairdryer.", null, true, null, 5, "City Hotel Mostar", true, null, null, 5, null, true },
                    { 12, "29. hercegovačke udarne divizije 3, Mostar 88000", true, 2, 1, new DateTime(2025, 9, 5, 12, 16, 28, 803, DateTimeKind.Unspecified), null, "Comfortable Accommodations: Hotel Amicus in Mostar offers 4-star comfort with air-conditioned rooms featuring private bathrooms, balconies, and garden or pool views. Each room includes a TV, soundproofing, and free WiFi.", null, true, null, 5, "Hotel Amicus", true, true, null, 5, null, true }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "AC", "Capacity", "CityView", "DeletedTime", "Description", "HotelId", "IsAvailable", "IsDeleted", "Name", "PricePerNight", "QueenBed", "RoomTypeId", "WiFi" },
                values: new object[,]
                {
                    { 1, null, 3, null, null, null, 1, null, null, "Deluxe Triple Room", 150m, true, 1, null },
                    { 2, null, 2, null, null, "string", 1, true, null, "Deluxe Double Room", 100m, true, 2, true },
                    { 3, null, 2, true, null, "string", 2, true, null, "Superior Double Room", 125m, true, 3, true },
                    { 4, null, 2, null, null, "Providing free toiletries, this double room includes a private bathroom with a bath or a shower and a hairdryer. The spacious double room provides air conditioning, soundproof walls, a mini-bar, a wardrobe, as well as a flat-screen TV with cable channels. The unit offers 1 bed.", 5, true, null, "Deluxe Double Room", 100m, null, 2, null },
                    { 5, null, 2, null, null, "The spacious triple room offers air conditioning, soundproof walls, as well as a private bathroom boasting a walk-in shower and a hairdryer. The triple room has carpeted floors, a seating area with a flat-screen TV, a mini-bar, a tea and coffee maker as well as mountain views. The unit has 2 beds.", 3, true, null, "Superior Double Room", 140m, null, 3, null },
                    { 6, null, 2, null, null, "Standard Double Room", 3, true, null, "Standard Double Room", 100m, null, 4, null },
                    { 7, null, 3, null, null, "Standard King Room", 3, true, null, "Standard King Room", 180m, null, 5, null },
                    { 8, null, 1, null, null, "Single Superior Room", 5, true, null, "Single Superior Room", 80m, null, 7, null },
                    { 9, null, 1, null, null, "Twin Superior Room", 5, true, null, "Twin Superior Room", 130m, null, 6, null },
                    { 15, null, 1, null, null, "Standard, Guest room, 1 King, City view", 7, true, null, "Standard, Guest room", 89m, null, 10, null },
                    { 16, null, 2, null, null, "Superior, Guest room, 2 Twin/Single Bed(s), City view", 7, true, null, "Superior, Guest room", 109m, null, 10, null },
                    { 17, null, 3, null, null, "Triple Room", 8, true, null, "Triple Room", 130m, null, 12, null },
                    { 18, null, 4, null, null, "Premium Quadruple Room", 8, true, null, "Premium Quadruple Room", 150m, null, 13, null },
                    { 19, null, 2, null, null, "Double Room with Balcony", 9, true, null, "Double Room with Balcony", 69m, null, 14, null },
                    { 20, null, 4, null, null, "1 twin bed and 1 queen bed", 9, true, null, "Standard Family Room", 119m, null, 15, null },
                    { 21, null, 3, null, null, "1 sofa bed and 1 queen bed", 10, true, null, "Superior Suite", 145m, null, 16, null },
                    { 22, null, 3, null, null, "1 sofa bed and 1 queen bed", 10, true, null, "Family Suite", 125m, null, 17, null },
                    { 23, null, 3, null, null, "1 king bed and 1 sofa bed", 11, true, null, "Presidential Suite", 150m, null, 18, null },
                    { 24, null, 1, null, null, "Comfort Single Room", 12, true, null, "Comfort Single Room", 100m, null, 19, null },
                    { 25, null, 1, null, null, "Deluxe Single Room", 12, true, null, "Deluxe Single Room", 89m, null, 20, null }
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
                    { 1, new DateTime(2025, 8, 6, 10, 4, 4, 297, DateTimeKind.Unspecified), null, "hills1.jpg", 1, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 49, 57, 56, 48, 49, 55, 48, 50, 50, 46, 106, 112, 103, 63, 107, 61, 50, 54, 100, 52, 52, 102, 51, 55, 99, 51, 57, 98, 97, 49, 102, 50, 101, 48, 49, 52, 57, 56, 100, 48, 53, 100, 100, 97, 49, 51, 102, 97, 48, 53, 100, 99, 56, 101, 57, 57, 102, 57, 100, 50, 50, 57, 98, 57, 98, 48, 100, 57, 52, 55, 102, 100, 100, 99, 57, 48, 101, 56, 53, 99, 38, 111, 61 }, null, null, "image/jpeg", 1 },
                    { 2, new DateTime(2025, 8, 6, 10, 44, 1, 87, DateTimeKind.Unspecified), null, "hills2.jpg", 1, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 49, 57, 56, 48, 49, 55, 48, 51, 57, 46, 106, 112, 103, 63, 107, 61, 51, 52, 49, 54, 101, 50, 57, 100, 57, 56, 50, 56, 53, 100, 97, 51, 51, 100, 97, 49, 102, 99, 54, 57, 56, 101, 55, 98, 101, 100, 55, 55, 54, 54, 100, 100, 54, 48, 97, 55, 50, 52, 49, 50, 102, 51, 49, 51, 57, 57, 102, 56, 57, 102, 98, 48, 48, 98, 52, 53, 55, 101, 50, 54, 38, 111, 61 }, null, null, "image/jpeg", 2 },
                    { 3, new DateTime(2025, 8, 6, 11, 9, 30, 427, DateTimeKind.Unspecified), null, "hills1.2.jpg", 1, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 52, 56, 50, 55, 52, 56, 51, 56, 49, 46, 106, 112, 103, 63, 107, 61, 49, 49, 50, 51, 54, 55, 53, 99, 53, 97, 48, 49, 54, 102, 51, 57, 100, 99, 99, 102, 102, 55, 51, 49, 97, 52, 101, 49, 49, 52, 101, 99, 53, 57, 53, 56, 101, 51, 99, 100, 50, 52, 54, 55, 50, 102, 55, 49, 53, 48, 50, 52, 100, 57, 101, 99, 102, 102, 56, 54, 54, 49, 53, 53, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 52, 56, 50, 55, 52, 56, 51, 56, 49, 46, 106, 112, 103, 63, 107, 61, 49, 49, 50, 51, 54, 55, 53, 99, 53, 97, 48, 49, 54, 102, 51, 57, 100, 99, 99, 102, 102, 55, 51, 49, 97, 52, 101, 49, 49, 52, 101, 99, 53, 57, 53, 56, 101, 51, 99, 100, 50, 52, 54, 55, 50, 102, 55, 49, 53, 48, 50, 52, 100, 57, 101, 99, 102, 102, 56, 54, 54, 49, 53, 53, 38, 111, 61 }, null, "image/jpeg", 2 },
                    { 4, new DateTime(2025, 8, 12, 17, 13, 49, 637, DateTimeKind.Unspecified), null, "europe1.jpg", 2, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 53, 56, 51, 52, 48, 52, 49, 53, 48, 46, 106, 112, 103, 63, 107, 61, 49, 99, 102, 52, 101, 99, 49, 52, 100, 97, 50, 48, 53, 99, 52, 49, 48, 57, 48, 54, 97, 52, 57, 50, 57, 52, 56, 50, 100, 57, 99, 53, 50, 48, 53, 50, 51, 99, 49, 55, 55, 54, 101, 100, 97, 101, 50, 51, 100, 49, 51, 48, 54, 99, 56, 54, 56, 55, 49, 53, 99, 54, 53, 56, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 53, 56, 51, 52, 48, 52, 49, 53, 48, 46, 106, 112, 103, 63, 107, 61, 49, 99, 102, 52, 101, 99, 49, 52, 100, 97, 50, 48, 53, 99, 52, 49, 48, 57, 48, 54, 97, 52, 57, 50, 57, 52, 56, 50, 100, 57, 99, 53, 50, 48, 53, 50, 51, 99, 49, 55, 55, 54, 101, 100, 97, 101, 50, 51, 100, 49, 51, 48, 54, 99, 56, 54, 56, 55, 49, 53, 99, 54, 53, 56, 38, 111, 61 }, null, "image/jpeg", 3 },
                    { 5, new DateTime(2025, 8, 15, 9, 49, 6, 313, DateTimeKind.Unspecified), null, "europe2.jpg", 2, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 50, 52, 49, 51, 48, 55, 54, 48, 51, 46, 106, 112, 103, 63, 107, 61, 57, 51, 97, 98, 97, 50, 98, 56, 99, 52, 57, 48, 57, 101, 57, 48, 99, 98, 54, 101, 53, 101, 53, 53, 102, 102, 49, 54, 55, 54, 100, 100, 54, 97, 57, 52, 52, 49, 51, 57, 52, 50, 51, 98, 97, 57, 57, 101, 48, 97, 100, 51, 50, 99, 98, 53, 56, 57, 54, 48, 54, 102, 50, 56, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 48, 52, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 50, 52, 49, 51, 48, 55, 54, 48, 51, 46, 106, 112, 103, 63, 107, 61, 57, 51, 97, 98, 97, 50, 98, 56, 99, 52, 57, 48, 57, 101, 57, 48, 99, 98, 54, 101, 53, 101, 53, 53, 102, 102, 49, 54, 55, 54, 100, 100, 54, 97, 57, 52, 52, 49, 51, 57, 52, 50, 51, 98, 97, 57, 57, 101, 48, 97, 100, 51, 50, 99, 98, 53, 56, 57, 54, 48, 54, 102, 50, 56, 38, 111, 61 }, null, "image/jpeg", 3 },
                    { 6, new DateTime(2025, 8, 15, 9, 49, 6, 313, DateTimeKind.Unspecified), null, "europe3.jpg", 2, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 53, 48, 48, 47, 52, 57, 56, 51, 54, 49, 49, 57, 46, 106, 112, 103, 63, 107, 61, 56, 52, 49, 50, 101, 54, 51, 99, 97, 49, 102, 55, 101, 51, 52, 55, 56, 102, 54, 101, 57, 100, 99, 100, 55, 53, 55, 101, 49, 101, 51, 98, 56, 50, 54, 100, 97, 102, 102, 49, 99, 52, 57, 50, 51, 48, 57, 52, 97, 48, 54, 52, 49, 52, 49, 54, 54, 97, 48, 101, 98, 57, 53, 101, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 53, 48, 48, 47, 52, 57, 56, 51, 54, 49, 49, 57, 46, 106, 112, 103, 63, 107, 61, 56, 52, 49, 50, 101, 54, 51, 99, 97, 49, 102, 55, 101, 51, 52, 55, 56, 102, 54, 101, 57, 100, 99, 100, 55, 53, 55, 101, 49, 101, 51, 98, 56, 50, 54, 100, 97, 102, 102, 49, 99, 52, 57, 50, 51, 48, 57, 52, 97, 48, 54, 52, 49, 52, 49, 54, 54, 97, 48, 101, 98, 57, 53, 101, 38, 111, 61 }, null, "image/jpeg", 3 },
                    { 7, new DateTime(2025, 8, 25, 4, 55, 37, 867, DateTimeKind.Unspecified), null, "swissotel.jpg", 3, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 49, 53, 50, 53, 52, 48, 49, 55, 55, 46, 106, 112, 103, 63, 107, 61, 55, 100, 56, 52, 57, 98, 100, 55, 50, 51, 52, 99, 55, 55, 50, 48, 98, 98, 100, 53, 49, 100, 56, 57, 50, 51, 55, 53, 48, 100, 101, 99, 102, 101, 54, 48, 49, 55, 53, 51, 98, 102, 51, 57, 100, 53, 51, 53, 53, 97, 102, 52, 49, 50, 102, 99, 55, 98, 102, 98, 101, 49, 98, 101, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 49, 53, 50, 53, 52, 48, 49, 55, 55, 46, 106, 112, 103, 63, 107, 61, 55, 100, 56, 52, 57, 98, 100, 55, 50, 51, 52, 99, 55, 55, 50, 48, 98, 98, 100, 53, 49, 100, 56, 57, 50, 51, 55, 53, 48, 100, 101, 99, 102, 101, 54, 48, 49, 55, 53, 51, 98, 102, 51, 57, 100, 53, 51, 53, 53, 97, 102, 52, 49, 50, 102, 99, 55, 98, 102, 98, 101, 49, 98, 101, 38, 111, 61 }, null, "image/jpeg", 5 },
                    { 8, new DateTime(2025, 8, 25, 4, 55, 37, 867, DateTimeKind.Unspecified), null, "swissotel5.jpg", 3, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 49, 54, 57, 54, 56, 53, 54, 56, 57, 46, 106, 112, 103, 63, 107, 61, 56, 102, 101, 52, 102, 51, 102, 48, 100, 54, 97, 98, 53, 53, 55, 55, 101, 50, 50, 48, 51, 52, 51, 48, 56, 56, 49, 51, 54, 97, 101, 49, 51, 53, 49, 57, 97, 53, 57, 52, 50, 56, 100, 101, 57, 52, 56, 54, 56, 97, 50, 100, 49, 57, 52, 54, 48, 48, 48, 99, 51, 48, 99, 50, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 49, 54, 57, 54, 56, 53, 54, 56, 57, 46, 106, 112, 103, 63, 107, 61, 56, 102, 101, 52, 102, 51, 102, 48, 100, 54, 97, 98, 53, 53, 55, 55, 101, 50, 50, 48, 51, 52, 51, 48, 56, 56, 49, 51, 54, 97, 101, 49, 51, 53, 49, 57, 97, 53, 57, 52, 50, 56, 100, 94, 57, 52, 56, 54, 56, 97, 50, 100, 49, 57, 52, 54, 48, 48, 48, 99, 51, 48, 99, 50, 38, 111, 61 }, null, "image/jpeg", 6 },
                    { 9, new DateTime(2025, 8, 25, 5, 10, 40, 583, DateTimeKind.Unspecified), null, "swissotel1.jpeg", 3, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 49, 52, 53, 48, 54, 57, 55, 49, 53, 46, 106, 112, 103, 63, 107, 61, 98, 102, 97, 52, 56, 101, 52, 101, 102, 53, 57, 50, 53, 102, 98, 55, 98, 99, 51, 102, 100, 54, 56, 57, 49, 97, 49, 52, 51, 102, 51, 100, 51, 102, 50, 51, 102, 98, 98, 101, 50, 98, 98, 48, 55, 51, 102, 102, 51, 54, 52, 102, 53, 102, 54, 57, 49, 50, 97, 98, 55, 102, 52, 48, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 49, 52, 53, 48, 54, 57, 55, 49, 57, 46, 106, 112, 103, 63, 107, 61, 98, 102, 97, 52, 56, 101, 52, 101, 102, 53, 57, 50, 53, 102, 98, 55, 98, 99, 51, 102, 100, 54, 56, 57, 49, 97, 49, 52, 51, 102, 51, 100, 51, 102, 50, 51, 102, 98, 98, 101, 50, 98, 98, 48, 55, 51, 102, 102, 51, 54, 52, 102, 53, 102, 54, 57, 49, 50, 97, 98, 55, 102, 52, 48, 38, 111, 61 }, null, "image/jpeg", 7 },
                    { 10, new DateTime(2025, 8, 25, 5, 10, 40, 583, DateTimeKind.Unspecified), null, "swissotel2.jpeg", 3, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 53, 48, 48, 47, 49, 53, 50, 53, 50, 49, 50, 50, 53, 46, 106, 112, 103, 63, 107, 61, 50, 51, 48, 50, 55, 101, 48, 55, 54, 56, 57, 98, 97, 56, 53, 100, 51, 49, 99, 50, 102, 57, 56, 102, 50, 101, 49, 101, 51, 49, 52, 56, 97, 50, 100, 49, 101, 97, 101, 97, 98, 48, 101, 99, 100, 97, 100, 48, 54, 102, 99, 101, 102, 52, 56, 57, 51, 98, 50, 48, 100, 49, 99, 51, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 53, 48, 48, 47, 49, 53, 50, 53, 50, 49, 50, 50, 53, 46, 106, 112, 103, 63, 107, 61, 50, 51, 48, 50, 55, 101, 48, 55, 54, 56, 57, 98, 97, 56, 53, 100, 51, 49, 99, 50, 102, 57, 56, 102, 50, 101, 49, 101, 51, 49, 52, 56, 97, 50, 100, 49, 101, 97, 101, 97, 98, 48, 101, 99, 100, 97, 100, 48, 54, 102, 99, 101, 102, 52, 56, 57, 51, 98, 50, 48, 100, 49, 99, 51, 38, 111, 61 }, null, "image/jpeg", 5 },
                    { 11, new DateTime(2025, 8, 25, 5, 10, 40, 583, DateTimeKind.Unspecified), null, "swissotel3.jpeg", 3, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 51, 48, 48, 47, 49, 54, 57, 55, 49, 52, 49, 49, 55, 46, 106, 112, 103, 63, 107, 61, 49, 54, 54, 55, 54, 54, 50, 48, 98, 48, 51, 100, 97, 98, 99, 97, 50, 55, 49, 53, 49, 56, 49, 98, 56, 100, 56, 52, 50, 55, 49, 101, 100, 97, 49, 54, 51, 98, 49, 50, 53, 52, 49, 54, 100, 57, 49, 99, 50, 101, 56, 50, 52, 53, 48, 57, 99, 52, 57, 48, 50, 55, 54, 53, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 51, 48, 48, 47, 49, 54, 57, 55, 49, 52, 49, 49, 55, 46, 106, 112, 103, 63, 107, 61, 49, 54, 54, 55, 54, 54, 50, 48, 98, 48, 51, 100, 97, 98, 99, 97, 50, 55, 49, 53, 49, 56, 49, 98, 56, 100, 56, 52, 50, 55, 49, 101, 100, 97, 49, 54, 51, 98, 49, 50, 53, 52, 49, 54, 100, 57, 49, 99, 50, 101, 56, 50, 52, 53, 48, 57, 99, 52, 57, 48, 50, 55, 54, 53, 38, 111, 61 }, null, "image/jpeg", 6 },
                    { 12, new DateTime(2025, 8, 25, 5, 10, 40, 583, DateTimeKind.Unspecified), null, "hollywood1.jpeg", 5, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 52, 48, 51, 55, 55, 51, 54, 50, 49, 46, 106, 112, 103, 63, 107, 61, 51, 55, 98, 99, 48, 102, 97, 55, 48, 50, 98, 55, 99, 55, 51, 56, 51, 51, 56, 101, 52, 102, 49, 55, 98, 52, 57, 99, 97, 52, 101, 49, 50, 99, 57, 99, 56, 97, 56, 99, 50, 54, 99, 54, 99, 97, 98, 57, 52, 52, 50, 48, 100, 50, 56, 100, 57, 52, 99, 55, 102, 101, 57, 99, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 52, 48, 51, 55, 55, 51, 54, 50, 49, 46, 106, 112, 103, 63, 107, 61, 51, 55, 98, 99, 48, 102, 97, 55, 48, 50, 98, 55, 99, 55, 51, 56, 51, 51, 56, 101, 52, 102, 49, 55, 98, 52, 57, 99, 97, 52, 101, 49, 50, 99, 57, 99, 56, 97, 56, 99, 50, 54, 99, 54, 99, 97, 98, 57, 52, 52, 50, 48, 100, 50, 56, 100, 57, 52, 99, 55, 102, 101, 57, 99, 38, 111, 61 }, null, "image/jpeg", 4 },
                    { 13, new DateTime(2025, 8, 25, 5, 10, 40, 583, DateTimeKind.Unspecified), null, "hollywood2.jpeg", 5, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 98, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 53, 48, 48, 47, 52, 48, 50, 56, 53, 48, 56, 55, 56, 46, 106, 112, 103, 63, 107, 61, 99, 97, 100, 101, 49, 49, 57, 54, 102, 57, 99, 98, 55, 100, 51, 98, 56, 57, 99, 52, 49, 99, 55, 48, 50, 56, 52, 49, 101, 55, 98, 50, 48, 102, 52, 102, 54, 54, 51, 48, 101, 56, 55, 97, 54, 53, 56, 99, 50, 101, 100, 55, 50, 102, 53, 49, 48, 101, 52, 99, 101, 97, 98, 56, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 98, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 53, 48, 48, 47, 52, 48, 50, 56, 53, 48, 56, 55, 56, 46, 106, 112, 103, 63, 107, 61, 99, 97, 100, 101, 49, 49, 57, 54, 102, 57, 99, 98, 55, 100, 51, 98, 56, 57, 99, 52, 49, 99, 55, 48, 50, 56, 52, 49, 101, 55, 98, 50, 48, 102, 52, 102, 54, 54, 51, 48, 101, 56, 55, 97, 54, 53, 56, 99, 50, 101, 100, 55, 50, 102, 53, 49, 48, 101, 52, 99, 101, 97, 98, 56, 38, 111, 61 }, null, "image/jpeg", 4 },
                    { 14, new DateTime(2025, 8, 25, 5, 10, 40, 583, DateTimeKind.Unspecified), null, "hollywood3.jpeg", 5, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 49, 57, 54, 57, 57, 48, 55, 48, 54, 46, 106, 112, 103, 63, 107, 61, 52, 52, 101, 99, 51, 100, 102, 50, 48, 97, 57, 98, 99, 50, 48, 50, 56, 48, 57, 101, 102, 49, 53, 50, 49, 54, 100, 50, 102, 98, 48, 50, 55, 53, 57, 99, 52, 102, 52, 53, 101, 55, 100, 99, 54, 101, 98, 53, 54, 56, 57, 57, 98, 98, 57, 49, 50, 53, 53, 54, 102, 53, 50, 102, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 49, 57, 54, 57, 57, 48, 55, 48, 54, 46, 106, 112, 103, 63, 107, 61, 52, 52, 101, 99, 51, 100, 102, 50, 48, 97, 57, 98, 99, 50, 48, 50, 56, 48, 57, 101, 102, 49, 53, 50, 49, 54, 100, 50, 102, 98, 48, 50, 55, 53, 57, 99, 52, 102, 52, 53, 101, 55, 100, 99, 54, 101, 98, 53, 54, 56, 57, 57, 98, 98, 57, 49, 50, 53, 53, 54, 102, 53, 50, 102, 38, 111, 61 }, null, "image/jpeg", 4 },
                    { 15, new DateTime(2025, 9, 5, 14, 52, 20, 817, DateTimeKind.Unspecified), null, "marriot_bl_1.jpg", 7, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 54, 50, 55, 50, 57, 49, 48, 54, 55, 46, 106, 112, 103, 63, 107, 61, 49, 48, 52, 57, 56, 102, 97, 51, 51, 49, 53, 101, 102, 102, 53, 99, 98, 55, 101, 52, 50, 55, 98, 102, 102, 48, 101, 51, 55, 98, 100, 53, 99, 57, 56, 51, 54, 54, 56, 55, 102, 48, 56, 102, 56, 52, 53, 102, 97, 102, 55, 51, 56, 99, 101, 54, 53, 54, 53, 49, 99, 50, 98, 55, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 54, 50, 55, 50, 57, 49, 48, 54, 55, 46, 106, 112, 103, 63, 107, 61, 49, 48, 52, 57, 56, 102, 97, 51, 51, 49, 53, 101, 102, 102, 53, 99, 98, 55, 101, 52, 50, 55, 98, 102, 102, 48, 101, 51, 55, 98, 100, 53, 99, 57, 56, 51, 54, 54, 56, 55, 102, 48, 56, 102, 56, 52, 53, 102, 97, 102, 55, 51, 56, 99, 101, 54, 53, 54, 53, 49, 99, 50, 98, 55, 38, 111, 61 }, null, "image/jpeg", 16 },
                    { 16, new DateTime(2025, 9, 5, 14, 52, 20, 817, DateTimeKind.Unspecified), null, "integra_bl_0.jpg", 8, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 51, 51, 48, 49, 54, 52, 49, 53, 57, 46, 106, 112, 103, 63, 107, 61, 55, 52, 57, 49, 102, 98, 100, 57, 53, 49, 48, 55, 98, 97, 100, 50, 50, 97, 48, 99, 52, 55, 48, 54, 57, 52, 53, 101, 55, 51, 54, 50, 49, 48, 98, 98, 51, 102, 57, 52, 57, 53, 101, 49, 98, 102, 98, 100, 49, 99, 50, 102, 53, 51, 54, 100, 102, 52, 54, 97, 57, 48, 49, 98, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 51, 51, 48, 49, 54, 52, 49, 53, 57, 46, 106, 112, 103, 63, 107, 61, 55, 52, 57, 49, 102, 98, 100, 57, 53, 49, 48, 55, 98, 97, 100, 50, 50, 97, 48, 99, 52, 55, 48, 54, 57, 52, 53, 101, 55, 51, 54, 50, 49, 48, 98, 98, 51, 102, 57, 52, 57, 53, 101, 49, 98, 102, 98, 100, 49, 99, 50, 102, 53, 51, 54, 100, 102, 52, 54, 97, 57, 48, 49, 98, 38, 111, 61 }, null, "image/jpeg", 17 },
                    { 17, new DateTime(2025, 9, 5, 14, 52, 20, 817, DateTimeKind.Unspecified), null, "integra_bl_1.jpg", 8, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 53, 48, 48, 47, 51, 51, 48, 49, 54, 51, 57, 52, 56, 46, 106, 112, 103, 63, 107, 61, 97, 101, 49, 54, 102, 100, 53, 56, 55, 54, 54, 99, 50, 49, 49, 55, 102, 55, 51, 100, 48, 51, 56, 55, 57, 99, 102, 56, 52, 55, 102, 52, 101, 99, 54, 101, 54, 52, 49, 100, 98, 54, 52, 56, 98, 98, 52, 49, 57, 98, 52, 52, 99, 102, 55, 51, 54, 100, 57, 52, 101, 101, 102, 53, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 53, 48, 48, 47, 51, 51, 48, 49, 54, 51, 57, 52, 56, 46, 106, 112, 103, 63, 107, 61, 97, 101, 49, 54, 102, 100, 53, 56, 55, 54, 54, 99, 50, 49, 49, 55, 102, 55, 51, 100, 48, 51, 56, 55, 57, 99, 102, 56, 52, 55, 102, 52, 101, 99, 54, 101, 54, 52, 49, 100, 98, 54, 52, 56, 98, 98, 52, 49, 57, 98, 52, 52, 99, 102, 55, 51, 54, 100, 57, 52, 101, 101, 102, 53, 38, 111, 61 }, null, "image/jpeg", 18 },
                    { 18, new DateTime(2025, 9, 5, 14, 52, 20, 817, DateTimeKind.Unspecified), null, "cezar_bl_0.jpg", 9, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 53, 57, 55, 57, 50, 54, 48, 55, 49, 46, 106, 112, 103, 63, 107, 61, 50, 49, 48, 49, 57, 57, 97, 50, 56, 54, 97, 102, 52, 48, 54, 53, 57, 54, 97, 97, 49, 98, 48, 100, 50, 55, 55, 100, 56, 51, 55, 56, 99, 101, 99, 54, 49, 51, 102, 49, 50, 101, 98, 101, 57, 57, 100, 97, 55, 99, 51, 99, 52, 49, 99, 50, 98, 102, 52, 57, 98, 48, 52, 50, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 53, 57, 55, 57, 50, 54, 48, 55, 49, 46, 106, 112, 103, 63, 107, 61, 50, 49, 48, 49, 57, 57, 97, 50, 56, 54, 97, 102, 52, 48, 54, 53, 57, 54, 97, 97, 49, 98, 48, 100, 50, 55, 55, 100, 56, 51, 55, 56, 99, 101, 99, 54, 49, 51, 102, 49, 50, 101, 98, 101, 57, 57, 100, 97, 55, 99, 51, 99, 52, 49, 99, 50, 98, 102, 52, 57, 98, 48, 52, 50, 38, 111, 61 }, null, "image/jpeg", 19 },
                    { 19, new DateTime(2025, 9, 5, 14, 52, 20, 817, DateTimeKind.Unspecified), null, "cezar_bl_0.jpg", 9, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 51, 48, 48, 47, 50, 48, 54, 50, 55, 56, 57, 53, 49, 46, 106, 112, 103, 63, 107, 61, 56, 97, 56, 98, 102, 51, 100, 99, 49, 102, 97, 57, 57, 54, 56, 55, 55, 53, 98, 97, 101, 56, 97, 55, 99, 57, 102, 98, 53, 52, 102, 101, 97, 99, 102, 48, 53, 48, 48, 55, 102, 98, 54, 50, 52, 48, 52, 57, 50, 100, 98, 98, 53, 49, 53, 101, 49, 99, 99, 54, 51, 48, 51, 49, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 51, 48, 48, 47, 50, 48, 54, 50, 55, 56, 57, 53, 49, 46, 106, 112, 103, 63, 107, 61, 56, 97, 56, 98, 102, 51, 100, 99, 49, 102, 97, 57, 57, 54, 56, 55, 55, 53, 98, 97, 101, 56, 97, 55, 99, 57, 102, 98, 53, 52, 102, 101, 97, 99, 102, 48, 53, 48, 48, 55, 102, 98, 54, 50, 52, 48, 52, 57, 50, 100, 98, 98, 53, 49, 53, 101, 49, 99, 99, 54, 51, 48, 51, 49, 38, 111, 61 }, null, "image/jpeg", 20 },
                    { 20, new DateTime(2025, 9, 5, 14, 52, 20, 817, DateTimeKind.Unspecified), null, "mepas_0.jpg", 10, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 51, 50, 56, 48, 57, 57, 55, 53, 46, 106, 112, 103, 63, 107, 61, 52, 57, 53, 97, 52, 55, 49, 98, 101, 100, 100, 101, 99, 49, 99, 55, 48, 51, 56, 53, 53, 52, 55, 101, 56, 48, 98, 49, 97, 50, 97, 54, 51, 52, 97, 48, 97, 49, 55, 56, 99, 56, 98, 55, 98, 51, 49, 98, 97, 97, 97, 51, 48, 56, 50, 49, 98, 98, 53, 56, 99, 48, 48, 50, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 51, 50, 56, 48, 57, 57, 55, 53, 46, 106, 112, 103, 63, 107, 61, 52, 57, 53, 97, 52, 55, 49, 98, 101, 100, 100, 101, 99, 49, 99, 55, 48, 51, 56, 53, 53, 52, 55, 101, 56, 48, 98, 49, 97, 50, 97, 54, 51, 52, 97, 48, 97, 49, 55, 56, 99, 56, 98, 55, 98, 51, 49, 98, 97, 97, 97, 51, 48, 56, 50, 49, 98, 98, 53, 56, 99, 48, 48, 50, 38, 111, 61 }, null, "image/jpeg", 21 },
                    { 21, new DateTime(2025, 9, 5, 14, 52, 20, 817, DateTimeKind.Unspecified), null, "mepas_1.jpg", 10, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 51, 48, 48, 47, 51, 50, 56, 49, 48, 48, 50, 57, 46, 106, 112, 103, 63, 107, 61, 98, 56, 52, 49, 48, 51, 54, 49, 48, 99, 56, 51, 48, 54, 52, 51, 57, 100, 52, 49, 101, 100, 100, 54, 97, 54, 99, 48, 101, 101, 101, 53, 54, 52, 55, 53, 56, 98, 100, 98, 48, 51, 48, 54, 48, 54, 98, 57, 50, 98, 50, 54, 49, 57, 101, 49, 102, 98, 48, 57, 54, 50, 50, 50, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 51, 48, 48, 47, 51, 50, 56, 49, 48, 48, 50, 57, 46, 106, 112, 103, 63, 107, 61, 98, 56, 52, 49, 48, 51, 54, 49, 48, 99, 56, 51, 48, 58, 52, 51, 57, 100, 52, 49, 101, 100, 100, 54, 97, 54, 99, 48, 101, 101, 101, 53, 54, 52, 55, 53, 56, 98, 100, 98, 48, 51, 48, 54, 48, 54, 98, 57, 50, 98, 50, 54, 49, 57, 101, 49, 102, 98, 48, 57, 54, 50, 50, 50, 38, 111, 61 }, null, "image/jpeg", 22 },
                    { 22, new DateTime(2025, 9, 5, 14, 52, 20, 817, DateTimeKind.Unspecified), null, "city_0.jpg", 11, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 49, 53, 53, 52, 50, 56, 57, 53, 46, 106, 112, 103, 63, 107, 61, 101, 100, 54, 54, 98, 55, 53, 51, 55, 50, 99, 57, 55, 51, 50, 57, 99, 97, 53, 97, 52, 102, 52, 50, 54, 48, 53, 51, 50, 51, 101, 51, 48, 52, 55, 97, 52, 49, 52, 97, 57, 51, 52, 50, 102, 56, 99, 55, 48, 100, 55, 54, 57, 51, 97, 57, 102, 49, 50, 50, 98, 97, 54, 50, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 49, 53, 53, 52, 50, 56, 57, 53, 46, 106, 112, 103, 63, 107, 61, 101, 100, 54, 54, 98, 55, 53, 51, 55, 50, 99, 57, 55, 51, 50, 57, 99, 97, 53, 97, 52, 102, 52, 50, 54, 48, 53, 51, 50, 51, 101, 51, 48, 52, 55, 97, 52, 49, 52, 97, 57, 51, 52, 50, 102, 56, 99, 55, 48, 100, 55, 54, 57, 51, 97, 57, 102, 49, 50, 50, 98, 97, 54, 50, 38, 111, 61 }, null, "image/jpeg", 23 },
                    { 23, new DateTime(2025, 9, 5, 14, 52, 20, 817, DateTimeKind.Unspecified), null, "amicus_0.jpg", 12, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 51, 48, 48, 47, 50, 54, 53, 55, 53, 51, 49, 55, 52, 46, 106, 112, 103, 63, 107, 61, 49, 99, 53, 50, 50, 100, 102, 54, 53, 52, 52, 49, 100, 102, 101, 57, 99, 53, 56, 51, 51, 100, 49, 49, 53, 98, 53, 100, 57, 98, 99, 54, 51, 102, 48, 53, 102, 98, 54, 55, 98, 57, 49, 53, 99, 49, 99, 53, 56, 102, 99, 98, 56, 48, 50, 57, 57, 51, 57, 48, 102, 55, 57, 55, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 51, 48, 48, 47, 50, 54, 53, 55, 53, 51, 49, 55, 52, 46, 106, 112, 103, 63, 107, 61, 49, 99, 53, 50, 50, 100, 102, 54, 53, 52, 52, 49, 100, 102, 101, 57, 99, 53, 56, 51, 51, 100, 49, 49, 53, 98, 53, 100, 57, 98, 99, 54, 51, 102, 48, 53, 102, 98, 54, 55, 98, 57, 49, 53, 99, 49, 99, 53, 56, 102, 99, 98, 56, 48, 50, 57, 57, 51, 57, 48, 102, 55, 57, 55, 38, 111, 61 }, null, "image/jpeg", 24 },
                    { 24, new DateTime(2025, 9, 5, 14, 52, 20, 817, DateTimeKind.Unspecified), null, "amicus_1.jpg", 12, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 50, 55, 48, 49, 49, 57, 53, 49, 48, 46, 106, 112, 103, 63, 107, 61, 57, 54, 48, 97, 101, 52, 50, 49, 56, 54, 102, 99, 56, 52, 51, 102, 52, 99, 53, 49, 57, 99, 52, 98, 50, 49, 100, 57, 101, 55, 52, 102, 98, 56, 57, 100, 53, 51, 49, 52, 100, 50, 55, 101, 97, 99, 100, 102, 52, 53, 99, 100, 97, 53, 52, 102, 55, 98, 54, 49, 51, 51, 52, 98, 38, 111, 61 }, new byte[] { 104, 116, 116, 112, 115, 58, 47, 47, 99, 102, 46, 98, 115, 116, 97, 116, 105, 99, 46, 99, 111, 109, 47, 120, 100, 97, 116, 97, 47, 105, 109, 97, 103, 101, 115, 47, 104, 111, 116, 101, 108, 47, 109, 97, 120, 49, 48, 50, 52, 120, 55, 54, 56, 47, 50, 55, 48, 49, 49, 57, 53, 49, 48, 46, 106, 112, 103, 63, 107, 61, 57, 54, 48, 97, 101, 52, 50, 49, 56, 54, 102, 99, 56, 52, 51, 102, 52, 99, 53, 49, 57, 99, 52, 98, 50, 49, 100, 57, 101, 55, 52, 102, 98, 56, 57, 100, 53, 51, 49, 52, 100, 50, 55, 101, 97, 99, 100, 102, 52, 53, 99, 100, 97, 53, 52, 102, 55, 98, 54, 49, 51, 51, 52, 98, 38, 111, 61 }, null, "image/jpeg", 25 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "CheckInDate", "CheckOutDate", "CreatedAt", "DeletedTime", "IsDeleted", "RoomId", "Status", "TotalPrice", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 7, 10, 0, 0, 100, DateTimeKind.Unspecified), new DateTime(2025, 8, 17, 10, 0, 0, 100, DateTimeKind.Unspecified), new DateTime(2025, 8, 6, 18, 5, 59, 100, DateTimeKind.Unspecified), null, null, 1, null, 1500m, 1 },
                    { 2, new DateTime(2025, 8, 7, 10, 0, 0, 100, DateTimeKind.Unspecified), new DateTime(2025, 8, 17, 10, 0, 0, 100, DateTimeKind.Unspecified), new DateTime(2025, 8, 6, 18, 5, 59, 100, DateTimeKind.Unspecified), null, null, 2, null, 1500m, 1 },
                    { 3, new DateTime(2025, 8, 20, 10, 0, 0, 680, DateTimeKind.Unspecified), new DateTime(2025, 8, 22, 10, 0, 0, 680, DateTimeKind.Unspecified), new DateTime(2025, 8, 7, 6, 30, 15, 680, DateTimeKind.Unspecified), null, null, 1, "Completed", 300m, 2 },
                    { 4, new DateTime(2025, 8, 9, 10, 28, 1, 710, DateTimeKind.Unspecified), new DateTime(2025, 8, 14, 10, 28, 1, 710, DateTimeKind.Unspecified), new DateTime(2025, 8, 8, 18, 28, 1, 710, DateTimeKind.Unspecified), null, null, 1, "Completed", 750m, 6 },
                    { 6, new DateTime(2025, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Pending", 650m, 6 },
                    { 7, new DateTime(2025, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Pending", 500m, 6 },
                    { 8, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Pending", 250m, 6 },
                    { 9, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Pending", 250m, 6 },
                    { 10, new DateTime(2025, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Pending", 150m, 6 },
                    { 11, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 520m, 6 },
                    { 12, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 5, "Pending", 560m, 6 },
                    { 13, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 5, "Pending", 560m, 6 },
                    { 14, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 5, "Pending", 560m, 6 },
                    { 15, new DateTime(2025, 8, 16, 21, 59, 11, 450, DateTimeKind.Unspecified), new DateTime(2025, 8, 16, 21, 59, 11, 450, DateTimeKind.Unspecified), new DateTime(2025, 8, 16, 21, 59, 11, 450, DateTimeKind.Unspecified), null, null, 3, "booked", 500m, 6 },
                    { 16, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 5, "Pending", 420m, 6 },
                    { 17, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 5, "Pending", 420m, 6 },
                    { 18, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 5, "Pending", 420m, 6 },
                    { 19, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 4, "Pending", 300m, 6 },
                    { 20, new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 4, "Pending", 300m, 6 },
                    { 21, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 520m, 6 },
                    { 22, new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 395m, 6 },
                    { 23, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 5, "Pending", 560m, 6 },
                    { 24, new DateTime(2025, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 4, "Pending", 500m, 6 },
                    { 25, new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 5, "Pending", 560m, 6 },
                    { 26, new DateTime(2025, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Pending", 300m, 6 },
                    { 27, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Pending", 350m, 6 },
                    { 28, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Pending", 350m, 6 },
                    { 29, new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 4, "Pending", 400m, 6 },
                    { 30, new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 4, "Pending", 400m, 6 },
                    { 31, new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 4, "Pending", 400m, 6 },
                    { 32, new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 4, "Pending", 400m, 6 },
                    { 33, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Pending", 650m, 6 },
                    { 34, new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Pending", 800m, 6 },
                    { 35, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 645m, 6 },
                    { 36, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Pending", 1100m, 6 },
                    { 37, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Pending", 1100m, 6 },
                    { 38, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Pending", 1100m, 6 },
                    { 39, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Pending", 450m, 6 },
                    { 40, new DateTime(2025, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 770m, 6 },
                    { 41, new DateTime(2025, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 770m, 6 },
                    { 42, new DateTime(2025, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 770m, 6 },
                    { 43, new DateTime(2025, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Pending", 950m, 6 },
                    { 44, new DateTime(2025, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 645m, 6 },
                    { 45, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 250m, 6 },
                    { 46, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 270m, 6 },
                    { 47, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 270m, 6 },
                    { 48, new DateTime(2025, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 250m, 6 },
                    { 49, new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Pending", 550m, 6 },
                    { 50, new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Pending", 550m, 6 },
                    { 51, new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Pending", 550m, 6 },
                    { 52, new DateTime(2025, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 645m, 6 },
                    { 53, new DateTime(2025, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 645m, 6 },
                    { 54, new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Pending", 250m, 6 },
                    { 55, new DateTime(2025, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Pending", 550m, 6 },
                    { 56, new DateTime(2025, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Pending", 550m, 6 },
                    { 57, new DateTime(2025, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Pending", 550m, 6 },
                    { 58, new DateTime(2025, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 645m, 6 },
                    { 59, new DateTime(2025, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 645m, 6 },
                    { 60, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 520m, 6 },
                    { 61, new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 645m, 6 },
                    { 62, new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 645m, 6 },
                    { 63, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Confirmed", 520m, 6 },
                    { 64, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Confirmed", 520m, 6 },
                    { 65, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Confirmed", 520m, 6 },
                    { 66, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Confirmed", 520m, 6 },
                    { 67, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 5, "Confirmed", 560m, 6 },
                    { 68, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 4, "Confirmed", 400m, 6 },
                    { 69, new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Confirmed", 645m, 6 },
                    { 70, new DateTime(2025, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Confirmed", 800m, 6 },
                    { 71, new DateTime(2025, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Confirmed", 650m, 13 },
                    { 72, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Confirmed", 900m, 13 },
                    { 73, new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Confirmed", 1050m, 13 },
                    { 74, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Confirmed", 650m, 13 },
                    { 75, new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Confirmed", 1000m, 13 },
                    { 76, new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Confirmed", 270m, 13 },
                    { 77, new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Confirmed", 520m, 13 },
                    { 78, new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Confirmed", 625m, 13 },
                    { 79, new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Confirmed", 625m, 13 },
                    { 80, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Confirmed", 1770m, 13 },
                    { 81, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 1770m, 13 },
                    { 82, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Pending", 550m, 13 },
                    { 83, new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 2, "Pending", 550m, 13 },
                    { 84, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 6, "Pending", 600m, 13 },
                    { 85, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 8, "Pending", 320m, 13 },
                    { 86, new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 3, "Pending", 770m, 13 },
                    { 87, new DateTime(2025, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 8, "Pending", 400m, 13 },
                    { 88, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 9, "Confirmed", 780m, 13 },
                    { 89, new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Pending", 1100m, 13 },
                    { 90, new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, 1, "Pending", 1100m, 13 }
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
                table: "Payment",
                columns: new[] { "Id", "Amount", "CreatedAt", "Currency", "DeletedTime", "IsDeleted", "Provider", "ProviderPaymentId", "ReservationId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 645m, new DateTime(2025, 8, 18, 16, 7, 0, 347, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxVaKKH5av5GhJI0TtbCxav", 53, "requires_payment_method", null },
                    { 2, 550m, new DateTime(2025, 8, 18, 19, 47, 56, 530, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxZ28KH5av5GhJI0CUJ63XF", 56, "requires_payment_method", null },
                    { 3, 550m, new DateTime(2025, 8, 18, 19, 48, 44, 650, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxZ2uKH5av5GhJI1j745cw1", 56, "requires_payment_method", null },
                    { 4, 550m, new DateTime(2025, 8, 18, 19, 51, 26, 887, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxZ5WKH5av5GhJI1RVq9UBb", 57, "requires_payment_method", null },
                    { 5, 645m, new DateTime(2025, 8, 18, 20, 0, 22, 53, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxZEAKH5av5GhJI1w9aQe3w", 59, "requires_payment_method", null },
                    { 6, 645m, new DateTime(2025, 8, 18, 20, 1, 44, 410, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxZFUKH5av5GhJI01TZhB1z", 59, "requires_payment_method", null },
                    { 7, 520m, new DateTime(2025, 8, 18, 20, 6, 13, 140, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxZJpKH5av5GhJI0nOZjyEP", 60, "requires_payment_method", null },
                    { 8, 645m, new DateTime(2025, 8, 19, 7, 23, 57, 447, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxjthKH5av5GhJI02tmdHJd", 61, "requires_payment_method", null },
                    { 9, 645m, new DateTime(2025, 8, 19, 8, 0, 21, 243, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxkSvKH5av5GhJI1AhEKH1y", 62, "requires_payment_method", null },
                    { 10, 520m, new DateTime(2025, 8, 19, 8, 2, 54, 910, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxkVPKH5av5GhJI1a1s20y8", 63, "requires_payment_method", null },
                    { 11, 520m, new DateTime(2025, 8, 19, 8, 2, 56, 970, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxkVRKH5av5GhJI0cBEouxn", 63, "succeeded", new DateTime(2025, 8, 19, 8, 3, 34, 327, DateTimeKind.Unspecified) },
                    { 12, 520m, new DateTime(2025, 8, 19, 8, 14, 11, 967, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxkgKKH5av5GhJI0IghVNgj", 64, "succeeded", new DateTime(2025, 8, 19, 8, 14, 44, 967, DateTimeKind.Unspecified) },
                    { 13, 520m, new DateTime(2025, 8, 19, 8, 15, 41, 460, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxkhmKH5av5GhJI1VvZgCc7", 65, "succeeded", new DateTime(2025, 8, 19, 8, 15, 59, 103, DateTimeKind.Unspecified) },
                    { 14, 520m, new DateTime(2025, 8, 19, 8, 16, 6, 870, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxkiBKH5av5GhJI0FDlMPWT", 66, "requires_payment_method", null },
                    { 15, 520m, new DateTime(2025, 8, 19, 8, 20, 26, 13, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxkmMKH5av5GhJI1HXaZSZc", 66, "succeeded", new DateTime(2025, 8, 19, 8, 20, 49, 230, DateTimeKind.Unspecified) },
                    { 16, 560m, new DateTime(2025, 8, 19, 9, 12, 47, 587, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3Rxlb2KH5av5GhJI1DiObtS8", 67, "succeeded", new DateTime(2025, 8, 19, 9, 13, 6, 757, DateTimeKind.Unspecified) },
                    { 17, 400m, new DateTime(2025, 8, 19, 10, 13, 58, 470, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RxmYFKH5av5GhJI1iksnFs6", 68, "succeeded", new DateTime(2025, 8, 19, 10, 14, 17, 830, DateTimeKind.Unspecified) },
                    { 18, 645m, new DateTime(2025, 8, 21, 8, 18, 34, 100, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RyThdKH5av5GhJI0ZsV59V5", 69, "succeeded", new DateTime(2025, 8, 21, 8, 18, 57, 480, DateTimeKind.Unspecified) },
                    { 19, 800m, new DateTime(2025, 8, 21, 8, 26, 37, 363, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RyTpRKH5av5GhJI1bWbc61O", 70, "succeeded", new DateTime(2025, 8, 21, 8, 26, 55, 447, DateTimeKind.Unspecified) },
                    { 20, 650m, new DateTime(2025, 8, 22, 16, 30, 6, 500, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RyxqsKH5av5GhJI1UpyMP6h", 71, "succeeded", new DateTime(2025, 8, 22, 16, 30, 26, 347, DateTimeKind.Unspecified) },
                    { 21, 900m, new DateTime(2025, 8, 22, 16, 30, 40, 337, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RyxrQKH5av5GhJI1B65tf0S", 72, "succeeded", new DateTime(2025, 8, 22, 16, 30, 57, 720, DateTimeKind.Unspecified) },
                    { 22, 1050m, new DateTime(2025, 8, 22, 16, 31, 10, 283, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RyxruKH5av5GhJI1ud55iEj", 73, "succeeded", new DateTime(2025, 8, 22, 16, 31, 25, 113, DateTimeKind.Unspecified) },
                    { 23, 650m, new DateTime(2025, 8, 22, 16, 31, 44, 53, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RyxsSKH5av5GhJI1wYe3PAa", 74, "succeeded", new DateTime(2025, 8, 22, 16, 31, 57, 480, DateTimeKind.Unspecified) },
                    { 24, 1000m, new DateTime(2025, 8, 22, 16, 32, 12, 943, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RyxsvKH5av5GhJI1uSbokVs", 75, "succeeded", new DateTime(2025, 8, 22, 16, 32, 26, 407, DateTimeKind.Unspecified) },
                    { 25, 270m, new DateTime(2025, 8, 22, 16, 32, 41, 370, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RyxtNKH5av5GhJI1MFgRmlY", 76, "succeeded", new DateTime(2025, 8, 22, 16, 32, 57, 203, DateTimeKind.Unspecified) },
                    { 26, 520m, new DateTime(2025, 8, 22, 16, 33, 16, 237, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RyxtwKH5av5GhJI05yLJEBT", 77, "succeeded", new DateTime(2025, 8, 22, 16, 33, 36, 877, DateTimeKind.Unspecified) },
                    { 27, 625m, new DateTime(2025, 8, 22, 16, 33, 48, 720, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RyxuSKH5av5GhJI04plVd0n", 78, "succeeded", new DateTime(2025, 8, 22, 16, 34, 2, 237, DateTimeKind.Unspecified) },
                    { 28, 625m, new DateTime(2025, 8, 22, 16, 34, 6, 373, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RyxukKH5av5GhJI1K0DqUVl", 79, "succeeded", new DateTime(2025, 8, 22, 16, 34, 23, 357, DateTimeKind.Unspecified) },
                    { 29, 1770m, new DateTime(2025, 8, 22, 16, 34, 38, 27, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RyxvGKH5av5GhJI12RQJgwG", 80, "succeeded", new DateTime(2025, 8, 22, 16, 34, 51, 967, DateTimeKind.Unspecified) },
                    { 30, 780m, new DateTime(2025, 8, 25, 6, 56, 32, 93, DateTimeKind.Unspecified), "USD", null, null, "stripe", "pi_3RzuKQKH5av5GhJI0XCCT0Qn", 88, "succeeded", new DateTime(2025, 8, 25, 6, 56, 55, 410, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "DeletedTime", "HotelId", "IsDeleted", "Rating", "ReservationId", "ReviewDate", "UserId" },
                values: new object[,]
                {
                    { 1, "Very good", null, 1, null, 4, 3, new DateTime(2025, 8, 7, 6, 40, 56, 70, DateTimeKind.Unspecified), 2 },
                    { 2, "Very good", null, 1, null, 5, 1, new DateTime(2025, 8, 22, 18, 15, 14, 967, DateTimeKind.Unspecified), 1 },
                    { 3, "Super", null, 1, null, 5, 1, new DateTime(2025, 8, 22, 18, 29, 8, 153, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

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
                table: "Assets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Assets",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

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
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Payment",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

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
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 14 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 15 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 16 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 17 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 18 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "Assets",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Assets",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__UserRole__AF2760AD81661037",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK__UserRoles__RoleI__6477ECF3",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__UserRoles__UserI__6383C8BA",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
