using Application.Contracts.Persistence;
using Application.Core;
using MediatR;
using Domain;

namespace Application.User;

public class List
{
    public class Query : IRequest<Result<PagedList<AppUser>>>
    {
        public PagingParams? Params { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<PagedList<AppUser>>>
    {
        private readonly IUserRepository _context;

        public Handler(IUserRepository context)
        {
            _context = context;
        }

        public async Task<Result<PagedList<AppUser>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = await _context.GetAllUser();

            return Result<PagedList<AppUser>>.Success(
                await PagedList<AppUser>.CreateAsync(query, request.Params!.PageNumber,
                    request.Params.PageSize));
        }
    }

}
