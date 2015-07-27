using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Nexient.Net.Orgchart.Data.Models;
using Nexient.Net.Orgchart.Data.NHibernate;
using Nexient.Net.Orgchart.Data.Repository;

namespace Orgchart.Controllers
{
    public class JobTitleController : ApiController
    {
        public IHttpActionResult GetJobTitles()
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                var jobTitleRepository = new JobTitleRepository(uow.Session);
                var jobTitles = jobTitleRepository.GetAllJobTitles();

                var ret = Json(jobTitles);

                return ret;
            }
        }

        [HttpGet]
        public void DeleteJobTitle(int id)
        {
            using (var uow = new UnitOfWork())
            {
                uow.BeginTransaction();

                var jobTitleRepository = new JobTitleRepository(uow.Session);
                int ret = jobTitleRepository.DeleteJobTitle(id);

                uow.Commit();
            }
        }
    }
}
