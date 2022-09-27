using MedFarmAPI;
using MedFarmAPI.Data;
using MedFarmAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

ConfigureAuthentication(builder);
ConfigureServices(builder);
ConfigureMvc(builder);

var app = builder.Build();

LoadConfiguration(app);

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
//app.UseStaticFiles();    // Suporte para arquivos estáticos, HTML, CSS, JS, imagens...

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Habilitando a autenticação e a autorização para login
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();


// buscando credenciais para JWT, serviço de e-mail e storage no Firebase
void LoadConfiguration(WebApplication app)
{
    var connectionStrings = app.Configuration.GetConnectionString("DefaultConnection");
    Configuration.ConnectionStrings = connectionStrings;

    Configuration.JwtKey = app.Configuration.GetValue<string>("JwtKey");

    var smtp = new Configuration.SmtpConfiguration();
    app.Configuration.GetSection("Smtp").Bind(smtp);
    Configuration.Smtp = smtp;

    var firebaseStorage = new Configuration.FirebaseConfiguration();
    app.Configuration.GetSection("FirebaseStorage").Bind(firebaseStorage);
    Configuration.Firebase = firebaseStorage;
}

// Configurando Autenticação na API
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

// Configurando acesso MVC por controllers e liberando tamanho padrão de retorno JSON
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
    builder.Services.AddHttpContextAccessor();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    //builder.Services.AddAWSService<IAmazonS3>();
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

    // Injeção de dependencia
    builder.Services.AddDbContext<DataContext>();
    builder.Services.AddTransient<TokenService>();
    builder.Services.AddTransient<EmailService>();
    builder.Services.AddTransient<ViewBodyService>();
}
