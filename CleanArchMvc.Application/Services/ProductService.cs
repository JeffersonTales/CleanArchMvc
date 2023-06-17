using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services {
    public class ProductService : IProductService {

        #region Atributos     
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        #endregion

        #region Construtor
        public ProductService(IMapper mapper, IMediator mediator) {
            this._mapper = mapper;
            this._mediator = mediator;
        }
        #endregion

        #region Métodos do Contrato
        public async Task<IEnumerable<ProductDTO>> GetProductsAsync() {

            var productsQuery = new GetProductsQuery() ?? throw new Exception($"Entity cloud not be loaded.");

            var result = await this._mediator.Send(productsQuery);

            return this._mapper.Map<IEnumerable<ProductDTO>>(result);

        }

        public async Task<ProductDTO> GetByIdAsync(int? id) {

            var productByIdQuery = new GetProductByIdQuery(id.Value) ?? throw new Exception($"Entity cloud not be loaded.");

            var result = await this._mediator.Send(productByIdQuery);

            return this._mapper.Map<ProductDTO>(result);
        }

        //public async Task<ProductDTO> GetProductCategory(int? id) {

        //    var productByIdQuery = new GetProductByIdQuery(id.Value) ?? throw new Exception($"Entity cloud not be loaded.");

        //    var result = await this._mediator.Send(productByIdQuery);

        //    return this._mapper.Map<ProductDTO>(result);
        //}

        public async Task AddAsync(ProductDTO productDTO) {

            var productCreateCommand = this._mapper.Map<ProductCreateCommand>(productDTO);
            await this._mediator.Send(productCreateCommand);

        }

        public async Task UpdateAsync(ProductDTO productDTO) {

            var productUpdateCommand = this._mapper.Map<ProductUpdateCommand>(productDTO);
            await this._mediator.Send(productUpdateCommand);
        }

        public async Task RemoveAsync(int? id) {

            var productRemoveCommand = new ProductRemoveCommand(id.Value) ?? throw new Exception($"Entity cloud not be loaded.");

            await this._mediator.Send(productRemoveCommand);

        }
        #endregion

    }
}
