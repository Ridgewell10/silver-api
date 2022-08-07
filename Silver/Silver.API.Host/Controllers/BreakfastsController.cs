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

    }

}