<template>
  <div>
    <div class="loading" v-if = "inProgress">Loading&#8230;</div>

    <div class = "main-content">
        <span 
            style = "float: right; margin-right: 10px" 
            v-on:click = "clickChangePassword = true">
                <img 
                    title="Change password" 
                    style = "cursor: pointer; width: 30px; height: auto;" 
                    type = "img" 
                    src = "../assets/security-icon.png">
        </span>
        
        <span 
            style = "float: right; margin-top: 5px" 
            v-on:click = "clickEdit = true; fname = user.FirstName; lname = user.LastName; email = user.Email;">
                <img 
                    title="Edit profile" 
                    style = "cursor: pointer; width: 20px; height: auto;" 
                    type = "img" 
                    src = "../assets/edit-icon.png">
        </span>
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
            <span 
                style = "float: right; font-size:10px; cursor: pointer" 
                v-on:click = "closeEditForm()">
                    <img 
                        title="Close" 
                        style = "width: 20px; height: auto" 
                        type = "img" 
                        src = "../assets/close-icon.png">
            </span>
            
            <span 
                style = "float: right; font-size:10px; cursor: pointer" 
                v-on:click = "saveUser()">
                    <img 
                        title="Save" 
                        style = "width: 16px; height: auto; margin-top:2px;" 
                        type = "img" 
                        src = "../assets/save-icon.png">
            </span>
            
            <span>First name</span>
            
            <input 
                v-bind:class = "{'input-form--error': $v.fname.$error}" 
                class = "input-form" 
                type = "text" 
                v-model = "fname"/>
            <br/>
            
            <span>Last name</span>
            
            <input 
                v-bind:class = "{'input-form--error': $v.lname.$error}" 
                class = "input-form" 
                type = "text" 
                v-model = "lname"/>
            <br/>
            
            <span>Email</span>
            
            <input 
                v-bind:class = "{'input-form--error': $v.email.$error}" 
                class = "input-form" 
                type = "text" 
                v-model = "email"/>
            <br/>
            
            <span 
                v-if = "errorMessage" 
                class = "span-error-message">
                {{errorMessage}}. 
            </span>
            
            <span 
                v-if = "!$v.fname.required || !$v.lname.required || !$v.email.required" 
                class = "span-error-message">
                All fields must be filled in. 
            </span>
            
            <span 
                v-if = "!$v.email.email" 
                class = "span-error-message">
                Invalid Email. 
            </span>
          </div>
        </div>
    </div>
    
    <div v-if = "clickChangePassword" class = "b-popup">
        <div class = "b-popup-content">
          <div class = "form-border">
            <span 
                style = "float: right; font-size:10px; cursor: pointer" 
                v-on:click = "closeEditForm()">
                    <img 
                        title="Close" 
                        style = "width: 20px; height: auto" 
                        type = "img" 
                        src = "../assets/close-icon.png">
            </span>
            
            <span 
                style = "float: right; font-size:10px; cursor: pointer" 
                v-on:click = "changePassword()">
                    <img 
                        title="Change password" 
                        style = "width: 16px; height: auto; margin-top:2px;" 
                        type = "img" 
                        src = "../assets/save-icon.png">
            </span>
            
            <span>Old password</span>
            
            <input 
                v-bind:class = "{'input-form--error': $v.oldPassword.$error}" 
                class = "input-form" 
                type = "password" 
                v-model = "oldPassword" 
                style = "height: 30px; text-align: center;"/>
            <br/>
            
            <span>New password</span>
            
            <input 
                v-bind:class = "{'input-form--error': $v.newPassword.$error}" 
                class = "input-form" 
                type = "password" 
                v-model = "newPassword" 
                style = "height: 30px; text-align: center;"/>
            <br/>
            
            <span 
                v-if = "errorMessage" 
                class = "span-error-message">
                {{errorMessage}}. 
            </span>
            
            <span 
                v-if = "(!$v.oldPassword.required || !$v.newPassword.required) && ($v.oldPassword.$error || $v.newPassword.$error)" 
                class = "span-error-message">
                All fields must be filled in. 
            </span>
            
            <span 
                v-if = "!$v.newPassword.minlength && $v.newPassword.$error" 
                class = "span-error-message">
                The password must be at least 8 characters long, one big and small letter, one number.
            </span>
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
        fname: '',
        lname: '',
        email: '',
        errorMessage: '',
        clickEdit: false,
        clickChangePassword: false,
        oldPassword: '',
        newPassword: '',
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
            fname: { required },
            lname: { required },
            email: { required, email },
            oldPassword: { required },
            newPassword: 
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
          this.errorMessage = '';
          this.$v.$touch();
          if(this.$v.fname.$invalid || this.$v.lname.$invalid || this.$v.email.$invalid)
          {
              this.inProgress = false;
              return;
          }
          api.editUser({
            Id: this.user.Id,
            FirstName: this.fname,
            LastName: this.lname,
            Email: this.email,
            Role: this.user.Role
          })
          .then(result => {
              if(result === true){
                    this.inProgress = false;
                    this.user.FirstName = this.fname;
                    this.user.LastName = this.lname;
                    this.user.Email = this.email;
                    this.closeEditForm();
              }
              else
              {
                  if(result.response)
                  {
                      if(res.response.data.Message)
                      {
                        this.inProgress = false;
                        this.errorMessage = result.response.data.Message;
                        return;
                      }
                  }
                  this.inProgress = false;
                  this.errorMessage = 'The server is unavailable or you do not have the rights';
              }
          })
          .catch(e => {
              this.inProgress = false;
              this.errorMessage = 'The server is unavailable or you do not have the rights';
          })
          this.inProgress = false;
      },
      changePassword(){
          if(this.inProgress) return;
          this.inProgress = true;
          this.errorMessage = '';
          this.$v.$touch();
          if(this.$v.oldPassword.$invalid || this.$v.newPassword.$invalid)
          {
              this.inProgress = false;
              return;
          }
          api.changePassword(this.user.Id, this.oldPassword, this.newPassword)
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
                            this.errorMessage = result.response.data.Message;
                            return;
                        }
                    }
                    if(result === false)
                    {
                        this.inProgress = false;
                        this.errorMessage = 'Incorrect old password';
                        return;
                    }
                    this.inProgress = false;
                    this.errorMessage = 'The server is unavailable or you do not have the rights';
              }
          })
          .catch(e => {
              this.inProgress = false;
              this.errorMessage = 'The server is unavailable or you do not have the rights';
          })
          this.inProgress = false;
      },
      closeEditForm(){
        this.inProgress = false;
        this.errorMessage = '';
        this.clickEdit = false;
        this.clickChangePassword = false;
        this.oldPassword = '';
        this.newPassword = '';
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