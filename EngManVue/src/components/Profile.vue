<template>
  <div class="profile" id = "profile-form">
      <div class="loading" v-if = "inProgress">Loading&#8230;</div>
      <div v-if = "!$store.state.user.Logined">
          <span>Вам необходимо войти в профиль</span>
      </div>
      <div v-if = "$store.state.user.Logined" style = "display: grid">
          <a style = "margin-bottom: 5px" v-if = "$store.state.user.Logined"><b>{{$store.state.user.FirstName}}</b></a>
          <div class = "routes-admin" style = "padding: 5px; cursor: pointer" v-on:click = "toProfile()">Profile</div>
          <div class = "routes-admin" style = "padding: 5px; cursor: pointer" v-on:click = "logout()">Logout</div>
      </div>
  </div>
</template>

<script>
import api from '../api/api'
export default {
    data(){
        return {
            inProgress: false,
            clickAtForm: false
        }
    },
    methods: {
        logout(){
            if(this.inProgress) return;
            this.inProgress = true;
            var proxy = this.$store.state.connectionSignalR.createHubProxy('chat');
            proxy.invoke("Disconnect");
            api.signout()
            .then(res => {
                this.$cookie.delete('user.login.token.localhost:8080');
                this.$store.state.user.Logined = false;
                this.$store.state.user.Id = '';
                this.$store.state.user.Role = '';
                this.$store.state.user.FirstName = '';
                this.$store.state.user.LastName = '';
                this.$store.state.user.Email = '';
                this.inProgress = false;
                this.$router.push('/'); 
                this.$emit('closeform'); 
            })
            .catch(e => {
                console.log(e);
                this.inProgress = false;
            });
        },
        toProfile(){
            if(this.inProgress) return;
            this.inProgress = true;
            this.$router.push('/user');
            this.inProgress = false; 
            this.$emit('closeform');
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
                document.body.removeEventListener('click', clickAtBody);
                vue.$emit('closeform');
                return;
            }
            vue.clickAtForm = false;
        };
    }
}
</script>

<style scoped>
    .profile{
        background:rgba(205, 205, 205, 0.9);
        width: 173px;
        left: 0;
        top: 70px;
        position: absolute;
        z-index: 99;
        text-align: center;
        border: solid 1px rgb(19, 19, 19);
    }
</style>