using Microsoft.EntityFrameworkCore;
using SlojPodataka;
using SlojPodataka.Repozitorijumi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<KontekstBaze>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SkolaDB")));

builder.Services.AddScoped<RepozitorijumUcenika>();
builder.Services.AddScoped<RepozitorijumPredmeta>();
builder.Services.AddScoped<RepozitorijumCas>();
builder.Services.AddScoped<RepozitorijumPrisustva>();
builder.Services.AddScoped<RepozitorijumKorisnika>();
builder.Services.AddScoped<RepozitorijumUcenikPredmet>();
builder.Services.AddScoped<IEvidencijaServis, EvidencijaServis>();

var app = builder.Build();

app.MapControllers();

app.Run();