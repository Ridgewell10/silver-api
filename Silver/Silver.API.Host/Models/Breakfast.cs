using ErrorOr;
using Silver.Contracts;
using Silver.API.Host.ServiceError;

namespace Silver.API.Host.Models;

public class Breakfast
{
    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;

    public const int MinDescriptionLength = 50;
    public const int MaxDescriptionLength = 150;

    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public DateTimeOffset StartTimestamp { get; }
    public DateTimeOffset EndTimestamp { get; }
    public DateTimeOffset LastModified { get; }
    public List<string> Savory { get; }
    public List<string> Sweet { get; }

    private Breakfast(
        Guid id,
        string name,
        string description,
        DateTimeOffset startTimestamp,
        DateTimeOffset endTimestamp,
        DateTimeOffset lastModified,
        List<string> savory,
        List<string> sweet
    )
    {
        // enforce variants 
        Id = id;
        Name = name;
        Description = description;
        StartTimestamp = startTimestamp;
        EndTimestamp = endTimestamp;
        LastModified= lastModified;
        Savory = savory;
        Sweet = sweet;
    }

    public static ErrorOr<Breakfast> Create(

        string name,
        string description,
        DateTimeOffset startTimestamp,
        DateTimeOffset endTimestamp,
        List<string> savory,
        List<string> sweet,
        Guid? id = null
    )
    {
        List<Error> errors = new();

        if(name.Length is < MinNameLength or MaxNameLength)
        {
            errors.Add(Errors.Breakfast.InvalidName);
        }
        if(description.Length is < MinDescriptionLength or > MaxDescriptionLength)
        {
            errors.Add(Errors.Breakfast.InvalidDescription);
        }
        if(errors.Count > 0)
        {
            return  errors;
        }

        return new Breakfast(
            id ?? Guid.NewGuid(),
            name,
            description,
            startTimestamp,
            endTimestamp,
            DateTime.UtcNow,
            savory,
            sweet
        );
    }

    public static ErrorOr<Breakfast> From (CreateBreakfastRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.StartTimestamp,
            request.EndTimestamp,
            request.Savory,
            request.Sweet
        );
    }

    public static ErrorOr<Breakfast> From (Guid id, UpsertBreakfastRequest request)
    {
        return Create(
                        request.Name,
            request.Description,
            request.StartTimestamp,
            request.EndTimestamp,
            request.Savory,
            request.Sweet,
            id
        );
    }
}