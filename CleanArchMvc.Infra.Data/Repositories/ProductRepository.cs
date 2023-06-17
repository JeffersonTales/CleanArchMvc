using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Repositories {
    public class ProductRepository : IProductRepository {

        #region Atributos
        ApplicationDbContext _productContext;
        #endregion

        #region Construtor
        public ProductRepository(ApplicationDbContext context) { //injeta o contexto no construtor
            this._productContext = context;
        }
        #endregion

        #region Métodos do Contrato
        public async Task<Product> CreateAsync(Product product) {
            this._productContext.Products.Add(product);
            await this._productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(int? id) {
            //return await this._productContext.Products.FindAsync(id);

            return await this._productContext.Products.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id); //eager loading

        }

        //public async Task<Product> GetProductCategoryAsync(int? id) {
        //    return  await this._productContext.Products.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == id); //eager loading
        //}

        public async Task<IEnumerable<Product>> GetProductsAsync() {
            return await this._productContext.Products.ToListAsync();
        }

        public async Task<Product> RemoveAsync(Product product) {
            this._productContext.Remove(product);
            await this._productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product) {
            this._productContext.Update(product);
            await this._productContext.SaveChangesAsync();
            return product;
        }
        #endregion
    }
}
