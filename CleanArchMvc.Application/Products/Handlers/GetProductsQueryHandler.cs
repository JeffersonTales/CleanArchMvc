﻿using CleanArchMvc.Application.Products.Queries;
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

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>> {

        #region Atributos
        private readonly IProductRepository _productRepository;

        #endregion

        #region Construtor
        public GetProductsQueryHandler(IProductRepository productRepository) {
            this._productRepository = productRepository;
        }

        #endregion
        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, 
                                                       CancellationToken cancellationToken) {

            return await this._productRepository.GetProductsAsync();
        }

    }
}
