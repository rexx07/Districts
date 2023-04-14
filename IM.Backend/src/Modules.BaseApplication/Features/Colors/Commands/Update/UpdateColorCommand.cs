using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.Colors.Constants;
using Modules.BaseApplication.Features.Colors.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.Colors.Constants.ColorsOperationClaims;

namespace Modules.BaseApplication.Features.Colors.Commands.Update;

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