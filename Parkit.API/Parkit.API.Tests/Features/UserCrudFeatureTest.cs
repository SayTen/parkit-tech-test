using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Parkit.API.Tests.AutoFixture;
using Parkit.Core.Models;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Parkit.API.Tests.Features
{
    public class UserCrudFeatureTest
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private WebApplicationFactory<Startup> _webApplicationFactory;

        public UserCrudFeatureTest(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Theory]
        [AutoDomainData]
        public async Task UserControllerIsWiredUp(
            User user)
        {
            // Arrange
            user.Id = null;
            user.Created = null;
            user.Email = "test@sayten.me.uk";

            var client = _webApplicationFactory
                .CreateClient();

            // Act
            var createContent = JsonContent.Create(user);
            var createResponse = await client.PostAsync("/user", createContent);
            createResponse.EnsureSuccessStatusCode();
            var createUser = await createResponse.Content.ReadFromJsonAsync<User>();
            createUser.Should().NotBeNull();

            var getResponse = await client.GetAsync($"/user/{createUser.Id}");
            getResponse.EnsureSuccessStatusCode();
            var getUser = await getResponse.Content.ReadFromJsonAsync<User>();

            // Assert
            getUser.Should().BeEquivalentTo(createUser);
        }
    }
}
