using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MVC.Features.Players.Commands;
using MVC.Features.Players.Queries;
using MVC.Migrations;
using MVC.Models;
using MVC.Services;
using Newtonsoft.Json;
using System.Runtime.Versioning;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace MVC.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly IMediator _mediator;

        ////sourace: https://www.ezzylearning.net/tutorial/implement-cqrs-pattern-in-asp-net-core-5
        ////Without Mediator
        //private readonly IGetAllPlayersQuery2 getAllPlayersQuery2;

        //public PlayerController(IPlayerService playerService, IGetAllPlayersQuery2 getAllPlayersQuery2)
        //{
        //    this.playerService = playerService;
        //    this.getAllPlayersQuery2 = getAllPlayersQuery2;
        //}

        public PlayerController(IPlayerService playerService, IMediator mediator)
        {
            this.playerService = playerService;
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetPlayer()
        {
            //Call directly from database
            var playerDtos = await playerService.GetPlayers();
            
            //Without Mediator
            var playerDtosMediator = await _mediator.Send(new GetAllPlayersQuery());


            ////Without Mediator
            //var playerDtosWithoutMediator = await getAllPlayersQuery2.Execute();

            //call from API through httpClient
            //using (HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:7268/");

            //    string endPoint = "/GetPlayers";

            //    HttpResponseMessage response = await client.GetAsync(endPoint);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        string result = await response.Content.ReadAsStringAsync();
            //        var players= JsonConvert.DeserializeObject<List<PlayerDto>>(result);
            //    }
            //}


            return View(playerDtos);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerDto playerDto)
        {
            await playerService.CreatePlayer(playerDto);

            await _mediator.Send(new CreatePlayerCommand 
            { 
                Appearances = playerDto.Appearances, 
                Goals = playerDto.Goals, 
                Name = playerDto.Name, 
                ShirtNo = playerDto.ShirtNo 
            });

            return View();
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var playerDto = await playerService.GetPlayer(Id);

            var playerDtoMediator = await _mediator.Send(new GetPlayerByIdQuery() { Id = Id });

            return View(playerDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([FromRoute]int Id, PlayerDto playerDto)
        {
            if (Id != playerDto.Id) 
            {
                return Redirect($"/Player/Edit/{Id}");
            }
            
            await playerService.EditPlayer(Id, playerDto);

            await _mediator.Send(new UpdatePlayerCommand
            {
                Id = Id,
                Appearances = playerDto.Appearances,
                Goals = playerDto.Goals,
                Name = playerDto.Name,
                ShirtNo = playerDto.ShirtNo
            });

            return RedirectToAction("GetPlayer"); 
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            await playerService.DeletePlayer(Id);

            try
            {
                await _mediator.Send(new DeletePlayerCommand() { Id = Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to delete. ");
            }

            return RedirectToAction(nameof(GetPlayer));
        }
    }
}
