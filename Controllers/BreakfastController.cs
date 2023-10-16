using BuberBreakfast.Models;
using BuberBreakfast.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

[ApiController]
[Route("breakfasts")]
public class BreakfastController : Controller
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpGet]
    public ActionResult Get([FromQuery] SearchBreakfastDto searchBreakfastDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var instances = _breakfastService.GetMany(searchBreakfastDto);
        List<ResponseBreakfastDto> result = new List<ResponseBreakfastDto>();

        foreach (var instance in instances)
        {
            result.Add(new ResponseBreakfastDto(instance));
        }

        return Ok(result);
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateBreakfastDto createBreakfastDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var instance = _breakfastService.CreateUnique(createBreakfastDto);
        if (instance is null)
            return Conflict();
        return Ok(new ResponseBreakfastDto(instance));
    }

    [HttpGet("{id}")]
    public ActionResult Get(int id)
    {
        var instance = _breakfastService.GetUnique(id);
        if (instance is null)
            return NotFound();
        return Ok(new ResponseBreakfastDto(instance));
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] UpdateBreakfastDto updateBreakfastDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var instance = _breakfastService.UpdateUnique(id, updateBreakfastDto);
        if (instance is null)
            return NotFound();
        return Ok(new ResponseBreakfastDto(instance));
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var result = _breakfastService.DeleteUnique(id);
        if (result is false)
            return NotFound();
        return Ok();
    }
}
