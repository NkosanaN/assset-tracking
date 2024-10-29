//using Application.Contracts.Persistence;
//using Application.Core;
//using MediatR;
//using FluentValidation;
//using Domain;

//namespace Application.Departments;

//public class Create
//{
//	/*
//	 * Command don't return any thing
//	 */
//	public class Command : IRequest<Result<Unit>>
//	{
//		public DepartmentDto DepartmentRequest { get; set; } = new();
//	}

//	public class CommandValidator : AbstractValidator<Command>
//	{
//		public CommandValidator()
//		{
//			RuleFor(x => x.DepartmentRequest).SetValidator(new DepartmentValidator());
//		}
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
//			var model = new Department
//			{
//				DepartmentId = Guid.NewGuid(),
//				DepartmentName = request.DepartmentRequest.DepartmentName,
//				Description = request.DepartmentRequest.Description,
//			};

//			var result = await _context.CreateAsync(model);

//			if (!result)
//				return Result<Unit>.Failure("Fail to create Department");

//			return Result<Unit>.Success(Unit.Value);
//		}

//	}

//}
