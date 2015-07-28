using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Nexient.Net.Orgchart.Data;
using Nexient.Net.Orgchart.Data.Ninject;
using Nexient.Net.Orgchart.Data.NHibernate;
using Nexient.Net.Orgchart.Data.Repository;
using Ninject;

namespace Orgchart.Controllers
{
    public class JobTitleController : ApiController
    {
        public IHttpActionResult GetJobTitles()
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                var jobTitleRepository = NinjectBag.Kernel.Get<IJobTitleRepository>();
                var jobTitles = jobTitleRepository.GetAllJobTitles();

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

                var jobTitleRepository = NinjectBag.Kernel.Get<IJobTitleRepository>();
                jobTitleRepository.SetSession(uow.Session);
                int ret = jobTitleRepository.DeleteJobTitle(id);

                uow.Commit();

                var response = Request.CreateResponse(HttpStatusCode.Moved);
                string uri = "http://" + Request.RequestUri.Authority + "/Views/index.html";
                response.Headers.Location = new Uri(uri);
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
        }

        [HttpGet]
        public HttpResponseMessage CreateJobTitleShowForm(int id1, int id2)
        {
                var response = Request.CreateResponse(HttpStatusCode.Moved);
                string uri = "http://" + Request.RequestUri.Authority + "/Views/create.html";
                response.Headers.Location = new Uri(uri);
                return response;
        }

        [HttpPost]
        public HttpResponseMessage CreateJobTitleDoCreate(int id, int id2, int id3)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                var jobTitleRepository = NinjectBag.Kernel.Get<IJobTitleRepository>();
                jobTitleRepository.SetSession(uow.Session);
                jobTitleRepository.CreateJobTitle(id.ToString());

                uow.Commit();

                var response = Request.CreateResponse(HttpStatusCode.Moved);
                string uri = "http://" + Request.RequestUri.Authority + "/Views/index.html";
                response.Headers.Location = new Uri(uri);
                return response;
            }
        }
    }
}
