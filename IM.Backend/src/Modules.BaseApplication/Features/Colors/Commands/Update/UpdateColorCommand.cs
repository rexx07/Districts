using Application.Features.Colors.Constants;
using Application.Features.Colors.Rules;
using Application.Pipelines.Authorization;
using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Entities.Land;
using MediatR;
using static Application.Features.Colors.Constants.ColorsOperationClaims;

namespace Application.Features.Colors.Commands.Update;

public class UpdateColorCommand : IRequest<UpdatedColorResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, ColorsOperationClaims.Update };

    public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommand, UpdatedColorResponse>
    {
        private readonly ColorBusinessRules _colorBusinessRules;

        public UpdateColorCommandHandler(IColorRepository colorRepository, IMapper mapper,
                                         ColorBusinessRules colorBusinessRules)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
            _colorBusinessRules = colorBusinessRules;
        }

        private IColorRepository _colorRepository { get; }
        private IMapper _mapper { get; }

        public async Task<UpdatedColorResponse> Handle(UpdateColorCommand request, CancellationToken cancellationToken)
        {
            await _colorBusinessRules.ColorIdShouldExistWhenSelected(request.Id);
            Color mappedColor = _mapper.Map<Color>(request);
            Color updatedColor = await _colorRepository.UpdateAsync(mappedColor);
            UpdatedColorResponse updatedColorDto = _mapper.Map<UpdatedColorResponse>(updatedColor);
            return updatedColorDto;
        }
    }
}