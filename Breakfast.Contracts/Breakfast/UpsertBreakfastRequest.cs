namespace Breakfast.Contracts.Breakfast;
    public record UpsertBreakfastRequest(
        string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        List<string> SavouryItems,
        List<string> SweetItems
    );