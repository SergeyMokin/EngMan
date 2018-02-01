<template>
  <div v-show = "$store.state.user.Role == 'admin'" class = "users-view">
        <router-link to="/admin/rules" class = "routes-admin">Правила </router-link>
        <router-link to="/admin/sentences" class = "routes-admin">Предложения </router-link>
        <router-link to="/admin/words" class = "routes-admin">Словарь </router-link>
        <router-link to="/admin/users" class = "routes-admin">Пользователи </router-link><br/><br/>
        <select class = "select-form" v-model = "role">
            <option selected>
                user
            </option>
            <option>
                admin
            </option>
        </select>
        <div v-for = 'el in users' :key = 'el.Id' class = "form-border">
            <div class = "users-list--element">
                <span class = "span-user--element">{{el.FirstName}} {{el.LastName}} <b>[e-mail: {{el.Email}}]</b></span>
            </div>
            <button v-if = "el.Role == 'user'" type = "submit" v-on:click = "changeRole(el)">Повысить</button>
            <button v-if = "el.Role == 'user'" type = "submit" v-on:click = "deleteUser(el.Id)">Удалить</button>
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

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.users-view{
    margin: 5% 20% 5% 20%;
    width: 60%;
    text-align: center;
    display: table;
}
.users-list--element{
    margin: 10px;
    padding: 3px;
    text-align: left;
    cursor: default;
    background: rgb(248, 248, 248);
    height: 35px;
    border: none;
    outline:none;
    border-radius: 10px;
}
.span-user--element{
    margin: 15px;
    font-size: 18px;
}
.select-form{
    outline:none;
    background: rgb(248, 248, 248);
    border: none;
    border-radius: 10px;
    text-align: center;
    font-size: 16px;
    margin: auto;
    margin-bottom: 10px;
    resize: none;
    height: 25px;
    width: 155px;
}
</style>