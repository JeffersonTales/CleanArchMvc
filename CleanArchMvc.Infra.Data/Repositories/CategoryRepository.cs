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

    public class CategoryRepository : ICategoryRepository {

        #region Atributos
        ApplicationDbContext _contextCategory;
        #endregion

        #region Construtor
        public CategoryRepository(ApplicationDbContext context) { //injeta o contexto no construtor
            this._contextCategory = context;
        }
        #endregion


        #region Métodos do Contrato
        public async Task<Category> CreateAsync(Category category) {
            this._contextCategory.Add(category);
            await this._contextCategory.SaveChangesAsync();
            return category;
        }

        public async Task<Category> GetByIdAsync(int? id) {
            return await this._contextCategory.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync() {
            return await this._contextCategory.Categories.ToListAsync();
        }

        public async Task<Category> RemoveAsync(Category category) {
            this._contextCategory.Remove(category);
            await this._contextCategory.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(Category category) {
            this._contextCategory.Update(category);
            await this._contextCategory.SaveChangesAsync(); 
            return category;    
        }
        #endregion
    }
}
