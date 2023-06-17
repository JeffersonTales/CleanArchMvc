using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Products.Handlers {
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product> {

        #region Atributos
        private readonly IProductRepository _productRepository;

        #endregion

        #region Construtor
        public GetProductByIdQueryHandler(IProductRepository productRepository) {
            this._productRepository = productRepository;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, 
                                          CancellationToken cancellationToken) {

           return await this._productRepository.GetByIdAsync(request.Id);   
        }
        #endregion


    }
}
