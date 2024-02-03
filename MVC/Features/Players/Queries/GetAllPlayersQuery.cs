using MediatR;
using MVC.Models;
using MVC.Services;

namespace MVC.Features.Players.Queries
{
    public class GetAllPlayersQuery : IRequest<IEnumerable<PlayerDto>>
    {
        public class GetAllPlayersQueryHandler : IRequestHandler<GetAllPlayersQuery, IEnumerable<PlayerDto>>
        {
            private readonly IPlayerService _playerService;

            public GetAllPlayersQueryHandler(IPlayerService playerService)
            {
                _playerService = playerService;
            }

            public async Task<IEnumerable<PlayerDto>> Handle(GetAllPlayersQuery query, CancellationToken cancellationToken)
            {
                return await _playerService.GetPlayers();
            }
        }
    }
}
