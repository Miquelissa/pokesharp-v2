﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pokesharp.Base;

namespace Pokesharp.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20191205224120_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pokesharp.Models.Friendship", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("Enabled");

                    b.Property<int>("FriendID");

                    b.Property<int>("PlayerID");

                    b.HasKey("ID");

                    b.HasIndex("FriendID");

                    b.HasIndex("PlayerID");

                    b.ToTable("Friendship");
                });

            modelBuilder.Entity("Pokesharp.Models.Local", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("Enabled");

                    b.Property<string>("ImageID");

                    b.Property<string>("Name");

                    b.Property<int>("RegionID");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ID");

                    b.HasIndex("RegionID");

                    b.ToTable("Local");
                });

            modelBuilder.Entity("Pokesharp.Models.LocalPokemon", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("Enabled");

                    b.Property<int>("LocalID");

                    b.Property<int>("MaximumLevel");

                    b.Property<int>("MinimumLevel");

                    b.Property<int>("PokemonID");

                    b.Property<string>("Rarity");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ID");

                    b.HasIndex("LocalID");

                    b.HasIndex("PokemonID");

                    b.ToTable("LocalPokemon");
                });

            modelBuilder.Entity("Pokesharp.Models.Player", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("Enabled");

                    b.Property<int?>("MainPokedexPokemonID");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<int>("PokedexID");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.HasIndex("MainPokedexPokemonID");

                    b.HasIndex("PokedexID");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("Pokesharp.Models.Pokedex", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("Enabled");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ID");

                    b.ToTable("Pokedex");
                });

            modelBuilder.Entity("Pokesharp.Models.PokedexPokemon", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Catched");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("Enabled");

                    b.Property<int>("EncountersCount");

                    b.Property<int>("Level");

                    b.Property<string>("Nickname");

                    b.Property<string>("Notes");

                    b.Property<int?>("PokedexID");

                    b.Property<int?>("PokemonID");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ID");

                    b.HasIndex("PokedexID");

                    b.HasIndex("PokemonID");

                    b.ToTable("PokedexPokemon");
                });

            modelBuilder.Entity("Pokesharp.Models.Pokemon", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<bool>("Enabled");

                    b.Property<string>("ImageID");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ID");

                    b.ToTable("Pokemon");
                });

            modelBuilder.Entity("Pokesharp.Models.Region", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("Enabled");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ID");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("Pokesharp.Models.Friendship", b =>
                {
                    b.HasOne("Pokesharp.Models.Player", "Friend")
                        .WithMany()
                        .HasForeignKey("FriendID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokesharp.Models.Player")
                        .WithMany("Friendships")
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokesharp.Models.Local", b =>
                {
                    b.HasOne("Pokesharp.Models.Region")
                        .WithMany("Locals")
                        .HasForeignKey("RegionID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokesharp.Models.LocalPokemon", b =>
                {
                    b.HasOne("Pokesharp.Models.Local")
                        .WithMany("Pokemons")
                        .HasForeignKey("LocalID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Pokesharp.Models.Pokemon", "Pokemon")
                        .WithMany()
                        .HasForeignKey("PokemonID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokesharp.Models.Player", b =>
                {
                    b.HasOne("Pokesharp.Models.PokedexPokemon", "MainPokedexPokemon")
                        .WithMany()
                        .HasForeignKey("MainPokedexPokemonID");

                    b.HasOne("Pokesharp.Models.Pokedex", "Pokedex")
                        .WithMany()
                        .HasForeignKey("PokedexID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Pokesharp.Models.PokedexPokemon", b =>
                {
                    b.HasOne("Pokesharp.Models.Pokedex")
                        .WithMany("Pokemons")
                        .HasForeignKey("PokedexID");

                    b.HasOne("Pokesharp.Models.Pokemon", "Pokemon")
                        .WithMany()
                        .HasForeignKey("PokemonID");
                });
#pragma warning restore 612, 618
        }
    }
}
