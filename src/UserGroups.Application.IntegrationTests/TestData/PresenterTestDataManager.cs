using System.Threading.Tasks;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.TestData
{
    using static Testing;
    public class PresenterTestDataManager
    {
        public static async Task<Presenter> CreateAndyPresenter()
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Andy",
                Bio = @"
Andy has been part of the Omaha .Net community for over 15 years and is currently a Principal Architect at Buildertrend, which is the leading construction management platform for small to mid-size home builders and contractors. Throughout the years, he has presented at several local conferences as well as the .Net user group. More recently, his focus has been on cloud roll-outs (Azure/GCP), system integration, services, and DevOps.  
"
            });
        }


    }
}
