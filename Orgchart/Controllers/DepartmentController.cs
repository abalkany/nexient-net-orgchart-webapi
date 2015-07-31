using System;
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

        public IHttpActionResult GetAllDepartments()
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();
                var departments = _departmentRepository.GetAllDepartments();
                var ret = Json(departments);

                return ret;
            }
        }

        [HttpPost]
        public HttpResponseMessage CreateDepartment(string name, int managerId, int parentDepartmentId)
        {
            int departmentId;

            using (var uow = new UnitOfWork())
            {
                _departmentRepository.SetSession(uow.Session);
                uow.BeginTransaction();
                departmentId = 0;

                var department = _departmentRepository.CreateDepartment(name);

                if (department != null)
                {
                    departmentId = department.DepartmentId;
                    _departmentRepository.UpdateDepartmentsParentDepartment(departmentId, parentDepartmentId);
                    _departmentRepository.UpdateDepartmentsManager(departmentId, managerId);

                    uow.Session.Flush();
                    uow.Commit();
                }
            }

            var response = new HttpResponseMessage();
            var uri = "http://Views/index.html";
            response.Headers.Location = new Uri(uri);
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }

    }
}
