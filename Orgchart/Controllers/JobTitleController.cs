﻿using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nexient.Net.Orgchart.Data;
using Nexient.Net.Orgchart.Data.Ninject;
using Nexient.Net.Orgchart.Data.NHibernate;
using Ninject;

namespace Nexient.Net.Orgchart.WebAPI.Controllers
{
    public class JobTitleController : ApiController
    {
        private IJobTitleRepository _jobTitleRepository;

        public JobTitleController(IJobTitleRepository repository)
        {
            _jobTitleRepository = repository;
        }

        public JobTitleController()
        {
            _jobTitleRepository = NinjectBag.Kernel.Get<IJobTitleRepository>();
        }

        public IHttpActionResult GetJobTitles()
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();
                var jobTitles = _jobTitleRepository.GetAllJobTitles();
                var ret = Json(jobTitles);

                return ret;
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteJobTitle(int id)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                _jobTitleRepository.SetSession(uow.Session);
                var ret = _jobTitleRepository.DeleteJobTitle(id);

                if (ret == 0)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                uow.Commit();

                var response = new HttpResponseMessage();
                var uri = "http://Views/index.html";
                response.Headers.Location = new Uri(uri);
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
        }

        [HttpPut]
        public HttpResponseMessage CreateJobTitle(string id)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                _jobTitleRepository.SetSession(uow.Session);
                _jobTitleRepository.CreateJobTitle(id);

                uow.Commit();

                var response = new HttpResponseMessage();
                var uri = "http://Views/index.html";
                response.Headers.Location = new Uri(uri);
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobTitle(string oldDescription, string newDescription)
        {
            var response = Request.CreateResponse(HttpStatusCode.Redirect);
            var uri = "http://Views/update.html";
            response.Headers.Location = new Uri(uri);
            return response;
        }
    }
}
