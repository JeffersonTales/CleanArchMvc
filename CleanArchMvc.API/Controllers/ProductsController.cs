using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase {

        #region Atributos
        private readonly IProductService _productService;
        #endregion

        #region Construtor
        public ProductsController(IProductService productService) {
            this._productService = productService;
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get() {

            var products = await this._productService.GetProductsAsync();

            if (products == null) {
                return this.NotFound("Products not found");
            }
            return this.Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id) {

            var product = await this._productService.GetByIdAsync(id);

            if (product == null) {
                return this.NotFound("Product not found");
            }
            return this.Ok(product);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO) {

            if (productDTO == null) return this.BadRequest("Data invalid");

            await this._productService.AddAsync(productDTO);

            return new CreatedAtRouteResult(routeName: "GetProduct",
                                            routeValues: new { id = productDTO.Id },
                                            value: productDTO);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,
                                            [FromBody] ProductDTO productDTO) {

            if (id != productDTO.Id) return BadRequest("Data invalid");

            if (productDTO == null) return BadRequest("Data invalid");

            await this._productService.UpdateAsync(productDTO);

            return this.Ok(productDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id) {

            var productDTO = await this._productService.GetByIdAsync(id);

            if (productDTO == null) return this.NotFound("Product not found");

            await this._productService.RemoveAsync(id);

            return this.Ok(productDTO);
        }

    }
}
