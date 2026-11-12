namespace GameStore.Api.DTOs;

public record CreateGameDTOs(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
