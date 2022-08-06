namespace Silver.Contracts;

public record CreateBreakfastRequest(
    string  Name,
    string Description,
    DateTimeOffset StartTimestamp,
    DateTimeOffset EndTimestamp,
    List<string> Savory,
    List<string> Sweet
);