using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration {
    public class CategoryConfiguration : IEntityTypeConfiguration<Category> {

        public void Configure(EntityTypeBuilder<Category> builder) {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

            //quando executar a migração, irá criar essas informações se elas não existirem na tabela.
            builder.HasData(new Category(id: 1, name: "Material Escolar"),
                            new Category(id: 2, name: "Eletrônicos"),
                            new Category(id: 3, name: "Acessórios"));


        }

    }
}
