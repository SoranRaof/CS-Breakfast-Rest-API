using Breakfast.Contracts.Breakfast;
using Breakfast.Models;
using Breakfast.ServiceErrors;
using Breakfast.Services;
using Breakfast.Services.Breakfast;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Breakfast.Controller;

public class BreakfastController : ApiController
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        ErrorOr<BallinBreakfast> requestToBreakfastResult = BallinBreakfast.From(request);

        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfast = requestToBreakfastResult.Value;

        ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);

        //TODO: save data to db
        if (createBreakfastResult.IsError)
        {
            return Problem(createBreakfastResult.Errors);
        }

        return createBreakfastResult.Match(
            created => CreatedAtGetBreakfast(breakfast),
            Problem
            );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfastById(Guid id)
    {
        ErrorOr<BallinBreakfast> getBreakfastResult = _breakfastService.GetBreakfastById(id);

        return getBreakfastResult.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            Problem);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        ErrorOr<BallinBreakfast> requestToBreakfastResult = BallinBreakfast.From(id, request);

        if (requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfast = requestToBreakfastResult.Value;

        ErrorOr<UpsertedBreakfast> upsertBreakfastResult = _breakfastService.UpsertBreakfast(breakfast);

         return upsertBreakfastResult.Match(
            upserted =>  upserted.isNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(),
            Problem
         );
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfastById(Guid id)
    {
        ErrorOr<Deleted> deleteBreakfastResult = _breakfastService.DeleteBreakfastById(id);

        return deleteBreakfastResult.Match(
                deleted => NoContent(),
                Problem
        );
    }

        private static BreakfastResponse MapBreakfastResponse(BallinBreakfast breakfast)
    {
        return new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.SavouryItems,
            breakfast.SweetItems
        );
    }

    private CreatedAtActionResult CreatedAtGetBreakfast(BallinBreakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfastById),
            routeValues: new { id = breakfast.Id },
            value: MapBreakfastResponse(breakfast)
        );
}
}