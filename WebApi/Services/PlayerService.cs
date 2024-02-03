using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly PlayerDbContext playerDbContext;

        public PlayerService(PlayerDbContext playerDbContext)
        {
            this.playerDbContext = playerDbContext;
        }

        public async Task<IEnumerable<PlayerDto>> GetPlayers()
        {
            var player = await playerDbContext.Players.ToListAsync();

            List<PlayerDto> playerDtos = new List<PlayerDto>();

            foreach (var item in player)
            {
                playerDtos.Add(new PlayerDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Appearances = item.Appearances,
                    Goals = item.Goals,
                    ShirtNo = item.ShirtNo,
                });
            };

            return playerDtos;
        }

        public async Task<PlayerDto> EditPlayer(int Id, PlayerDto playerDto)
        {
            var player = await playerDbContext.Players.FirstOrDefaultAsync(x => x.Id == Id);

            player.ShirtNo = playerDto.ShirtNo;
            player.Name = playerDto.Name;
            player.Appearances = playerDto.Appearances;
            player.Goals = playerDto.Goals;

            await playerDbContext.SaveChangesAsync();

            return playerDto;
        }

        public async Task<PlayerDto> CreatePlayer(PlayerDto playerDto)
        {
            var player = new Player() 
            {
                Appearances = playerDto.Appearances,
                Goals = playerDto.Goals,
                Name = playerDto.Name,
                ShirtNo = playerDto.ShirtNo,
            };
            
            await playerDbContext.Players.AddAsync(player);

            await playerDbContext.SaveChangesAsync();

            return playerDto;
        }

        public async Task<PlayerDto> DeletePlayer(int Id)
        {
            var player = await playerDbContext.Players.FirstOrDefaultAsync(x => x.Id == Id);

            var playerDto = new PlayerDto()
            {
                Appearances = player.Appearances,
                Goals = player.Goals,   
                Name = player.Name,
                ShirtNo = player.ShirtNo,   
            };

            playerDbContext.Players.Remove(player);

            await playerDbContext.SaveChangesAsync();   

            return playerDto;
        }
    }
}
