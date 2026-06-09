using WebApplication1.Application.Common;
using WebApplication1.Application.Dtos;
using WebApplication1.Application.Interfaces.ICommon;
using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Application.Interfaces.IServices;
using WebApplication1.Application.Interfaces.IUseCases;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Application.UseCases;

public class EmployeeUseCase : IEmployeeUseCase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public EmployeeUseCase(
        IEmployeeRepository employeeRepository,
        IUnitOfWork unitOfWork,
        IFileStorageService fileStorageService)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<Result<IReadOnlyList<EmployeeDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var employees = await _employeeRepository.GetAllAsync(cancellationToken);
        var dtos = employees.Select(MapToDto).ToList();
        return Result<IReadOnlyList<EmployeeDto>>.Success(dtos);
    }

    public async Task<Result<string>> CreateAsync(EmployeeDto dto, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(dto);
        await _employeeRepository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Success("Added Successful");
    }

    public async Task<Result<string>> UpdateAsync(EmployeeDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _employeeRepository.GetByIdAsync(dto.EmployeeId, cancellationToken);
        if (existing is null)
            return Result<string>.Failure("Employee not found.");

        existing.EmployeeName = dto.EmployeeName;
        existing.Department = dto.Department;
        existing.Position = dto.Position;
        existing.DateOfJoining = ParseDate(dto.DateOfJoining);
        existing.PhotoFileName = dto.PhotoFileName;
        existing.UpdatedAt = DateTime.UtcNow;

        await _employeeRepository.UpdateAsync(existing, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Success("Updated Successful");
    }

    public async Task<Result<string>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = await _employeeRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return Result<string>.Failure("Employee not found.");

        await _employeeRepository.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Success("Deleted Successful");
    }

    public async Task<Result<string>> SavePhotoAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
    {
        try
        {
            var savedFileName = await _fileStorageService.SavePhotoAsync(fileStream, fileName, cancellationToken);
            return Result<string>.Success(savedFileName);
        }
        catch
        {
            return Result<string>.Success("Đào Thanh Phong.jpg");
        }
    }

    private static EmployeeDto MapToDto(EmployeeEntity entity) => new()
    {
        EmployeeId = entity.EmployeeId,
        EmployeeName = entity.EmployeeName,
        Department = entity.Department,
        Position = entity.Position,
        DateOfJoining = entity.DateOfJoining?.ToString("yyyy-MM-dd") ?? string.Empty,
        PhotoFileName = entity.PhotoFileName
    };

    private static DateTime? ParseDate(string value)
    {
        return DateTime.TryParse(value, out var date) ? date : null;
    }

    private static EmployeeEntity MapToEntity(EmployeeDto dto) => new()
    {
        EmployeeName = dto.EmployeeName,
        Department = dto.Department,
        Position = dto.Position,
        DateOfJoining = ParseDate(dto.DateOfJoining),
        PhotoFileName = dto.PhotoFileName
    };
}
