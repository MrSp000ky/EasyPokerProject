using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPoker_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyPoker_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly EasyPokerContext easyPokerContext;
        private readonly PokerServerService pokerServerService;

        public GameController(EasyPokerContext context, PokerServerService pokerService)
        {
            easyPokerContext = context;
            pokerServerService = pokerService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartGame([FromBody] List<string> playerNames)
        {
            if (playerNames.Count != 4) return BadRequest("Exactly 4 players are required.");

            var game = pokerServerService.CreateGame(playerNames);
            easyPokerContext.Games.Add(game);
            await easyPokerContext.SaveChangesAsync();

            return Ok(new
            {
                game.Id,
                game.Timestamp,
                game.Winner,
                Players = game.Players.Select(p => new { p.PlayerName }),
                Hands = game.PlayerHands.Select(h => new { h.Player!.PlayerName, h.Card1, h.Card2, h.Card3, h.Card4, h.Card5 }),
                RemainingCards = game.RemainingCards.Select(r => r.Card)
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame(int id)
        {
            var game = await easyPokerContext.Games
                .Include(g => g.Players)
                .Include(g => g.PlayerHands)
                .Include(g => g.RemainingCards)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null) return NotFound();

            return Ok(new
            {
                game.Id,
                game.Timestamp,
                game.Winner,
                Players = game.Players.Select(p => new { p.PlayerName }),
                Hands = game.PlayerHands.Select(h => new { h.Player!.PlayerName, h.Card1, h.Card2, h.Card3, h.Card4, h.Card5 }),
                RemainingCards = game.RemainingCards.Select(r => r.Card)
            });
        }

    }
}