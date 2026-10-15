using MediatR;
using Products.Application.DTOs;
using Products.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Features.Products.Queries
{
    public class GetProductsQueryHandler
    : IRequestHandler<GetProductsQuery, List<ProductResponseDTO>>
    {
        private readonly IProductRepository _repository;

        public GetProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductResponseDTO>> Handle(
            GetProductsQuery request,
            CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();

            return products.Select(product => new ProductResponseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            }).ToList();
        }
    }
}

