using Microsoft.AspNetCore.Mvc;
using Pokesharp.Data;
using Pokesharp.Models;
using Pokesharp.Game.Models;
using System;

namespace Pokesharp.Controllers {
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly Pokedexes Pokedexes;
        private readonly Players Players;

        public PlayersController(Players players, Pokedexes pokedexes) {
            Players = players;
            Pokedexes = pokedexes;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(PlayerLogin playerLogin) {
            Player player = Players.FindOneByLogin(playerLogin.Username, playerLogin.Password);
     
            if(player != null) {
                return Ok(player);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("")]
        public IActionResult List() {
            try {
                return Ok(Players.List());
            } catch (Exception error) {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id?}")]
        public IActionResult Get(int id) {
            try {
                return Ok(Players.FindOneByID(id));
            } catch (Exception error) {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult Register(Player player) {
            try {
                Pokedex playerPokedex = Pokedexes.Add(new Pokedex());
                player.PokedexID = playerPokedex.ID;

                Players.Add(player);
                return Ok();
            } catch (Exception error) {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [Route("{id?}")]
        public IActionResult Update(int id, PlayerUpdate playerUpdate) {
            try {
                playerUpdate.ID = id;
                Player player = Players.Update(playerUpdate);

                return Ok(player);
            } catch (Exception error) {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("friends")]
        public IActionResult AddFriend(PlayerFriendship playerFriendship) {
            try {
                Friendship friendship = new Friendship();

                friendship.PlayerID = playerFriendship.PlayerID;
                friendship.FriendID = playerFriendship.FriendID;

                Players.AddFriendship(friendship);
                return Ok(Players.FindOneByID(playerFriendship.PlayerID));
            } catch (Exception error) {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("transfer")]
        public IActionResult TransferPokemon(PokemonTransfer pokemonTransfer) {
            try {
                Player player = Players.FindOneByID(pokemonTransfer.PlayerID);
                Player friend = Players.FindOneByID(pokemonTransfer.FriendID);

                PokedexPokemon pokedexPokemon = 
                    player.Pokedex.Pokemons.Find(_pokedexPokemon => _pokedexPokemon.ID == pokemonTransfer.PokedexPokemonID);

                Pokedexes.TransferPokemon(player.Pokedex, friend.Pokedex, pokedexPokemon);
                return Ok();
            } catch (Exception error) {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("pokemons/main")]
        public IActionResult MakePokemonMain(PokemonChange pokemonChange) {
            try {
                Player player = Players.FindOneByID(pokemonChange.PlayerID);
                player.MainPokedexPokemonID = pokemonChange.PokedexPokemonID;

                Players.Update(player);

                return Ok();
            } catch (Exception error) {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("pokemons/{pokedexPokemonId?}")]
        public IActionResult DeletePokemon(int pokedexPokemonId) {
            try {
                PokedexPokemon pokedexPokemon = Pokedexes.FindOnePokemonByID(pokedexPokemonId);
                Pokedexes.DisablePokemon(pokedexPokemon);

                return Ok();
            } catch (Exception error) {
                return StatusCode(500);
            }
        }

    }
}