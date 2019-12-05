<template lang="pug">
#Register
  v-container(fluid='', fill-height='')
    v-layout(align-center='', justify-center='')
      v-card(width="450px").elevation-12.card-login
        v-card-text.pa-5
          v-layout(row wrap align-center='' justify-center='')
            v-flex(xs12).center
              h1.ma-3 Pokesharp
            v-flex(xs12).center.mb-5
              h1.ma-3 Register
            v-flex(xs12).px-5
              v-form(ref="form" @keyup.enter.native="auth")
                v-flex(xs12)
                  v-text-field(v-model="name" solo background-color="white" label="Name")
                v-flex(xs12)
                  v-text-field(v-model="username" solo background-color="white" label="Username")
                v-flex(xs12).mb-3
                  v-text-field(v-model="password" solo background-color="white" label="Password" type="password")
                v-flex(xs12)
                  .actions.mb-3
                    v-btn(@click="auth").btn-entrar Register
                v-flex(xs12)
                  .actions.mb-3
                    v-btn(@click="$router.push('/')").btn-entrar Back
</template>

<script>
import axios from 'axios'

export default {
  data: () => ({
    name: '',
    username: '',
    password: ''
  }),
  methods: {
    auth() {
      if (this.username !== '' && this.password !== '' && this.name !== '') {
        axios
          .post('http://localhost:5000/api/players', {
            name: this.name,
            username: this.username,
            password: this.password
          })
          .then(response => {
            alert('Welcome ' + this.name + '! Now, type your data to enter to the Pokémon world!')
            this.$router.push('/')
          })
          .catch(error => alert('An error has occurred, please try again later'))
      } else {
        alert('Please fill in all fields to register')
      }
    }
  },
  created() {
    if (this.user) {
      this.$router.push('/home')
    }
  }
}
</script>

<style lang="scss">
@import url('https://fonts.googleapis.com/css?family=Blinker&display=swap');
#Register {
  height: 100%;
  width: 100%;
  background-image: url('../assets/wallpaper.jpg');
  color: white !important;
  background-size: cover;
  overflow: hidden;
  .card-login {
    border: 3px solid #e25909;
    background-color: black;
    .center {
      display: flex;
      flex-direction: row;
      justify-content: center;
    }
    .actions {
      width: 100%;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      .btn-criar {
        border: 1px solid #bb1634;
        color: #ff6a00;
        border-radius: 15px;
        font-weight: 800 !important;
        font-family: 'Blinker', sans-serif !important;
        width: 45%;
        font-size: 1em;
      }
      .btn-entrar {
        color: white;
        font-weight: 800 !important;
        font-family: 'Blinker', sans-serif !important;
        background: linear-gradient(90deg, #ff6a00 0%, #f80759 100%);
        border-radius: 15px;
        width: 100%;
        font-size: 1em;
      }
    }
    img {
      height: auto;
      width: 200px;
    }
    h1 {
      font-family: 'Blinker', sans-serif !important;
      color: white !important;
      font-size: 1.2em !important;
      font-style: normal;
      font-weight: 800 !important;
    }
    h2 {
      font-family: 'Blinker', sans-serif;
      color: white !important;
      font-size: 0.8em !important;
    }
  }
}
</style>
