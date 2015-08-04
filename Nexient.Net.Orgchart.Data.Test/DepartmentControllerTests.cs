using Moq;
using NUnit.Framework;
using Nexient.Net.Orgchart.WebAPI.Controllers;
using Nexient.Net.Orgchart.Data.Ninject;

namespace Nexient.Net.Orgchart.Data.Test
{
    [TestFixture]
    public class DepartmentControllerTests
    {
        private DepartmentController _sut;
        private Mock<IDepartmentRepository> _repository;
        private string _departmentName = "a department name";
        private int _departmentId = 111111;
        private int _managerId = 222222;
        private int _parentDepartmentId = 333;

        [SetUp]
        public void Setup()
        {
            NinjectBag.InitializeNinject();
            _repository = new Mock<IDepartmentRepository>();
            _sut = new DepartmentController(_repository.Object);
        }

        [Test]
        public void CreateDepartmentCallsCreateDepartmentInRepository()
        {
            _sut.CreateDepartment(_departmentName, _managerId, _parentDepartmentId);
            _repository.Verify(_ => _.CreateDepartment(_departmentName));
        }

        [Test]
        public void GetAllDepartmentsCallsGetAllDepartmentsInRepository()
        {
            _sut.GetAllDepartments();
            _repository.Verify(_ => _.GetAllDepartments());
        }

        [Test]
        public void FindDepartmentByIdCallsFindDepartmentByIdInRepository()
        {
            _sut.FindDepartmentById(_departmentId);
            _repository.Verify(_ => _.FindDepartmentById(_departmentId));
        }

        [Test]
        public void DeleteDepartmentByIdCallsDeleteDepartmentByIdInRepository()
        {
            _sut.DeleteDepartmentById(_departmentId);
            _repository.Verify(_ => _.DeleteDepartmentById(_departmentId));
        }

        [Test]
        public void UpdateDepartmentCallsThreeUpdateMethodsInRepository()
        {
            _sut.UpdateDepartment(_departmentId, _departmentName, _managerId, _parentDepartmentId);
            _repository.Verify(_ => _.UpdateDepartmentsName(_departmentId, _departmentName));
            _repository.Verify(_ => _.UpdateDepartmentsManager(_departmentId, _managerId));
            _repository.Verify(_ => _.UpdateDepartmentsParentDepartment(_departmentId, _parentDepartmentId));
        }
    }
}
