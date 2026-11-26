using Microsoft.EntityFrameworkCore;
using ClientManagementAPI.Data;
using ClientManagementAPI.Profiles;
using ClientManagementAPI.Interfaces;
using ClientManagementAPI.Services;
using ClientManagementAPI.DTO.ClientDTO;
using ClientManagementAPI.Filters;
using ClientManagementAPI.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddProblemDetails();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<IClientsService, ClientsService>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddScoped<IValidator<ClientCreateDto>, ClientCreateDtoValidator>();
builder.Services.AddScoped<ValidationFilter<ClientCreateDto>>();


builder.Services.AddAutoMapper(
    cfg => { },
    typeof(ClientsProfile)
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
