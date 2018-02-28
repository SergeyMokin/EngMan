<template>
  <div class="login" id = "login-form" v-bind:class = "{blackout: registration||authentification}">
      <div class="loading" v-if = "inprogress">Loading&#8230;</div>
      <div v-if = "!isLogined">
        <span style = "font-size: 25px">To access the content of the site you need to log in or register: </span><br/><br/>
            <div class = "routes-admin" style = "padding: 15px; cursor: pointer; font-size:25px" v-on:click = "authentification = !authentification; registration = false;">Log in</div>
            <div class = "routes-admin" style = "padding: 15px; cursor: pointer; font-size:25px" v-on:click = "registration = !registration; authentification = false;">Registration</div>
        <div class = "blackout authentification-form" v-if = "authentification">
            <div class = "login-form">
                <label for = "username-login">Email</label><br/>
                <input v-bind:class = "{'input-form--error': $v.user.username.$error}" class = "input-form" id = "username-login" type = 'text' v-model = "user.username"/><br/>
            </div>
            <div class = "login-form">
                <label for = "password-login">Password</label><br/>
                <input v-bind:class = "{'input-form--error': $v.user.password.$error}" class = "input-form" id = "password-login" type = 'password' v-model = "user.password"/><br/>
            </div>
            <div class = "routes-admin" style = "padding: 5px; cursor: pointer; margin: 5px;width: 100px;" v-on:click = "login()">Login</div><br/>
            <span v-if = "$v.user.$error" class = "span-error-message">Mail and password are required<br/></span>
            <span v-if = "badrequest" class = "span-error-message">Incorrect email or password<br/></span>
        </div>
        <div class = "authentification-form" v-if = "registration">
            <div class = "login-form">
                <label for = "firstname-registration">First name</label><br/>
                <input v-bind:class = "{'input-form--error': $v.registrationUser.FirstName.$error}" class = "input-form" id = "firstname-registration" type = 'text' v-model = "registrationUser.FirstName"/><br/>
            </div>
            <div class = "login-form">
                <label for = "lastname-registration">Last name</label><br/>
                <input v-bind:class = "{'input-form--error': $v.registrationUser.LastName.$error}" class = "input-form" id = "lastname-registration" type = 'text' v-model = "registrationUser.LastName"/><br/>
            </div>
            <div class = "login-form">
                <label for = "email-registration">Email</label><br/>
                <input v-bind:class = "{'input-form--error': $v.registrationUser.Email.$error}" class = "input-form" id = "email-registration" type = 'text' v-model = "registrationUser.Email"/><br/>
            </div>
            <div class = "login-form">
                <label for = "password-registration">Password</label><br/>
                <input v-bind:class = "{'input-form--error': $v.registrationUser.Password.$error}" class = "input-form" id = "password-registration" type = 'password' v-model = "registrationUser.Password"/><br/>
            </div>
            <div class = "login-form">
                <label for = "confirmpassword-registration">Confirm password</label><br/>
                <input v-bind:class = "{'input-form--error': $v.registrationUser.confirmPassword.$error}" class = "input-form" id = "confirmpassword-registration" type = 'password' v-model = "registrationUser.confirmPassword"/><br/>
            </div>
            <div class = "routes-admin" style = "padding: 5px; cursor: pointer; margin: 5px;" v-on:click = "registrationUserReq()">Sign Up</div><br/>
            <span v-if = "$v.registrationUser.$error" class = "span-error-message">All fields must be filled in<br/></span>
            <span v-if = "badrequest" class = "span-error-message">A user with this email already exists<br/></span>
            <span v-if = "$v.registrationUser.Email.$error" class = "span-error-message">Invalid Email<br/></span>
            <span v-if = "$v.registrationUser.Password.$error" class = "span-error-message">The password must be at least 8 characters long, one big and small letter, one number<br/></span>
            <span v-if = "$v.registrationUser.confirmPassword.$error" class = "span-error-message">The password and its confirmation must match<br/></span>
        </div>
      </div>
      <div v-if = "isLogined" class = "home-page-info">
          <span>Welcome to the site!</span><br/><br/>
          <span>Here you can study the material about English and practice your knowledge.</span>
      </div>
  </div>
</template>

<style scoped>
.home-page-info{
    font-size: 25px;
}
.authentification-form {
    width: 250px;
    position: absolute;
    top: 45%;
    left: 50%;
    margin: -125px 0 0 -125px;
}
.login-form{
    margin-top: 2%;
}
.login{
    margin: 5%;
    text-align: center;
}
</style>

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
            api.login( 
            {
                Email: this.user.username,
                Password: this.user.password
            })
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
                            this.$store.dispatch('connectToServ', this.$store.state.user);
                            this.clearLoginForm();
                            this.inprogress = false;
                        }
                        else{
                            console.log(result);
                            console.log('NotFound404');
                            this.inprogress = false;
                            this.badrequest = true;
                        }
                    })
                }
                else{
                    console.log(result);
                    console.log('NotFound404');
                    this.inprogress = false;
                    this.badrequest = true;
                }
            })
            .catch(e => {
                console.log('NotFound404');
                this.inprogress = false;
                this.badrequest = true;
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
