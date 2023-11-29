using ApplicationCore_API.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_API.SeedData
{
    public class CategorySeedData : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
                (
                    new Category { Id = 1, Name = "Kasap", Description = "Et ve Tavuk Ürünleri" },
                    new Category { Id = 2, Name = "Manav", Description = "Meyve ve Sebze Ürünleri" },
                    new Category { Id = 3, Name = "Şarküteri", Description = "Süt Ürünleri" }
                );
        }
    }
}
