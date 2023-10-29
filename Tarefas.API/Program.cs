using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Tarefas.API.Context;
using Tarefas.API.Interfaces;
using Tarefas.API.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Swagger habilitado para realizar autentica��o de usu�rios
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tarefas.API", Version = "v1" });

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Header de autoriza��o JWT usando o esquema Bearer. \r\n\r\nInforme 'Bearer' [espa�o] e seu token. \r\n\r\n Exemplo: 'Bearer a1b2c3d4e5f6'"
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});

builder.Services.AddSwaggerGen(c =>
{	
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile) ;
	c.IncludeXmlComments(xmlPath);
});

//Configura��o de Interfaces
builder.Services.AddTransient<ITarefas, TarefasService>();

//Configura��o do Banco de Dados
builder.Services.AddDbContext<AppDbContext>();

//Configura��o para autentifica��o de acesso de usu�rio usu�rio
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddAuthentication(
	JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
		ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Adiciona os middlewares de autentica��o e autoriza��o
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
