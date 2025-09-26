using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentManagement.Data;

using StudentManagement.Data;

using var host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddDbContext<SchoolContext>(options =>
        options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudentManagementDB;Trusted_Connection=True;"));
}).Build();

using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SchoolContext>();
    db.Database.Migrate();
}

Console.WriteLine("V1 Schema Ready");