using System.Threading.Tasks;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.IntegrationTests.TestData
{
    using static Testing;
    public class HostTestDataManager
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

        public static async Task<Host> CreateBuildertrendHost()
        {
            return await AddAsync(new Host
            {
                Blurb = "[11818 I St](https://goo.gl/maps/UpbHSe9ubyop11du8)",
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Buildertrend ",
            });
        }

        public static async Task<Host> CreateModernWorkHost()
        {
            return await AddAsync(new Host
            {
                Blurb = "[8790 F Street](https://goo.gl/maps/UeoDCmByhWSzbZKM7)",
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Modern Work",
            });
        }

        public static async Task<Host> CreateBlueCrossBlueShieldOfNebraskaHost()
        {
            return await AddAsync(new Host
            {
                Blurb = "[1919 Ak-Sar-Ben Dr](https://goo.gl/maps/rTUj2GfAq2VTBR7w5)",
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Blue Cross Blue Shield of Nebraska",
            });
        }

        public static async Task<Host> CreateKiewitUniversityHost()
        {
            return await AddAsync(new Host
            {
                Blurb = @"
1450 Mike Fahey St  
[https://goo.gl/maps/4f6oZbBVqfn](https://goo.gl/maps/4f6oZbBVqfn)  
Parking is available to the north of the building in the marked Kiewit stalls and there is metered parking on the street.  Additional parking is also available at the [Clearway Energy parking lot](https://odnug.blob.core.windows.net/odnug/Clearway%20Parking%20Map.pdf?st=2020-02-04T19%3A29%3A25Z&se=2021-02-05T19%3A29%3A00Z&sp=rl&sv=2018-03-28&sr=b&sig=M1g0xlCeH0z3f6Sqzvszq0FPZcJ4FLWKM0WBzJXaxXo%3D).  [Additional Parking Image](https://odnug.blob.core.windows.net/odnug/KU%20Parking.png?st=2020-02-04T21%3A00%3A31Z&se=2021-02-05T21%3A00%3A00Z&sp=rl&sv=2018-03-28&sr=b&sig=sq4i7Kmcg2nNZYhWTusLRn0q%2BMSndbdbHiRYekDp7ZM%3D)
                        ",
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Kiewit University",
            });
        }

        public static async Task<Host> CreateFarmCreditServicesOfAmericaHost()
        {
            return await AddAsync(new Host
            {
                Blurb = "[5015 S 118th Street](https://goo.gl/maps/aVKVHnJDZAVxAtrM9)",
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Farm Credit Services of America",
            });
        }

        public static async Task<Host> CreateYouTubeHost()
        {
            return await AddAsync(new Host
            {
                Blurb = "[YouTube](https://youtu.be/-uAX9OD8PN8)",
                IsDeleted = false,
                Name = "YouTube",
            });
        }
    }
}
