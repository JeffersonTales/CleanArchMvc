using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace CleanArchMvc.Application.Products.Handlers {
    public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product> {

        #region Atributos
        private readonly IProductRepository _productRepository;
        #endregion

        #region Construtor
        public ProductRemoveCommandHandler(IProductRepository productRepository) {

            this._productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }
        #endregion

        public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken) {

            var product = await this._productRepository.GetByIdAsync(request.Id);

            if (product == null) {
                throw new ApplicationException($"Entity could not be found");
            }
            else {

                return await this._productRepository.RemoveAsync(product);
                
            }

        }

    }
}
