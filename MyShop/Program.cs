using Microsoft.EntityFrameworkCore;
using MyShop.Middelware;
using NLog.Web;
using PresidentsApp.Middlewares;
using Repository;
using service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();

builder.Services.AddDbContext<ApiDbToCodeContext>(options => options.UseSqlServer(
    "Server=SRV2\\PUPILS;Database=api_db_to_code;Trusted_Connection=True;TrustServerCertificate=True"));
//(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Seminar")));
builder.Host.UseNLog();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRatingMiddleware();
app.UseHttpsRedirection();

app.UseStaticFiles();


app.UseErrorHandlingMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
