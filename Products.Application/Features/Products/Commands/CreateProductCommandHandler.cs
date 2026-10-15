using MediatR;
using Products.Application.DTOs;
using Products.Application.Interfaces;
using Products.Domain.Entities;

namespace Products.Application.Features.Products.Commands
{
    public class CreateProductCommandHandler: IRequestHandler<CreateProductCommand, ProductResponseDTO>
        {
            private readonly IProductRepository _repository;

            public CreateProductCommandHandler(IProductRepository repository)
            {
                _repository = repository;
            }

            public async Task<ProductResponseDTO> Handle(
                CreateProductCommand request,
                CancellationToken cancellationToken)
            {
                var product = new Product
                {
                    Name = request.Name,
                    Price = request.Price
                };

                await _repository.AddAsync(product);
                await _repository.SaveChangesAsync();

                return new ProductResponseDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
                };
            }
        }
    }

