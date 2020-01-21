using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ReadersApi.Providers;

namespace ReadersApi.Controllers
{
    [Route("api/[controller]")]
    public class LogicController : ControllerBase
    {
        private ITemplateHelper _templateHelper;
        private IMailHelper _mail;
        private IHostingEnvironment _environment;

        public LogicController(ITemplateHelper helper, IMailHelper mail, IHostingEnvironment environment)
        {
            _templateHelper = helper;
            _mail = mail;
            _environment = environment;
        }

        [HttpGet]
        [Route("allreaders")]
        public List<Reader> GetReaders()
        {
            var readers = ReaderStore.Readers.ToList();
            return readers;
        }

        [HttpGet]
        [Route("allusers")]
        public List<User> GetUsers()
        {
            var readers = ReaderStore.Users.ToList();
            return readers;
        }

        [HttpGet]
        [Route("template")]
        public async Task<string> GetTemplate()
        {
            var model = new MailViewModel();

            var response = await _templateHelper.GetTemplateHtmlAsStringAsync<List<Reader>>("Templates/Content", ReaderStore.Readers);

            model.Content = response;

            var headerImagePath = string.Format("{0}/{1}", _environment.ContentRootPath, "Images/banner-image.png");

            // var base64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(headerImage));

            model.HeaderImage = new LinkedResource()
            {
                ContentId = "header",
                ContentPath = headerImagePath,
                ContentType = "image/png"
            }; //string.Format("data:image/png;base64,{0}", base64);

            _mail.SendMail("sriramkumar563@gmail.com", "Reader Test Data", model);

            return response;
        }
    }
}

namespace ReadersApi.Controllers
{
    public class MailViewModel
    {
        public LinkedResource HeaderImage { get; set; }
        public string Content { get; set; }
        public LinkedResource FooterImage { get; set; }
    }

    public class LinkedResource
    {
        public string ContentId { get; set; }
        public string ContentPath { get; set; }
        public string ContentType { get; set; }
    }
}