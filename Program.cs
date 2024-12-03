using Data.Mappings;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5500/")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .WithExposedHeaders("A ccess-control-allow-origin");
                      });
});
builder.Services.AddControllers();

builder.Services.AddDbContext<ListaCasamentoDataContext>();

var app = builder.Build();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseAuthorization();

app.MapControllers();

app.Run();