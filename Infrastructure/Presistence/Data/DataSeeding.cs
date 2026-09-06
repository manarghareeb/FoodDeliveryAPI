using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Domain.Entities.OrderModule;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Presistence.Data
{
    public class DataSeeding(StoreDbContext _dbContext, RoleManager<IdentityRole> _roleManager, UserManager<User> _userManager) : IDataSeeding
    {
        public async Task SeedDataAsync()
        {
            try
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }
                if (!_dbContext.ProductBrands.Any())
                {
                    var productBrandsData = File.OpenRead("..\\infrastructure\\Presistence\\Data\\DataSeed\\brands.json");
                    var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(productBrandsData);
                    if (productBrands is not null && productBrands.Any())
                        await _dbContext.ProductBrands.AddRangeAsync(productBrands);
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var productTypesData = File.OpenRead("..\\infrastructure\\Presistence\\Data\\DataSeed\\types.json");
                    var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(productTypesData);
                    if (productTypes is not null && productTypes.Any())
                        await _dbContext.ProductTypes.AddRangeAsync(productTypes);
                }
                if (!_dbContext.Products.Any())
                {
                    var productData = File.OpenRead("..\\infrastructure\\Presistence\\Data\\DataSeed\\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productData);
                    if (products is not null && products.Any())
                        await _dbContext.Products.AddRangeAsync(products);
                }
                if (!_dbContext.DeliveryMethods.Any())
                {
                    var deliveryMethodsData = File.OpenRead("..\\infrastructure\\Presistence\\Data\\DataSeed\\delivery.json");
                    var DeliveryMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(deliveryMethodsData);
                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                        await _dbContext.DeliveryMethods.AddRangeAsync(DeliveryMethods);
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                
            }
        }

        public async Task SeedIdentityDataAsync()
        {
            try
            {
                // 1] Seed Role
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                // 2] Seed User
                if(!_userManager.Users.Any())
                {
                    var adminUser = new User()
                    {
                        DisplayName = "Admin",
                        UserName = "Admin",
                        Email = "Admin@gmail.com",
                        PhoneNumber = "1234567890",
                    };
                    var superAdminUser = new User()
                    {
                        DisplayName = "SuperAdmin",
                        UserName = "SuperAdmin",
                        Email = "SuperAdmin@gmail.com",
                        PhoneNumber = "01098765432",
                    };
                    await _userManager.CreateAsync(adminUser, "P@ssw0rd");
                    await _userManager.CreateAsync(superAdminUser, "Pa$$w0rd");
                    // 3] Assign Roles ==> Users
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                    await _userManager.AddToRoleAsync(adminUser, "SuperAdmin");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
