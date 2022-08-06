namespace Silver.Contracts;

public record BresakfastResponse(
    Guid Id,
    string Name,
    string Description,
    DateTimeOffset StartTimestamp,
    DateTimeOffset EndTimestamp,
    DateTimeOffset LastModified,
    List<string> Savory,
    List<string> Sweet
);