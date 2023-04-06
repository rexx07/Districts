﻿using Application.Features.Colors.Rules;
using Application.Pipelines.Authorization;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.Colors.Constants.ColorsOperationClaims;

namespace Application.Features.Colors.Commands.Create;

public class CreateColorCommand : IRequest<CreatedColorResponse>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, CreatedColorResponse>
    {
        private readonly ColorBusinessRules _colorBusinessRules;
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public CreateColorCommandHandler(IColorRepository colorRepository, IMapper mapper,
                                         ColorBusinessRules colorBusinessRules)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
            _colorBusinessRules = colorBusinessRules;
        }

        public async Task<CreatedColorResponse> Handle(CreateColorCommand request, CancellationToken cancellationToken)
        {
            await _colorBusinessRules.ColorNameCanNotBeDuplicatedWhenInserted(request.Name);

            Color mappedColor = _mapper.Map<Color>(request);
            Color createdColor = await _colorRepository.AddAsync(mappedColor);
            CreatedColorResponse createdColorDto = _mapper.Map<CreatedColorResponse>(createdColor);
            return createdColorDto;
        }
    }
}