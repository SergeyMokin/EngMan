<template>
  <div v-show = "$store.state.user.Role == 'admin'">
      <div v-if = "!clickWord" class="words-view" >
        <router-link to="/admin/rules" class = "routes-admin">Правила </router-link>
        <router-link to="/admin/sentences" class = "routes-admin">Предложения </router-link>
        <router-link to="/admin/words" class = "routes-admin">Словарь </router-link><br/><br/>
        <button type = "submit" v-on:click = "addWord()">Добавить</button><br/><br/>
        <select class = "select-form" v-model = "category">
            <option v-for = "category in categories" :key = "category">
                {{category}}
            </option>
        </select>
        <div v-for = 'el in words' :key = 'el.WordId' class = "form-border">
            <div class = "words-list--element">
                <a>{{el.Original}} - {{el.Translate}}</a>
            </div>
            <button type = "submit" v-on:click = "editWord(el.WordId)">Изменить</button>
            <button type = "submit" v-on:click = "deleteWord(el.WordId)">Удалить</button>
        </div>
      </div>
      <div v-if = "clickWord" style = "text-align: center">
          <br/><br/>
          <button type = "submit" v-on:click = "closeEditForm()" class = "button-close">Закрыть</button>
          <span>Категория</span>
          <textarea type = "text" v-model = "word.Category" class = "word-edit"/><br/>
          <span>Оригинал на английском</span>
          <textarea type = "text" v-model = "word.Original" class = "word-edit"/><br/>
          <span>Русский перевод</span>
          <textarea type = "text" v-model = "word.Translate" class = "word-edit"/><br/>
          <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
          <button type = "submit" v-on:click = "saveWord(word)"><span style = "color: rgb(248, 248, 248);" v-if = "!clickAddWord">Сохранить</span><span style = "color: rgb(248, 248, 248);" v-if = "clickAddWord">Добавить</span></button><br/><br/>
      </div>
  </div>
</template>

<script>
import api from '../api/api'

export default {
  name: 'words-view',
  data () {
    return {
        inProgress: false,
        errormessage: '',
        categories: [],
        category: 'none',
        clickAddWord: false,
        clickWord: false
        , word: {
            WordId: 0,
            Original: '',
            Translate: '',
            Category: ''
        }
    }
  },
  methods:{
      editWord(id){
          if(this.inProgress) return;
          this.inProgress = true;
          api.getWord(id)
          .then(result => {
                this.inProgress = false;
                this.word = result;
                this.clickWord = true;
              })
          .catch(e => this.inProgress = false);
      },
      saveWord(word){
          if(this.inProgress) return;
          this.errormessage = '';
          this.inProgress = true;
          if(this.word.Original.length == 0 || this.word.Translate.length == 0 || this.word.Category.length == 0)
          {
              this.errormessage = 'Заполните все поля';
              this.inProgress = false;
              return;
          }
          if(!this.clickAddWord){
            api.editWord(word)
            .then(result => {
                if(result.WordId > 0){
                    this.$store.dispatch('getWords');
                    this.closeEditForm();
                }
            })
            .catch(e => {
                this.errormessage = 'Сервер недоступен или у вас нет прав';
                this.inProgress = false;  
            })
          } else{
              api.createWord(word)
              .then(result =>{
                  if(result.WordId > 0){
                      this.$store.dispatch('getWords');
                      this.closeEditForm();
                  }
              })
              .catch(e => {
                  this.errormessage = 'Сервер недоступен или у вас нет прав';
                  this.inProgress = false;
              })
          }
      },
      deleteWord(id){
          if(this.inProgress) return;
          this.inProgress = true;
          if(confirm("Вы уверены?") == true){
            api.deleteWord(id)
            .then(result =>{
                this.inProgress = false;
                this.$store.dispatch('getWords');
            })
          } else{
              this.inProgress = false;
              alert("Вы отменили удаление!");
          }
          this.inProgress = false;
      },
      addWord(){
          this.clickWord = true;
          this.clickAddWord = true;
      },
      closeEditForm(){
        this.inProgress = false;
        this.errormessage = '';
        this.category = 'none';
        this.clickAddWord = false;
        this.clickWord = false;
        this.word = {
            WordId: 0,
            Original: '',
            Translate: '',
            Category: ''
        }
      }
  },
  computed: 
  {
    words()
    {
      var vue = this;
      if(this.category == 'none' || this.category == '')
      {
          return this.$store.getters.words;
      }
      return this.$store.getters.words.filter(function(sentence){
          return sentence.Category.toLowerCase().indexOf(vue.category.toLowerCase()) > -1;
      });
    }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      api.getAllCategoriesWords()
      .then(res => {
          this.categories = res;
          this.categories.push('none');
      })
      this.$store.dispatch('getWords');
      this.inProgress = false;
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.words-view{
    margin: 5% 20% 5% 20%;
    width: 60%;
    text-align: center;
    display: table;
}
.words-list--element{
    margin: 10px;
    padding: 3px;
    text-align: left;
    cursor: default;
    background: rgb(248, 248, 248);
    height: 35px;
    border: none;
    outline:none;
    border-radius: 10px;
}
.span-word--element{
    margin: 15px;
    font-size: 18px;
}
.button-close{
    position: absolute;
    right: 18.65%;
    top: 75px;
}
.word-edit{
    resize: none;
    text-align: left;
    width: 60%;
    margin: 0.5% 20% 0% 20%;
    padding: 0.5%;
    background: rgb(248, 248, 248);
    border: none;
    outline:none;
    border-radius: 10px;
}
.select-form{
    outline:none;
    background: rgb(248, 248, 248);
    border: none;
    border-radius: 10px;
    text-align: center;
    font-size: 16px;
    margin: auto;
    margin-bottom: 10px;
    resize: none;
    height: 25px;
    width: 155px;
}
</style>