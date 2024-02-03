using MVC.Models;

namespace MVC.Features.Players.Queries
{
    public interface IGetAllPlayersQuery2
    {
        Task<IEnumerable<PlayerDto>> Execute();
    }
}