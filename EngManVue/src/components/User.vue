<template>
  <div>
    <div class="loading" v-if = "inProgress">Loading&#8230;</div>
    <div v-if = "!clickEdit && !clickChangePassword" class = "users-view-list">
        <span style = "float: right; margin-right: 10px" v-on:click = "clickChangePassword = true"><img title="Изменить пароль" style = "cursor: pointer; width: 30px; height: auto;" type = "img" src = "../assets/security-icon.png"></span>
        <span style = "float: right; margin-top: 5px" v-on:click = "clickEdit = true"><img title="Редактировать профиль" style = "cursor: pointer; width: 20px; height: auto;" type = "img" src = "../assets/edit-icon.png"></span>
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
        </div>
        <br/><br/>
    </div>
    <div v-if = "clickEdit" class = "users-view-list">
          <br/><br/>
          <div class = "form-border">
            <span v-on:click = "closeEditForm()"><img title="Закрыть" style = "width: 20px; height: auto;" class = "button-close" type = "img" src = "../assets/close-icon.png"></span>
            <span v-on:click = "saveUser()"><img title="Сохранить изменения" style = "width: 18px; height: auto; margin-right: 30px; margin-top: 2px" class = "button-close" type = "img" src = "../assets/save-icon.png"></span>
            <span>Имя</span>
            <textarea class = "user-edit" type = "text" v-model = "user.FirstName"/><br/>
            <span>Фамилия</span>
            <textarea class = "user-edit" type = "text" v-model = "user.LastName"/><br/>
            <span>Мэил</span>
            <textarea class = "user-edit" type = "text" v-model = "user.Email"/><br/>
            <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
            <span v-if = "!$v.user.FirstName.required || !$v.user.LastName.required || !$v.user.Email.required" class = "span-error-message">Заполните все поля<br/></span>
            <span v-if = "!$v.user.Email.email" class = "span-error-message">Введите корректный мэил<br/></span>
          </div>
          <br/><br/>
    </div>
    <div v-if = "clickChangePassword" class = "users-view-list">
          <br/><br/>
          <div class = "form-border">
            <span v-on:click = "closeEditForm()"><img title="Закрыть" style = "width: 20px; height: auto;" class = "button-close" type = "img" src = "../assets/close-icon.png"></span>
            <span v-on:click = "changePassword()"><img title="Изменить пароль" style = "width: 18px; height: auto; margin-right: 30px; margin-top: 2px" class = "button-close" type = "img" src = "../assets/save-icon.png"></span>
            <span>Старый пароль</span>
            <input class = "user-edit" type = "password" v-model = "oldpassword" style = "height: 30px; text-align: center;"/><br/>
            <span>Новый пароль</span>
            <input class = "user-edit" type = "password" v-model = "newpassword" style = "height: 30px; text-align: center;"/><br/>
            <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
            <span v-if = "(!$v.oldpassword.required || !$v.newpassword.required) && ($v.oldpassword.$error || $v.newpassword.$error)" class = "span-error-message">Заполните все поля<br/></span>
            <span v-if = "!$v.newpassword.minlength && $v.newpassword.$error" class = "span-error-message">Новый пароль должен содержать не меньше 8 символов<br/></span>
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