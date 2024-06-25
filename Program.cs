using Agri_Energy_Connect.Data;
using Agri_Energy_Connect.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Seed data: roles and default users
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var roles = new[] { "Employee", "Farmer" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Default employee user
    string employeeEmail = "employee@employee.com";
    string employeePassword = "Admin1234#";

    if (await userManager.FindByEmailAsync(employeeEmail) == null)
    {
        var employeeUser = new User { UserName = employeeEmail, Email = employeeEmail, EmailConfirmed = true };
        await userManager.CreateAsync(employeeUser, employeePassword);
        await userManager.AddToRoleAsync(employeeUser, "Employee");
    }

    // Default farmer user
    string farmerEmail = "john@doefarming.com";
    string farmerPassword = "Farmer1234#";

    if (await userManager.FindByEmailAsync(farmerEmail) == null)
    {
        var farmerUser = new User { UserName = farmerEmail, Email = farmerEmail, EmailConfirmed = true };
        await userManager.CreateAsync(farmerUser, farmerPassword);
        await userManager.AddToRoleAsync(farmerUser, "Farmer");

        // Add Farmer details
        var farmer = new Farmer
        {
            Name = "John",
            Email = farmerEmail,
            PhoneNumber = "1234567890",
            IsApproved = true
        };

        context.Farmers.Add(farmer);
        await context.SaveChangesAsync();

        // Add products for the farmer
        var products = new[]
        {
            new Product { Name = "Carrots", Category = "Vegetable", ProductionDate = DateTime.Now.AddDays(-10), FarmerId = farmer.FarmerId },
            new Product { Name = "Chicken Eggs", Category = "Protein", ProductionDate = DateTime.Now.AddDays(-5), FarmerId = farmer.FarmerId },
            new Product { Name = "Bananas", Category = "Fruit", ProductionDate = DateTime.Now.AddDays(-5), FarmerId = farmer.FarmerId }
        };

        context.Products.AddRange(products);
        await context.SaveChangesAsync();
    }
}

app.Run();
