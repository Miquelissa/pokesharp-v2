<template lang="pug">
.home
  v-dialog(v-model='dialog', width='500')
    v-card
      v-card-title.headline.grey.lighten-2(primary-title='')
        | Friends
      v-card-text.pa-5
        v-simple-table(height="200px")
          tbody
            tr(v-for="friendship in user.friendships" :key="friendship.id")
              td {{ friendship.friend.name }}
              td.text-right
                v-btn(color="primary" @click="openDialogPokemon(friendship.friend)") Send pokemon
      v-divider
      v-card-actions
        v-btn(color='primary' @click='addFriends()')
          | Add friend
        v-spacer
        v-btn(color='primary', text='', @click='dialog = false')
          | Close
  v-dialog(v-model='pokedexDialog', width='300' :persistent="true")
    v-card
      v-card-title.headline.grey.lighten-2(primary-title='')
        | Pokémons
      v-card-text.pa-5
        v-simple-table(height="200px")
          tbody
            tr(v-for="pokedexPokemon in user.pokedex.pokemons" :key="pokedexPokemon.id")
              td {{ pokedexPokemon.pokemon.name }}
              td.text-right
                v-btn(color="primary" @click="sendPokemon(pokedexPokemon)") Send
      v-divider
      v-card-actions
        v-btn(color='primary', text='', @click='closeDialogPokemon')
          | Close       
  v-dialog(v-model='addFriend', width='300')
    v-card
      v-card-title.headline.grey.lighten-2(primary-title='')
        | Users
      v-card-text.pa-5
        v-simple-table(height="200px")
          tbody
            tr(v-for="user in users" :key="user.id")
              td {{ user.name }}
              td.text-right
                v-btn(color="primary" @click="addFriendOnUser(user)") add
      v-divider
      v-card-actions
        v-btn(color='primary', text='', @click='addFriend = false')
          | Close
  v-dialog(v-model="dialogPokemon" width="600")
    pokemon(:pokedexPokemon="currentPokedexPokemon" v-if="dialogPokemon")
  v-dialog(v-model="dialogSearching" width='500' :persistent="true")
    SearchPokemon(@action="actionPokemon" :encounter="encounter" v-if="dialogSearching")
  v-dialog(v-model="editUser" width='500')
    User(@closeDialog="closeEditUser")
  .friends
    v-btn.mx-2(fab='', dark='', color='blue' @click="dialog = true")
      v-icon(dark='') mdi-account-multiple
  header.pa-3.mb-5
    img(:src="require('../assets/pokemon-logo.png')" height="100px" width="auto")
    h1 Welcome {{ user.name }}
    .actionsHome
      v-btn.mx-2( dark='', color='blue' @click="editUser = true")
        v-icon(dark='') mdi-account
      v-btn.mx-2( dark='', color='blue' @click="logout")
        v-icon(dark='') mdi-exit-to-app
  section.px-5
    v-layout(row wrap)
      v-flex(sm12 lg4).pa-2
        v-card(v-if="world")
          v-layout(row wrap).justify-center.pt-5.mx-5
            h1 World
            v-spacer
          v-card-text
            v-alert(text color="info") Select a region to navigate
            v-simple-table(height="550px" fixed-header)
              template(v-slot:default="")
                thead
                  tr
                    th.text-left Name
                tbody
                  tr(v-for="region in regions" :key="region.id" @click="selectRegion(region)").clickable
                    td {{ region.name }}
        v-card(v-else)
          v-layout(row wrap).justify-center.pt-5.mx-5
            h1 {{ currentRegion.name }}
            v-spacer
            v-btn(text  @click="backToWorld") 
              v-icon.mr-2 mdi-arrow-left
              | Back to World
          v-spacer
            v-card-text
              v-alert(text color="info") Select a local to search a pokémon
              v-simple-table(height="550px" fixed-header)
                template(v-slot:default="")
                  thead
                    tr
                      th.text-left Name
                  tbody
                    tr(v-for="local in currentRegion.locals" :key="local.id" @click="searchPokemon(local)").clickable
                      td {{ local.name }}
      v-flex(sm12 lg8).pa-2
        v-card
          v-layout(row wrap).justify-center.pt-5
            h1 Pokedex
          v-card-text
            v-simple-table(height="550px" fixed-header)
              template(v-slot:default="")
                thead
                  tr
                    th.text-left Name
                    th.text-center Make Main
                    th.text-center Delete
                tbody
                  tr(v-for="pokedexPokemon in user.pokedex.pokemons" :key="pokedexPokemon.id")
                    td(@click="getPokemonData(pokedexPokemon)") {{ pokedexPokemon.pokemon.name }}
                    td.text-center
                      v-btn.mx-2( dark='', color='blue' icon @click="setMain(pokedexPokemon)" :disabled="user.mainPokedexPokemonID === pokedexPokemon.id")
                        v-icon(dark='') mdi-eye
                    td.text-center
                      v-btn.mx-2( dark='', color='red' icon @click="deletePokemon(pokedexPokemon)")
                        v-icon(dark='') mdi-delete
