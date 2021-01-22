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

        public static async Task<Sponsor> CreateAureusItSponsor()
        {
            return await AddAsync(new Sponsor
            {

                ContactInfo = "Contact Using Email fake@Example.com",
                IncludeInBannerRotation = false,
                IsDeleted = false,
                Name = "Aureus IT",
                Blurb = @"
Aureus IT is a leading provider of recruitment and career solutions. We forge a path to success for professionals seeking career opportunities and for companies in need of stand-out talent - uncovering the people and the positions that are the most sought after in the marketplace. 

More than ever before, companies and firms rely on  IT professionals who are proficient, creative in developing solutions, adaptable to rapid change and are ready for what’s to come. At Aureus IT, we understand the nuances of recruiting in the technology sector and the complexities of this dynamic arena. We bring a dedicated team of people-centered recruiters focused solely on pinpointing careers in the technology sector that align with your skill set, career aspirations, goals, and present the right culture fit. 

Working with Aureus IT means you’ll have a partner with the experience, business connections, and methodologies to give you an advantage in a competitive market and best position you for success. We’ll execute a complete search on your behalf while providing you with individualized service and communication throughout the process. 

The strength of technology lies with the people behind it. Power your search forward with Aureus IT. 
                        ",
            });
        }

        public static async Task<Sponsor> CreateFarmCreditServicesOfAmericaSponsor()
        {
            return await AddAsync(new Sponsor
            {

                ContactInfo = "Contact Using Email fake@Example.com",
                IncludeInBannerRotation = false,
                IsDeleted = false,
                Name = "Farm Credit Services of America",
                Blurb = @"
Farm Credit Services of America is a great place to work. You see it in our people and the relationships they have with each other and our customers. Our passion and commitment to serving both rural America and each other is key to our success in the marketplace. That is why we seek highly motivated, positive-thinking people who foster honesty and integrity – the core values that guide how we work and treat others. Truth and ethics allow us not only to be proud of our success, but also to be proud of the way it is achieved.

Benefits include:

 - Generous Time Away. Teammates start with
   - 18 vacation days which is increased over years of service
   - 7 holidays and 15 sick leave days
 - Insurance.  Medical, dental, life and disability.
 - 401k.  Dollar-for-dollar match on the first 6% invested PLUS a 3% employer contribution.  That means if you put in 6%, FCSAmerica puts in another 9%.
 - Donation Match.  Dollar-for-dollar up to $250/year.
 - Parental leave Program.  Six weeks of 100% paid leave for parents following the birth or placement of an adopted child.
 - For more details, visit http://www.farmcredit.info/FCSABenefits2019/

Farm Credit Services of America is well established in software development and is growing to 15 application development teams.  We believe in giving back to the development community through sponsoring and hosting events that encourage networking and continual learning.

Farm Credit Services of America has Application Developer job postings available.

To learn more, visit [www.fcsamerica.com/careers](http://www.fcsamerica.com/careers)
                        ",
            });
        }

        public static async Task<Sponsor> CreateBuildertrendSponsor()
        {
            return await AddAsync(new Sponsor
            {

                ContactInfo = "Contact Using Email fake@Example.com",
                IncludeInBannerRotation = false,
                IsDeleted = false,
                Name = "Buildertrend ",
                Blurb = @"
Buildertrend is a cutting-edge, cloud-based project management software. With nearly 1 million users across the globe, we empower the construction industry with a better way to build. At Buildertrend, our employees are driven by a collective winning attitude, opportunities for career growth, and the genuine connections experienced every day. We have a relentless focus on continuously improving, winning together and celebrating our successes. To be a member of the Buildertrend team means coming to work every day with high expectations, unbelievable drive and a great attitude. Together, we achieve the impossible.
                        ",
            });
        }

        public static async Task<Sponsor> CreateDiversifiedSolutionsSponsor()
        {
            return await AddAsync(new Sponsor
            {

                ContactInfo = "Contact Using Email fake@Example.com",
                IncludeInBannerRotation = false,
                IsDeleted = false,
                Name = "Diversified Solutions ",
                Blurb = @"
Diversified Solutions is a leader in Information Technology Consulting and permanent placement opportunities since 1997.  We provide the most discreet and professional transitions for people in the pursuit of new career opportunities in Information Technology.  

Diversified Solutions focuses on working with local clients – from Fortune 500 companies to small start-ups and everything in between.  We provide consulting, option-to-hire and permanent IT job opportunities with relationship-based recruiting focused on improving your career.  For us, it’s about people, not pressure.  It’s about you, not us.  We treat you like you’ve always wanted to be treated by an IT recruiter!

                        ",
            });
        }

        public static async Task<Sponsor> CreateHarbingerPartnersSponsor()
        {
            return await AddAsync(new Sponsor
            {

                ContactInfo = "Contact Using Email fake@Example.com",
                IncludeInBannerRotation = false,
                IsDeleted = false,
                Name = "Harbinger Partners, Inc",
                Blurb = @"
Harbinger Partners, Inc. (aka Bass & Associates) provides high-quality technology consulting, contract-to-hire, and permanent placements to businesses in and around the Omaha, St. Paul/Minneapolis and Dallas areas. Established in 1999, we offer strategic IT consulting and outsourcing services designed to streamline internal and external business processes and communications. Our areas of expertise include Project Management, Business Analysis, Development and Architecture (both Java and .NET), Quality Assurance, and Data Warehousing.

Our goal is simple: to partner with our clients & candidates for their success, and to delight them in the process.
                        ",
            });
        }

        public static async Task<Sponsor> CreateAdvantageTechSponsor()
        {
            return await AddAsync(new Sponsor
            {

                ContactInfo = "Contact Using Email fake@Example.com",
                IncludeInBannerRotation = false,
                IsDeleted = false,
                Name = "Advantage Tech",
                Blurb = @"
Advantage Tech is a leader in technical and professional staffing and recruiting. By listening to our clients and understanding their needs — whether it’s contract, contract-to-hire or permanent openings — we are able to provide well-qualified professionals that are essential to your mission. Advantage Tech strives to create long-term partnerships, helping you reach your goals by hiring the right people.  Advantage Tech has been in the KC market for over 22 years and proud to be a part of the Omaha community for the last year.
                        ",
            });
        }

        public static async Task<Sponsor> CreateBlueCrossBlueShieldOfNebraskaSponsor()
        {
            return await AddAsync(new Sponsor
            {

                ContactInfo = "Contact Using Email fake@Example.com",
                IncludeInBannerRotation = false,
                IsDeleted = false,
                Name = "Blue Cross and Blue Shield of Nebraska",
                Blurb = @"
Blue Cross and Blue Shield of Nebraska (BCBSNE) is more than just an insurance company with a solid foundation; our sights are set on reinventing what health care can and should be. As the industry rapidly evolves and we seek ways to optimize business processes and customer experiences, there’s no greater time for forward-thinking professionals like you to join us.

Our employees champion change and are inspired to transform the communities we serve every day. As a member of “the Blues Crew,” you’ll find purpose, opportunities and the support you need to build a meaningful career. Learn more about our culture and what makes BCBSNE such an exceptional place to work by visiting [www.NebraskaBlue.com/Careers](http://www.NebraskaBlue.com/Careers).

Our Information Services teams work with new technologies, designing and implementing highly-scalable, complex integrations that improve usability and increase performance. You’ll find opportunities rather than boundaries in this role!  We are focused on serving our customers, acting as trusted partners who deliver high-quality, effective solutions. We are positive, collaborative, committed, engaged and fun.
                        ",
            });
        }

        public static async Task<Sponsor> CreateKiewitSponsor()
        {
            return await AddAsync(new Sponsor
            {

                ContactInfo = "Contact Using Email fake@Example.com",
                IncludeInBannerRotation = false,
                IsDeleted = false,
                Name = "Kiewit",
                Blurb = @"
Kiewit Technology Group’s (KTG) mission is to deliver project schedule and cost certainty by employing technology designed by and for the construction industry. Our team deploys apps to the field that increase profitability by maximizing the way we use our people and resources in daily operations. KTG uses the Kiewit Management System (KMS), which includes systems and tools that manage every part of Kiewit’s business and lifecycle of a project, to improve planning and day-to-day execution in the field by giving our people real-time data to make faster, smarter decisions. Every day our employees make an impact on the efficiency of the iconic projects Kiewit builds and you can too. 

Check out our benefits here: [https://1drv.ms/b/s!Ap4GTt9ubzRMjMtEVamx7npHtmhXtw](https://1drv.ms/b/s!Ap4GTt9ubzRMjMtEVamx7npHtmhXtw)

Find your next career with Kiewit here: [https://kiewitcareers.kiewit.com/go/Information-Technology/875600/](https://kiewitcareers.kiewit.com/go/Information-Technology/875600/)
                        ",
            });
        }
    }
}
