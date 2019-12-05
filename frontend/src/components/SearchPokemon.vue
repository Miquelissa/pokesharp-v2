<template lang="pug">
  v-card(v-if="encounter")
    v-card-text.pa-4
      v-layout(column justify-center align-center)
        h1 {{ encounter.pokemonEncountered.localPokemon.pokemon.name }} (lvl. {{ encounter.pokemonEncountered.level }})
        img(:src="encounter.pokemonEncountered.data.sprites.front_default" width="96px" height="auto")
        v-layout(row wrap).pa-5
          v-btn(color="primary" fab x-large @click="action('battle')" :disabled="!encounter.canBattle")
            | Battle
          v-spacer
          v-btn(color="primary" fab x-large @click="action('catch')").ml-4
            | Catch
          v-spacer
          v-btn(color="primary" fab x-large @click="action('run')").ml-4
            | Run
</template>

<script>
export default {
  props: {
    encounter: {
      type: Object
    }
  },
  methods: {
    action(action, value) {
      this.$emit('action', { action, encounter: this.encounter })
    }
  }
}
</script>
