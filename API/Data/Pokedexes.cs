using Pokesharp.Base;
using Pokesharp.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokesharp.Data
{
    public class Pokedexes
    {

        private readonly Context db;

        public Pokedexes(Context context) {
            db = context;
        }

        public Pokedex Add(Pokedex pokedex) {

            pokedex.CreatedAt = DateTime.Now;
            pokedex.UpdatedAt = DateTime.Now;
            pokedex.Enabled = true;

            db.Pokedex.Add(pokedex);
            db.SaveChanges();

            return pokedex;
        
         }

         public PokedexPokemon FindOnePokemonByID(int id) {
            return db.PokedexPokemon.Where(pokedexPokemon => pokedexPokemon.ID == id).FirstOrDefault();
         }
        
         public PokedexPokemon AddPokemon(PokedexPokemon pokedexPokemon, Pokedex pokedex) {

            pokedexPokemon.PokedexID = pokedex.ID;
            pokedexPokemon.CreatedAt = DateTime.Now;
            pokedexPokemon.UpdatedAt = DateTime.Now;
            pokedexPokemon.Enabled = true;

            pokedex.Pokemons.Add(pokedexPokemon);

            db.Pokedex.Attach(pokedex);
            db.Entry(pokedex).State = EntityState.Modified;
            db.SaveChanges();

            return pokedexPokemon;

         }

          
         public PokedexPokemon UpdatePokemon(PokedexPokemon pokedexPokemon, Pokedex pokedex) {
            pokedexPokemon.PokedexID = pokedex.ID;
            pokedexPokemon.UpdatedAt = DateTime.Now;
            pokedexPokemon.Enabled = true;

            db.PokedexPokemon.Attach(pokedexPokemon);
            db.Entry(pokedexPokemon).State = EntityState.Modified;
            db.SaveChanges();

            return pokedexPokemon;
         }

         public void TransferPokemon(Pokedex pokedexPlayer, Pokedex pokedexFriend, PokedexPokemon pokedexPokemon) {
            pokedexPokemon.PokedexID = pokedexFriend.ID;

            pokedexPlayer.Pokemons.Remove(pokedexPokemon);
            pokedexFriend.Pokemons.Add(pokedexPokemon);

            db.PokedexPokemon.Attach(pokedexPokemon);
            db.Entry(pokedexPokemon).State = EntityState.Modified;

            db.Pokedex.Attach(pokedexPlayer);
            db.Entry(pokedexPlayer).State = EntityState.Modified;

            db.Pokedex.Attach(pokedexFriend);
            db.Entry(pokedexFriend).State = EntityState.Modified;

            db.SaveChanges();
        }

        public PokedexPokemon DisablePokemon(PokedexPokemon pokedexPokemon)
        {
            pokedexPokemon.Enabled = false;

            db.PokedexPokemon.Attach(pokedexPokemon);
            db.Entry(pokedexPokemon).State = EntityState.Modified;
            db.SaveChanges();

            return pokedexPokemon;
        }

    }

}
