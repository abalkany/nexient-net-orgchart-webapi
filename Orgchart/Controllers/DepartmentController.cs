﻿using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nexient.Net.Orgchart.Data;
using Nexient.Net.Orgchart.Data.NHibernate;
using Nexient.Net.Orgchart.Data.Ninject;
using Ninject;
using Nexient.Net.Orgchart.Data.Models;

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

        [HttpGet]
        public IHttpActionResult FindDepartmentById(int id)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();
                var department = _departmentRepository.FindDepartmentById(id);
                var ret = Json(department);

                return ret;
            }
        }

        [HttpPost]
        public HttpResponseMessage CreateDepartment(string name, int managerId, int parentDepartmentId)
        {
            int departmentId;
            Department department = null;

            using (var uow = new UnitOfWork())
            {
                _departmentRepository.SetSession(uow.Session);
                uow.BeginTransaction();
                departmentId = 0;

                department = _departmentRepository.CreateDepartment(name);

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
            response.StatusCode = (department == null) ? HttpStatusCode.BadRequest : HttpStatusCode.OK;

            return response;
        }
    
        [HttpDelete]
        public IHttpActionResult DeleteDepartmentById(int id)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();
                _departmentRepository.SetSession(uow.Session);
                int numDeleted = _departmentRepository.DeleteDepartmentById(id);

                try
                {
                    uow.Commit();
                }
                catch (Exception)
                {
                    numDeleted = 0;
                }
                return Json<bool>(numDeleted > 0);
            }
        }
    }
}
