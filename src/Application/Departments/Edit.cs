//using Application.Contracts.Persistence;
//using Application.Core;
//using MediatR;
//using Domain;

//namespace Application.Departments;

//public class Edit
//{
//	/*
//	 * Command don't return any thing
//	 */
//	public class Command : IRequest<Result<Unit>>
//	{
//		public Department? Department { get; set; }
//	}

//	public class Handler : IRequestHandler<Command, Result<Unit>>
//	{
//		private readonly IDepartmentRepository _context;

//		public Handler(IDepartmentRepository context)
//		{
//			_context = context;
//		}

//		public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
//		{
//			var depart = await _context.GetByIdAsync(request.Department!.DepartmentId);

//			if (depart is null)
//			{
//				return null!;
//			}

//			depart.DepartmentName = request.Department.DepartmentName;
//			depart.Description = request.Department.Description;

//			bool result = await _context.UpdateAsync(depart);

//			if (!result)
//			{
//				return Result<Unit>.Failure("Failed to update Department");
//			}

//			//Unit.Value is the same as return nothing as Command don't return anything
//			return Result<Unit>.Success(Unit.Value);
//		}
//	}
//}
