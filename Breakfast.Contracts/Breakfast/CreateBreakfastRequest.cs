namespace Breakfast.Contracts.Breakfast;
    public record CreateBreakfastRequest(
        string Name,
        string Description,
        DateTime StartDateTime,
        DateTime EndDateTime,
        List<string> SavouryItems,
        List<string> SweetItems
    );