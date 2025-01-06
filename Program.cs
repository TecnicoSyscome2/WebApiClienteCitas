using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApi.Citas.ClientesApp.Auth;
using WebApi.Citas.ClientesApp.DAL;
using WebApi.Citas.ClientesApp.Modelos;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddScoped<administradorDAL>();  // Registra administradorDAL como servicio
builder.Services.AddMemoryCache();  // Registrar IMemoryCache para el controlador de Login
builder.Services.AddScoped<userModel>();
// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});



// Configurar DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BDConexion>(options =>
    options.UseSqlServer(connectionString));

// Configurar autenticación JWT
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

// Registrar servicio de autenticación personalizado
builder.Services.AddSingleton(new JwtAuthenticationService(jwtKey));

// Registrar IMemoryCache
builder.Services.AddMemoryCache();  // Esta línea es importante

// Configurar controladores y JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Configurar Swagger con soporte para JWT
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

// Construir la aplicación
var app = builder.Build();

// Configurar el pipeline de middleware

// Mostrar excepciones y Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiRest v1"));
}

app.UseHttpsRedirection();

// Aplicar política de CORS
app.UseCors("MyPolicy");

// Configurar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

// Ejecutar la aplicación
app.Run();
