using Microsoft.EntityFrameworkCore;
using core.entity;

namespace core.data.Configurations
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
                new Product(){ProductId=1,Name="Samsung S5",Url="samsung-s5",Price=2000,Description="iyi telefon", IsApproved=true},
                new Product(){ProductId=2,Name="Samsung S6",Url="samsung-s6",Price=3000,Description="iyi telefon", IsApproved=false},
                new Product(){ProductId=3,Name="Samsung S7",Url="samsung-s7",Price=4000,Description="iyi telefon", IsApproved=true},
                new Product(){ProductId=4,Name="Samsung S8",Url="samsung-s8",Price=5000,Description="iyi telefon", IsApproved=false},
                new Product(){ProductId=5,Name="Samsung S9",Url="samsung-s9",Price=6000,Description="iyi telefon", IsApproved=true}
            );

            builder.Entity<Category>().HasData(
                new Category(){CategoryId=1,Name="Telefon",Url="telefon"},
                new Category(){CategoryId=2,Name="Bilgisayar",Url="bilgisayar"},
                new Category(){CategoryId=3,Name="Elektronik",Url="elektronik"},
                new Category(){CategoryId=4,Name="Beyaz Eşya",Url="beyaz-esya"}
            );

            builder.Entity<ProductCategory>().HasData(
                new ProductCategory(){ProductId=1,CategoryId=1},
                new ProductCategory(){ProductId=1,CategoryId=2},
                new ProductCategory(){ProductId=1,CategoryId=3},
                new ProductCategory(){ProductId=2,CategoryId=1},
                new ProductCategory(){ProductId=2,CategoryId=2},
                new ProductCategory(){ProductId=2,CategoryId=3},
                new ProductCategory(){ProductId=3,CategoryId=4},
                new ProductCategory(){ProductId=4,CategoryId=3},
                new ProductCategory(){ProductId=5,CategoryId=3},
                new ProductCategory(){ProductId=5,CategoryId=1}

           );
        }
    }
}