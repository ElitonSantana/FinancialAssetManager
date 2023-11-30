using FinancialAssetManager;
using FinancialAssetManager.Domain.Options;
using FinancialAssetManager.Infra.MongoConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ServiceCollectionExtension.ConfigureServices(builder.Services);
ServiceCollectionExtension.ConfigureRepositorys(builder.Services);

//MongoDD Settings Configure
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
builder.Services.Configure<CredentialsOptions>(builder.Configuration.GetSection("Credentials"));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
