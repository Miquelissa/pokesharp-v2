export default {
  setDataUser: (state, user) => {
    state.user = user
    localStorage.setItem('user', JSON.stringify(user))
  },
  toggleLoading: state => {
    state.loading = !state.loading
  }
}
