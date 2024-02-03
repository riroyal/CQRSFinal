using MediatR;
using MVC.Models;
using MVC.Services;

namespace MVC.Features.Players.Queries
{
    public class GetPlayerByIdQuery : IRequest<PlayerDto>
    {
        public int Id { get; set; }

        public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, PlayerDto>
        {
            private readonly IPlayerService _playerService;

            public GetPlayerByIdQueryHandler(IPlayerService playerService)
            {
                _playerService = playerService;
            }

            public async Task<PlayerDto> Handle(GetPlayerByIdQuery query, CancellationToken cancellationToken)
            {
                return await _playerService.GetPlayer(query.Id);
            }
        }
    }
}
