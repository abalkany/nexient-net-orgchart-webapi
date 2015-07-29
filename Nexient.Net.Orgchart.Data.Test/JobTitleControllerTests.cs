﻿using System;
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
        private int _jobTitleId = 111;
        private string _jobTitleDescription = "a-description";

        [SetUp]
        public void Setup()
        {
            NinjectBag.InitializeNinject();
            _repository = new Mock<IJobTitleRepository>();
            _sut = new JobTitleController(_repository.Object);
        }

        [Test]
        public void GetJobTitlesCallsGetAllJobTitlesInRepository()
        {
            _sut.GetJobTitles();
            _repository.Verify(_ => _.GetAllJobTitles());
        }

        [Test]
        public void DeleteJobTitleCallsDeleteJobTitleInRepository()
        {
            _sut.DeleteJobTitle(_jobTitleId);
            _repository.Verify(_ => _.DeleteJobTitle(_jobTitleId));
        }

        [Test]
        public void CreateJobTitleCallsCreateJobTitleInRepository()
        {
            _sut.CreateJobTitle(_jobTitleDescription);
            _repository.Verify(_ => _.CreateJobTitle(_jobTitleDescription));
        }
    }
}
