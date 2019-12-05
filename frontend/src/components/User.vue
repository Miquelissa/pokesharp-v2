<template lang="pug">
  v-card
    v-card-text
      v-layout(row wrap).pa-5
        h1.mb-5 Edit user
        v-flex(xs12)
          v-text-field(label="Name" outlined v-model="name")
        v-flex(xs12)
          v-btn(color='primary' @click="update")
            | Edit user
          v-btn(color='primary', text='', @click="$emit('closeDialog', '')")
            | Close
</template>

<script>
import { mapMutations, mapState } from 'vuex'
import axios from 'axios'

export default {
  data: () => ({
    name: ''
  }),
  computed: {
    ...mapState('app', ['user'])
  },
  methods: {
    ...mapMutations('app', ['setDataUser']),
    update() {
      if (this.name !== '') {
        axios
          .patch('http://localhost:5000/api/players/' + this.user.id, {
            name: this.name
          })
          .then(response => {
            this.setDataUser(response.data)
            this.$emit('closeDialog', true)
          })
          .catch(error => alert('An error has occurred, please try again later'))
      } else {
        alert('Please fill in all fields to register')
      }
    }
  },
  mounted() {
    this.name = this.user.name
  }
}
</script>
