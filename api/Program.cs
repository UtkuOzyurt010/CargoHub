using api.Models;
using api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options => 
{
    options.IdleTimeout = TimeSpan.FromMinutes(40);
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true; 
});

builder.Services.AddDbContext<DatabaseContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresDb")));

builder.Services.AddTransient<IGenericService<Client>, ClientService>();
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IGenericService<Inventory>, InventoryService>();
builder.Services.AddTransient<IGenericService<ItemGroup>, ItemGroupService>();
builder.Services.AddTransient<IGenericService<ItemLine>, ItemLineService>();
builder.Services.AddTransient<IGenericService<ItemType>, ItemTypeService>();
builder.Services.AddTransient<IGenericService<Location>, LocationService>();
builder.Services.AddTransient<IGenericService<Order>, OrderService>();
builder.Services.AddTransient<IGenericService<Shipment>, ShipmentService>();
builder.Services.AddTransient<IGenericService<Supplier>, SupplierService>();
builder.Services.AddTransient<IGenericService<Transfer>, TransferService>();
builder.Services.AddTransient<IGenericService<Warehouse>, WarehouseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
