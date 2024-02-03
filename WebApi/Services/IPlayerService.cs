using WebApi.Models;

namespace WebApi.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDto>> GetPlayers();
        Task<PlayerDto> EditPlayer(int Id, PlayerDto playerReques);
        Task<PlayerDto> CreatePlayer(PlayerDto playerRequest);
        Task<PlayerDto> DeletePlayer(int Id);
    }
}