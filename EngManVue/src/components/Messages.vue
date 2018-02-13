<template>
  <div id = "messages-view" class = 'messages-view'>
      <div class="loading" v-if = "inProgress">Loading&#8230;</div>
      <div class = "label-form-mes">
            <span>Chat</span>
            <span style = "margin-left: 370px; font-size:10px; cursor: pointer" v-on:click = "closeform(); clickCloseButton = true;"><img title="Закрыть окно" style = "width: 20px; height: auto" type = "img" src = "../assets/close-icon.png"></span>
      </div>
      <input placeholder="Введите/выберите мэил" type="text" class = "select-form-mes" list="users_emails" v-model = "beneficiaryEmail" v-on:change = "changeBeneficiary(beneficiaryEmail)"/>
      <datalist id = "users_emails">
            <option v-for = "user in users" v-if = "user.Id != $store.state.user.Id" :key = "user.Id">
                {{user.Email}}
            </option>
      </datalist>
      <div v-if = "beneficiary == undefined && beneficiaryEmail == '' && $store.state.newmessUsers.length > 0" class = "new-mess-users-list">
          <p><b>New messages:</b></p>
          <div v-for = "el in $store.state.newmessUsers" :key = "el.Id" class = "new-mess-users-element" v-on:click = "beneficiaryEmail = el.Email;changeBeneficiary(beneficiaryEmail)">
            {{el.Email}}
          </div>
      </div>
      <div v-else class = "messages">
          <div v-for = "el in sortedmessages" :key = "el.MessageId">
              <div v-if = "el.Sender.Id == sender.Id" class = "label-sender"><span style = "font-size: 10px; cursor: pointer" v-on:click = "deleteMessage(el.MessageId)">delete</span> <span style = "font-size: 10px">{{dateTime(el.Time)}}</span> You</div>
              <div v-if = "el.Sender.Id == sender.Id" class = "message-sender">
                  {{el.Text}}
              </div>
              <div v-if = "el.Sender.Id != sender.Id" class = "label-beneficiary">{{el.Sender.FirstName}} <span style = "font-size: 10px">{{dateTime(el.Time)}}</span> <span style = "font-size: 10px; cursor: pointer" v-on:click = "deleteMessage(el.MessageId)">delete</span></div>
              <div v-if = "el.Sender.Id != sender.Id" class = "message-beneficiary">
                  {{el.Text}}
              </div>
          </div>
      </div>
      <div class = "input-form-mes">
            <textarea class = "textarea-mes" type = "text" v-model = "message" :disabled = "beneficiaryEmail == 'none'"/>
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
      closeform(){
          this.inProgress = false;
          this.message = '';
          this.beneficiary = undefined;
          this.beneficiaryEmail = 'none';
          this.sender = '';
          this.$emit('closeMessages');
      },
      deleteMessage(id){
          if(this.inProgress) return;
          this.inProgress = true;
          if(confirm("Вы уверены, что хотите удалить сообщение?") == true){
            api.deleteMessage(id)
            .then(result =>{
                this.$store.dispatch('getMessages');
                this.inProgress = false;
            })
          } else{
              alert("Вы отменили удаление!");
              this.inProgress = false;
          }
      },
      changeBeneficiary(email){
          if(this.inProgress) return;
          this.inProgress = true;
          this.message = '';
          if(this.beneficiaryEmail == 'none'){
               this.beneficiary = undefined;
               this.inProgress = false;
               return;
          }
          this.beneficiary = this.$store.getters.users.filter(function(user){
              return user.Email == email;
          })[0];
          if(this.$store.state.newmess)
          {
            for(var i = 0; i < this.sortedmessages.length; i++)
            {
                if(!this.sortedmessages[i].CheckReadMes)
                {
                    api.ReadMessages(this.sortedmessages)
                    .then(res => {
                        this.inProgress = false;
                        if(res != undefined)
                        {
                            if(res.length > 0)
                            {
                                this.$store.dispatch('getMessages');
                            }
                        }
                    })
                    .catch(e => {
                        this.inProgress = false;
                    })
                }
            }
            this.inProgress = false;
          }
          else{
            this.inProgress = false;
          }
      },
      sendMessage(){
          if(this.inProgress) return;
          this.inProgress = true;
          if(this.beneficiary != undefined)
          {
            if(this.message.length > 0)
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
                    if(res.MessageId > 0)
                    {
                        this.$store.dispatch('getMessages')
                    }
                })
            }   
          }
          this.message = '';
          this.inProgress = false;
      },
      dateTime(_date){
                var date = new Date(_date);
                var hours = date.getHours() < 10 ? "0"+date.getHours() : date.getHours();
                var minutes = date.getMinutes() < 10 ? "0"+date.getMinutes() : date.getMinutes();
                return date.getDate() + '.' + (date.getMonth()+1) + '.' + date.getFullYear() + ' - ' + hours + ":" + minutes;
      }
  },
  computed: {
      users(){
          return this.$store.getters.users;
      },
      sortedmessages(){
          this.sender = this.$store.state.user;
          var vue = this;
          if(this.beneficiary != undefined && this.beneficiaryEmail != '')
          return this.$store.getters.messages.filter(function(mes){
              return mes.Beneficiary.Id == vue.beneficiary.Id || mes.Sender.Id == vue.beneficiary.Id
          });
      }
  },
  created(){
      this.$store.dispatch('getMessages');
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