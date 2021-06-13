using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestApp.BL;
using TestApp.Core;
using TestApp.DI;
using TestApp.Tests.Core;

namespace TestApp.Tests
{
    [TestClass]
    public class EnforcmentServiceUnitTest : BaseTest
    {
        #region Fields

        private EnforcmentService service;

        public EnforcmentServiceUnitTest()
        {
        }

        #endregion Fields

        #region Test Helpers

        [TestInitialize]
        public void Init()
        {
            _iHttpGetRequestSender = new Mock<IHttpGetRequestSender>();

            IoCC.Instance.Register<IHttpGetRequestSender>(_iHttpGetRequestSender.Object);

            _iHttpGetRequestSender.Setup(f => f.GetAsync("https://api.fda.gov/food/enforcement.json?search=report_date:[20120101+TO+20121231]&api_key=L6c0owIiFxWQxDl6xyLPWGefxt2uwuaBBtF5mfn8&limit=1000&skip=0"))
                .Returns(Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = GetHttpContent() }));

            service = new EnforcmentService("https://api.fda.gov/food/enforcement.json",
           "report_date:[20120101+TO+20121231]",
           "L6c0owIiFxWQxDl6xyLPWGefxt2uwuaBBtF5mfn8",
           0,
           1000);
        }

        #endregion Test Helpers

        [TestMethod]
        public void GetFewestRecalsReportDate_Test()
        {

            string result = service.GetFewestRecalsReportDate();
            Assert.AreEqual(result, "20121030");
        }

        private HttpContent GetHttpContent()
        {
            string content = "{  \"meta\": {  },  \"results\": [    {\"report_date\": \"20121031\",        },{\"report_date\": \"20121031\",        },{\"report_date\": \"20121030\",        }]}";
            return new StringContent(content);
        }
    }
}
