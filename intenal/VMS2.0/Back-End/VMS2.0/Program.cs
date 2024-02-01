using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Encodings.Web;
using VMS2._0.Data;
using VMS2._0.Repositories;
using VMS2._0.Repositories.IRepository;
using VMS2._0.Repositories.Repository;
using VMS2._0.Services;
using VMS2._0.Services.IService;
using VMS2._0.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "VMS2.0 API", Version = "v1" });
});
builder.Services.AddDbContext<VMSDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IVisitorRepository, VisitorRepository>();
builder.Services.AddScoped<IVisitRepository, VisitRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
// builder.Services.AddScoped<IURLRepository, URLRepository>();


// Register services
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<IVisitService, VisitService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IURLService, URLService>();


// Other configurations
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VMS2.0 API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
