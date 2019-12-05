using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pokesharp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokedex",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokedex", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageID = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PokedexPokemon",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PokedexID = table.Column<int>(nullable: true),
                    PokemonID = table.Column<int>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Nickname = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Catched = table.Column<bool>(nullable: false),
                    EncountersCount = table.Column<int>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokedexPokemon", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PokedexPokemon_Pokedex_PokedexID",
                        column: x => x.PokedexID,
                        principalTable: "Pokedex",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PokedexPokemon_Pokemon_PokemonID",
                        column: x => x.PokemonID,
                        principalTable: "Pokemon",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Local",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RegionID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ImageID = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Local", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Local_Region_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Region",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PokedexID = table.Column<int>(nullable: false),
                    MainPokedexPokemonID = table.Column<int>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Player_PokedexPokemon_MainPokedexPokemonID",
                        column: x => x.MainPokedexPokemonID,
                        principalTable: "PokedexPokemon",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Player_Pokedex_PokedexID",
                        column: x => x.PokedexID,
                        principalTable: "Pokedex",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocalPokemon",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LocalID = table.Column<int>(nullable: false),
                    PokemonID = table.Column<int>(nullable: false),
                    Rarity = table.Column<string>(nullable: true),
                    MinimumLevel = table.Column<int>(nullable: false),
                    MaximumLevel = table.Column<int>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalPokemon", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LocalPokemon_Local_LocalID",
                        column: x => x.LocalID,
                        principalTable: "Local",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocalPokemon_Pokemon_PokemonID",
                        column: x => x.PokemonID,
                        principalTable: "Pokemon",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Friendship",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlayerID = table.Column<int>(nullable: false),
                    FriendID = table.Column<int>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendship", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Friendship_Player_FriendID",
                        column: x => x.FriendID,
                        principalTable: "Player",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendship_Player_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Player",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_FriendID",
                table: "Friendship",
                column: "FriendID");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_PlayerID",
                table: "Friendship",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Local_RegionID",
                table: "Local",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_LocalPokemon_LocalID",
                table: "LocalPokemon",
                column: "LocalID");

            migrationBuilder.CreateIndex(
                name: "IX_LocalPokemon_PokemonID",
                table: "LocalPokemon",
                column: "PokemonID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_MainPokedexPokemonID",
                table: "Player",
                column: "MainPokedexPokemonID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_PokedexID",
                table: "Player",
                column: "PokedexID");

            migrationBuilder.CreateIndex(
                name: "IX_PokedexPokemon_PokedexID",
                table: "PokedexPokemon",
                column: "PokedexID");

            migrationBuilder.CreateIndex(
                name: "IX_PokedexPokemon_PokemonID",
                table: "PokedexPokemon",
                column: "PokemonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friendship");

            migrationBuilder.DropTable(
                name: "LocalPokemon");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Local");

            migrationBuilder.DropTable(
                name: "PokedexPokemon");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "Pokedex");

            migrationBuilder.DropTable(
                name: "Pokemon");
        }
    }
}
