using MediatR;
using MVC.Models;
using MVC.Services;

namespace MVC.Features.Players.Commands
{
    public class UpdatePlayerCommand : IRequest<PlayerDto>
    {
        public int Id { get; set; }
        public int? ShirtNo { get; set; }
        public string Name { get; set; }
        public int? Appearances { get; set; }
        public int? Goals { get; set; }

        public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand, PlayerDto>
        {
            private readonly IPlayerService _playerService;

            public UpdatePlayerCommandHandler(IPlayerService playerService)
            {
                _playerService = playerService;
            }

            public async Task<PlayerDto> Handle(UpdatePlayerCommand command, CancellationToken cancellationToken)
            {
                //var player = await _playerService.GetPlayerById(command.Id);
                //if (player == null)
                //    return default;

                //player.ShirtNo = command.ShirtNo;
                //player.Name = command.Name;
                //player.Appearances = command.Appearances;
                //player.Goals = command.Goals;

                var playerDto = new PlayerDto()
                {
                    ShirtNo = command.ShirtNo,
                    Name = command.Name,
                    Appearances = command.Appearances,
                    Goals = command.Goals
                };

                return await _playerService.EditPlayer(command.Id, playerDto);
            }
        }
    }
}
