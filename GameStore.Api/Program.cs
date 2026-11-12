using GameStore.Api.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<GameDTO> games = [
    new (
        1,
        "Echoes",
        "MMORPG",
        99.99M,
        new DateOnly(2026, 11, 7)
    ),
    new (
        2,
        "Tindahan",
        "Horror",
        1.95M,
        new DateOnly(2026, 05, 1)
    ),
];

// GET /games
app.MapGet("/games", () => games);

// GET /games/$id
app.MapGet("games/{id}", (int id) =>
{
    return games.Find(g => g.Id == id);
});

app.Run();
