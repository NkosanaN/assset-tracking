//using Application.Contracts.Persistence;
//using AutoMapper;
//using NSubstitute;

//namespace ItemManagementSystem.T1.Tests.Feature;

//public class SupplierHandlerTests
//{
//    private readonly ISupplierRepository _supplierRepository;
//    private IMapper _mapper;
//    public SupplierHandlerTests(ISupplierRepository supplierRepository, IMapper mapper)
//    {
//        _supplierRepository = Substitute.For<ISupplierRepository>();
//        _mapper = mapper;
//    }

//    private struct Mocks
//    {
//        public ISupplierRepository IsupplierRepositoryMock { get; set; }
//    }

//    private static Mocks GetMock()
//    {
//        var mock = new Mocks
//        {
//            IsupplierRepositoryMock = Substitute.For<ISupplierRepository>()
//        };

//        return mock;
//    }

//    private static SupplierHandler GetSystemUnderTest(Mocks mocks)
//    {
//        return  
//    }

//    public class AssetTrackingGetAllSupplierTest
//    {
//        public Task GivenSupplierFound_WhenGetAllSupplierCalled_ShouldReturnAllSupplier()
//        {
//            //Arrange
//            //var mock = GetMock();
//            //var testSupplier

//            _supplierRepository.GetAllSupplier()
//        }

//    }
//}
