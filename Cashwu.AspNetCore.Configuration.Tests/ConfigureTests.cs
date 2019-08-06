using System;
using System.Collections.Generic;
using System.IO;
using ExpectedObjects;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Cashwu.AspNetCore.Configuration.Tests
{
    public class ConfigureTests
    {
        [Fact]
        public void SingleConfig()
        {
            GivenServiceProvider()
                    .GetRequiredService<SingleConfig>()
                    .Id.Should()
                    .Be(123);
        }

        [Fact]
        public void ArrayConfigStruct()
        {
            var config = GivenServiceProvider().GetRequiredService<List<int>>();

            new List<int> { 1, 2, 3 }.ToExpectedObject().ShouldEqual(config);
        }

        [Fact]
        public void ArrayConfig()
        {
            var config = GivenServiceProvider().GetRequiredService<List<ArrayConfigObject>>();

            new List<ArrayConfigObject>
                    {
                        new ArrayConfigObject
                        {
                            Id = 1,
                            Name = "AA"
                        },
                        new ArrayConfigObject
                        {
                            Id = 2,
                            Name = "BB"
                        },
                        new ArrayConfigObject
                        {
                            Id = 3,
                            Name = "CC"
                        }
                    }
                    .ToExpectedObject()
                    .ShouldEqual(config);
        }

        [Fact]
        public void Not_pass_IConfiguration()
        {
            Action action = () => new ServiceCollection().AddConfig(null, typeof(ConfigureTests).Assembly.FullName);

            action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be(nameof(IConfiguration));
        }

        private static ServiceProvider GivenServiceProvider()
        {
            return new ServiceCollection()
                   .AddConfig(GivenConfiguration(), typeof(ConfigureTests).Assembly.FullName)
                   .BuildServiceProvider();
        }

        private static IConfigurationRoot GivenConfiguration()
        {
            return new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("profile.json")
                   .Build();
        }
    }
}