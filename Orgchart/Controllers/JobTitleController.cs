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
    public class JobTitleController : ApiController
    {
        private readonly IJobTitleRepository _jobTitleRepository;
        private const string _indexUri = "http://Views/index.html";

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
                    return MakeResponseMessage(HttpStatusCode.NotFound);

                uow.Commit();
                return MakeResponseMessage();
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
                return MakeResponseMessage();
            }
        }

        [HttpGet]
        public HttpResponseMessage GetJobTitleFromId(int id)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                _jobTitleRepository.SetSession(uow.Session);
                var jobTitle = _jobTitleRepository.GetJobTitleFromId(id);

                if (jobTitle == null)
                    return MakeResponseMessage(HttpStatusCode.BadRequest);

                var response = new HttpResponseMessage();
                response.Headers.Location = new Uri(_indexUri);
                response.Headers.Add("JobTitle", jobTitle.Description);
                response.StatusCode = HttpStatusCode.OK;

                return response;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateJobTitle(string oldDescription, string newDescription)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                _jobTitleRepository.SetSession(uow.Session);
                var ok = _jobTitleRepository.UpdateJobTitle(oldDescription, newDescription);

                uow.Commit();
                return MakeResponseMessage();
            }
        }

        private HttpResponseMessage MakeResponseMessage(HttpStatusCode code = HttpStatusCode.OK)
        {
            var response = new HttpResponseMessage();
            response.Headers.Location = new Uri(_indexUri);
            response.StatusCode = code;

            return response;
        }
    }
}
