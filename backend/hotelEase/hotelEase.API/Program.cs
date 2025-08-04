using hotelEase.Services;
using hotelEase.Services.Database;
using Microsoft.EntityFrameworkCore;
using Mapster;
using hotelEase.Services.HotelsStateMachine;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IHotelsService, HotelsService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IRoomTypesService, RoomTypesService>();

builder.Services.AddTransient<BaseHotelsState>();
builder.Services.AddTransient<InitialHotelState>();
builder.Services.AddTransient<DraftHotelsState>();
builder.Services.AddTransient<ActiveHotelsState>();
builder.Services.AddTransient<HiddenHotelsState>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("HotelEaseConnection");

builder.Services.AddDbContext<HotelEaseContext>(options => 
    options.UseSqlServer(connectionString));


builder.Services.AddMapster();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
