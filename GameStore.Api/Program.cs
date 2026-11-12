using GameStore.Api.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

const string GetGameEndpointName = "GetGame";

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDTO> games = [
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

// GET /games
app.MapGet("/games", () => games);

// GET /games/$id
app.MapGet("/games/{id}", (int id) => games.Find(g => g.Id == id))
.WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (CreateGameDTOs newGame) =>
{
    GameDTO game = new (
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    return Results.CreatedAtRoute(GetGameEndpointName,new {id = game.Id} ,game);
});

app.Run();
