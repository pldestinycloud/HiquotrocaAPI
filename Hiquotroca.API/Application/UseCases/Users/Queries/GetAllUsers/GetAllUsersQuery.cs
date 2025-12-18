using Hiquotroca.API.DTOs.Users;
using MediatR;
using System.Collections.Generic;

namespace Hiquotroca.API.Application.UseCases.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IRequest<List<UserBriefDataDto>>;
