using System.Threading.Tasks;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.TestData
{
    using static Testing;
    public static class HostTestDataManager
    {
        public static async Task<Host> CreateCandAHost()
        {
            return await AddAsync(new Host
            {
                Blurb = @"
[13609 California Street](https://goo.gl/maps/k4WB8jsFqogch5Ls7)  
Webster Suite (second floor through the back doors and to the left -  you can use the stairs or elevator)  
Park in the visitor parking, or feel free to park anywhere in the parking lot.   
                        ",
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "C&A Plaza",
            });
        }

    }
}
