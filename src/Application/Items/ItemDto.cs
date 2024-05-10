using Domain;

namespace Application.Items;

public record ItemDto(Guid ItemId, 
    DateTime TimeStamp,
    string Name, 
    string Description,
    string? Serialno, 
    string? ItemTag,
    float Cost,
    float Qty, 
    DateTime DatePurchased, 
    ShelveType? ShelveBy,
    bool DueforRepair);
