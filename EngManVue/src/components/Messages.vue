<template>
<div>
  <div class="loading" v-if = "inProgress">Loading&#8230;</div>
  <div id = "messages-view" class = 'messages-view'>
      <div class = "label-form-mes">
            <span>Chat</span>
            <span style = "float: right; font-size:10px; cursor: pointer" v-on:click = "closeform(); clickCloseButton = true;"><img title="Close" style = "width: 20px; height: auto" type = "img" src = "../assets/close-icon.png"></span>
      </div>
      <input :disabled = "chooseUser" v-bind:class = "{'messages-input-choosen': chooseUser}" placeholder="Search..." type="text" class = "select-form-mes" list="users_emails" v-model = "beneficiaryEmail" v-on:change = "changeBeneficiary(beneficiaryEmail)"/>
      <span v-if = "chooseUser" style = "float: left; font-size:10px; cursor: pointer" v-on:click = "activeInputChooseUser"><img title="Return to user selection" style = "width: 20px; height: auto" type = "img" src = "../assets/arrow-up.png"></span>
      <datalist id = "users_emails">
            <option v-for = "user in users" v-if = "user.Id != $store.state.user.Id" :key = "user.Id">
                {{user.Email}}
            </option>
      </datalist>
      <div v-if = "beneficiary == undefined && beneficiaryEmail == ''" class = "new-mess-users-list" style = "height: 330px;">
          <div v-for = "el in $store.state.newmessages" :key = "el.MessageId" class = "message-beneficiary pointer" style = "white-space: nowrap;text-overflow: ellipsis;overflow: hidden;" v-on:click = "beneficiaryEmail = el.Sender.Email;changeBeneficiary(beneficiaryEmail)">
            {{el.Sender.FirstName}}: {{el.Text}}
          </div>
      </div>
      <div v-else class = "messages">
          <div v-for = "el in $store.getters.messages" :key = "el.MessageId">
              <div v-if = "el.Sender.Id == sender.Id" class = "label-sender"><span style = "font-size: 10px; cursor: pointer" v-on:click = "deleteMessage(el.MessageId)">delete</span> <span style = "font-size: 10px">{{dateTime(el.Time)}}</span> You</div>
              <div v-if = "el.Sender.Id == sender.Id" class = "message-sender">
                  {{el.Text}}
              </div>
              <div v-if = "el.Sender.Id != sender.Id" class = "label-beneficiary">{{el.Sender.FirstName}} <span style = "font-size: 10px">{{dateTime(el.Time)}}</span> <span style = "font-size: 10px; cursor: pointer" v-on:click = "deleteMessage(el.MessageId)">delete</span></div>
              <div v-if = "el.Sender.Id != sender.Id" class = "message-beneficiary">
                  {{el.Text}}
              </div>
          </div>
            <a v-if = "canDownloadMore" v-on:click="loadingMessages"
               style = "-moz-user-select: none;-khtml-user-select: none; user-select: none; cursor:pointer;">
                    more...
            </a>
      </div>
      <div v-if = "chooseUser" class = "input-form-mes" v-on:keyup.enter="sendMessage()">
            <textarea placeholder = "Type a message" class = "textarea-mes" type = "text" v-model = "message" :disabled = "beneficiaryEmail == ''" v-on:click = "readUnreadMessages()"/>
            <span style = "float: right; font-size: 10px; cursor: pointer; margin-right: 25px;" v-on:click = "sendMessage();"><img title="Send" style = "width: 30px; height: auto" type = "img" src = "../assets/send-icon.png"></span>
      </div>
  </div>
</div>
</template>

<script>
import api from '../api/api'

