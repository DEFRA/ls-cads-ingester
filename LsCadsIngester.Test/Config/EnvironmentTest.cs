using Microsoft.AspNetCore.Builder;

namespace LsCadsIngester.Test.Config;

public class EnvironmentTest
{

   [Fact]
   public void IsNotDevModeByDefault()
   { 
       var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions());
       var isDev = LsCadsIngester.Config.Environment.IsDevMode(builder);
       Assert.False(isDev);
   }
}
