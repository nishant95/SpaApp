#region Namespaces

using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SpaData.DataSeed;

#endregion

namespace SpaData.Context
{
    public static class SpaContextExtensions
    {
        #region Methods

        /// <summary>
        /// Check if all migrations have been applied to the context.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        /// <summary>
        /// Ensures that data is seeded if the Db is empty
        /// </summary>
        /// <param name="context"></param>
        public static void EnsureSeedData(this SpaContext context)
        {
            if (!context.AllMigrationsApplied()) return;
            if (!context.Person.Any())
            {
                context.Person.AddRange(PersonSeed.Persons);
                context.SaveChanges();
            }
        }

        #endregion

    }
}
