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
        private int _departmentId = 111;
        private string _jobTitleDescription = "a-description";
        private string _jobTitleDescription2 = "another-description";

        [SetUp]
        public void Setup()
        {
            NinjectBag.InitializeNinject();
            _repository = new Mock<IDepartmentRepository>();
            _sut = new DepartmentController(_repository.Object);
        }

        [Test]
        public void GetAllDepartmentsCallsGetAllDepartmentsInRepository()
        {
            _sut.GetAllDepartments();
            _repository.Verify(_ => _.GetAllDepartments());
        }
    }
}
