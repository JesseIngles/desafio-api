using System.Net.Http.Headers;
using System.Text;
using api.DAL;
using api.DTOs;
using api.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adiciona Swagger/OpenAPI com suporte a autenticação básica
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Autenticação básica com usuário e senha"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference { Type= ReferenceType.SecurityScheme, Id= "basic"}
            },
            new string[]{ }
        }
    });
});
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Middleware simples para autenticação básica
app.MapPost("/login", async (LogarCredenciaisDto dto, UsuarioService usuarioService) =>
{
    var result = await usuarioService.LogarAsync(dto);
    if (!result.IsSuccess)
        return Results.Unauthorized();

    return Results.Ok(new { token = result.Data, message = result.ErrorMessage });
});

app.MapPost("/login", () =>
{
    return Results.Ok(new { message = "Use Authorization header para acessar outros endpoints" });
})
.WithTags("Auth")
.WithName("Login");

// Simula dados na memória
var resources = new List<string> { "Evento 1", "Evento 2", "Evento 3" };
var reservations = new List<(int userId, string resource, DateTime date)>();

// GET /resources - lista recursos principais
app.MapGet("/resources", () =>
{
    return Results.Ok(resources);
})
.WithTags("MVP")
.RequireAuthorization();

// POST /reservations - cria reserva
app.MapPost("/reservations", (ReservationRequest req) =>
{
    reservations.Add((req.UserId, req.Resource, req.Date));
    return Results.Created($"/reservations/{req.UserId}", req);
})
.WithTags("MVP")
.RequireAuthorization();

// GET /users/{id}/reservations - lista reservas do usuário
app.MapGet("/users/{id}/reservations", (int id) =>
{
    var userReservations = reservations.Where(r => r.userId == id)
                                       .Select(r => new { r.resource, r.date });
    return Results.Ok(userReservations);
})
.WithTags("MVP")
.RequireAuthorization();

app.Run();

record ReservationRequest(int UserId, string Resource, DateTime Date);
