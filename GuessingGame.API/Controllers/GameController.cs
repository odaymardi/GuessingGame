namespace GuessingGame.API.Controllers
{
    using GuessingGame.API.Models;
    using GuessingGame.API.Services;
    using GuessingGame.API.Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("bet")]
        public IActionResult PlaceBet([FromBody] BetRequest request)
        {
            if (!HttpContext.Items.TryGetValue("PlayerId", out var playerIdObj) || playerIdObj is not Guid playerId)
                return BadRequest(new { error = "PlayerId not found in context." });

            try
            {
                var result = _gameService.PlaceBet(playerId, request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InsufficientFundsException ex)
            {
                return StatusCode(409, new { error = ex.Message });
            }
        }
    }
}
