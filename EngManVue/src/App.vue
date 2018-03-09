<template>
  <div id="app">
    <div class="loading" v-if = "inProgress">Loading&#8230;</div>
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
  data: () => {
    return {
      inProgress: false
    }
  },
  components: {
    headerofapp 
    , footerofapp
  },
  created(){
      if(this.inProgress) return;
      var token = this.$cookie.get('user.login.token.localhost:8080');
      if(token)
      {
        this.inProgress = true;
        api.addToken(token);
        api.getUserData()
        .then(res => {
          if(res.message)
          {
            if(res.message == "Network Error")
            {
              this.inProgress = false;
              this.$router.push('/networkerror');
              return;
            }
          }
          if(res.Email)
          {
            this.$store.dispatch('getNewMessages');
            this.$store.state.user.Logined = true;
            this.$store.state.user.Id = res.Id;
            this.$store.state.user.Role = res.Role;
            this.$store.state.user.FirstName = res.FirstName;
            this.$store.state.user.LastName = res.LastName;
            this.$store.state.user.Email = res.Email;
            this.$store.dispatch('connectToServ', this.$store.state.user);
            this.inProgress = false;
          }
          else{
            api.deleteToken();
            this.inProgress = false;
          }
          if(!this.$store.state.user.Logined)
          {
            this.$router.push('/');
          }
        })
        .catch(e => {
            api.deleteToken();
            this.inProgress = false;
            this.$router.push('/networkerror');
            return;
        })
      }
      else
      {
        this.$router.push('/');
      }
  }
}
</script>

<style src = "./css/styles.css"/>
