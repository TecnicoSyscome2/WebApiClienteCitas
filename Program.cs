//Tecnologia de ProyectoCitasSycome Con tecnologias WebApi Propuesta, Creada y probada
//Por Tecnico: Javier Alexander Rivera Fuentes para Syscome

using citasApp.clientes.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApi.Citas.ClientesApp.Auth;
using WebApi.Citas.ClientesApp.DAL;
using WebApi.Citas.ClientesApp.MailBoxService;
using WebApi.Citas.ClientesApp.Modelos;

var builder = WebApplication.CreateBuilder(args);

// Configuración de Servicios
builder.Services.AddScoped<ClientesDAL>();
builder.Services.AddScoped<CitasDAL>();
builder.Services.AddScoped<ServicioEmailBox>();
builder.Services.AddScoped<AsesoresDAL>();
builder.Services.AddScoped<EmpresaDAL>();
builder.Services.AddScoped<ProductosDAL>();
builder.Services.AddScoped<administradorDAL>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<Logins, userModel>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin() // Cambiar a WithOrigins("https://tu-dominio.com") para producción
               .AllowAnyMethod()
               .AllowAnyHeader();
              // .AllowCredentials(); 
    });
});

// Configuración de conexión a base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BDConexion>(options =>
    options.UseSqlServer(connectionString));

// Configuración de JWT
var jwtKey = builder.Configuration["JwtSetting:_Key"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});

builder.Services.AddSingleton(new JwtAuthenticationService(jwtKey));
builder.Services.AddMemoryCache();

// Configuración de controladores y JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Configuración de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiRest", Version = "v1" });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        },
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Ingrese el token JWT para autenticar"
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

// Configuración de la aplicación
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiRest v1"));
}

// Redirección HTTPS
app.UseHttpsRedirection();

// Aplicar política de CORS
app.UseCors("MyPolicy");

// Middleware de autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

// Ejecutar la aplicación
app.Run();

