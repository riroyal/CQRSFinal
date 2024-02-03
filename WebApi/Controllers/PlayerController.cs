using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApi.Models;
using WebApi.Services;
using static System.Net.Mime.MediaTypeNames;

namespace MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService playerService;
        private readonly IMemoryCache _memoryCache;

        public PlayerController(IPlayerService playerService, IMemoryCache memoryCache)
        {
            this.playerService = playerService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("/GetPlayers")]
        public async Task<IActionResult> GetPlayers()
        {
            var test = _memoryCache.Get<List<PlayerDto>>("Entry");

            IEnumerable<PlayerDto> playerDtos = new List<PlayerDto>();

            if (test == null)
            {
                playerDtos = await playerService.GetPlayers();

                var options = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(15));

                _memoryCache.Set("Entry", playerDtos, options);
            }
            return Ok(test == null ? playerDtos : test);
        }

        [HttpPut]
        [Route("/EditPlayer")]
        public async Task<IActionResult> EditPlayer([FromQuery]int Id, [FromBody] PlayerDto playerRequest)
        {
            if (Id != playerRequest.Id)
            {
                return BadRequest();
            }

            var playerDto = await playerService.EditPlayer(Id, playerRequest);

            if (playerDto == null)
            {
                return NotFound();
            }

            return Ok(playerDto);
        }

        [HttpPost]
        [Route("/SavePlayers")]
        public async Task<IActionResult> SavePlayer([FromBody]PlayerDto playerRequest)
        {
            var playerDto = await playerService.CreatePlayer(playerRequest);

            return Ok(playerDto);
        }

        [HttpDelete]
        [Route("/DeletePlayer")]
        public async Task<IActionResult> DeletePlayer([FromQuery] int Id)
        {
            var playerDto = await playerService.DeletePlayer(Id);

            if (playerDto == null)
            {
                return NotFound();
            }

            return Ok(playerDto);
        }
    }
}
