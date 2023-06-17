using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Products.Handlers {
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product> {

        #region Atributos
        private readonly IProductRepository _productRepository;
        #endregion

        #region Construtor
        public ProductUpdateCommandHandler(IProductRepository productRepository) {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        #endregion

        public async Task<Product> Handle(ProductUpdateCommand request, 
                                          CancellationToken cancellationToken) {

            var product = await this._productRepository.GetByIdAsync(request.Id);

            if (product == null) {
                throw new ApplicationException($"Entity could not be found");
            }
            else {
                product.Update(name: request.Name,
                               description: request.Description,
                               price: request.Price,
                               stock: request.Stock,
                               image: request.Image,
                               categoryId: request.CategoryId);

                 return await this._productRepository.UpdateAsync(product);

            }
        }

    }
}
