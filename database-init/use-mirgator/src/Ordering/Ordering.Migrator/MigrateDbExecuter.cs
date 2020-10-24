using System;
using Microsoft.EntityFrameworkCore;
using Ordering.EFCore;
using Ordering.EFCore.Seed;

namespace Ordering.Migrator
{
    public class MigrateDbExecuter
    {
        private readonly Log _log;
        private readonly string _connectionString;

        public MigrateDbExecuter(string connectionString)
        {
            _log = new Log();
            _connectionString = connectionString;
        }

        public bool Run()
        {
            _log.Write("Database: " + _connectionString);
            _log.Write("Database migration started...");

            try
            {
                var builder = new DbContextOptionsBuilder<OrderingDbContext>().UseSqlServer(_connectionString);
                var migrator = new OrderingDbMigrator(new OrderingDbContext(builder.Options));

                migrator.CreateOrMigrate(SeedHelper.SeedDb);
            }
            catch (Exception ex)
            {
                _log.Write("An error occured during migration of database:");
                _log.Write(ex.ToString());
                _log.Write("Canceled migrations.");
                return false;
            }

            _log.Write("Database migration completed.");

            return true;
        }
    }
}
