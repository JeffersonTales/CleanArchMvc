using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services {
    public class CategoryService : ICategoryService {

        #region Atributos
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Construtor
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) {
            this._categoryRepository = categoryRepository;
            this._mapper = mapper;
        }
        #endregion

        #region Métodos do Contrato
        public async Task AddAsync(CategoryDTO categoryDTO) {
            var categoryEntity = this._mapper.Map<Category>(categoryDTO);
            await this._categoryRepository.CreateAsync(categoryEntity);
        }

        public async Task<CategoryDTO> GetByIdAsync(int? id) {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);
            return this._mapper.Map<CategoryDTO>(categoryEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync() {
            var categoriesEntity = await this._categoryRepository.GetCategoriesAsync();
            return this._mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task RemoveAsync(int? id) {
            var categoryEntity = this._categoryRepository.GetByIdAsync(id);
            await _categoryRepository.RemoveAsync(categoryEntity.Result);
        }

        public async Task UpdateAsync(CategoryDTO categoryDTO) {
            var categoryEntity = this._mapper.Map<Category>(categoryDTO);
            await this._categoryRepository.UpdateAsync(categoryEntity);
        }
        #endregion
    }

}
