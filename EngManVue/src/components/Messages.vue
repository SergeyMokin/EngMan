<template>
  <div class = 'messages-view'>
      <div class = "label-form">
            <span>Chat</span>
            <span style = "margin-left: 370px; font-size:10px; cursor: pointer" v-on:click = "closeform()">close</span>
      </div>
      <select class = "select-form" v-model = "beneficiaryEmail" v-on:change = "changeBeneficiary(beneficiaryEmail)">
            <option v-for = "user in users" v-if = "user.Id != $store.state.user.Id" :key = "user.Id">
                {{user.Email}}
            </option>
            <option>
                none
            </option>
      </select>
      <div class = "messages">
          <div v-for = "el in sortedmessages" :key = "el.MessageId">
              <div v-if = "el.Sender.Id == sender.Id" class = "label-sender"><span style = "font-size: 10px; cursor: pointer" v-on:click = "deleteMessage(el.MessageId)">delete</span> You</div>
              <div v-if = "el.Sender.Id == sender.Id" class = "message-sender">
                  {{el.Text}}
              </div>
              <div v-if = "el.Sender.Id != sender.Id" class = "label-beneficiary">{{el.Sender.FirstName}} <span style = "font-size: 10px; cursor: pointer" v-on:click = "deleteMessage(el.MessageId)">delete</span></div>
              <div v-if = "el.Sender.Id != sender.Id" class = "message-beneficiary">
                  {{el.Text}}
              </div>
          </div>
      </div>
      <div class = "input-form">
            <textarea type = "text" v-model = "message" :disabled = "beneficiaryEmail == 'none'"/>
      </div>
      <button v-on:click = "sendMessage">Отправить</button>
  </div>
</template>

<script>
import api from '../api/api'

export default {
  name: 'messages-view',
  data: () => {
      return {
          message: '',
          beneficiary: undefined,
          beneficiaryEmail: 'none',
          sender: ''
      }
  },
  methods: {
      closeform(){
          this.message = '';
          this.beneficiary = undefined;
          this.beneficiaryEmail = 'none';
          this.sender = '';
          this.$emit('closeMessages');
      },
      deleteMessage(id){
          if(confirm("Вы уверены, что хотите удалить сообщение?") == true){
            api.deleteMessage(id)
            .then(result =>{
                this.$store.dispatch('getMessages');
            })
          } else{
              alert("Вы отменили удаление!");
          }
      },
      changeBeneficiary(email){
          this.message = '';
          if(this.beneficiaryEmail == 'none'){
               this.beneficiary = undefined;
               return;
          }
          this.beneficiary = this.$store.getters.users.filter(function(user){
              return user.Email == email;
          })[0];
      },
      sendMessage(){
          if(this.beneficiary != undefined)
          {
            if(this.message.length > 0)
            {
                api.sendMessage({
                    MessageId: 0,
                    SenderId: this.sender.Id,
                    BeneficiaryId: this.beneficiary.Id,
                    Text: this.message
                })
                .then(res => 
                {
                    if(res.MessageId > 0)
                    {
                        this.$store.dispatch('getMessages')
                    }
                })
            }   
          }
          this.message = '';
      }
  },
  computed: {
      users(){
          return this.$store.getters.users;
      },
      sortedmessages(){
          var vue = this;
          if(this.beneficiary != undefined)
          return this.$store.getters.messages.filter(function(mes){
              return mes.Beneficiary.Id == vue.beneficiary.Id || mes.Sender.Id == vue.beneficiary.Id
          });
      }
  },
  created(){
      this.$store.dispatch('getMessages');
      this.$store.dispatch('getUsers');
      this.sender = this.$store.state.user;
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.messages-view{
    position: fixed;
    right: 1%;
    bottom: 2.5%;
    width: 450px;
    height: 500px;
    text-align: center;
    display: table;
    background: rgb(248, 248, 248);
    border: solid 1px;
    outline:none;
    border-radius: 10px;
}
.messages{
    margin: 0.5%;
    margin-left: 10px;
    height: 345px;
    overflow-y: scroll;
}
.label-form{
    background: rgba(0,0,0,.15);
    border-radius: 10px 10px 0 0;
    text-align: left;
    padding: 0.5%;
    height: 25px;
    font-weight: bold;
}
.message-beneficiary{
    margin: 0.5%;
    text-align: left;
    padding: 0.5%;
    border: none;
    border-bottom-style: dashed;
    border-width: 0.7px;
    outline:none;
    font-size: 16px;
}
.message-sender{
    margin: 0.5%;
    text-align: right;
    padding: 0.5%;
    border: none;
    border-bottom-style: dashed;
    border-width: 0.7px;
    outline:none;
    font-size: 16px;
}
.label-sender{
    margin: 0.5%;
    text-align: right;
    font-size: 16px;
    font-weight: bold;
}
.label-beneficiary{
    margin: 0.5%;
    text-align: left;
    font-size: 16px;
    font-weight: bold;
}
.input-form{
    height: 50px;
}
textarea{
    width: 400px;
    height: 45px;
    border-radius: 10px;
    border-style: solid;
    border-width: 1px;
    border-color:rgb(75, 75, 75);
    outline: none;
    resize: none;
    padding: 0.5%;
}
.select-form{
    outline:none;
    background: rgba(0,0,0,.1);
    border: none;
    border-radius: 10px;
    text-align: center;
    font-size: 16px;
    margin: 0.5%;
    resize: none;
    height: 25px;
    width: 350px;
}
</style>