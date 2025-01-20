using Microsoft.AspNetCore.Mvc.Testing;

namespace CargoHub.Tests.IntegrationTests
{
    public class RunTests
    {
        [Fact]
        public void RunIntegrationTests()
        {
            var getIntegrationTests = new GetIntegrationTests(new WebApplicationFactory<Program>());
            getIntegrationTests.Test_Get_Id_Endpoints().Wait();
            var postIntegrationTests = new PostIntegrationTests(new WebApplicationFactory<Program>());
            postIntegrationTests.Test_Post_Id_Endpoints().Wait();
            var putIntegrationTests = new PutIntegrationTests(new WebApplicationFactory<Program>());
            putIntegrationTests.Test_Put_Id_Endpoints().Wait();
            var deleteIntegrationTests = new DeleteIntegrationTests(new WebApplicationFactory<Program>());
            deleteIntegrationTests.Test_Delete_Id_Endpoints().Wait();
        }
    }
}