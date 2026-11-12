namespace GameStore.Api;

public record UpdateGameDTOs(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
