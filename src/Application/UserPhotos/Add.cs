using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.UserPhotos
{
    public class Add
    {
        /*
         * Though is command here will return UserPhoto Model exception 
         */

        public class Command : IRequest<Result<UserPhoto>>
        {
            public IFormFile File { get; set; }
        }
        public class Handler  : IRequestHandler<Command, Result<UserPhoto>>
        {
            private readonly DataContext _context;
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context ,
                IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
            {
                _context  = context;
                _photoAccessor  = photoAccessor;
                _userAccessor = userAccessor;

            }

            public async Task<Result<UserPhoto>> Handle(Command request, CancellationToken cancellationToken)
            {

                var user = await _context.Users.Include(p => p.UserPhotos)
                    .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername(), cancellationToken);

                if (user is null) return null!;

                var photoUploadResult = await _photoAccessor.AddPhoto(request.File);

                var photo = new UserPhoto
                {
                    Id = photoUploadResult.PublicId,
                    Url = photoUploadResult.Url

                };
                //check whether user has the main photo

                if(!user.UserPhotos.Any(x=>x.IsMain)) photo.IsMain = true;

                user.UserPhotos.Add(photo);

                var result = await _context.SaveChangesAsync(cancellationToken) > 0;

                if(result) return Result<UserPhoto>.Success(photo);

                return Result<UserPhoto>.Failure("Problem adding photo");

            }
        }
    }
}
