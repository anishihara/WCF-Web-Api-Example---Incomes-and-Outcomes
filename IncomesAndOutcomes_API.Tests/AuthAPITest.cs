using IncomesAndOutcomes_API.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using IncomesAndOutcomes_API.Resources;
using IncomesAndOutcomes_API.Models;
using IncomesAndOutcomes_API.Tests.RepositoryMockups;

namespace IncomesAndOutcomes_API.Tests
{
    
    
    /// <summary>
    ///This is a test class for AuthAPITest and is intended
    ///to contain all AuthAPITest Unit Tests
    ///</summary>
    [TestClass()]
    public class AuthAPITest
    {

        UserRepositoryMockup userRepository = new UserRepositoryMockup();
        UserSessionRepositoryMockup userSessionRepository = new UserSessionRepositoryMockup();
        Guid actualUserSessionId;

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for LogOn
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
       // [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\dev\\WCF-Web-Api-Example---Incomes-and-Outcomes\\IncomesAndOutcomes_API", "/")]
       // [UrlToTest("http://localhost:49160/")]
        public void LogOnTest()
        {

            AuthAPI target = new AuthAPI(userRepository, userSessionRepository);
            UserResource userResource = new UserResource { Email=@"nishihara@gmail.com", Password="Teste" }; 
            Guid actual;
            actual = target.LogOn(userResource);
            actualUserSessionId = actual;
            Assert.IsFalse(actual == Guid.Empty);
            var userSession = userSessionRepository.Find(actual);
            Guid expected = userSession.Id;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AuthAPI Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\dev\\WCF-Web-Api-Example---Incomes-and-Outcomes\\IncomesAndOutcomes_API", "/")]
        [UrlToTest("http://localhost:49160/")]
        public void AuthAPIConstructorTest()
        {
            IUserRepository userRepository = new UserRepositoryMockup(); // TODO: Initialize to an appropriate value
            IUserSessionRepository userSessionRepository = new UserSessionRepositoryMockup(); // TODO: Initialize to an appropriate value
            AuthAPI target = new AuthAPI(userRepository, userSessionRepository);
        }

        /// <summary>
        ///A test for AuthAPI Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\dev\\WCF-Web-Api-Example---Incomes-and-Outcomes\\IncomesAndOutcomes_API", "/")]
        [UrlToTest("http://localhost:49160/")]
        public void AuthAPIConstructorTest1()
        {
            AuthAPI target = new AuthAPI();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for LogOff
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
       // [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\dev\\WCF-Web-Api-Example---Incomes-and-Outcomes\\IncomesAndOutcomes_API", "/")]
       // [UrlToTest("http://localhost:49160/")]
        public void LogOffTest()
        {
            AuthAPI target = new AuthAPI(userRepository,userSessionRepository); // TODO: Initialize to an appropriate value
            Guid UserSession = new Guid(); // TODO: Initialize to an appropriate value
            
            //Caso em que não há sesssionId
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.LogOff(UserSession);
            Assert.AreEqual(expected, actual);

            // Caso em que existe sessionId e é deslogado com sucesso
            expected = true;
            var userSession = userSessionRepository.All.GetEnumerator();
            actual = target.LogOff(userSession.Current.Id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
