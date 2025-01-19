using CargoHub.Models;
using CargoHub.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;

namespace CargoHub;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseUrls("http://0.0.0.0:8080");
        //builder.WebHost.UseUrls("http://dotnet run --urls "http://0.0.0.0:8080":8080");


        builder.Services.AddControllers();
        // I added this so we can control what fields are sent back with the response json
        builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("SqliteDb") + ";Cache=Shared"), ServiceLifetime.Scoped);

        // Register services
        builder.Services.AddTransient<IGenericService<Client>, ClientService>();
        builder.Services.AddTransient<IItemService, ItemService>();
        builder.Services.AddTransient<IGenericService<Inventory>, InventoryService>();
        builder.Services.AddTransient<IGenericService<ItemGroup>, ItemGroupService>();
        builder.Services.AddTransient<IGenericService<ItemLine>, ItemLineService>();
        builder.Services.AddTransient<IGenericService<ItemType>, ItemTypeService>();
        builder.Services.AddTransient<ILocationService, LocationService>();
        builder.Services.AddTransient<IOrderService, OrderService>();
        builder.Services.AddTransient<IShipmentService, ShipmentService>();
        builder.Services.AddTransient<IGenericService<Supplier>, SupplierService>();
        builder.Services.AddTransient<ITransfer, TransferService>();
        builder.Services.AddTransient<IGenericService<Warehouse>, WarehouseService>();
        builder.Services.AddTransient<IInventoryService, InventoryService>();
        builder.Services.AddTransient<IItemService, ItemService>();

        //Swagger
        builder.Services.AddSwaggerGen(options =>
        {
            options.OperationFilter<AddSauceHeaderOperationFilter>();
        });

        builder.Services.AddAutoMapper(typeof(MappingProfile));

        builder.Services.AddScoped<MigrationService>();

        var app = builder.Build();
        // only uncomment when you intend to migrate the data from json to another database.
        // using (var scope = app.Services.CreateScope())
        // {
        //     var migrationService = scope.ServiceProvider.GetRequiredService<MigrationService>();
        //     await migrationService.MigrateAll();
        // }

        app.UseRouting();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "CargoHub API v1");
                options.RoutePrefix = string.Empty; // Optioneel: maakt Swagger toegankelijk op de root URL.
            });
        }
        app.UseAuthorization();
        
        //checking API key
        app.UseMiddleware<ApiKeyMiddleware>();
        
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.Run();
    }
}

public class AddSauceHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "API_KEY",
            In = ParameterLocation.Header,
            Description = "The API key string",
            Required = false, // If we want it required set this to true
            Schema = new OpenApiSchema
            {
                Type = "string"
            }
        });
    }
}
