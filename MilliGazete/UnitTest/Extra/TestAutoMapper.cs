using AutoMapper;
using Business.AutoMapper;
using System;
using System.Linq;

namespace UnitTest.Extra
{
    public class TestAutoMapper
    {
        public readonly IMapper _mapper;

        public TestAutoMapper()
        {
            var profiles = typeof(AutoMapperConfiguration).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));

            var mockMapper = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
                }
            });
            _mapper = mockMapper.CreateMapper();
        }
    }
}
