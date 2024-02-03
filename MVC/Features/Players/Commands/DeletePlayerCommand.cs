using MediatR;
using MVC.Services;

namespace MVC.Features.Players.Commands
{
    public class DeletePlayerCommand : IRequest<int>
    {
        public int Id { get; set; }

        public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand, int>
        {
            private readonly IPlayerService _playerService;

            public DeletePlayerCommandHandler(IPlayerService playerService)
            {
                _playerService = playerService;
            }

            public async Task<int> Handle(DeletePlayerCommand command, CancellationToken cancellationToken)
            {
                return await _playerService.DeletePlayer(command.Id);
            }
        }
    }
}
