using FluentAssertions;
using Golden.Raspberry.Awards.Api.Domain.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Golden.Raspberry.Awards.Api;
using Org.BouncyCastle.Asn1.Cmp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;

namespace Golden.Raspberry.Awards.Api.Test
{
    public class ProducerTest
    {
        private readonly RestClient _client;
        private readonly IConfigurationRoot _configuration;

        public ProducerTest()
        {
            _configuration = GetIConfigurationRoot();
            _client = new RestClient(_configuration["RootApiUrl"]);
        }

        [Fact]
        public void GetProducer()
        {
            var restRequest = new RestRequest("v1/Producer");
            var restResponse = _client.Execute<ProducerResponseViewModel>(restRequest);

            Assert.NotNull(restResponse);
            Assert.True(restResponse.IsSuccessStatusCode);

            var producerResponseViewModel = restResponse.Data;

            producerResponseViewModel.Min.Should().HaveCount(1);
            producerResponseViewModel.Max.Should().HaveCount(1);

            producerResponseViewModel.Min.First().Interval.Should().Be(1);
            producerResponseViewModel.Max.First().Interval.Should().Be(13);
        }


        public static IConfigurationRoot GetIConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }
    }
}