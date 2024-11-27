using CafeManagement.Application.Dto;
using MediatR;

public record GetCafeByIdQuery(Guid Id) : IRequest<CafeDto>;