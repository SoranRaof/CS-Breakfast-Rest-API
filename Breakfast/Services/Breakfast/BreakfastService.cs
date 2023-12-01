using Breakfast.Models;
using Breakfast.ServiceErrors;
using Breakfast.Services.Breakfast;
using ErrorOr;

namespace Breakfast.Services;

public class BreakfastService : IBreakfastService
{

    private static readonly Dictionary<Guid, BallinBreakfast> _breakfasts = new();
    
    public ErrorOr<Created> CreateBreakfast(BallinBreakfast breakfast)
    {
        _breakfasts.Add(breakfast.Id, breakfast);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfastById(Guid id)
    {
        _breakfasts.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<BallinBreakfast> GetBreakfastById(Guid id)
    {
        if (_breakfasts.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }
        else
        {
            return Errors.Breakfast.NotFound;
        }
    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(BallinBreakfast breakfast)
    {
        var isNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);

        _breakfasts[breakfast.Id] = breakfast;

        return new UpsertedBreakfast(isNewlyCreated);
    }
}