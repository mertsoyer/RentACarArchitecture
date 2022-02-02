using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand:IRequest<Brand>
    {
        public string Name { get; set; }

        public class CreateBrandCommandHandler:IRequestHandler<CreateBrandCommand,Brand>
        {
            IBrandRepository _brandrepository;
            IMapper _mapper;
            BrandBusinessRules _brandBusinessRules;

            public CreateBrandCommandHandler(IBrandRepository brandrepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
            {
                _brandrepository = brandrepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<Brand> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);
                var mappedBrand =_mapper.Map<Brand>(request);
                //Brand brand = new Brand();
                //brand.Name = request.Name;
                var createdBrand=await _brandrepository.AddAsync(mappedBrand);
                return createdBrand;
            }
        }


    }
}
