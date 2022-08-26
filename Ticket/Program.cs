using Microsoft.EntityFrameworkCore;
using Ticket.Data;
using Ticket.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TicketDbContext>(builder =>
{
    builder.UseSqlServer("Server=89.252.185.155\\MSSQLSERVER2012;Database=tayfunu1_Ticket;User Id=tayfunu1_ticket;Password=414623752A.;Trusted_Connection=False");
    builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<CustomerRequestService>();
builder.Services.AddScoped<UserRequestService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        b => b
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true)
        .AllowCredentials()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticket v1"));
}
app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapControllers();

app.Urls.Clear();
app.Urls.Add("http://*:5000");

app.Run();
