
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nexient.Net.Orgchart.Data;
using Nexient.Net.Orgchart.Data.NHibernate;
using Nexient.Net.Orgchart.Data.Ninject;
using Ninject;

namespace Nexient.Net.Orgchart.WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository repository)
        {
            _employeeRepository = repository;
        }

        public EmployeeController()
        {
            _employeeRepository = NinjectBag.Kernel.Get<IEmployeeRepository>();
        }

        [HttpGet]
        public IHttpActionResult GetAllEmployees()
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();
                var employees = _employeeRepository.GetAllEmployees();
                var ret = Json(employees);

                return ret;
            }
        }
    }
}
