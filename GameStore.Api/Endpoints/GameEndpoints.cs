using System;
using GameStore.Api.DTOs;
using Microsoft.VisualBasic;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static readonly List<GameDTO> games = [
        new (
            0,
            "Echoes",
            "MMORPG",
            99.99M,
            new DateOnly(2026, 11, 7)
        ),
        new (
            1,
            "Tindahan",
            "Horror",
            1.95M,
            new DateOnly(2026, 05, 1)
        ),
    ];

    public static void MapGameEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // GET /games
        group.MapGet("/", () => games);

        // GET /games/{id}
        group.MapGet("/{id}", (int id) => {
            var game = games.Find(g => g.Id == id);
            
            return game is null? Results.NotFound() : Results.Ok(game);
            })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDTOs newGame) =>
        {
            var newId = games[games.Count-1].Id + 1;

            GameDTO game = new (
                newId,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName,new {id = game.Id} ,game);
        });

        // PUT /games/{id}
        group.MapPut("/{id}", (int id, UpdateGameDTOs updatedGame) =>
        {
            var index = games.FindIndex(g => g.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }
            games[index] = new (
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // DELETE /games/{id}

        group.MapDelete("/{id}",(int id) =>
        {
            games.RemoveAll(g => g.Id == id);

            return Results.NoContent();
        });

    }

}