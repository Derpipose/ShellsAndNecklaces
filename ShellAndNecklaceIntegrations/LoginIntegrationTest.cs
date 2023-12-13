using ShellsAndNecklacesApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellAndNecklaceIntegrations
{
    public class LoginIntegrationTest : BlazorIntegrationTestContext
    {
        [Fact]
        public async Task Login()
        {
            var cut = RenderComponent<LoginControl>();


        }
    }
}
