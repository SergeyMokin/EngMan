<template>
  <div>
    <div class="loading" v-if = "inProgress">Loading&#8230;</div>
    <div class = "main-content">
        <span style = "float: right; margin-right: 10px" v-on:click = "clickChangePassword = true"><img title="Change password" style = "cursor: pointer; width: 30px; height: auto;" type = "img" src = "../assets/security-icon.png"></span>
        <span style = "float: right; margin-top: 5px" v-on:click = "clickEdit = true"><img title="Edit profile" style = "cursor: pointer; width: 20px; height: auto;" type = "img" src = "../assets/edit-icon.png"></span>
        <br/><br/>
         <div>
            <div class = "list--element">
                <a><b>First name:</b> {{user.FirstName}}</a>
            </div>
            <div class = "list--element">
                <a><b>Last name:</b> {{user.LastName}}</a>
            </div>
            <div class = "list--element">
                <a><b>Email:</b> {{user.Email}}</a>
            </div>
        </div>
        <br/><br/>
    </div>
    <div v-if = "clickEdit" class = "b-popup">
        <div class = "b-popup-content">
          <div>
            <span style = "float: right; font-size:10px; cursor: pointer" v-on:click = "closeEditForm()"><img title="Close" style = "width: 20px; height: auto" type = "img" src = "../assets/close-icon.png"></span>
            <span style = "float: right; font-size:10px; cursor: pointer" v-on:click = "saveUser()"><img title="Change password" style = "width: 16px; height: auto; margin-top:2px;" type = "img" src = "../assets/save-icon.png"></span>
            <span>First name</span>
            <input  v-bind:class = "{'input-form--error': $v.user.FirstName.$error}" class = "input-form" type = "text" v-model = "user.FirstName"/><br/>
            <span>Last name</span>
            <input v-bind:class = "{'input-form--error': $v.user.LastName.$error}" class = "input-form" type = "text" v-model = "user.LastName"/><br/>
            <span>Email</span>
            <input v-bind:class = "{'input-form--error': $v.user.Email.$error}" class = "input-form" type = "text" v-model = "user.Email"/><br/>
            <span v-if = "errormessage" class = "span-error-message">{{errormessage}}. </span>
            <span v-if = "!$v.user.FirstName.required || !$v.user.LastName.required || !$v.user.Email.required" class = "span-error-message">All fields must be filled in. </span>
            <span v-if = "!$v.user.Email.email" class = "span-error-message">Invalid Email. </span>
          </div>
        </div>
    </div>
    <div v-if = "clickChangePassword" class = "b-popup">
        <div class = "b-popup-content">
          <div class = "form-border">
            <span style = "float: right; font-size:10px; cursor: pointer" v-on:click = "closeEditForm()"><img title="Close" style = "width: 20px; height: auto" type = "img" src = "../assets/close-icon.png"></span>
            <span style = "float: right; font-size:10px; cursor: pointer" v-on:click = "changePassword()"><img title="Change password" style = "width: 16px; height: auto; margin-top:2px;" type = "img" src = "../assets/save-icon.png"></span>
            <span>Old password</span>
            <input v-bind:class = "{'input-form--error': $v.oldpassword.$error}" class = "input-form" type = "password" v-model = "oldpassword" style = "height: 30px; text-align: center;"/><br/>
            <span>New password</span>
            <input v-bind:class = "{'input-form--error': $v.newpassword.$error}" class = "input-form" type = "password" v-model = "newpassword" style = "height: 30px; text-align: center;"/><br/>
            <span v-if = "errormessage" class = "span-error-message">{{errormessage}}. </span>
            <span v-if = "(!$v.oldpassword.required || !$v.newpassword.required) && ($v.oldpassword.$error || $v.newpassword.$error)" class = "span-error-message">All fields must be filled in. </span>
            <span v-if = "!$v.newpassword.minlength && $v.newpassword.$error" class = "span-error-message">The password must be at least 8 characters long, one big and small letter, one number.</span>
          </div>
        </div>
    </div>
  </div>
</template>

<script>
import api from '../api/api'
import { required, requiredIf, sameAs, email } from 'vuelidate/lib/validators'

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
            newpassword: 
            { 
                required,
                isPassword: function (val) {
                    if (val === 'undefined' || val === null || val === '') return true;
                    if(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$/.test(val)) return true;
                    return false;
                }
            }
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
              if(result === true){
                    this.inProgress = false;
                    this.closeEditForm();
              }
              else
              {
                  if(result.response)
                  {
                      if(res.response.data.Message)
                      {
                        this.inProgress = false;
                        this.errormessage = result.response.data.Message;
                        return;
                      }
                  }
                  this.inProgress = false;
                  this.errormessage = 'The server is unavailable or you do not have the rights';
              }
          })
          .catch(e => {
              this.inProgress = false;
              this.errormessage = 'The server is unavailable or you do not have the rights';
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
              if(result === true){
                    this.inProgress = false;
                    this.closeEditForm();
              }
              else {
                  console.log(result)
                    if(result.response)
                    {
                        if(res.response.data.Message)
                        {
                            this.inProgress = false;
                            this.errormessage = result.response.data.Message;
                            return;
                        }
                    }
                    if(result === false)
                    {
                        this.inProgress = false;
                        this.errormessage = 'Incorrect old password';
                        return;
                    }
                    this.inProgress = false;
                    this.errormessage = 'The server is unavailable or you do not have the rights';
              }
          })
          .catch(e => {
              this.inProgress = false;
              this.errormessage = 'The server is unavailable or you do not have the rights';
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
      }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      api.getUserData()
      .then(res => 
      {
          if(res.Email)
          {
              this.user = res;
              this.inProgress = false;
          }
          else
          {
              if(res.response)
              {
                  if(res.response.data.Message)
                  {
                      console.log(res.response.data.Message);
                      this.inProgress = false;
                      return;
                  }
                  console.log('The server is unavailable or you do not have the rights');
                  this.inProgress = false;
              }
          }
      })
      .catch(e => 
      {
          console.log(e);
          this.inProgress = false;
      })
  }
}
</script>