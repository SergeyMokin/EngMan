<template>
  <div class="header">
    <img src = "../assets/logo.png" style = "margin: 5px; width: 70px; height: 70px;" v-on:click = "clickProfile">
    <profile v-show = "displayProfile" v-on:closeform = "displayProfile = false" />
    <img v-show = "$store.state.user.Logined" src = "../assets/menu.png" style = "margin: 5px; width: 35px; height: 35px;" v-on:mouseover = "changeShow()" v-on:mouseout = "changeShow()">
    <ol class="square" v-show = "hovermenu" v-on:mouseover = "changeShow()" v-on:mouseout = "changeShow()">
    <ul>
        <li><router-link to="/">Домой</router-link></li>
        <li><router-link to="/wordmap">Карты слов</router-link></li>
        <li><router-link to="/rules">Правила</router-link></li>
        <li><router-link to="/sentencetask">Задания по предложениям</router-link></li>
        <li v-if = "this.$store.state.user.Role == 'admin'"><router-link to="/admin/rules">Панель администратора</router-link></li>
    </ul>
    </ol>
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