</template>

<script>
// @ is an alias to /src
import { mapMutations, mapState } from 'vuex'
import HelloWorld from '@/components/HelloWorld.vue'
import Pokemon from '@/components/Pokemon.vue'
import SearchPokemon from '@/components/SearchPokemon.vue'
import User from '@/components/User.vue'
import axios from 'axios'

export default {
  name: 'Home',
  components: {
    HelloWorld,
    Pokemon,
    SearchPokemon,
    User
  },
  data: () => ({
    dialog: false,
    world: true,
    currentFriendTransfer: undefined,
    pokedexDialog: false,
    loadingPokemon: false,
    currentPokemon: undefined,
    dialogPokemon: false,
    dialogSearching: false,
    addFriend: false,
    editUser: false,
    users: [],
    currentPokedexPokemon: null,
    regions: [],
    currentRegion: null,
    encounter: null
  }),
  computed: {
    ...mapState('app', ['user'])
  },
  methods: {
    ...mapMutations('app', ['setDataUser', 'toggleLoading']),
    selectRegion(region) {
      this.world = false
      this.currentRegion = region
    },
    backToWorld() {
      this.world = true
      this.currentRegion = null
    },
    getPokemonData(pokedexPokemon) {
      axios
        .get(`https://pokeapi.co/api/v2/pokemon/${pokedexPokemon.pokemon.name.toLowerCase()}`)
        .then(response => {
          this.currentPokedexPokemon = pokedexPokemon
          this.currentPokedexPokemon.data = response.data

          this.dialogPokemon = true
        })
        .catch(error => alert('An error has occurred, please try again later'))
    },
    searchPokemon(local) {
      this.encounter = null

      axios
        .post('http://localhost:5000/api/game/encounter', {
          localId: local.id,
          playerId: this.user.id
        })
        .then(async response => {
          const encounter = response.data

          if (encounter.pokemonEncountered == null) {
            alert('No pokémons were found! Please, try again!')
          } else {
            const encounterDataPokemon = await axios.get(`https://pokeapi.co/api/v2/pokemon/${encounter.pokemonEncountered.localPokemon.pokemon.name.toLowerCase()}`).then(response => response.data)

            this.encounter = encounter
            this.encounter.pokemonEncountered.data = encounterDataPokemon

            alert('A wild ' + encounter.pokemonEncountered.localPokemon.pokemon.name + ' appears!')

            this.dialogSearching = true
          }
        })
        .catch(error => alert('An error has occurred, please try again later'))
    },
    actionPokemon(event) {
      if (event.action == 'catch') {
        this.catch(event.encounter)
      }
      if (event.action == 'run') {
        this.run()
      }
      if (event.action == 'battle') {
        this.battle(event.encounter)
      }
    },
    addFriendOnUser(friend) {
      axios
        .post('http://localhost:5000/api/players/friends', {
          playerId: this.user.id,
          friendId: friend.id
        })
        .then(response => {
          const player = response.data
          alert('Friend ' + friend.name + ' added!')

          this.setDataUser(player)
          this.addFriend = false
        })
        .catch(error => alert('An error has occurred, please try again later'))
    },
    catch(encounter) {
      axios
        .post('http://localhost:5000/api/game/catch/' + encounter.id, {
          encounterId: encounter.id
        })
        .then(response => {
          const { caught, player } = response.data

          if (!caught) {
            alert('The Pokémon broken free. Wild ' + encounter.pokemonEncountered.localPokemon.pokemon.name + ' ran from battle!')
          } else {
            alert(encounter.pokemonEncountered.localPokemon.pokemon.name + ' was caught!')
          }

          this.reloadPlayer()
          this.encounter = null
          this.dialogSearching = false
        })
        .catch(error => alert('An error has occurred, please try again later'))
    },
    openDialogPokemon(friend) {
      this.currentFriendTransfer = friend
      this.pokedexDialog = true
    },
    closeDialogPokemon() {
      this.currentFriendTransfer = null
      this.pokedexDialog = false
    },
    closeEditUser() {
      this.editUser = false
    },
    run() {
      alert('Got away safely!')

      this.encounter = null
      this.dialogSearching = false
    },
    sendPokemon(pokedexPokemon) {
      axios
        .post('http://localhost:5000/api/players/transfer', {
          pokedexPokemonId: pokedexPokemon.id,
          playerId: this.user.id,
          friendId: this.currentFriendTransfer.id
        })
        .then(response => {
          alert(pokedexPokemon.pokemon.name + ' sent!')

          this.reloadPlayer()
          this.pokedexDialog = false
        })
        .catch(error => alert('An error has occurred, please try again later'))
    },
    addFriends() {
      this.addFriend = true

      axios
        .get('http://localhost:5000/api/players')
        .then(response => {
          this.users = response.data.filter(user => user.id !== this.user.id && !this.user.friendships.find(friendship => friendship.friend.id === user.id))
        })
        .catch(error => alert('An error has occurred, please try again later'))
    },
    battle(encounter) {
      axios
        .post('http://localhost:5000/api/game/battle/' + encounter.id, {
          encounterId: encounter.id
        })
        .then(response => {
          const { win, mainPokemon } = response.data

          if (win) {
            alert(mainPokemon.pokemon.name + ' grew to lvl ' + mainPokemon.level + '!')
          } else {
            alert(mainPokemon.pokemon.name + ' was fainted.')
          }

          this.reloadPlayer()
          this.encounter = null
          this.dialogSearching = false
        })
        .catch(error => alert('An error has occurred, please try again later'))
    },
    logout() {
      localStorage.setItem('user', null)
      this.setDataUser(null)
      this.$router.push('/')
    },
    reloadPlayer() {
      axios
        .get('http://localhost:5000/api/players/' + this.user.id)
        .then(response => {
          this.setDataUser(response.data)
        })
        .catch(error => alert('An error has occurred, please try again later'))
    },
    reloadRegions() {
      axios
        .get('http://localhost:5000/api/regions')
        .then(response => {
          this.regions = response.data
        })
        .catch(error => alert('An error has occurred, please try again later'))
    },
    setMain(pokedexPokemon) {
      axios
        .post('http://localhost:5000/api/players/pokemons/main', {
          pokedexPokemonId: pokedexPokemon.id,
          playerId: this.user.id
        })
        .then(response => {
          alert(pokedexPokemon.pokemon.name + ' is now your main!')
          this.reloadPlayer()
        })
        .catch(error => alert('An error has occurred, please try again later'))
    },
    deletePokemon(pokedexPokemon) {
      axios
        .delete('http://localhost:5000/api/players/pokemons/' + pokedexPokemon.id)
        .then(response => {
          alert(pokedexPokemon.pokemon.name + ' is released!')
          this.reloadPlayer()
        })
        .catch(error => alert('An error has occurred, please try again later'))
    }
  },
  created() {
    if (!this.user) {
      this.$router.push('/')
    } else {
      this.reloadPlayer()
      this.reloadRegions()
    }
  }
}
</script>

<style lang="scss">
.friends {
  position: fixed;
  bottom: 15px;
  right: 10px;
  z-index: 10;
}
.actionsHome {
  position: fixed;
  top: 25px;
  right: 25px;
  z-index: 10;
}
.clickable {
  cursor: pointer;
}
</style>
