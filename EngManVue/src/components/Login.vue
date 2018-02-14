<template>
  <div class="login" id = "login-form">
      <div class="loading" v-if = "inprogress">Loading&#8230;</div>
      <div v-if = "!isLogined">
        <span>Для доступа к контенту сайта вам необходимо войти в свой аккаунт или зарегистрироваться: </span><br/>
            <div class = "routes-admin" style = "padding: 5px; cursor: pointer;" v-on:click = "authentification = !authentification; registration = false;">Войти в аккаунт</div>
            <div class = "routes-admin" style = "padding: 5px; cursor: pointer;" v-on:click = "registration = !registration; authentification = false;">Регистрация</div>
        <div class = "authentification-form form-border" v-if = "authentification">
            <div class = "form">
                <label for = "username-login">Мэил</label><br/>
                <input v-bind:class = "{'input-form--error': $v.user.username.$error}" class = "input-form" id = "username-login" type = 'text' v-model = "user.username"/><br/>
            </div>
            <div class = "form">
                <label for = "password-login">Пароль</label><br/>
                <input v-bind:class = "{'input-form--error': $v.user.password.$error}" class = "input-form" id = "password-login" type = 'password' v-model = "user.password"/><br/>
            </div>
            <div class = "routes-admin" style = "padding: 5px; cursor: pointer; margin: 5px;" v-on:click = "login()">Войти</div><br/>
            <span v-if = "$v.user.$error" class = "span-error-message">Мэил и пароль - обязательные поля для заполнения<br/></span>
            <span v-if = "badrequest" class = "span-error-message">Мэил или пароль введены неверно<br/></span>
        </div>
        <div class = "registration-form form-border" v-if = "registration">
            <div class = "form">
                <label for = "firstname-registration">Имя</label><br/>
                <input v-bind:class = "{'input-form--error': $v.registrationUser.FirstName.$error}" class = "input-form" id = "firstname-registration" type = 'text' v-model = "registrationUser.FirstName"/><br/>
            </div>
            <div class = "form">
                <label for = "lastname-registration">Фамилия</label><br/>
                <input v-bind:class = "{'input-form--error': $v.registrationUser.LastName.$error}" class = "input-form" id = "lastname-registration" type = 'text' v-model = "registrationUser.LastName"/><br/>
            </div>
            <div class = "form">
                <label for = "email-registration">Мэил</label><br/>
                <input v-bind:class = "{'input-form--error': $v.registrationUser.Email.$error}" class = "input-form" id = "email-registration" type = 'text' v-model = "registrationUser.Email"/><br/>
            </div>
            <div class = "form">
                <label for = "password-registration">Пароль</label><br/>
                <input v-bind:class = "{'input-form--error': $v.registrationUser.Password.$error}" class = "input-form" id = "password-registration" type = 'password' v-model = "registrationUser.Password"/><br/>
            </div>
            <div class = "form">
                <label for = "confirmpassword-registration">Подтвердите пароль</label><br/>
                <input v-bind:class = "{'input-form--error': $v.registrationUser.confirmPassword.$error}" class = "input-form" id = "confirmpassword-registration" type = 'password' v-model = "registrationUser.confirmPassword"/><br/>
            </div>
            <div class = "routes-admin" style = "padding: 5px; cursor: pointer; margin: 5px;" v-on:click = "login()">Зарегистрироваться</div><br/>
            <span v-if = "$v.registrationUser.$error" class = "span-error-message">Все поля должны быть заполнены<br/></span>
            <span v-if = "badrequest" class = "span-error-message">Пользователь с таким мэилом уже существует<br/></span>
            <span v-if = "$v.registrationUser.Email.$error" class = "span-error-message">Неверный мэил<br/></span>
            <span v-if = "$v.registrationUser.Password.$error" class = "span-error-message">Пароль должен иметь длину не менее 8 символов<br/></span>
            <span v-if = "$v.registrationUser.confirmPassword.$error" class = "span-error-message">Пароль и его подтверждение должны совпадать<br/></span>
        </div>
      </div>
      <div v-if = "isLogined">
          <span>Добро пожаловать на сайт, наведите мышкой на меню и перейдите в нужную вкладку!</span><br/><br/><br/>
          <!--<div class = "form-border">
            <span>Для навигации по сайту наведите мышкой на иконку меню</span><br/>
            <img title="Картинка навигации по сайту" src = "../assets/help-menu.png" style = "width: 300px; border: none; border-radius: 10px;"><br/>
            <span>Для управления аккаунтом нажмите на иконку головы слева вверху экрана</span><br/>
            <img title="Картинка навигации по сайту" src = "../assets/help-account.png" style = "width: 300px; border: none; border-radius: 10px;"><br/>
          </div>-->
      </div>
  </div>
