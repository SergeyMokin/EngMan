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

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.header img:hover{
    cursor: pointer;
}
.header{
    background-color: rgba(0,0,0,.1);
    display: flex;
    width: 100%;
}
.square {
  margin-left: 120px;
  position: absolute;
  z-index: 999;
  counter-reset: li;
  list-style: none;
  background:rgba(201, 201, 201, 0.9);
  padding: 10px;
}
.square li {
  position: relative;
  margin: 0 0 10px 2em;
  padding: 4px 8px;
  border-top: 2px solid rgb(17, 17, 17);
  transition: .3s linear;
}
.square li:last-child {margin-bottom: 0;}
.square li:before {
  content: counter(li);
  counter-increment: li;
  position: absolute;
  top: -2px;
  left: -2em;
  width: 2em;
  box-sizing: border-box;
  margin-right: 8px;
  padding: 4px;
  border-top: 2px solid rgb(21, 21, 21);
  border-left: 2px solid transparent;
  border-right: 2px solid transparent;
  border-bottom: 2px solid transparent;
  background: rgb(17, 17, 17);
  color: rgb(248, 248, 248);
  font-weight: bold;
  text-align: center;
  transition: .3s linear;
}
.square li:hover {
    border-top: 2px solid rgb(21, 21, 21);
    cursor: pointer;
}
.square li:hover:before {
  border: 2px solid rgb(21, 21, 21);
  background: rgb(200, 200, 200);
}
.link{
  outline: none;
}
</style>
