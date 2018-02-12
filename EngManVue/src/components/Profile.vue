<template>
  <div class="profile" id = "profile-form">
      <div class="loading" v-if = "inprogress">Loading&#8230;</div>
      <div v-if = "!$store.state.user.Logined">
          <span class = "span-anonymous">Вам необходимо войти в профиль</span>
      </div>
      <div v-if = "$store.state.user.Logined">
          <a v-if = "$store.state.user.Logined"><b>{{$store.state.user.FirstName}}</b></a>
          <button type = "submit" v-on:click = "toProfile()">Профиль</button>
          <button type = "submit" v-on:click = "logout()">Выйти</button>
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
        },
        toProfile(){
            if(this.inprogress) return;
            this.inprogress = true;
            this.$router.push('/user');
            this.inprogress = false; 
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
                document.getElementById('profile-form').removeEventListener('click', clickAtForm);
                document.body.removeEventListener('click', clickAtBody);
                vue.$emit('closeform');
                return;
            }
            vue.clickAtForm = false;
        };
    }
}
</script>