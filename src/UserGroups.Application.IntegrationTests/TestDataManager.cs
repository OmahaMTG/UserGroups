using System.Threading.Tasks;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests
{
    using static Testing;
    public class TestDataManager
    {
        public static async Task<Host> CreateTestHost(string blurb = "Test Blurb", string name = "Test Name", string contactInfo = "Test Contact Info", bool deleted = false)
        {
            return await AddAsync(new Host
            {
                Blurb = blurb,
                ContactInfo = contactInfo,
                IsDeleted = deleted,
                Name = name,
            });
        }

        public static async Task<Presenter> CreateTestPresenter(string name = "Test Name", string contactInfo = "TestContactInfo", string bio = "Test Bio", bool deleted = false)
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = contactInfo,
                IsDeleted = deleted,
                Name = name,
                Bio = bio
            });
        }

        public static async Task<Sponsor> CreateTestSponsor()
        {
            return await AddAsync(new Sponsor
            {
                Blurb = "Test Blurb",
                ContactInfo = "Test Contact Info",
                IncludeInBannerRotation = true,
                IsDeleted = false,
                Name = "Test Name",
                ShortBlurb = "Test Short Blurb",
                SponsorUrl = "http://test.url"
            });
        }


    }
}
