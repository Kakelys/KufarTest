using Api.Services;
using Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IKufarClient, KufarClient>();
builder.Services.AddScoped<IKufarService, KufarService>();

var app = builder.Build();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(opts => {
    opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    opts.RoutePrefix = "";
});

app.MapControllers();

app.Run();