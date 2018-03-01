<template>
<div>
  <div class="loading" v-if = "inProgress">Loading&#8230;</div>
  <div v-if = "$store.state.user.Role == 'admin'" class = "view-list">
        <router-link to="/admin/rules" class = "routes-admin">Правила </router-link>
        <router-link to="/admin/sentences" class = "routes-admin">Предложения </router-link>
        <router-link to="/admin/words" class = "routes-admin">Словарь </router-link>
        <router-link to="/admin/users" class = "routes-admin">Пользователи </router-link>
        <router-link to="/admin/guessestheimages" class = "routes-admin">Задания по картинкам</router-link><br/><br/>
        <select class = "select-form" v-model = "role">
            <option selected>
                user
            </option>
            <option>
                admin
            </option>
        </select>
        <div v-for = 'el in users' :key = 'el.Id'>
            <div class = "list--element">
                <span class = "span--element">
                    {{el.FirstName}} {{el.LastName}} <b>[e-mail: {{el.Email}}]</b>  
                    <span v-if = "el.Role == 'user'" style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "deleteUser(el.Id)"><img title="Удалить" style = "width: 20px; height: auto" type = "img" src = "../assets/close-icon.png"></span>
                    <span v-if = "el.Role == 'user'" style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "changeRole(el)"><img title="Повысить" style = "margin-right: 5px; width: 20px; height: auto" type = "img" src = "../assets/arrow-up.png"></span>
                </span>
            </div>
        </div>
  </div>
</div>
</template>

<script>
import api from '../api/api'

export default {
  name: 'users-view',
  data () {
    return {
        inProgress: false,
        role: 'user'
    }
  },
  methods:{
      deleteUser(id){
          if(this.inProgress) return;
          this.inProgress = true;
          if(this.role == 'user'){
            if(confirm("Вы уверены, что хотите удалить пользователя?") == true){
                api.deleteUser(id)
                .then(result =>{
                    this.inProgress = false;
                    this.$store.dispatch('getUsers');
                    this.closeEditForm();
                })
            } else{
                this.inProgress = false;
                alert("Вы отменили удаление!");
                this.closeEditForm();
            }
          }
          this.inProgress = false;
      },
      changeRole(user){
          if(this.inProgress) return;
          this.inProgress = true;
          if(this.role == 'user'){
            if(confirm("Вы уверены, что хотите дать пользователю права администратора?") == true){
                user.Role = 'admin';
                api.changeRole(user)
                .then(result =>{
                    this.inProgress = false;
                    this.$store.dispatch('getUsers');
                    this.closeEditForm();
                })
            } else{
                this.inProgress = false;
                alert("Вы отменили повышение пользователя!");
                this.closeEditForm();
            }
          }
          this.inProgress = false;
      },
      closeEditForm(){
        this.inProgress = false;
        this.role = 'user';
      }
  },
  computed: 
  {
    users()
    {
      var vue = this;
      return this.$store.getters.users.filter(function(user){
          return user.Role.toLowerCase().indexOf(vue.role.toLowerCase()) > -1;
      });
    }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      this.$store.dispatch('getUsers');
      this.inProgress = false;
  }
}
</script>