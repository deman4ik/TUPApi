using tupapiService.Helpers.DBHelpers;
using tupapiService.Models;

namespace tupapiService.Test.Infrastructure
{
    public class BaseControllerTest
    {
        protected readonly ITupapiContext TestContext;
        protected readonly TestDbPopulator TestDbPopulator;

        public BaseControllerTest()
        {
            TestContext = new TestTupContext();
            TestDbPopulator = new TestDbPopulator(TestContext);
        }
    }
}