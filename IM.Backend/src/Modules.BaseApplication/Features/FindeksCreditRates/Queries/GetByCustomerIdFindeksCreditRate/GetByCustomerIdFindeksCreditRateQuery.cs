﻿using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.FindeksCreditRates.Rules;

namespace Modules.BaseApplication.Features.FindeksCreditRates.Queries.GetByCustomerIdFindeksCreditRate;

public class GetByCustomerIdFindeksCreditRateQuery : IRequest<GetByCustomerIdFindeksCreditRateResponse>
{
    public int CustomerId { get; set; }

    public class GetByIdFindeksCreditRateQueryHandler
        : IRequestHandler<GetByCustomerIdFindeksCreditRateQuery, GetByCustomerIdFindeksCreditRateResponse>
    {
        private readonly FindeksCreditRateBusinessRules _findeksCreditRateBusinessRules;
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IMapper _mapper;

        public GetByIdFindeksCreditRateQueryHandler(
            IFindeksCreditRateRepository findeksCreditRateRepository,
            FindeksCreditRateBusinessRules findeksCreditRateBusinessRules,
            IMapper mapper
        )
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _findeksCreditRateBusinessRules = findeksCreditRateBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByCustomerIdFindeksCreditRateResponse> Handle(
            GetByCustomerIdFindeksCreditRateQuery request,
            CancellationToken cancellationToken
        )
        {
            FindeksCreditRate? findeksCreditRate =
                await _findeksCreditRateRepository.GetAsync(b => b.CustomerId == request.CustomerId);
            await _findeksCreditRateBusinessRules.FindeksCreditShouldBeExist(findeksCreditRate);

            GetByCustomerIdFindeksCreditRateResponse? findeksCreditRateDto =
                _mapper.Map<GetByCustomerIdFindeksCreditRateResponse>(
                    findeksCreditRate
                );
            return findeksCreditRateDto;
        }
    }
}