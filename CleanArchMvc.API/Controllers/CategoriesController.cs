using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {

        #region Atributos
        private readonly ICategoryService _categoryService;
        #endregion

        #region Construtor
        public CategoriesController(ICategoryService categoryService) {
            this._categoryService = categoryService;
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get() {

            var categories = await this._categoryService.GetCategoriesAsync();

            if (categories == null) {
                return this.NotFound("Categories not found");
            }
            return this.Ok(categories);
        }


        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id) {

            var category = await this._categoryService.GetByIdAsync(id);

            if (category == null) {
                return this.NotFound("Category not found");
            }
            return this.Ok(category);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO) {

            if (categoryDTO == null) return this.BadRequest("Data invalid");

            await this._categoryService.AddAsync(categoryDTO);

            return new CreatedAtRouteResult(routeName: "GetCategory",
                                            routeValues: new { id = categoryDTO.Id },
                                            value: categoryDTO);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,
                                            [FromBody] CategoryDTO categoryDTO) {

            if (id != categoryDTO.Id) return BadRequest();

            if (categoryDTO == null) return BadRequest();

            await this._categoryService.UpdateAsync(categoryDTO);

            return this.Ok(categoryDTO);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id) {

            var category = await this._categoryService.GetByIdAsync(id);

            if (category == null) return this.NotFound("Category not found");

            await this._categoryService.RemoveAsync(id);

            return this.Ok(category);
        }

    }

}
