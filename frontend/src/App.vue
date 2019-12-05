<template lang="pug">
  v-app
    router-view
    .loading(v-if="loading")
      img(src='https://66.media.tumblr.com/c99a579db3ae0fc164bf4cca148885d3/tumblr_mjgv8kEuMg1s87n79o1_400.gif', alt='this slowpoke moves', width='150/')
</template>

<script>
import { mapState } from 'vuex'

export default {
  name: 'App',
  computed: {
    ...mapState('app', ['loading'])
  }
}
</script>

<style lang="scss">
.loading {
  position: fixed;
  z-index: 10;
  width: 100%;
  height: 100vh;
  background: rgba(255, 255, 255, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  overflow: hidden;
}
</style>
