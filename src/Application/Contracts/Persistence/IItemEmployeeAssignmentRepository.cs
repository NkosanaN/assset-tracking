using Domain;

namespace Application.Contracts.Persistence;

public interface IItemEmployeeAssignmentRepository : IGenericRepository<ItemEmployeeAssignment>
{
	Task<IQueryable<ItemEmployeeAssignment>> GetItemEmployeeAssignmentList();
	Task<ItemEmployeeAssignment> GetItemEmployeeAssignmentById(Guid id);
	Task<(bool, string)> CheckHowManyTimesReceiverHasTakenOutItemWithOutReturn(string id);
	Task<bool> CompareIssuerIdAndReceiver(string receiverId, string issuerId);
	Task<int> GetItemEmployeeAssignmentLinkedWithUser(string id);
	Task<bool> ReturnItemEmployeeAssignment(Guid id, string note);
}

