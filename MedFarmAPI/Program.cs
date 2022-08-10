using MedFarmAPI;
using MedFarmAPI.Data;
using MedFarmAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

ConfigureAuthentication(builder);
ConfigureServices(builder);
ConfigureMvc(builder);

var app = builder.Build();

LoadConfiguration(app);

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
//app.UseStaticFiles();    // Suporte para arquivos est�ticos, HTML, CSS, JS, imagens...

app.UseHttpsRedirection();

// Habilitando a autentica��o e a autoriza��o para login
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


// buscando credenciais para JWT e servi�o de e-mail
void LoadConfiguration(WebApplication app)
{
    Configuration.JwtKey = app.Configuration.GetValue<string>("JwtKey");

    var smtp = new Configuration.SmtpConfiguration();
    app.Configuration.GetSection("Smtp").Bind(smtp);
    Configuration.Smtp = smtp;
}

// Configurando Autentica��o na API
void ConfigureAuthentication(WebApplicationBuilder builder)
{
    var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,

        };
    });
}

// Configurando acesso MVC por controllers e liberando tamanho padr�o de retorno JSON
void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => {
        options.SuppressModelStateInvalidFilter = true;
    });

    builder.Services.AddControllers().AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
}

void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddRazorPages();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MedFarm Api", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description =
                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Scheme = "Bearer",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
    });

    // Inje��o de dependencia
    builder.Services.AddDbContext<DataContext>();
    builder.Services.AddTransient<TokenService>();
    builder.Services.AddTransient<EmailService>();
    builder.Services.AddTransient<ViewBodyService>();
}
