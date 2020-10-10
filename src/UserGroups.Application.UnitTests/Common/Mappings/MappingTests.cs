using AutoMapper;
using NUnit.Framework;
using System;
using UserGroups.Application.Common.Mappings;
using UserGroups.Application.UserGroups.Sponsors.Queries;
using UserGroups.Domain.Entities;

namespace UserGroups.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Test]
        [TestCase(typeof(Sponsor), typeof(SponsorDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }
    }
}
