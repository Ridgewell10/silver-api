using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using Silver.Contracts;
using Silver.API.Host.Services;
using Silver.API.Host.Models;

namespace Silver.API.Host.Controllers;


public class BreakfastsController : ApiController
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreafast(CreateBreakfastRequest request)
    {
        ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(request);

        if(requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }

        var breakfast = requestToBreakfastResult.Value;
        ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);

        return createBreakfastResult.Match(
            created => CreatedAtGetBreakfast(breakfast),
            errors => Problem(errors));      
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

        return getBreakfastResult.Match(
            breakfast => Ok(MapBreakfastResponse(breakfast)),
            errors => Problem(errors)
        );

    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        ErrorOr<Breakfast> requestToBreakfastResult = Breakfast.From(id, request);

        if(requestToBreakfastResult.IsError)
        {
            return Problem(requestToBreakfastResult.Errors);
        }
        
        var breakfast = requestToBreakfastResult.Value;
        ErrorOr<UpsertedBreakfast> upsertBreakfastResult = _breakfastService.UpsertBreakfast(breakfast);
        
        return upsertBreakfastResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast (Guid id)
    {
        ErrorOr<Deleted> deleteBreakfastResult = _breakfastService.DeleteBreakfast(id);

        return deleteBreakfastResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }

    private static BresakfastResponse MapBreakfastResponse(Breakfast breakfast)
    {
        return new BresakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartTimestamp,
            breakfast.EndTimestamp,
            breakfast.LastModified,
            breakfast.Savory,
            breakfast.Sweet
        );
    }

    private CreatedAtActionResult CreatedAtGetBreakfast (Breakfast breakfast)
    {
        return CreatedAtAction(
            actionName: nameof(GetBreakfast),
            routeValues: new {id = breakfast.Id },
            value: MapBreakfastResponse(breakfast)
        );
    }

}