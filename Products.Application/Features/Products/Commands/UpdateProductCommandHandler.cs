using MediatR;
using Products.Application.DTOs;
using Products.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Features.Products.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponseDTO?>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(
            IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductResponseDTO?> Handle(
            UpdateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);

            if (product is null)
            {
                return null;
            }

            product.Name = request.Name;
            product.Price = request.Price;

            await _repository.UpdateAsync(product);
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
