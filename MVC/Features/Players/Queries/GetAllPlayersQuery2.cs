using MVC.Models;
using MVC.Services;

namespace MVC.Features.Players.Queries
{
    public class GetAllPlayersQuery2 : IGetAllPlayersQuery2
    {
        private readonly IPlayerService playerService;

        public GetAllPlayersQuery2(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        public async Task<IEnumerable<PlayerDto>> Execute()
        {
            return await playerService.GetPlayers();
        }
    }
}
