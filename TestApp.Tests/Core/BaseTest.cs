using Moq;
using NUnit.Framework;
using TestApp.Core;
using TestApp.DI;
using Unity;
using Unity.Lifetime;

namespace TestApp.Tests.Core
{
    public class BaseTest
    {
        #region Properties
        protected UnityContainer _container;
        protected Mock<IHttpGetRequestSender> _iHttpGetRequestSender { get; set; }

        #endregion

        #region Constructors
        public BaseTest()
        {
            _container = new UnityContainer();
            //RegisterComponents();
        }


        #endregion

        #region Methods
        [OneTimeSetUp]
        public virtual void ClassSetup()
        {
            _iHttpGetRequestSender = new Mock<IHttpGetRequestSender>();

            _container.RegisterInstance(_iHttpGetRequestSender.Object);
        

            IoCC.Instance.Register<IHttpGetRequestSender>(_iHttpGetRequestSender.Object);

        }


        private void RegisterComponents()
        {
        }
        #endregion
    }
}
