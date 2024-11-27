using CafeManagement.Application.Dto;
using MediatR;

namespace CafeManagement.Application.Cafes.Queries;

public class GetAllCafesQuery : IRequest<IEnumerable<CafeDto>>;