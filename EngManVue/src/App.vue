<template>
  <div id="app">
    <headerofapp/>
    <router-view></router-view>
    <footerofapp/>
  </div>
</template>

<script>
import headerofapp from './components/Header'
import footerofapp from './components/Footer'
import api from './api/api'

export default {
  name: 'app',
  components: {
    headerofapp 
    , footerofapp
  },
  created(){
      if(this.$cookie.get('user.login.token.localhost:8080'))
      {
        api.addToken(this.$cookie.get('user.login.token.localhost:8080'));
        api.getUserData()
        .then(res => {
          if(res.Id)
          {
            this.$store.state.user.Logined = true;
            this.$store.state.user.Id = res.Id;
            this.$store.state.user.Role = res.Role;
            this.$store.state.user.FirstName = res.FirstName;
            this.$store.state.user.LastName = res.LastName;
            this.$store.state.user.Email = res.Email;
          }
          else{
            console.log('NotFound404');
            api.deleteToken();
            this.$cookie.delete('user.login.token.localhost:8080');
          }
        })
        return;
      }
      if(!this.$store.state.user.Logined)
      {
        this.$router.push('/');
      }
  }
}
</script>

<style src = "./css/styles.css"/>
