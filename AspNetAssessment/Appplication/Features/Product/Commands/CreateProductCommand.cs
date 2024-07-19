using Appplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appplication.Features.Product.Commands
{
    public class CreateProductCommand:IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public CreateProductCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var product = new  Domain.Entities.Product();
                product.Name = request.Name;
                product.Description = request.Description;
                product.Rate = request.Rate;
                await _applicationDbContext.Products.AddAsync(product,cancellationToken);
                await _applicationDbContext.SaveChangesAsyncs();
                return product.Id;

            }
        }
    }
}
