using WebApplication1.Application.Common;
using WebApplication1.Application.Dtos;
using WebApplication1.Application.Interfaces.ICommon;
using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Application.Interfaces.IUseCases;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Application.UseCases;

public class DepartmentUseCase : IDepartmentUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentUseCase(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
    {
        _departmentRepository = departmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IReadOnlyList<DepartmentDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var departments = await _departmentRepository.GetAllAsync(cancellationToken);
        var dtos = departments.Select(d => new DepartmentDto
        {
            DepartmentId = d.DepartmentId,
            DepartmentName = d.DepartmentName
        }).ToList();

        return Result<IReadOnlyList<DepartmentDto>>.Success(dtos);
    }

    public async Task<Result<string>> CreateAsync(DepartmentDto dto, CancellationToken cancellationToken = default)
    {
        await _departmentRepository.AddAsync(new DepartmentEntity
        {
            DepartmentName = dto.DepartmentName
        }, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Success("Added Successful");
    }

    public async Task<Result<string>> UpdateAsync(DepartmentDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _departmentRepository.GetByIdAsync(dto.DepartmentId, cancellationToken);
        if (existing is null)
            return Result<string>.Failure("Department not found.");

        existing.DepartmentName = dto.DepartmentName;
        existing.UpdatedAt = DateTime.UtcNow;

        await _departmentRepository.UpdateAsync(existing, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Success("Updated Successful");
    }

    public async Task<Result<string>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = await _departmentRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return Result<string>.Failure("Department not found.");

        await _departmentRepository.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Success("Deleted Successful");
    }
}
