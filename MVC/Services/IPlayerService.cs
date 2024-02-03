using MVC.Models;

namespace MVC.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDto>> GetPlayers();
        Task<PlayerDto> GetPlayer(int Id);
        Task<PlayerDto> EditPlayer(int Id, PlayerDto playerDto);
        Task<PlayerDto> CreatePlayer(PlayerDto playerRequest);
        Task<int> DeletePlayer(int Id);

    }
}