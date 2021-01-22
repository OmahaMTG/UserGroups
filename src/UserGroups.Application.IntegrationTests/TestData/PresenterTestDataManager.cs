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

        public static async Task<Presenter> CreateMattRuwePresenter()
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Matt Ruwe",
                Bio = @"
Matt has been building software systems in the Omaha area for more than 20 years.  He has worked in a variety of industries including finance, agri-business, e-commerce, genetic-engineering, real estate, and most recently, construction.  Matt has been leading the Omaha .NET User's Group since 2008 and has organized approximately 130 meetings over the years.  He is passionate about helping people learn .NET and related technologies.  Matt has been working for Kiewit as an Application Architect for the past 2 years and worked as a consultant prior to that.  Matt has been married to his beautiful wife Charla for nearly 20 years and they have 4 children.
"
            });
        }

        public static async Task<Presenter> CreateMarcusKernPresenter()
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Marcus Kern",
                Bio = @"
Marcus has 10 years of .NET experience. He is a professional developer, hobbyist, tinkerer, and a F.U.N. person.  His passion is technology and his joy is solving problems. He achieves this by using a unique thought process that combines creativity, ingenuity, quickly navigating decision trees and not stopping at the first solution that presents itself. He strongly believes that you truly don’t know something until you can successfully teach it.
"
            });
        }

        public static async Task<Presenter> CreateJonathanAyoubPresenter()
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Jonathan Ayoub",
                Bio = @"
Jonathan Ayoub is a full-stack Software Developer with over 7 years of experience who has focused on Object-Oriented Programming in the Microsoft .NET ecosystem. He has worked in the Insurance, Finance, and Energy industries, and currently works for Diversified Financial Services, an agricultural lender. Jonathan enjoys fitness-related activities, reading, studying programming topics, playing the guitar, and hanging out with his wife and two kids.
"
            });
        }

        public static async Task<Presenter> CreateVaibhavGujralPresenter()
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Vaibhav Gujral",
                Bio = @"
Vaibhav Gujral has over 13 years of experience around designing and developing enterprise-class applications using Microsoft technologies and is currently working at Kiewit as a Cloud Architect. He started his career with VB.NET based desktop applications and moved on to ASP.Net based web applications. He has been working with Microsoft Azure since 2010 and is a Microsoft Certified Azure Architect. At different times in his career, he has worked in the capacity of an enterprise architect, solution architect, project lead, tech lead, and senior developer. He has worked across multiple industries including insurance, banking, finance, engineering, healthcare, and construction. In his free time, he likes to spend time with his family and he can be reached at gujral.vaibhav@gmail.com.
"
            });
        }

        public static async Task<Presenter> CreateJavierLozanoPresenter()
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Javier Lozano",
                Bio = @"
Javier is CEO and Founder of Lozanotek, Inc., a .NET focused software development boutique based in Des Moines, IA. His specializations are in ASP.NET, Azure, system design, and developer mentoring. Javier is also a Certified Scrum Master (CSM), Certified Scrum Product Owner (CSPO), GitHub Certified Trainer, an ASP.NET Insider, Azure Insider, and has been awarded the Microsoft MVP Award for over 14 years for all of his contributions to products and community. For fun, Javier runs and manages the popular .NET Conf virtual conference and the Iowa .NET User Group. He’s is an avid supporter of the community and likes to give back by speaking at user groups, local/regional/national .NET events. In his spare time, Javier loves spending time with his family and enjoys writing about himself in the third person.
"
            });
        }

        public static async Task<Presenter> CreateAdamKnodelPresenter()
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Adam Knodel",
                Bio = @"
Adam Knodel is an avid programmer, hardware enthusiast and gamer looking to improve the technical world by solving complex issues, primarily in programming.  Constantly learning new techniques and strategies to scale software systems for consumers worldwide.  Over a decade of programming has provided background in programming styles such as functional and object oriented, architecture, frontend development, and scripting.  Always looking to learn and use new tools, frameworks and programming techniques to improve career, personal life, and Destiny.  Fight forever guardians!
"
            });
        }

        public static async Task<Presenter> CreateRichKalaskyPresenter()
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Rich Kalasky",
                Bio = @"
Rich Kalasky is a software architect looking to learn as much as possible from the people I work with. Experience with a wide array of software development tools. Interested in new technology and new business ideas.
"
            });
        }

        public static async Task<Presenter> CreateJeffBramwellPresenter()
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Jeff Bramwell",
                Bio = @"
Jeff Bramwell is Vice President - Solutions Architecture with Farm Credit Services of America. He has over 20 years of software development experience and has been working with .NET technologies since the early pre-release days. Having focused on Visual Studio and Azure DevOps for several years now, Jeff has presented at multiple user groups and conferences. Jeff is a Microsoft Visual Studio and Development Technologies MVP and makes every attempt to post useful information on his blog at [https://blog.devmatter.com/](https://blog.devmatter.com/). Follow him on twitter at @jbramwell. 
"
            });
        }

        public static async Task<Presenter> CreateTonyWilsmanPresenter()
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "TonyWilsman",
                Bio = @"
**Tony Wilsman** is a Software Architect with over fifteen years of experience creating solutions for a wide variety of clients and platforms. He is a firm believer in cloud-based solutions, and is intrigued — but not entirely convinced —  by the promise of 'Serverless.' When he's not working, Tony is usually mitigating the damage done to his home by three kids and a dog.
"
            });
        }

        public static async Task<Presenter> CreateMattWillPresenter()
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Matt Will",
                Bio = @"
**Matt Will** is a Software Architect who has a great passion for developing quality software. He has been developing software for a while and his love of writing code was reignited when he first learned about IDesign. Being a code wrangler means he spends a lot of time looking at bad code and knowing that there has to be a better way to do it. Since joining the team, Matt has worked on a wide variety of projects. Most recently, his interests in building his own mobile applications have grown based on what he has learned working with partners on their mobile apps. Outside of work, Matt loves spending time outdoors including camping, hiking, running, and bicycling. He is also a dog lover as proven by his ever-growing collection of bite marks and scratches from his dogs.
"
            });
        }

        public static async Task<Presenter> CreateDustinHornePresenter()
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "Dustin Horne",
                Bio = @"
**Dustin Horne** is a developer with experience in consulting and leadership roles, most recently as a Squad Lead at Spreetail.  He is a dynamic developer with a background in architecture, backend, and frontend development.  Dustin has developed a popular Unity asset which has been widely used development projects across all platforms and in titles such as Pokemon, The Long Dark, and Google's Poly API.  When not on the job he enjoys time with his family and friends, with a particular appreciation of taking Eric's money in poker.
"
            });
        }

        public static async Task<Presenter> CreateEricPetersPresenter()
        {
            return await AddAsync(new Presenter
            {
                ContactInfo = "Contact Using Email fake@Example.com",
                IsDeleted = false,
                Name = "",
                Bio = @"
**Eric Peters** is a developer with experiences primarily in startups and consulting roles.  His current role is as the Director of IT at SportsSense, a Nashville, TN based startup, where he supports development of technology products focused on measuring the cognitive ""intangibles"" of professional and amateur athletes.  Eric has developed several academic and commercial applications using DirectX and UWP targeting Desktop and Xbox platforms.  He spends his free-time with his family, and aspires to become a woodworker (but really just likes buying more tools).
"
            });
        }
    }
}
