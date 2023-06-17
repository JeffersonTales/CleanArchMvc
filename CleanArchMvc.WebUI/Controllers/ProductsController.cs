using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers {
    public class ProductsController : Controller {

        #region Atributos
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;
        #endregion

        #region Construtor
        public ProductsController(IProductService productService,
                                  ICategoryService categoryService,
                                  IWebHostEnvironment environment) {

            this._productService = productService;
            this._categoryService = categoryService;
            this._environment = environment;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Index() {
            var products = await this._productService.GetProductsAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create() {

            this.ViewBag.CategoryId = new SelectList(items: await this._categoryService.GetCategoriesAsync(),
                                                     dataValueField: "Id",
                                                     dataTextField: "Name");

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO) {

            if (this.ModelState.IsValid) {
                await this._productService.AddAsync(productDTO);
                return this.RedirectToAction(nameof(Index));
            }

            return View(model: productDTO);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id) {

            if (id == null) return this.NotFound();

            var productDto = await this._productService.GetByIdAsync(id);

            if (productDto == null) return this.NotFound();

            var categories = await this._categoryService.GetCategoriesAsync();

            this.ViewBag.CategoryId = new SelectList(items: categories,
                                                     dataValueField: "Id",
                                                     dataTextField: "Name",
                                                     selectedValue: productDto.CategoryId);

            return View(model: productDto);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO productDTO) {

            if (this.ModelState.IsValid) {
                await this._productService.UpdateAsync(productDTO);
                return this.RedirectToAction(nameof(Index));
            }

            return View(model: productDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id) {

            if (id == null) return this.NotFound();

            var productDto = await this._productService.GetByIdAsync(id);

            if (productDto == null) return this.NotFound();

            return View(model: productDto);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) {

            await this._productService.RemoveAsync(id);
            return this.RedirectToAction("Index");

        }

        public async Task<IActionResult> Details(int? id) {

            if (id == null) return this.NotFound();

            var productDto = await this._productService.GetByIdAsync(id);

            var wwwroot = _environment.WebRootPath;

            var image = Path.Combine(wwwroot, "images\\" + productDto.Image);

            var exists = System.IO.File.Exists(image);

            this.ViewBag.ImageExist = exists;

            return View(model: productDto); 

        }

    }
}
