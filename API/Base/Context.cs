using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Pokesharp.Models;

namespace Pokesharp.Base
{

    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options)
            : base(options) { }

        public DbSet<Player> Player
        {
            get;
            set;
        }

        public DbSet<Pokemon> Pokemon
        {
            get;
            set;
        }

        public DbSet<PokedexPokemon> PokedexPokemon
        {
            get;
            set;
        }

        public DbSet<Pokedex> Pokedex
        {
            get;
            set;
        }

        public DbSet<Region> Region
        {
            get;
            set;
        }

        public DbSet<Local> Local
        {
            get;
            set;
        }

        public DbSet<LocalPokemon> LocalPokemon
        {
            get;
            set;
        }

        public DbSet<Friendship> Friendship 
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}