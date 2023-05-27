//using Application.Contracts.Persistence;
//using Application.Core;
//using AutoMapper;
//using MediatR;

//namespace Application.Profiles
//{
//    public class Details
//    {
//        public class Query : IRequest<Result<Profile>>
//        {
//            public string? Username { get; set; }
//        }

//        public class Handler : IRequestHandler<Query, Result<Profile>>
//        {
//            private readonly IProfileRepository _context;
//            private readonly IMapper _mapper;

//            public Handler(IProfileRepository context, IMapper mapper)
//            {
//                _context = context;
//                _mapper = mapper;
//            }

//            public async Task<Result<Profile>> Handle(Query request, CancellationToken cancellationToken)
//            {
//                var user = await _context.GetAllAsync();

//                //.ProjectTo<Profile>(_mapper.ConfigurationProvider)
//                //.SingleOrDefaultAsync(x => x.Username == request.Username,
//                //    cancellationToken);

//                if (user == null) return null!;

//                return Result<Profile>.Success(user);
//            }
//        }
//    }
//}
