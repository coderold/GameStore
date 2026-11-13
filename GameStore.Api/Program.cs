using GameStore.Api;
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

// GET /games/{id}
app.MapGet("/games/{id}", (int id) => games.Find(g => g.Id == id))
.WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (CreateGameDTOs newGame) =>
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
app.MapPut("games/{id}", (int id, UpdateGameDTOs updatedGame) =>
{
    var index = games.FindIndex(g => g.Id == id);

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

app.MapDelete("/games/{id}",(int id) =>
{
    games.RemoveAll(g => g.Id == id);

    return Results.NoContent();
});


app.Run();
