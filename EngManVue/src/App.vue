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

<style>
body{
  background: #e6e6e6;
}
#app {
  font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
  font-size: 18px;
  color: #32353a;
}
button{
    outline:none;
    margin: 10px;
    cursor: pointer;
    width: 85px;
    border: none;
    border-radius: 10px;
    width: 155px;
    height: 25px;
    background: rgb(75, 75, 75);
    font-size: 16px;
    color: rgb(248, 248, 248);
}
* {
  margin: 0%;
  padding: 0%;
}
a {
    outline: none;
    text-decoration: none;
    color: rgb(75, 75, 75);
}
span{
  color: rgb(75, 75, 75);
}
.span-error-message{
    color: red;
    font-size: 16px;
}
.span-complete-message{
    color: green;
    font-size: 20px;
}
label{
  color: rgb(75, 75, 75);
}
.form-border{
    border: solid;
    border-width: 1px;
    border-radius: 10px;
    border-color: rgb(248, 248, 248);
    margin-bottom: 1%;
}
.routes-admin{
    border: solid;
    border-width: 1px;
    border-radius: 10px;
    border-color: rgb(248, 248, 248);
    margin: 0.2%;
    padding-left: 5px;
    color: rgb(75, 75, 75);
}
</style>
