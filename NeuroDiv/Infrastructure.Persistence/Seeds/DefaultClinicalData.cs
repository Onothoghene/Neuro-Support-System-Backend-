using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Seeds
{
    public static class DefaultClinicalData
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // ── Diagnosis Types 
            if (!context.DiagnosisType.Any())
            {
                var diagnosisTypes = new List<DiagnosisType>
                {
                    new() { Name = "Autism Spectrum Disorder",       Code = "ASD"  },
                    new() { Name = "Attention Deficit Hyperactivity Disorder", Code = "ADHD" },
                    new() { Name = "Dyslexia",                       Code = "DYS"  },
                    new() { Name = "Down Syndrome",                  Code = "DS"   },
                    new() { Name = "Cerebral Palsy",                 Code = "CP"   },
                    new() { Name = "Developmental Delay",            Code = "DD"   },
                    new() { Name = "Sensory Processing Disorder",    Code = "SPD"  },
                    new() { Name = "Speech and Language Disorder",   Code = "SLD"  },
                    new() { Name = "Intellectual Disability",        Code = "ID"   },
                    new() { Name = "Traumatic Brain Injury",         Code = "TBI"  },
                    new() { Name = "Other",                          Code = "OTH"  },
                };

                await context.DiagnosisType.AddRangeAsync(diagnosisTypes);
            }

            // ── Goal Categories
            if (!context.GoalCategory.Any())
            {
                var goalCategories = new List<GoalCategory>
                {
                    new() { Name = "Communication",     Description = "Speech, language, and communication skills" },
                    new() { Name = "Motor Skills",      Description = "Fine and gross motor development" },
                    new() { Name = "Behavioral",        Description = "Behavioral regulation and management" },
                    new() { Name = "Social",            Description = "Social interaction and relationship skills" },
                    new() { Name = "Academic",          Description = "Learning and academic performance" },
                    new() { Name = "Sensory",           Description = "Sensory processing and integration" },
                    new() { Name = "Self-Care",         Description = "Daily living and independence skills" },
                    new() { Name = "Emotional",         Description = "Emotional regulation and wellbeing" },
                };

                await context.GoalCategory.AddRangeAsync(goalCategories);
            }

            await context.SaveChangesAsync();
        }
    }
}