using Appplication.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appplication.Features.Product.Queries
{
    public class GetAllPorductsQuery : IRequest<IEnumerable<Domain.Entities.Product>>
    {
        internal class GetAllPorductsQueryHandler : IRequestHandler<GetAllPorductsQuery, IEnumerable<Domain.Entities.Product>>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public GetAllPorductsQueryHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<IEnumerable<Domain.Entities.Product>> Handle(GetAllPorductsQuery request, CancellationToken cancellationToken)
            {
                var result= await _applicationDbContext.Products.ToListAsync(cancellationToken);
                return result;
            }
        }
    }
    
}
