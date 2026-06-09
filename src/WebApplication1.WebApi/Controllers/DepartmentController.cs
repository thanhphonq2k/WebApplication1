using Microsoft.AspNetCore.Mvc;
using WebApplication1.Application.Dtos;
using WebApplication1.Application.Interfaces.IUseCases;

namespace WebApplication1.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentUseCase _departmentUseCase;

    public DepartmentController(IDepartmentUseCase departmentUseCase)
    {
        _departmentUseCase = departmentUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _departmentUseCase.GetAllAsync(cancellationToken);
        return result.IsSuccess
            ? new JsonResult(result.Data)
            : BadRequest(result.ErrorMessage);
    }

    [HttpPost]
    public async Task<IActionResult> Post(DepartmentDto department, CancellationToken cancellationToken)
    {
        var result = await _departmentUseCase.CreateAsync(department, cancellationToken);
        return result.IsSuccess
            ? new JsonResult(result.Data)
            : BadRequest(result.ErrorMessage);
    }

    [HttpPut]
    public async Task<IActionResult> Put(DepartmentDto department, CancellationToken cancellationToken)
    {
        var result = await _departmentUseCase.UpdateAsync(department, cancellationToken);
        return result.IsSuccess
            ? new JsonResult(result.Data)
            : BadRequest(result.ErrorMessage);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _departmentUseCase.DeleteAsync(id, cancellationToken);
        return result.IsSuccess
            ? new JsonResult(result.Data)
            : BadRequest(result.ErrorMessage);
    }
}
