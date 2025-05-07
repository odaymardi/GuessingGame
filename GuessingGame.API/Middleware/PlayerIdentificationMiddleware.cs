namespace GuessingGame.API.Middleware
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Threading.Tasks;

    public class PlayerIdentificationMiddleware
    {
        private readonly RequestDelegate _next;

        public PlayerIdentificationMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("Player-Id", out var playerIdStr) ||
                !Guid.TryParse(playerIdStr, out var playerId))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { error = "Missing or invalid Player-Id header." });
                return;
            }

            context.Items["PlayerId"] = playerId;
            await _next(context);
        }
    }
}
