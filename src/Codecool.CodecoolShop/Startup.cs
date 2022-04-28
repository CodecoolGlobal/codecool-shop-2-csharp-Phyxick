using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Codecool.CodecoolShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Product/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });

            SetupInMemoryDatabases();
        }

        private void SetupInMemoryDatabases()
        {
            IProductDao productDataStore = ProductDaoMemory.GetInstance();
            IProductCategoryDao productCategoryDataStore = ProductCategoryDaoMemory.GetInstance();
            ISupplierDao supplierDataStore = SupplierDaoMemory.GetInstance();

            Supplier amazon = new Supplier{Name = "Amazon", Description = "Digital content and services"};
            supplierDataStore.Add(amazon);
            Supplier lenovo = new Supplier{Name = "Lenovo", Description = "Computers"};
            supplierDataStore.Add(lenovo);
            ProductCategory chakram = new ProductCategory {Name = "Chakram", Department = "Weapon", Description = "Round shaped weapon that works like a frizby." };
            ProductCategory crossbow = new ProductCategory {Name = "Crossbow", Department = "Weapon", Description = "A bow for people who like to hold heavy weapons with two hands." };
            ProductCategory dagger = new ProductCategory {Name = "Dagger", Department = "Weapon", Description = "A fine piece of weapon suitable for a fine fighter." };
            ProductCategory shield = new ProductCategory {Name = "Shield", Department = "Armour", Description = "Something to protect you against your foes." };
            ProductCategory sword = new ProductCategory {Name = "Sword", Department = "Weapon", Description = "A weapon for a real man." };
            ProductCategory nunchaku = new ProductCategory {Name = "Nunchaku", Department = "Weapon", Description = "You don't want to be hit by this." };
            ProductCategory ship = new ProductCategory {Name = "Ship", Department = "Weapon", Description = "A first class method of transportation" };
            ProductCategory spear = new ProductCategory {Name = "Spear", Department = "Weapon", Description = "If you want to reach far, but stay protected at the same time." };
            ProductCategory helmet = new ProductCategory {Name = "Helmet", Department = "Armour", Description = "For your fine head." };
            ProductCategory mace = new ProductCategory {Name = "Mace", Department = "Weapon", Description = "Weapon for a tough guy." };
            ProductCategory axe = new ProductCategory {Name = "Axe", Department = "Weapon", Description = "Weapon for a tough guy." };
            ProductCategory armour = new ProductCategory {Name = "Armour", Department = "Armour", Description = "A fine piece of body wear to protect from all your foes." };
            ProductCategory sandal = new ProductCategory {Name = "Sandal", Department = "Footwear", Description = "Great footwear for even the longest fights." };
            ProductCategory other = new ProductCategory {Name = "Various items", Department = "Other", Description = "Various items." };
            ProductCategory trident = new ProductCategory {Name = "Trident", Department = "Weapon", Description = "A weapon for a God." };
            productCategoryDataStore.Add(chakram);
            productCategoryDataStore.Add(crossbow);
            productCategoryDataStore.Add(dagger);
            productCategoryDataStore.Add(shield);
            productCategoryDataStore.Add(sword);
            productCategoryDataStore.Add(nunchaku);
            productCategoryDataStore.Add(ship);
            productCategoryDataStore.Add(spear);
            productCategoryDataStore.Add(helmet);
            productCategoryDataStore.Add(axe);
            productCategoryDataStore.Add(armour);
            productCategoryDataStore.Add(sandal);
            productCategoryDataStore.Add(other);
            productCategoryDataStore.Add(trident);
            productDataStore.Add(new Product { Name = "Xena Chakram", DefaultPrice = 49.9m, Image = "chakram-xena.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = chakram, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Xena Chakram Alt", DefaultPrice = 49.9m, Image = "chakram-xena2.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = chakram, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Metallic Chakram", DefaultPrice = 49.9m, Image = "chakram-metallic.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = chakram, Supplier = amazon });
            productDataStore.Add(new Product { Name = "LMBTQ Chakram", DefaultPrice = 49.9m, Image = "chakram-lmbtq.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = chakram, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Crossbow", DefaultPrice = 49.9m, Image = "crossbow1.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = crossbow, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Crossbow", DefaultPrice = 49.9m, Image = "crossbow2.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = crossbow, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Crossbow", DefaultPrice = 49.9m, Image = "crossbow2.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = crossbow, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Dagger", DefaultPrice = 49.9m, Image = "dagger-sword.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = dagger, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Gastraphetes", DefaultPrice = 49.9m, Image = "gastraphetes.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = crossbow, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Gastraphetes", DefaultPrice = 49.9m, Image = "gastraphetes2.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = crossbow, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Hoplon", DefaultPrice = 49.9m, Image = "hoplon.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = shield, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Kopis", DefaultPrice = 49.9m, Image = "kopis.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = sword, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Kopis of Alexander the Great", DefaultPrice = 49.9m, Image = "kopis-alexander-the-great.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = sword, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Leonidas' warrior knife", DefaultPrice = 49.9m, Image = "leonidas-warrior-knife.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = sword, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Nunchaku", DefaultPrice = 49.9m, Image = "nunchaku1.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = nunchaku, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Nunchaku", DefaultPrice = 49.9m, Image = "nunchaku2.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = nunchaku, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Ship", DefaultPrice = 49.9m, Image = "ship1.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = ship, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Ship", DefaultPrice = 49.9m, Image = "ship2.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = ship, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Persian Ship", DefaultPrice = 49.9m, Image = "ship-persian.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = ship, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Spartan Army Bowie Knife", DefaultPrice = 49.9m, Image = "spartan-army-bowie-knife.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = sword, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Xiphos", DefaultPrice = 49.9m, Image = "xiphos.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = sword, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Spartan Army Spear", DefaultPrice = 49.9m, Image = "spartan-army-spear.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = spear, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Red Hoplon", DefaultPrice = 49.9m, Image = "spartan-hoplon.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = shield, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Red and Yellow Hoplon", DefaultPrice = 49.9m, Image = "spartan-hoplon2.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = shield, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Scorpion Hoplon", DefaultPrice = 49.9m, Image = "spartan-hoplon3.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = shield, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Spartan Spear", DefaultPrice = 49.9m, Image = "spartan-spear.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = spear, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Double Sided Spear", DefaultPrice = 49.9m, Image = "spear-double-sided.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = spear, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Helmet", DefaultPrice = 49.9m, Image = "greekHelmet.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = helmet, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Helmet", DefaultPrice = 49.9m, Image = "greekHelmet2.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = helmet, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Helmet", DefaultPrice = 49.9m, Image = "greekHelmet3.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = helmet, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Sword", DefaultPrice = 49.9m, Image = "sword1.png", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = sword, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Sword", DefaultPrice = 49.9m, Image = "sword2.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = sword, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Sword", DefaultPrice = 49.9m, Image = "sword3.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = sword, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Sword", DefaultPrice = 49.9m, Image = "sword4.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = sword, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Ship", DefaultPrice = 49.9m, Image = "ship3.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = ship, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Ship", DefaultPrice = 49.9m, Image = "ship4.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = ship, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Ship", DefaultPrice = 49.9m, Image = "ship5.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = ship, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Spear", DefaultPrice = 49.9m, Image = "spear.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = spear, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Spear", DefaultPrice = 49.9m, Image = "spear2.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = spear, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Spear", DefaultPrice = 49.9m, Image = "spear3.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = spear, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Spear", DefaultPrice = 49.9m, Image = "spear4.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = spear, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Spear", DefaultPrice = 49.9m, Image = "spear5.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = spear, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Spear", DefaultPrice = 49.9m, Image = "spear6.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = spear, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Mace", DefaultPrice = 49.9m, Image = "mace.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = mace, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Mace", DefaultPrice = 49.9m, Image = "mace2.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = mace, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Mace", DefaultPrice = 49.9m, Image = "mace3.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = mace, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Axe", DefaultPrice = 49.9m, Image = "axe.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = axe, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Axe of Hephaistos", DefaultPrice = 49.9m, Image = "axe-of-hephaistos.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = axe, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Shield", DefaultPrice = 49.9m, Image = "shield1.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = shield, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Shield", DefaultPrice = 49.9m, Image = "shield2.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = shield, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Shield", DefaultPrice = 49.9m, Image = "shield3.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = shield, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Shield", DefaultPrice = 49.9m, Image = "shield4.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = shield, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Armour", DefaultPrice = 49.9m, Image = "armour.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = armour, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Complete Armour", DefaultPrice = 49.9m, Image = "complete-armour.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = armour, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Complete Armour", DefaultPrice = 49.9m, Image = "complete-armour2.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = armour, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Complete Armour", DefaultPrice = 49.9m, Image = "complete-armour3.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = armour, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Complete Armour", DefaultPrice = 49.9m, Image = "complete-armour4.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = armour, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Helmet", DefaultPrice = 49.9m, Image = "helmet.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = helmet, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Sandal", DefaultPrice = 49.9m, Image = "sandal.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = sandal, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Sandal", DefaultPrice = 49.9m, Image = "sandal2.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = sandal, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Armour", DefaultPrice = 49.9m, Image = "armour2.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = armour, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Armour", DefaultPrice = 49.9m, Image = "armour3.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = armour, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Armour", DefaultPrice = 49.9m, Image = "armour4.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = armour, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Xena Fancy Dress", DefaultPrice = 49.9m, Image = "xena-fancy-dress.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = other, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Roman Fancy Dress", DefaultPrice = 49.9m, Image = "roman-fancy-dress.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = other, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Grappa 1l", DefaultPrice = 49.9m, Image = "grappa.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = other, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Vase", DefaultPrice = 49.9m, Image = "vase.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = other, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Ancient Greek Phone", DefaultPrice = 49.9m, Image = "ancient-greek-phone.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = other, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Trident", DefaultPrice = 49.9m, Image = "trident.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = trident, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Trident", DefaultPrice = 49.9m, Image = "trident2.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = trident, Supplier = amazon });
            productDataStore.Add(new Product { Name = "Trident", DefaultPrice = 49.9m, Image = "trident3.jpg", Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = trident, Supplier = amazon });
        }
    }
}
