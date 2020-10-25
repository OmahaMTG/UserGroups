using NUnit.Framework;
using System.Threading.Tasks;

namespace UserGroups.Application.IntegrationTests
{
    using static Testing;

    public class TestBase
    {
        [TearDown]
        public async Task Cleanup()
        {
            await ResetState();
        }
    }
}
