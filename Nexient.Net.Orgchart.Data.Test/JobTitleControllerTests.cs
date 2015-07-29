using System.Web.Http;
using System.Collections.Generic;
using System.Linq;
using Moq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Nexient.Net.Orgchart.WebAPI.Controllers;
using Nexient.Net.Orgchart.Data.Ninject;

namespace Nexient.Net.Orgchart.Data.Test
{
    [TestFixture]
    class JobTitleControllerTests
    {
        private JobTitleController _sut;
        private Mock<IJobTitleRepository> _repository;

        [SetUp]
        public void Setup()
        {
            NinjectBag.InitializeNinject();
            _repository = new Mock<IJobTitleRepository>();
            _sut = new JobTitleController(_repository.Object);
        }
    }
}
