using MediatR;
using Products.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Features.Products.Commands
{
    public class DeleteProductCommandHandler
    : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(
            IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(
            DeleteProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);

            if (product is null)
            {
                return false;
            }

            await _repository.DeleteAsync(product);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}