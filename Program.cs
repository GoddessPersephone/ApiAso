using ApiAso.IOC;
using ApiAso.Repositorio.Cadastro;
using ApiAso.Repositorio.Interface.Cadastro;
using FluentValidation.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers().AddFluentValidation(config => { config.RegisterValidatorsFromAssembly(typeof(Program).Assembly); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
builder.Services.AddScoped<IEnderecoRepositorio, EnderecoRepositorio>();
builder.Services.AddScoped<ITipoContatoRepositorio, TipoContatoRepositorio>();
builder.Services.AddScoped<ITipoEnderecoRepositorio, TipoEnderecoRepositorio>();
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