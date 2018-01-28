<template>
  <div class="profile" id = "profile-form">
      <div v-show = "!$store.state.user.Logined">
          <span class = "span-anonymous">Вам необходимо войти в профиль</span>
      </div>
      <div v-show = "$store.state.user.Logined">
          <a v-show = "$store.state.user.Logined"><b>{{$store.state.user.FirstName}}</b>, Вы успешно авторизованы!</a>
          <button type = "submit" v-on:click = "logout">Выйти</button>
      </div>
  </div>
</template>

<script>
import api from '../api/api'
export default {
    data(){
        return {
            inprogress: false,
            clickAtForm: false
        }
    },
    methods: {
        logout(){
            if(this.inprogress) return;
            this.inprogress = true;
            api.signout()
            .then(res => {
                this.$cookie.delete('user.login.token.localhost:8080');
                this.$store.state.user.Logined = false;
                this.$store.state.user.Id = '';
                this.$store.state.user.Role = '';
                this.$store.state.user.FirstName = '';
                this.$store.state.user.LastName = '';
                this.$store.state.user.Email = '';
                this.inprogress = false;
                this.$router.push('/'); 
                this.$emit('closeform'); 
            })
            .catch(e => {
                console.log(e);
                this.inprogress = false;
            });
        }
    },
    mounted(){
        document.getElementById('profile-form').addEventListener('click', clickAtForm, false);
        document.body.addEventListener('click', clickAtBody, false);
        var vue = this;
        function clickAtForm(event){
            vue.clickAtForm = true;
        };
        function clickAtBody(event){
            if(!vue.clickAtForm){
                vue.clickAtForm = true;
                vue.$emit('closeform');
                return;
            }
            vue.clickAtForm = false;
        };
    }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.profile{
    background:rgba(205, 205, 205, 0.9);
    width: 173px;
    height: 119px;
    padding: 15px;
    left: 0;
    top: 80px;
    position: absolute;
    z-index: 99;
    text-align: center;
}
.span-anonymous{
    font-size: 20px;
}
</style>