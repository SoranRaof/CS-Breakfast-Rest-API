using Breakfast.Models;
using Breakfast.Services.Breakfast;
using ErrorOr;

namespace Breakfast.Services;

public interface IBreakfastService
{
    ErrorOr<Created> CreateBreakfast(BallinBreakfast breakfast);

    ErrorOr<Deleted> DeleteBreakfastById(Guid id);

    ErrorOr<BallinBreakfast> GetBreakfastById(Guid id);
    
    ErrorOr<UpsertedBreakfast> UpsertBreakfast(BallinBreakfast breakfast);
}