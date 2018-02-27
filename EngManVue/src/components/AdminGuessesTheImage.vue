<!--edit-->
<template>
  <div v-if = "$store.state.user.Role == 'admin'">
    <div class="loading" v-if = "inProgress">Loading&#8230;</div>
    <div v-if = "!click" class="view-list">
      <router-link to="/admin/rules" class = "routes-admin">Правила </router-link>
      <router-link to="/admin/sentences" class = "routes-admin">Предложения </router-link>
      <router-link to="/admin/words" class = "routes-admin">Словарь </router-link>
      <router-link to="/admin/users" class = "routes-admin">Пользователи </router-link>
      <router-link to="/admin/guessestheimages" class = "routes-admin">Задания по картинкам</router-link><br/><br/>
      <span style = "cursor: pointer;" v-on:click = "add()"><img title="Добавить" style = "width: 30px; height: auto" type = "img" src = "../assets/add-icon.png"></span><br/><br/>
      <input type = "text" v-model="searchKey" class = "select-form" placeholder = "Поиск..." v-on:click = "searchKey = ''"><br/>
      <div v-for = 'el in tasks' :key = 'el.Id'>
      <div class = "list--element">
        <span class = "span--element">
            {{el.Word.Original}}
            <span style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "deletetask(el.Id)"><img title="Удалить" style = "width: 20px; height: auto" type = "img" src = "../assets/close-icon.png"></span>
            <span style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "edit(el.Id)"><img title="Изменить" style = "margin-right: 5px; width: 18px; height: auto" type = "img" src = "../assets/edit-icon.png"></span>
        </span>
      </div>
    </div>
  </div>
  <div v-if = "click" style = "text-align: center">
          <br/><br/>
          <span v-on:click = "closeEditForm()"><img title="Закрыть" style = "width: 20px; height: auto;" class = "icon-close" type = "img" src = "../assets/close-icon.png"></span>
          <span v-on:click = "save(task)"><img title="Сохранить" style = "width: 18px; height: auto; margin-right: 30px; margin-top: 2px" class = "icon-close" type = "img" src = "../assets/save-icon.png"></span>
          <div style = "width: 60%; text-align: left; margin-left: 20%">
          <span>Категория: </span>
          <input placeholder="Выберите..." type="text" class = "select-form" list="guessestheimage_category" v-model = "searchKey" v-on:click = "searchKey = ''"/>
          <datalist id = "guessestheimage_category">
            <option v-for = "category in categories" :key = "category">
                {{category}}
            </option>
          </datalist><br/>
          <span>Слово на английском: </span>
          <input placeholder="Выберите..." type="text" class = "select-form" list="guessestheimage_word" v-model = "choosenWord" v-on:click = "choosenWord = ''"/>
          <datalist id = "guessestheimage_word">
            <option v-for = "word in words" :key = "word.WordId">
                {{word.Original}}
            </option>
          </datalist><br/>
          </div>
          <div style = "width: 60%; text-align: left; margin-left: 20%"><input type="file" accept="image/*" @change="onFileChange"><br/></div>
          <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span><br/><br/>
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
        categories: [],
        clickAdd: false,
        click: false,
        choosenWord: '',
        image: {
            Name: '',
            Data: ''
        },
        task: {
            Id: 0,
            WordId: 0,
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
          this.task.WordId = this.wordComputedId();
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
              api.addGuessesTheImage(task)
              .then(result =>{
                  if(result.Id > 0){
                      this.inProgress = false;
                      this.$store.dispatch('getGuessesTheImages');
                      this.closeEditForm();
                  }
                  this.errormessage = 'Сервер недоступен или у вас нет прав';
                  this.inProgress = false;
              })
              .catch(e => {
                  this.inProgress = false;
                  this.errormessage = 'Сервер недоступен или у вас нет прав';
              })
          }
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
          this.$store.dispatch('getWords');
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
          return task.Word.Original.toLowerCase().indexOf(vue.searchKey.toLowerCase()) > -1;
      });
    },
    words()
    {
      var vue = this;
      return this.$store.getters.words.filter(function(task){
          return task.Category.toLowerCase().indexOf(vue.searchKey.toLowerCase()) > -1;
      });
    },
    wordComputedId(){
        var index = this.words.indexOf(this.choosenWord);
        return index > -1 ? this.words[index].WordId : 0;
    }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      this.$store.dispatch('getGuessesTheImages');
      api.getAllCategoriesWords()
      .then(res => {
          this.categories = res;
          this.inProgress = false;
      })
      .catch(e => {
          this.inProgress = false;
      })
  }
}
</script>