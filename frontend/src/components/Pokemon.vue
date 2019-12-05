<template lang="pug">
  v-card
    v-card-text.pa-4
      // img(:src="pokemon.sprites.front_shiny || pokemon.sprites.front_default")
      v-layout(row wrap)
        v-flex(xs6)
          img(:src="pokedexPokemon.data.sprites.front_default" height="auto")
        v-flex(xs6).mt-5
          h1 Name: {{ pokedexPokemon.pokemon.name }}
          h2 Level: {{ pokedexPokemon.level }}
          h2(v-for="item in abilities" :key="item") Skill: {{ item }}

</template>

<script>
export default {
  props: {
    pokedexPokemon: {
      type: Object
    }
  },
  computed: {
    abilities() {
      const abilities = this.pokedexPokemon.data.abilities.length > 0 ? this.pokedexPokemon.data.abilities.map(item => item.ability.name) : ''
      return abilities
    }
  }
}
</script>