</template>



<script>
import api from '../api/api'
import { required, minLength, sameAs, email } from 'vuelidate/lib/validators'

export default {
    data(){
        return {
            inprogress: false,
            badrequest: false,
            authentification: false,
            registration: false,
            clickAtForm: true,
            user: {
                grant_type: 'password'
                , username: ''
                , password: ''
            },
            registrationUser: {
                FirstName: '',
                LastName: '',
                Email: '',
                Password: '',
                confirmPassword: ''
            },
        }
    },
    validations: {
            user: {
                username: { required },
                password: { required }
            },
            registrationUser: {
                FirstName: { required },
                LastName: { required },
                Email: { required, email },
                Password: { required, minLength: minLength(8) },
                confirmPassword: { sameAsPassword: sameAs('Password') }
            },
    },
    methods: {
        login(){
            if(this.inprogress) return;
            this.inprogress = true;
            this.badrequest = false;
            this.$v.$touch();
            if(this.$v.user.$invalid) 
            {
                this.inprogress = false;
                return;
            };
            api.login(this.user)
            .then(result =>{
                if(result.userName)
                {
                    this.$cookie.delete('user.login.token.localhost:8080');
                    this.$cookie.set('user.login.token.localhost:8080', result.access_token, 1);
                    api.getUserData()
                    .then(res => {
                        if(res.Id)
                        {
                            this.$store.state.user.Logined = true;
                            this.$store.state.user.Id = res.Id;
                            this.$store.state.user.Role = res.Role;
                            this.$store.state.user.FirstName = res.FirstName;
                            this.$store.state.user.LastName = res.LastName;
                            this.$store.state.user.Email = res.Email;
                            this.$store.dispatch('getMessages');
                            this.$store.dispatch('getUsers');
                            this.clearLoginForm();
                            this.inprogress = false;
                        }
                        else{
                            console.log('NotFound404');
                        }
                    })
                }
                this.badrequest = true;
                this.inprogress = false;
            })
        },
        registrationUserReq(){
            if(this.inprogress) return;
            this.inprogress = true;
            this.badrequest = false;
            this.$v.$touch();
            if(this.$v.registrationUser.$invalid) 
            {
                this.inprogress = false;
                return;
            };
            var user = {
                Id: 0,
                FirstName: this.capitalizeFirstLetter(this.registrationUser.FirstName),
                LastName: this.capitalizeFirstLetter(this.registrationUser.LastName),
                Email: this.registrationUser.Email,
                Password: this.registrationUser.Password,
                Role: 'user'
            }
            api.registration(user)
            .then(res => {
                this.user.username = this.registrationUser.Email;
                this.user.password = this.registrationUser.Password;
                this.authentification = false;
                this.registration = false;
                this.showHelp = false;
                this.inprogress = false;
                this.login();
            })
        },
        clearLoginForm(){
            if(this.$store.state.user.Logined)
            {
                this.user.username = '';
                this.user.password = '';
                this.registrationUser.FirstName = '';
                this.registrationUser.LastName = '';
                this.registrationUser.Email = '';
                this.registrationUser.Password = '';
                this.registrationUser.confirmPassword = '';
                this.authentification = false;
                this.registration = false;
                this.showHelp = false;
                this.badrequest = false;
                this.inprogress = false;
                this.$v.$reset();
            }
            else{
                this.badrequest = true;
            }
        },
        capitalizeFirstLetter(str){
            return str.charAt(0).toUpperCase() + str.slice(1);
        }
    },
    computed: {
        isLogined(){
            if(this.$store.state.user.Logined) return true;
            return false;
        }
    }
}
</script>