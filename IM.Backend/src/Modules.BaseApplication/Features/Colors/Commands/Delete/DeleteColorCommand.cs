using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.Colors.Constants;
using Modules.BaseApplication.Features.Colors.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.Colors.Constants.ColorsOperationClaims;

namespace Modules.BaseApplication.Features.Colors.Commands.Delete;

public class DeleteColorCommand : IRequest<DeletedColorResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, ColorsOperationClaims.Delete };

    public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand, DeletedColorResponse>
    {
        private readonly ColorBusinessRules _colorBusinessRules;
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public DeleteColorCommandHandler(IColorRepository colorRepository, IMapper mapper,
                                         ColorBusinessRules colorBusinessRules)
        {
            _colorRepository = colorRepository;
            _mapper = mapper;
            _colorBusinessRules = colorBusinessRules;
        }

        public async Task<DeletedColorResponse> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
        {
            await _colorBusinessRules.ColorIdShouldExistWhenSelected(request.Id);
            Color mappedColor = _mapper.Map<Color>(request);
            Color updatedColor = await _colorRepository.DeleteAsync(mappedColor);
            DeletedColorResponse deletedColorDto = _mapper.Map<DeletedColorResponse>(updatedColor);
            return deletedColorDto;
        }
    }
}