using MediatR;
using Products.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Features.Products.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductResponseDTO?>;
}
