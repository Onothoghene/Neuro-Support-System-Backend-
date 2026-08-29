using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Seeds
{
    public static class DefaultSessionDurations
    {
        public static async Task SeedAsync(ApplicationDbContext appDbContext)
        {
            if (!appDbContext.SessionDuration.Any())
            {
                var durations = new List<SessionDuration>
                {
                    new() { Label = "30 minutes",  Minutes = 30  },
                    new() { Label = "45 minutes",  Minutes = 45  },
                    new() { Label = "60 minutes",  Minutes = 60  },
                    new() { Label = "90 minutes",  Minutes = 90  },
                    new() { Label = "120 minutes", Minutes = 120 },
                    new() { Label = "Custom",      Minutes = 0   },
                };

                await appDbContext.SessionDuration.AddRangeAsync(durations);
                await appDbContext.SaveChangesAsync();
            }
        }
    }
}