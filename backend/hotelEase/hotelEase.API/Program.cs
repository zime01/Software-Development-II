using hotelEase.Services;
using hotelEase.Services.Database;
using Microsoft.EntityFrameworkCore;
using Mapster;
using hotelEase.Services.HotelsStateMachine;
using hotelEase.API.Filters;
using Microsoft.AspNetCore.Authentication;
using hotelEase.API;
using Microsoft.OpenApi.Models;
using EasyNetQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IHotelsService, HotelsService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IRoomTypesService, RoomTypesService>();
builder.Services.AddTransient<IRolesService, RolesService>();
builder.Services.AddTransient<ICitiesService, CitiesService>();
builder.Services.AddTransient<ICountriesService, CountriesService>();
builder.Services.AddTransient<IRoomsService, RoomService>();
builder.Services.AddTransient<IAssetsService, AssetsService>();
builder.Services.AddTransient<IRoomsAvailabilityService, RoomsAvailabilityService>();
builder.Services.AddTransient<IReservationsService, ReservationsService>();
builder.Services.AddTransient<IServicesService, ServicesService>();
builder.Services.AddTransient<IReviewsService, ReviewsService>();
builder.Services.AddTransient<INotificationsService, NotificationsService>();

var host = "localhost"; //Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "rabbitmq";
var user = "guest"; //Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "guest";
var pass = "guest"; // Environment.GetEnvironmentVariable("RABBITMQ_PASS") ?? "guest";

var connectionString1 = $"host={host};username={user};password={pass}";

builder.Services.AddSingleton(RabbitHutch.CreateBus(connectionString1));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHostedService<NotificationWorker>();


builder.Services.AddTransient<BaseHotelsState>();
builder.Services.AddTransient<InitialHotelState>();
builder.Services.AddTransient<DraftHotelsState>();
builder.Services.AddTransient<ActiveHotelsState>();
builder.Services.AddTransient<HiddenHotelsState>();

builder.Services.AddControllers(x =>
{
    x.Filters.Add<ExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("basicAuth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "basic"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id = "basicAuth"}
            },
            new string[]{}
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("HotelEaseConnection");
Console.WriteLine($"con string {connectionString}");
builder.Services.AddDbContext<HotelEaseContext>(options => 
    options.UseSqlServer(connectionString));


builder.Services.AddMapster();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<HotelEaseContext>();
    //dataContext.Database.EnsureCreated();

    //dataContext.Database.Migrate();
}

app.Run();
