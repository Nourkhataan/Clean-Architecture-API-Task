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
    public class GetProductByIdQueryHandler
     : IRequestHandler<GetProductByIdQuery, ProductResponseDTO?>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductResponseDTO?> Handle(
            GetProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);

            if (product is null)
            {
                return null;
            }

            return new ProductResponseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
