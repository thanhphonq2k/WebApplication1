using Microsoft.AspNetCore.Mvc;
using WebApplication1.Application.Dtos;
using WebApplication1.Application.Interfaces.IUseCases;

namespace WebApplication1.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PositionController : ControllerBase
{
    private readonly IPositionUseCase _positionUseCase;

    public PositionController(IPositionUseCase positionUseCase)
    {
        _positionUseCase = positionUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _positionUseCase.GetAllAsync(cancellationToken);
        return result.IsSuccess
            ? new JsonResult(result.Data)
            : BadRequest(result.ErrorMessage);
    }

    [HttpPost]
    public async Task<IActionResult> Post(PositionDto position, CancellationToken cancellationToken)
    {
        var result = await _positionUseCase.CreateAsync(position, cancellationToken);
        return result.IsSuccess
            ? new JsonResult(result.Data)
            : BadRequest(result.ErrorMessage);
    }

    [HttpPut]
    public async Task<IActionResult> Put(PositionDto position, CancellationToken cancellationToken)
    {
        var result = await _positionUseCase.UpdateAsync(position, cancellationToken);
        return result.IsSuccess
            ? new JsonResult(result.Data)
            : BadRequest(result.ErrorMessage);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _positionUseCase.DeleteAsync(id, cancellationToken);
        return result.IsSuccess
            ? new JsonResult(result.Data)
            : BadRequest(result.ErrorMessage);
    }
}
