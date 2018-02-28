<template>
  <div class="header">
    <img src = "../assets/account.png" title="Your profile" class = "icon-account" v-on:click = "clickProfile">
    <div class = "navbar">
      <router-link class = "routes-admin" title="Home" to="/"><img src = "../assets/home.png" class = "icon-navbar"></router-link>
      <router-link v-if = "this.$store.state.user.Logined" class = "routes-admin" title="Grammar" to="/grammar"><img src = "../assets/grammar.png" class = "icon-navbar"></router-link>
      <router-link v-if = "this.$store.state.user.Logined" class = "routes-admin" title="Trainings" to="/trainings"><img src = "../assets/training.png" class = "icon-navbar"></router-link>
      <router-link v-if = "this.$store.state.user.Logined" class = "routes-admin" title="Dictionary" to="/dictionary"><img src = "../assets/dictionary.svg" class = "icon-navbar"></router-link>
      <router-link v-if = "this.$store.state.user.Role == 'admin'" class = "routes-admin" title="Admin panel" to="/admin/rules"><img src = "../assets/adminpanel.png" class = "icon-navbar"></router-link>
    </div>
    <profile v-if = "displayProfile" v-on:closeform = "displayProfile = false" />
  </div>
</template>

<script>
import profile from './Profile';
export default {
  name: 'Header',
  components:{
    profile
  },
  data () {
    return {
      hovermenu: false
      , displayProfile: false
    }
  },
  methods: {
    changeShow(){
      this.hovermenu = !this.hovermenu;
    },
    clickProfile(){
      if(this.$store.state.user.Logined)
      {
        this.displayProfile = true;
        return;
      }
      this.$router.push('/');  
    }
  }
}
</script>

<style scoped>
.header img:hover{
    cursor: pointer;
}
.header{
    background-color: rgba(0,0,0,.1);
    flex-direction: row;    
    width: 100%;
}
.header-icons{
    display: flex;
    width: 10%;
}
.navbar{
  text-align: center;
}
.icon-account{
  position: absolute; 
  width: 70px;
  height:70px;
}
.icon-navbar{
  width: 30px;
}
</style>
