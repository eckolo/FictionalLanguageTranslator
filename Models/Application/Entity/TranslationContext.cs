using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Entity
{
    public class TranslationContext : DbContext
    {
        public TranslationContext(DbContextOptions<TranslationContext> options)
            : base(options)
        {
        }

        public DbSet<TranslationRecord> records => Set<TranslationRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<TranslationRecord>(etBuilder => {
                    etBuilder.HasKey(record => new { record.japanese, record.fictional });
                });
        }
    }
}
