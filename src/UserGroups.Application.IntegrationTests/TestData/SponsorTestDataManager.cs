using System.Threading.Tasks;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.TestData
{
    using static Testing;
    public class SponsorTestDataManager
    {
        public static async Task<Sponsor> CreateDontPanicLabsSponsor()
        {
            return await AddAsync(new Sponsor
            {

                ContactInfo = "Contact Using Email fake@Example.com",
                IncludeInBannerRotation = false,
                IsDeleted = false,
                Name = "Don’t Panic Labs",
                Blurb = @"
Don’t Panic Labs builds software and transforms development teams to make innovative ideas a reality. Their industry-recognized processes and know-how fuel their early-stage software product development. How they approach new software creation demonstrates that accurate estimates, better timelines, and consistently successful outcomes are possible. By applying proper engineering principles, they are able to architect products to withstand the largest constant in the software world: change. This lays a foundation for future growth in a way that the “rebuild it later” approach can never accomplish. Don’t Panic Labs is transforming the software development ecosystem by building software products for organizations of all sizes, working alongside existing product development teams, educating talent at all levels of experience, and evangelizing effective industry principles.

Don’t Panic Labs launched in 2010 as the software development arm of Nebraska Global. Their intergalactic headquarters are located in the Historic Haymarket District of Lincoln, Nebraska.
                        ",
            });
        }


    }
}
