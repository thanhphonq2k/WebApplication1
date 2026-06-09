using Microsoft.AspNetCore.Mvc;
using WebApplication1.Application.Dtos;
using WebApplication1.Application.Interfaces.IUseCases;

namespace WebApplication1.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeUseCase _employeeUseCase;

    public EmployeeController(IEmployeeUseCase employeeUseCase)
    {
        _employeeUseCase = employeeUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _employeeUseCase.GetAllAsync(cancellationToken);
        return result.IsSuccess
            ? new JsonResult(result.Data)
            : BadRequest(result.ErrorMessage);
    }

    [HttpPost]
    public async Task<IActionResult> Post(EmployeeDto employee, CancellationToken cancellationToken)
    {
        var result = await _employeeUseCase.CreateAsync(employee, cancellationToken);
        return result.IsSuccess
            ? new JsonResult(result.Data)
            : BadRequest(result.ErrorMessage);
    }

    [HttpPut]
    public async Task<IActionResult> Put(EmployeeDto employee, CancellationToken cancellationToken)
    {
        var result = await _employeeUseCase.UpdateAsync(employee, cancellationToken);
        return result.IsSuccess
            ? new JsonResult(result.Data)
            : BadRequest(result.ErrorMessage);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _employeeUseCase.DeleteAsync(id, cancellationToken);
        return result.IsSuccess
            ? new JsonResult(result.Data)
            : BadRequest(result.ErrorMessage);
    }

    [Route("SaveFile")]
    [HttpPost]
    public async Task<IActionResult> SaveFile(CancellationToken cancellationToken)
    {
        var postedFile = Request.Form.Files.FirstOrDefault();
        if (postedFile is null)
            return BadRequest("No file uploaded.");

        await using var stream = postedFile.OpenReadStream();
        var result = await _employeeUseCase.SavePhotoAsync(stream, postedFile.FileName, cancellationToken);
        return result.IsSuccess
            ? new JsonResult(result.Data)
            : BadRequest(result.ErrorMessage);
    }
}
