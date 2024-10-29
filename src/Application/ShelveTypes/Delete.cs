//using Application.Contracts.Persistence;
//using Application.Core;
//using MediatR;

//namespace Application.ShelveTypes;

//public class Delete
//{
//	/*
//	 * Command don't return any thing
//	 */
//	public class Command : IRequest<Result<Unit>>
//	{
//		public Guid Id { get; set; }
//	}

//	public class Handler : IRequestHandler<Command, Result<Unit>>
//	{
//		private readonly IShelveRepository _context;

//		public Handler(IShelveRepository context)
//		{
//			_context = context;
//		}

//		public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
//		{
//			var shelve = await _context.GetByIdAsync(request.Id);

//			if (shelve is null)
//			{
//				return null!;
//			}

//			bool result = await _context.DeleteAsync(shelve);

//			if (!result)
//			{
//				return Result<Unit>.Failure("Failed to delete the ShelveType");
//			}

//			//Unit.Value is the same as return nothing as Command don't return anything
//			return Result<Unit>.Success(Unit.Value);
//		}
//	}
//}
