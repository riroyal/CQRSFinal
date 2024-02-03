using MediatR;
using MVC.Models;
using MVC.Services;

namespace MVC.Features.Players.Commands
{
    public class CreatePlayerCommand : IRequest<PlayerDto>
    {
        public int? ShirtNo { get; set; }
        public string Name { get; set; }
        public int? Appearances { get; set; }
        public int? Goals { get; set; }

        public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, PlayerDto>
        {
            private readonly IPlayerService _playerService;

            public CreatePlayerCommandHandler(IPlayerService playerService)
            {
                _playerService = playerService;
            }

            public async Task<PlayerDto> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
            {
                var playerDto = new PlayerDto()
                {
                    ShirtNo = command.ShirtNo,
                    Name = command.Name,
                    Appearances = command.Appearances,
                    Goals = command.Goals
                };

                return await _playerService.CreatePlayer(playerDto);
            }
        }
    }
}
