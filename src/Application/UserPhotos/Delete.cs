using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UserPhotos;

public class Delete
{
    /*
     * Though is command here will return UserPhoto Model exception 
     */

    public class Command : IRequest<Result<Unit>>
    {
        public string Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context,
            IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
        {
            _context = context;
            _photoAccessor = photoAccessor;
            _userAccessor = userAccessor;

        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {

            var user = await _context.Users.Include(p => p.UserPhotos)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername(), cancellationToken);

            if (user is null) return null!;

            var photo = user.UserPhotos.FirstOrDefault(x => x.Id == request.Id);

            if (photo is null) return null!;

            if (photo.IsMain) return Result<Unit>.Failure("You cannot delete your main photo");

            var result = await _photoAccessor.DeletePhoto(photo.Id);

            if (result is null) return Result<Unit>.Failure("Problem deleting photo from Cloudinary");

            user.UserPhotos.Remove(photo);

            var success = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (success) return Result<Unit>.Success(Unit.Value);

            return Result<Unit>.Failure("Problem delete photo from API");

        }
    }
}

