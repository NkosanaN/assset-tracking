using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class ItemEmployeeAssignmentRepository : GenericRepository<ItemEmployeeAssignment>, IItemEmployeeAssignmentRepository
{
    public ItemEmployeeAssignmentRepository(DataContext context) : base(context)
    { }

    public async Task<(bool, string)> CheckHowManyTimesReceiverHasTakenOutItemWithOutReturn(string id)
    {
        var results = false;
        //get how many X's this user has taken Items without return
        var itemNames = await _dataContext.ItemEmployeeAssignments
            .Where(x => (x.ReceiverById == id && !x.IsReturned))
            .Select(c => c.Item!.Name)
            .ToListAsync();

        var combinedString = string.Join(", ", itemNames);

        return itemNames.Count == 2 ?
            (results, "You've reached the maximum request ." +
                      $"Please return this item(s) [{combinedString}] before requesting again")
            : (!results, "all good");
    }

    public Task<bool> CompareIssuerIdAndReceiver(string receiverId, string issuerId)
    {
        return Task.FromResult(receiverId == issuerId);
    }

    public async Task<ItemEmployeeAssignment> GetItemEmployeeAssignmentById(Guid id)
    {
        return await _dataContext.ItemEmployeeAssignments
            .Include(c => c.Item)
            .Include(x => x.IssuerBy)
            .Include(x => x.ReceiverBy)
            .FirstOrDefaultAsync(c => c.AssigmentId == id);
    }

    public Task<int> GetItemEmployeeAssignmentLinkedWithUser(string id)
    {
        throw new NotImplementedException();
    }

    //public Task<ItemEmployeeAssignment> GetItemEmployeeAssignmentDetail(Guid id)
    //{
    //    var singleItem = _dataContext.ItemEmployeeAssignments
    //        .Include(c => c.Item)
    //        .Include(x => x.IssuerBy)
    //        .Include(x => x.ReceiverBy)
    //        .SingleOrDefaultAsync(x=>x.Item == );

    //    return (Task<ItemEmployeeAssignment>)singleItem;
    //}

    public Task<IQueryable<ItemEmployeeAssignment>> GetItemEmployeeAssignmentList()
    {
        var listOfItems = _dataContext.ItemEmployeeAssignments.AsNoTracking()
            .Include(c => c.Item)
            .Include(x => x.IssuerBy)
            .Include(x => x.ReceiverBy).AsQueryable();

        return Task.FromResult(listOfItems);
    }

    public async Task<bool> ReturnItemEmployeeAssignment(Guid id, string note)
    {
        //var item = await _dataContext.ItemEmployeeAssignments.FirstOrDefaultAsync(c => c.AssigmentId == id);

        //_dataContext.ItemEmployeeAssignments
        //    .exc
        //    .FromSqlRaw(
        //        $"update [tblitememployeeassignment] " +
        //        $"set Condition = {note} ," +
        //        $"IsReturned = {1} ")
        //    .Where(c=>c.AssigmentId == id);

        var rowsModified = _dataContext.Database.ExecuteSql($"UPDATE [tblitememployeeassignment] SET [Condition] = {note}, IsReturned = {1} , DateReturned = {DateTime.Now} Where [AssigmentId] = {id}");

        return rowsModified == 1;
        
    }
}
