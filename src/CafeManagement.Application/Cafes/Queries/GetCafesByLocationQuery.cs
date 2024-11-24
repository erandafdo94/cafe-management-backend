using CafeManagement.Application.Dto;
using CafeManagement.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

public record GetCafesByLocationQuery : IRequest<IEnumerable<CafeDto>>
{
    public string? Location { get; init; }
}
