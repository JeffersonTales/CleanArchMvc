using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers {

    [Authorize]
    public class CategoriesController : Controller {

        #region Atributos
        private readonly ICategoryService _categoryService;
        #endregion

        #region Construtor
        public CategoriesController(ICategoryService categoryService) {
            this._categoryService = categoryService;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Index() {
            var categories = await this._categoryService.GetCategoriesAsync();
            return View(model: categories);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO category) {

            if (this.ModelState.IsValid) {
                await this._categoryService.AddAsync(category);
                return this.RedirectToAction(nameof(Index));
            }
            return View(model: category);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) return this.NotFound();

            var categoryDto = await this._categoryService.GetByIdAsync(id);

            if (categoryDto == null) return this.NotFound();

            return View(model: categoryDto);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO categoryDto) {

            if (this.ModelState.IsValid) {
                try {
                    await this._categoryService.UpdateAsync(categoryDto);
                }
                catch (System.Exception) {

                    throw;
                }
                return this.RedirectToAction(nameof(Index));
            }

            return View(model: categoryDto);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id) {

            if (id == null) return this.NotFound();

            var categoryDto = await this._categoryService.GetByIdAsync(id);

            if (categoryDto == null) return this.NotFound();

            return View(model: categoryDto);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) {

            await this._categoryService.RemoveAsync(id);
            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id) {

            if (id == null) return this.NotFound();

            var categoryDto = await this._categoryService.GetByIdAsync(id);

            if (categoryDto == null) return this.NotFound();

            return View(model: categoryDto);

        }

    }
}
