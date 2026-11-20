using Microsoft.EntityFrameworkCore;
using ClientManagementAPI.Data;
using ClientManagementAPI.Profiles;
using ClientManagementAPI.Interfaces;
using ClientManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<IClientsService, ClientsService>();

builder.Services.AddAutoMapper(
    cfg => { },
    typeof(ClientsProfile)
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
