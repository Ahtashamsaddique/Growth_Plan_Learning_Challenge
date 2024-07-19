using Appplication.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appplication.Features.Product.Commands
{
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
        {
            private readonly IApplicationDbContext _applicationDbContext;

            public UpdateProductCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _applicationDbContext.Products.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (product != null)
                {
                    product.Name = request.Name;
                    product.Description = request.Description;
                    product.Rate = request.Rate;
                    await _applicationDbContext.SaveChangesAsyncs();
                    return product.Id;
                }
                return default;

            }
        }
    }
}
