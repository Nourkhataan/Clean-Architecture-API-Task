
using MediatR;
using Products.Application.DTOs;
namespace Products.Application.Features.Products.Commands
{
    public record CreateProductCommand(
    string Name,
    decimal Price
) : IRequest<ProductResponseDTO>;
}
