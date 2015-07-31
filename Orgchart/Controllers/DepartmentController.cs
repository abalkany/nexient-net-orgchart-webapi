using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nexient.Net.Orgchart.Data;
using Nexient.Net.Orgchart.Data.Ninject;
using Nexient.Net.Orgchart.Data.NHibernate;
using Ninject;

namespace Nexient.Net.Orgchart.WebAPI.Controllers
{
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository repository)
        {
            _departmentRepository = repository;
        }

        public DepartmentController()
        {
            _departmentRepository = NinjectBag.Kernel.Get<IDepartmentRepository>();
        }

        [HttpGet]
        public IHttpActionResult GetAllDepartments()
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();
                var jobTitles = _departmentRepository.GetAllDepartments();
                var ret = Json(jobTitles);

                return ret;
            }
        }

    }
}
