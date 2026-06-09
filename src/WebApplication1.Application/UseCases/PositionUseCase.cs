using WebApplication1.Application.Common;
using WebApplication1.Application.Dtos;
using WebApplication1.Application.Interfaces.ICommon;
using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Application.Interfaces.IUseCases;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Application.UseCases;

public class PositionUseCase : IPositionUseCase
{
    private readonly IPositionRepository _positionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PositionUseCase(IPositionRepository positionRepository, IUnitOfWork unitOfWork)
    {
        _positionRepository = positionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IReadOnlyList<PositionDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var positions = await _positionRepository.GetAllAsync(cancellationToken);
        var dtos = positions.Select(p => new PositionDto
        {
            PositionId = p.PositionId,
            PositionName = p.PositionName
        }).ToList();

        return Result<IReadOnlyList<PositionDto>>.Success(dtos);
    }

    public async Task<Result<string>> CreateAsync(PositionDto dto, CancellationToken cancellationToken = default)
    {
        await _positionRepository.AddAsync(new PositionEntity
        {
            PositionName = dto.PositionName
        }, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Success("Added Successful");
    }

    public async Task<Result<string>> UpdateAsync(PositionDto dto, CancellationToken cancellationToken = default)
    {
        var existing = await _positionRepository.GetByIdAsync(dto.PositionId, cancellationToken);
        if (existing is null)
            return Result<string>.Failure("Position not found.");

        existing.PositionName = dto.PositionName;
        existing.UpdatedAt = DateTime.UtcNow;

        await _positionRepository.UpdateAsync(existing, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Success("Updated Successful");
    }

    public async Task<Result<string>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existing = await _positionRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return Result<string>.Failure("Position not found.");

        await _positionRepository.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Success("Deleted Successful");
    }
}
