using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pokesharp.Data;
using Pokesharp.Game;
using Pokesharp.Game.Models;
using Pokesharp.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Pokesharp.Controllers 
{

    public static class GameState 
    {
        public static int EncounterID = 0;
        public static List<Encounter> Encounters = new List<Encounter>();
    }

    [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {

        private readonly Players Players;
        private readonly Locals Locals;
        private readonly Pokedexes Pokedexes;

        public GameController(Locals locals, Players players, Pokedexes pokedexes) {
            Locals = locals;
            Players = players;
            Pokedexes = pokedexes;
        }

        [HttpPost]
        [Route("encounter")]
        public IActionResult Encounter(PokemonSearch pokemonSearch) {
            Local local = Locals.FindOneByID(pokemonSearch.LocalID);
            Player player = Players.FindOneByID(pokemonSearch.PlayerID);
     
            if(local != null && player != null) {
                Encounter encounter = new Encounter();
                encounter.ID = ++GameState.EncounterID;
                encounter.LocalID = pokemonSearch.LocalID;
                encounter.PlayerID = pokemonSearch.PlayerID;

                PokemonEncountered pokemonEncountered = PokemonEncounter.FindPokemon(local);
                encounter.PokemonEncountered = pokemonEncountered;

                if(pokemonEncountered != null) {
                    encounter.PokemonAlreadyCaught = player.Pokedex.Pokemons.FirstOrDefault(pokedexPokemon => pokedexPokemon.PokemonID == pokemonEncountered.LocalPokemon.PokemonID && pokedexPokemon.Enabled) != null;
                    encounter.CanBattle = player.CatchedAnyPokemon();
                }

                GameState.Encounters.Add(encounter);
                return Ok(encounter);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("catch/{encounterId?}")]
        public IActionResult Catch(int encounterId) {
            Encounter encounter = GameState.Encounters.FirstOrDefault(_encounter => _encounter.ID == encounterId);

            Local local = Locals.FindOneByID(encounter.LocalID);
            Player player = Players.FindOneByID(encounter.PlayerID);

            if (local != null && player != null) {
                bool caught = PokemonEncounter.TryCatch(encounter.PokemonEncountered);

                if (caught) {
                    PokedexPokemon pokemonCaught = player.Pokedex.Pokemons.FirstOrDefault(pokedexPokemon => pokedexPokemon.PokemonID == encounter.PokemonEncountered.LocalPokemon.PokemonID && pokedexPokemon.Enabled);

                    if (pokemonCaught == null) {
                        pokemonCaught = new PokedexPokemon();
                        pokemonCaught.PokemonID = encounter.PokemonEncountered.LocalPokemon.PokemonID;
                        pokemonCaught.EncountersCount = 1;
                        pokemonCaught.Level = encounter.PokemonEncountered.Level;
                    } else {
                        pokemonCaught.Level = encounter.PokemonEncountered.Level;
                        pokemonCaught.EncountersCount = pokemonCaught.EncountersCount + 1;
                    }

                    pokemonCaught.Catched = true;
                    pokemonCaught.Enabled = true;

                    if (!encounter.PokemonAlreadyCaught) {
                        pokemonCaught = Pokedexes.AddPokemon(pokemonCaught, player.Pokedex);
                    } else {
                        pokemonCaught = Pokedexes.UpdatePokemon(pokemonCaught, player.Pokedex);
                    }

                    if (!player.CatchedAnyPokemon()) {
                        player.MainPokedexPokemonID = pokemonCaught.ID;
                        Players.Update(player);
                    }

                }

                dynamic response = new ExpandoObject();
                response.caught = caught;

                GameState.Encounters.Remove(encounter);

                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("battle/{encounterId?}")]
        public IActionResult Battle(int encounterId) {
            Encounter encounter = GameState.Encounters.FirstOrDefault(_encounter => _encounter.ID == encounterId);

            Local local = Locals.FindOneByID(encounter.LocalID);
            Player player = Players.FindOneByID(encounter.PlayerID);

            if (local != null && player != null) {
                dynamic response = new ExpandoObject();

                if (encounter.CanBattle) {
                    bool win = PokemonEncounter.Battle(player.MainPokedexPokemon, encounter.PokemonEncountered);

                    if(win) {
                        player.MainPokedexPokemon.Level += 1;
                        Pokedexes.UpdatePokemon(player.MainPokedexPokemon, player.Pokedex);
                    }

                    response.win = win;
                    response.mainPokemon = player.MainPokedexPokemon;

                    GameState.Encounters.Remove(encounter);

                    return Ok(response);
                } else {
                    return StatusCode(500);
                }
            }

            return BadRequest();
        }

    }
}