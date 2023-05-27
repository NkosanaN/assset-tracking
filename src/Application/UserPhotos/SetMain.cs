//using Application.Contracts.Persistence;
//using Application.Core;
//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Persistence;

//namespace Application.UserPhotos;

//public class SetMain
//{
//    /*
//     * Though is command here will return UserPhoto Model exception 
//     */

//    public class Command : IRequest<Result<Unit>>
//    {
//        public string Id { get; set; }
//    }

//    public class Handler : IRequestHandler<Command, Result<Unit>>
//    {
//        private readonly DataContext _context;
//        private readonly IUserAccessor _userAccessor;

//        public Handler(DataContext context, IUserAccessor userAccessor)
//        {
//            _context = context;
//            _userAccessor = userAccessor;
//        }

//        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
//        {
//            var user = await _context.Users.Include(p => p.UserPhotos)
//                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername(), cancellationToken);

//            if (user is null) return null!;

//            var photo = user.UserPhotos.FirstOrDefault(x => x.Id == request.Id);

//            if (photo is null) return null!;

//            var currentMain = user.UserPhotos.FirstOrDefault(x => x.IsMain);

//            if (currentMain != null) currentMain.IsMain = false;

//            photo.IsMain = true;

//            if (!user.UserPhotos.Any(x => x.IsMain)) photo.IsMain = true;

//            user.UserPhotos.Add(photo);

//            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

//            if (success) return Result<Unit>.Success(Unit.Value);

//            return Result<Unit>.Failure("Problem setting main photo");

//        }
//    }
//}

