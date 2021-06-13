using TestApp.BL;
using TestApp.Core;
using TestApp.DI;

namespace TestApp.EnforcmentConsole
{
    public class IoCImpl
    {
        public IoCImpl()
        {
        }

        #region Public methods

        public void Configure()
        {
            IoCC.Instance.Register<IHttpGetRequestSender>(new HttpGetRequestSender());
        }

        #endregion Public methods
    }
}