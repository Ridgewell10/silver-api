namespace  Silver.Contracts;
public record UpsertBreakfastRequest(
    string Name,
    string Description,
    DateTimeOffset StartTimestamp,
    DateTimeOffset EndTimestamp,
    List<string> Savory,
    List<string> Sweet
);