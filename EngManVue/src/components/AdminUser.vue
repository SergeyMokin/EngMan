<template>
<div>
  <div class="loading" v-if = "inProgress">Loading&#8230;</div>
  <div v-if = "$store.state.user.Role == 'admin'" class = "view-list">
      <router-link to="/admin/rules" class = "routes-admin">Rules </router-link>
      <router-link to="/admin/sentences" class = "routes-admin">Sentences </router-link>
      <router-link to="/admin/words" class = "routes-admin">Words </router-link>
      <router-link to="/admin/users" class = "routes-admin">Users </router-link>
      <router-link to="/admin/guessestheimages" class = "routes-admin">Guesses the images</router-link><br/><br/>
        <select style = "width:250px" class = "select-form" v-model = "role">
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
                    <span v-if = "el.Role == 'user'" style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "deleteUser(el.Id)"><img title="Delete" style = "width: 20px; height: auto" type = "img" src = "../assets/close-icon.png"></span>
                    <span v-if = "el.Role == 'user'" style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "changeRole(el)"><img title="Improve" style = "margin-right: 5px; width: 20px; height: auto" type = "img" src = "../assets/arrow-up.png"></span>
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
            if(confirm("Are you sure?") == true){
                api.deleteUser(id)
                .then(result =>{
                    if(result.response)
                    {
                        if(result.response.data.Message)
                        {
                            this.inProgress = false;
                            console.log(result);
                            return;
                        }
                    }
                    if(result === "Delete completed successful")
                    {
                        this.inProgress = false;
                        this.$store.dispatch('getUsers');
                        this.closeEditForm();
                        return;
                    }
                    else
                    {
                        this.inProgress = false;
                        console.log(result);
                        return;
                    }
                })
            } else{
                this.inProgress = false;
                alert("You canceled the deletion!");
                this.closeEditForm();
            }
          }
      },
      changeRole(user){
          if(this.inProgress) return;
          this.inProgress = true;
          if(this.role == 'user'){
            if(confirm("Are you sure you want to give the user administrative privileges?") == true){
                user.Role = 'admin';
                api.changeRole(user)
                .then(result =>{
                    if(result.response)
                    {
                        if(result.response.data.Message)
                        {
                            this.inProgress = false;
                            console.log(result.response.data.Message);
                            return;
                        }
                    }
                    if(result === true)
                    {
                        this.inProgress = false;
                        this.$store.dispatch('getUsers');
                        this.closeEditForm();
                        return;
                    }
                    else
                    {
                        this.inProgress = false;
                        console.log(result);
                    }
                })
            } else{
                this.inProgress = false;
                alert("You canceled a user raise!");
                this.closeEditForm();
                return;
            }
          }
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
      this.$store.dispatch('getUsers');
  }
}
</script>