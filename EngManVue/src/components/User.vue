<template>
  <div>
    <div v-if = "!clickEdit && !clickChangePassword" class = "users-view-list">
        <br/><br/>
         <div class = "form-border">
            <div class = "users-list--element">
                <a><b>Имя:</b> {{user.FirstName}}</a>
            </div>
            <div class = "users-list--element">
                <a><b>Фамилия:</b> {{user.LastName}}</a>
            </div>
            <div class = "users-list--element">
                <a><b>Мэил:</b> {{user.Email}}</a>
            </div>
            <button type = "submit" v-on:click = "clickEdit = true">Изменить профиль</button>
            <button type = "submit" v-on:click = "clickChangePassword = true">Сменить пароль</button>
        </div>
        <br/><br/>
    </div>
    <div v-if = "clickEdit" class = "users-view-list">
          <br/><br/>
          <div class = "form-border">
            <button type = "submit" v-on:click = "closeEditForm()" class = "button-close">Закрыть</button>
            <span>Имя</span>
            <textarea class = "user-edit" type = "text" v-model = "user.FirstName"/><br/>
            <span>Фамилия</span>
            <textarea class = "user-edit" type = "text" v-model = "user.LastName"/><br/>
            <span>Мэил</span>
            <textarea class = "user-edit" type = "text" v-model = "user.Email"/><br/>
            <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
            <span v-if = "!$v.user.FirstName.required || !$v.user.LastName.required || !$v.user.Email.required" class = "span-error-message">Заполните все поля<br/></span>
            <span v-if = "!$v.user.Email.email" class = "span-error-message">Введите корректный мэил<br/></span>
            <button type = "submit" v-on:click = "saveUser()">Сохранить</button>
          </div>
          <br/><br/>
    </div>
    <div v-if = "clickChangePassword" class = "users-view-list">
          <br/><br/>
          <div class = "form-border">
            <button type = "submit" v-on:click = "closeEditForm()" class = "button-close">Закрыть</button>
            <span>Старый пароль</span>
            <input class = "user-edit" type = "password" v-model = "oldpassword" style = "height: 30px; text-align: center;"/><br/>
            <span>Новый пароль</span>
            <input class = "user-edit" type = "password" v-model = "newpassword" style = "height: 30px; text-align: center;"/><br/>
            <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
            <span v-if = "(!$v.oldpassword.required || !$v.newpassword.required) && ($v.oldpassword.$error || $v.newpassword.$error)" class = "span-error-message">Заполните все поля<br/></span>
            <span v-if = "!$v.newpassword.minlength && $v.newpassword.$error" class = "span-error-message">Новый пароль должен содержать не меньше 8 символов<br/></span>
            <button type = "submit" v-on:click = "changePassword()">Изменить</button>
          </div>
          <br/><br/>
    </div>
  </div>
</template>

<script>
import api from '../api/api'
import { required, minLength, sameAs, email } from 'vuelidate/lib/validators'

export default {
  name: 'users-view',
  data () {
    return {
        inProgress: false,
        errormessage: '',
        clickEdit: false,
        clickChangePassword: false,
        oldpassword: '',
        newpassword: '',
        user: {
            Id: 0,
            FirstName: '',
            LastName: '',
            Email: '',
            Role: ''
        }
    }
  },
  validations: {
            user: {
                Id: { required },
                FirstName: { required },
                LastName: { required },
                Email: { required, email },
                Role: { required }
            },
            oldpassword: { required },
            newpassword: { required, minLength: minLength(8) }
  },
  methods:{
      saveUser(){
          if(this.inProgress) return;
          this.inProgress = true;
          this.errormessage = '';
          this.$v.$touch();
          if(this.$v.user.$invalid)
          {
              this.inProgress = false;
              return;
          }
          api.editUser(this.user)
          .then(result => {
              if(result.Id > 0){
                    this.inProgress = false;
                    api.getUserData()
                    .then(res => this.user = res)
                    this.closeEditForm();
              }
          })
          .catch(e => {
              this.inProgress = false;
              this.errormessage = 'Сервер недоступен или у вас нет прав';
          })
          this.inProgress = false;
      },
      changePassword(){
          if(this.inProgress) return;
          this.inProgress = true;
          this.errormessage = '';
          this.$v.$touch();
          if(this.$v.oldpassword.$invalid || this.$v.newpassword.$invalid)
          {
              this.inProgress = false;
              return;
          }
          api.changePassword(this.user.Id, this.oldpassword, this.newpassword)
          .then(result => {
              if(result.Id > 0){
                    this.inProgress = false;
                    api.getUserData()
                    .then(res => this.user = res)
                    this.closeEditForm();
              }
              else {
                    this.inProgress = false;
                    this.errormessage = 'Неверный старый пароль';
              }
          })
          .catch(e => {
              this.inProgress = false;
              this.errormessage = 'Сервер недоступен или у вас нет прав';
          })
          this.inProgress = false;
      },
      closeEditForm(){
        this.inProgress = false;
        this.errormessage = '';
        this.clickEdit = false;
        this.clickChangePassword = false;
        this.oldpassword = '';
        this.newpassword = '';
        this.$v.$reset();
        api.getUserData()
        .then(res => this.user = res)
      }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      api.getUserData()
      .then(res => this.user = res)
      this.inProgress = false;
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.users-view-list{
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
.button-close{
    position: absolute;
    right: 18.65%;
    top: 75px;
}
.user-edit{
    resize: none;
    text-align: left;
    width: 95%;
    margin: 0.5% 2.5% 0% 2.5%;
    padding: 0.5%;
    background: rgb(248, 248, 248);
    border: none;
    outline:none;
    border-radius: 10px;
}
</style>