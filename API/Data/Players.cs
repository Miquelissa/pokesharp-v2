using Microsoft.EntityFrameworkCore;
using Pokesharp.Base;
using Pokesharp.Models;
using Pokesharp.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokesharp.Data
{
    public class Players
    {
  
        private readonly Context db;

        public Players(Context context) {
            db = context;
        }

        public List<Player> List() {
            return db.Player
                .Where(player => player.Enabled)
                .ToList();
        }

        public void Add(Player player) {

            player.CreatedAt = DateTime.Now;
            player.UpdatedAt = DateTime.Now;
            player.Enabled = true;
            player.Password = Util.Encryptor.MD5(player.Password);

            db.Player.Add(player);
            db.SaveChanges();
            
        }

        public Player FindOneByLogin(string username, string password) {

            password = Util.Encryptor.MD5(password);

            Player player = db.Player
                .Include("Pokedex")
                .Include("Pokedex.Pokemons")
                .Include("Pokedex.Pokemons.Pokemon")
                .Include("Friendships")
                .Include("Friendships.Friend")
                .Where(_player => _player.Enabled)
                .FirstOrDefault(_player => _player.Username.Equals(username) && _player.Password.Equals(password));

            if(player != null)
            {
                player.Pokedex.Pokemons = player.Pokedex.Pokemons.Where(pokemon => pokemon.Enabled).ToList();
            }

            return player;
        }
    
        public Player FindOneByID(int id) {

            Player player = db.Player
                .Include("Pokedex")
                .Include("Pokedex.Pokemons")
                .Include("Pokedex.Pokemons.Pokemon")
                .Include("Friendships")
                .Include("Friendships.Friend")
                .Where(_player => _player.Enabled)
                .FirstOrDefault(_player => _player.ID == id);

            if (player != null)
            {
                // filter enabled player pokemons
                player.Pokedex.Pokemons = player.Pokedex.Pokemons.Where(pokemon => pokemon.Enabled).ToList();
            }

            return player;

        }

        public Player Update(Player player) {
            db.Player.Attach(player);
            db.Entry(player).State = EntityState.Modified;
            db.SaveChanges();

            return player;
        }

        public Player Update(PlayerUpdate playerUpdate) {
            Player player = FindOneByID(playerUpdate.ID);
            player.Name = playerUpdate.Name;

            return Update(player);
        }

        public void AddFriendship(Friendship friendship) {

            friendship.CreatedAt = DateTime.Now;
            friendship.Enabled = true;

            db.Friendship.Add(friendship);
            db.SaveChanges();

        }

    }

}
