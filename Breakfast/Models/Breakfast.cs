using Breakfast.Contracts.Breakfast;
using Breakfast.ServiceErrors;
using ErrorOr;

namespace Breakfast.Models;

public class BallinBreakfast
{
    public const int MinNameLength = 3;
    public const int MaxNameLength = 20;

    public const int MinDescriptionLength = 3;
    public const int MaxDescriptionLength = 100;

    public Guid Id { get; }
    public string Name  { get; }
    public string Description { get; }
    public DateTime StartDateTime { get; }
    public DateTime EndDateTime { get; }
    public DateTime LastModifiedDateTime { get; }
    public List<string> SavouryItems { get; }
    public List<string> SweetItems { get; }

private BallinBreakfast(
    Guid id,
    string name,
    string description,
    DateTime startDateTime,
    DateTime endDateTime,
    DateTime lastModifiedDateTime,
    List<string> savouryItems,
    List<string> sweetItems
)
{
    Id = id;
    Name = name;
    Description = description;
    StartDateTime = startDateTime;
    EndDateTime = endDateTime;
    LastModifiedDateTime = lastModifiedDateTime;
    SavouryItems = savouryItems;
    SweetItems = sweetItems;
}

    public static ErrorOr<BallinBreakfast> Create(
        string name,
        string description,
        DateTime startDateTime,
        DateTime endDateTime,
        List<string> savouryItems,
        List<string> sweetItems,
        Guid? id = null
    ) {
        List<Error> errors = new();

        if (name.Length < MinNameLength || name.Length > MaxNameLength)
        {
           errors.Add(Errors.Breakfast.InvalidName);
        }

        if (description.Length < MinDescriptionLength || description.Length > MaxDescriptionLength)
        {
           errors.Add(Errors.Breakfast.InvalidDescription);
        }

        if(errors.Count > 0)
        {
            return errors;
        }

        return new BallinBreakfast(
        id ?? Guid.NewGuid(),
        name,
        description,
        startDateTime,
        endDateTime,
        DateTime.UtcNow,
        savouryItems,
        sweetItems
    );
    }

    public static ErrorOr<BallinBreakfast> From(CreateBreakfastRequest request) {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.SavouryItems,
            request.SweetItems
        );
    }

    public static ErrorOr<BallinBreakfast> From(Guid id, UpsertBreakfastRequest request) {
        return Create(
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            request.SavouryItems,
            request.SweetItems,
            id
        );
    }
}