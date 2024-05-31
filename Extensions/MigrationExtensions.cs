using Microsoft.EntityFrameworkCore;
using pokemonapi.Data;

namespace pokemonapi.Extentions {

  public static class MigrationExtensions 
  {
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
      using IServiceScope scope = app.ApplicationServices.CreateScope();

      using DataContext dbContext = 
      scope.ServiceProvider.GetRequiredService<DataContext>();

      dbContext.Database.Migrate();
    }
  }
  
}