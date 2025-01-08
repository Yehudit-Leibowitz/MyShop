using Microsoft.EntityFrameworkCore;
using NLog.Web;
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

builder.Services.AddDbContext<ApiDbToCodeContext>(options => options.UseSqlServer(
    "Server=SRV2\\PUPILS;Database=api_db_to_code;Trusted_Connection=True;TrustServerCertificate=True"));
  //  "Server=DESKTOP-58UFOBM;Database=ApiDbToCode;User Id=sa;Password=9553595535;Trusted_Connection=True;TrustServerCertificate=True"));


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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
