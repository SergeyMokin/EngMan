<template>
  <div v-if = "$store.state.user.Role == 'admin'">
    <div class="loading" v-if = "inProgress">Loading&#8230;</div>
    <div v-if = "!click" class="rules-view-list">
      <router-link to="/admin/rules" class = "routes-admin">Правила </router-link>
      <router-link to="/admin/sentences" class = "routes-admin">Предложения </router-link>
      <router-link to="/admin/words" class = "routes-admin">Словарь </router-link>
      <router-link to="/admin/users" class = "routes-admin">Пользователи </router-link>
      <router-link to="/admin/guessestheimages" class = "routes-admin">Задания по картинкам</router-link><br/><br/>
      <button type = "submit" v-on:click = "add()">Добавить</button><br/><br/>
      <input type = "text" v-model="searchKey" class = "search-form" placeholder = "Поиск..."><br/>
      <div v-for = 'el in tasks' :key = 'el.Id' class = "form-border">
      <div class = "rules-list--element admin">
        <span class = "span-rule--element">
            {{el.Word}}
        </span>
      </div>
      <div>
        <button style = "right: 100px" type = "submit" v-on:click = "edit(el.Id)">Изменить</button>
        <button type = "submit" v-on:click = "deletetask(el.Id)">Удалить</button>
      </div>
    </div>
  <br/><br/>  
  </div>
  <div v-if = "click" style = "text-align: center">
          <br/><br/>
          <button type = "submit" v-on:click = "closeEditForm()" class = "button-close">Закрыть</button>
          <span>Слово на английском</span>
          <textarea class = "rule-edit" type = "text" v-model = "task.Word"/><br/>
          <div v-if = "clickAdd" style = "width: 60%; text-align: left; margin-left: 20%"><input type="file" accept="image/*" @change="onFileChange" class = "button-classic"><br/></div>
          <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
          <button type = "submit" v-on:click = "save(task)"><span style = "color: rgb(248, 248, 248);" v-if = "!clickAdd">Сохранить</span><span style = "color: rgb(248, 248, 248);" v-if = "clickAdd">Добавить</span></button><br/><br/>
  </div>
  </div>
</template>

<script>
import api from '../api/api'

export default {
  name: 'rules-view',
  data () {
    return {
        inProgress: false,
        searchKey: '',
        errormessage: '',
        clickAdd: false,
        click: false,
        image: {
            Name: '',
            Data: ''
        },
        task: {
            Id: 0,
            Word: '',
            Path: ''
        }
    }
  },
  methods:{
      onFileChange(e) {
        var arr = e.target.files || e.dataTransfer.files;
        if (!arr.length)
            return;
        var vue = this;
        this.image = {
            Name: arr[0].name,
            Data: ''
        }
        var load = (e) => {
            vue.image.Data = e.target.result;
        };
        for(var i = 0; i < arr.length; i++)
        {
            var reader = new FileReader();
            reader.onload = load;
            reader.readAsBinaryString(arr[i]);
        }
      },
      edit(id){
        if(this.inProgress) return;
        this.inProgress = true;
        api.getGuessesTheImageById(id)
        .then(result => {
                this.inProgress = false;
                this.task = result;
                this.click = true;
        })
        .catch(e => {
            this.inProgress = false;
        });
        this.inProgress = false;
      },
      save(task){
          if(this.inProgress) return;
          this.inProgress = true;
          this.errormessage = '';
          if(this.image.Data.length == 0 && this.clickAdd){
              this.errormessage = 'Выберите изображение';
              this.inProgress = false;
              return;
          }
          if(this.task.Word.length == 0)
          {
              this.errormessage = 'Заполните все поля';
              this.inProgress = false;
              return;
          }
          if(!this.clickAdd){
            api.editGuessesTheImage(task)
            .then(result => {
                if(result.Id > 0){
                    this.inProgress = false;
                    this.$store.dispatch('getGuessesTheImages');
                    this.closeEditForm();
                }
            })
            .catch(e => {
                this.inProgress = false;
                this.errormessage = 'Сервер недоступен или у вас нет прав';
            })
          } else{
              api.addGuessesTheImage({
                Id: this.task.Id,
                Word: this.task.Word,
                Image: {
                    Name: this.image.Name,
                    Data: this.image.Data
                }
            })
              .then(result =>{
                  if(result.Id > 0){
                      this.inProgress = false;
                      this.$store.dispatch('getGuessesTheImages');
                      this.closeEditForm();
                  }
                  this.inProgress = false;
              })
              .catch(e => {
                  this.inProgress = false;
                  this.errormessage = 'Сервер недоступен или у вас нет прав';
              })
          }
          this.inProgress = false;
      },
      deletetask(id){
          if(this.inProgress) return;
          this.inProgress = true;
          if(confirm("Вы уверены?") == true){
            api.deleteGuessesTheImage(id)
            .then(result =>{
                this.inProgress = false;
                this.$store.dispatch('getGuessesTheImages');
            })
          } else{
              this.inProgress = false;
              alert("Вы отменили удаление!");
          }
          this.inProgress = false;
      },
      add(){
          this.click = true;
          this.clickAdd = true;
      },
      closeEditForm(){
        this.inProgress = false,
        this.searchKey = '',
        this.errormessage = '',
        this.clickAdd = false,
        this.click = false,
        this.image = {
            Name: '',
            Data: ''
        },
        this.task = {
            Id: 0,
            Word: '',
            Path: ''
        }
      }
  },
  computed: 
  {
    tasks()
    {
      var vue = this;
      return this.$store.getters.guessestheimages.filter(function(task){
          return task.Word.toLowerCase().indexOf(vue.searchKey.toLowerCase()) > -1;
      });
    }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      this.$store.dispatch('getGuessesTheImages');
      this.inProgress = false;
  }
}
</script>