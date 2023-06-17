using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Products.Handlers {

    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product> {

        #region Atributos
        private readonly IProductRepository _productRepository;
        #endregion

        #region Construtor
        public ProductCreateCommandHandler(IProductRepository productRepository) {
            this._productRepository = productRepository;
        }
        #endregion

        public async Task<Product> Handle(ProductCreateCommand request,
                                          CancellationToken cancellationToken) {

            var product = new Product(name: request.Name,
                                      description: request.Description,
                                      price: request.Price,
                                      stock: request.Stock,
                                      image: request.Image);

            if (product == null) {
                throw new ApplicationException($"Error creating entity");
            }
            else {
                product.CategoryId = request.CategoryId;    
                return await _productRepository.CreateAsync(product);
            }

        }

    }
}