export default {
  name: 'messages-view',
  data: () => {
      return {
          chooseUser: false,
          clickCloseButton: false,
          clickAtForm: false,
          inProgress: false,
          message: '',
          beneficiary: undefined,
          beneficiaryEmail: '',
          sender: ''
      }
  },
  methods: {
      loadingMessages(){
        this.$store.dispatch("getMessagesByUserId", { otherUserId: this.beneficiary.Id, lastReceivedMessageId: this.$store.getters.messages[this.$store.getters.messages.length-1].MessageId }); 
      },
      activeInputChooseUser(){
          this.chooseUser = false;
          this.beneficiary = undefined;
          this.beneficiaryEmail = '';
          this.$store.state.messages = [];
          this.$store.state.endOfMessages = false;
          this.message = '';
      },
      closeform(){
          this.inProgress = false;
          this.message = '';
          this.beneficiary = undefined;
          this.beneficiaryEmail = 'none';
          this.sender = '';
          this.$store.state.messages = [];
          this.$store.state.endOfMessages = false;
          this.$emit('closeMessages');
      },
      deleteMessage(id){
          if(this.inProgress) return;
          this.inProgress = true;
          if(confirm("Are you sure you want to delete the message?") == true){
            api.deleteMessage(id)
            .then(result =>{
                if(result === "Delete completed successful")
                {
                    this.$store.dispatch('getMessagesByUserId', { otherUserId: this.beneficiary.Id, lastReceivedMessageId: 0 });
                    this.inProgress = false;
                }
                else
                {
                    if(result.response)
                    {
                        if(result.response.data.Message)
                        {
                            this.inProgress = false;
                            console.log(result.response.data.Message);
                            return;
                        }
                    }
                    console.log('Bad request 400');
                    this.inProgress = false;
                }
            })
            .catch(e => {
                console.log(e);
                this.inProgress = false;
            })
          } else{
              alert("You canceled the deletion!");
              this.inProgress = false;
          }
      },
      changeBeneficiary(email){
          if(this.inProgress) return;
          this.inProgress = true;
          this.message = '';
          this.chooseUser = false;
          if(this.beneficiaryEmail == ''){
               this.beneficiary = undefined;
               this.inProgress = false;
               return;
          }
          this.beneficiary = this.$store.getters.users.filter(function(user){
              return user.Email == email;
          })[0];
          this.$store.dispatch("getMessagesByUserId", { otherUserId: this.beneficiary.Id, lastReceivedMessageId: 0 });
          this.inProgress = false;
          this.chooseUser = true;
      },
      sendMessage(){
          if(this.inProgress) return;
          this.inProgress = true;
          if(this.beneficiary != undefined)
          {
            if(this.message !== undefined && this.message !== null)
            {
                if(this.message.match(/([A-Z]|[a-z]|[0-9]|[А-Я]|[а-я])/g))
                {
                    var date = new Date(Date.now());
                    var hours = date.getHours() < 10 ? "0"+date.getHours() : date.getHours();
                    var minutes = date.getMinutes() < 10 ? "0"+date.getMinutes() : date.getMinutes();
                    var seconds = date.getSeconds() < 10 ? "0"+date.getSeconds() : date.getSeconds();
                    var month = (date.getMonth()+1) < 10 ? "0"+(date.getMonth()+1) : (date.getMonth()+1);
                    var day = date.getDate() < 10 ? "0"+date.getDate() : date.getDate();
                    api.sendMessage({
                        MessageId: 0,
                        SenderId: this.sender.Id,
                        BeneficiaryId: this.beneficiary.Id,
                        Text: this.message,
                        Time: date.getFullYear() + "-" + month + "-" + day + "T" + hours + ":" + minutes + ":" + seconds + "+03:00",
                        CheckReadMes: 0
                    })
                    .then(res => 
                    {
                        if(res === true)
                        {
                            var proxy = this.$store.state.connectionSignalR.createHubProxy('chat');
                            proxy.invoke("Send", {
                                MessageId: 0,
                                SenderId: this.sender.Id,
                                BeneficiaryId: this.beneficiary.Id,
                                Text: this.message,
                                Time: date.getFullYear() + "-" + month + "-" + day + "T" + hours + ":" + minutes + ":" + seconds + "+03:00",
                                CheckReadMes: 0
                            });
                            this.$store.dispatch('getMessagesByUserId', { otherUserId: this.beneficiary.Id, lastReceivedMessageId: 0 })
                        }
                        else
                        {
                            if(res.response)
                            {
                                if(res.response.data.Message)
                                {
                                    this.inProgress = false;
                                    console.log(res.response.data.Message);
                                    return;
                                }
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
          }
          this.message = '';
          this.inProgress = false;
      },
      dateTime(_date){
                var date = new Date(_date);
                var hours = date.getHours() < 10 ? "0"+date.getHours() : date.getHours();
                var minutes = date.getMinutes() < 10 ? "0"+date.getMinutes() : date.getMinutes();
                return date.getDate() + '.' + (date.getMonth()+1) + '.' + date.getFullYear() + ' ' + hours + ":" + minutes;
      },
      readUnreadMessages()
      {
          if(this.$store.state.newmess)
          {
              for(var i = 0; i < this.$store.state.newmessages.length; i++)
              {
                  if(!this.$store.state.newmessages[i].CheckReadMes)
                  {
                    if(this.beneficiary.Id === this.$store.state.newmessages[i].Sender.Id 
                    || this.beneficiary.Id === this.$store.state.newmessages[i].Beneficiary.Id)
                    {
                        api.ReadMessages(this.beneficiary.Id)
                        .then(res => {
                            this.inProgress = false;
                            if(res != undefined)
                            {
                                if(res.length > 0)
                                {
                                    this.$store.dispatch('getMessagesByUserId', { otherUserId: this.beneficiary.Id, lastReceivedMessageId: 0 });
                                    this.$store.dispatch('getNewMessages');
                                }
                                else
                                {
                                    if(res.response)
                                    {
                                        if(res.response.data.Message)
                                        {
                                            this.inProgress = false;
                                            console.log(res.response.data.Message);
                                            return;
                                        }
                                    }
                                    console.log('Bad request 400');
                                    this.inProgress = false;
                                }
                            }
                        })
                        .catch(e => {
                            console.log(e);
                            this.inProgress = false;
                        })
                    }
                  }
              }
          }
      }
  },
  computed: {
      users(){
          return this.$store.getters.users;
      },
      canDownloadMore()
      {
          return this.$store.getters.messages.length > 0 
          && this.$store.getters.messages.length % 100 == 0
          && !this.$store.state.endOfMessages;
      }  
  },
  created(){
      this.$store.dispatch('getNewMessages');
      this.$store.dispatch('getUsers');
      this.sender = this.$store.state.user;
  },
  mounted(){
        document.getElementById('messages-view').addEventListener('click', clickAtForm, false);
        document.body.addEventListener('click', clickAtBody, false);
        var vue = this;
        function clickAtForm(event){
            vue.clickAtForm = true;
            if(vue.clickCloseButton)
            {
                document.body.removeEventListener('click', clickAtBody);
            }
        };
        function clickAtBody(event){
            if(!vue.clickAtForm){
                vue.clickAtForm = true;
                document.body.removeEventListener('click', clickAtBody);
                vue.$emit('closeMessages');
                vue.closeform();
                return;
            }
            vue.clickAtForm = false;
        };
  }
}
</script>