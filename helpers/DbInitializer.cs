using System.Linq;
using leashApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace leashed.helpers
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer (IServiceScopeFactory scopeFactory) {
            this._scopeFactory = scopeFactory;
        }

        public void Initialize () {
            using (var serviceScope = _scopeFactory.CreateScope ()) {
                using (var context = serviceScope.ServiceProvider.GetService<ParkContext> ()) {
                    context.Database.Migrate ();
                }
            }
        }

        public void SeedData () {
            using (var serviceScope = _scopeFactory.CreateScope ()) {
                using (var context = serviceScope.ServiceProvider.GetService<ParkContext> ()) {

                    //add parks
                    var parks = LoadParkData.loadData();
                    if(context.ParkItems.Any(x => x.Name == parks[0].Name)) return;
                    context.ParkItems.AddRange(parks);
                    context.SaveChanges ();
                    return;
                    
                }
            }
        }
    }
}