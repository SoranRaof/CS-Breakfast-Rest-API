namespace Breakfast.Contracts.Breakfast;
    public record BreakfastResponse(
        Guid Id,
        string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        DateTime LastModifiedDateTime,
        List<string> SavouryItems,
        List<string> SweetItems
    );