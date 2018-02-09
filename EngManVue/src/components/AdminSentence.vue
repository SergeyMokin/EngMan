<template>
  <div v-show = "$store.state.user.Role == 'admin'">
      <div class="loading" v-if = "inProgress">Loading&#8230;</div>
      <div v-if = "!clickSentence" class = "sentences-view">
        <router-link to="/admin/rules" class = "routes-admin">Правила </router-link>
        <router-link to="/admin/sentences" class = "routes-admin">Предложения </router-link>
        <router-link to="/admin/words" class = "routes-admin">Словарь </router-link>
        <router-link to="/admin/users" class = "routes-admin">Пользователи </router-link>
      <router-link to="/admin/guessestheimages" class = "routes-admin">Задания по картинкам</router-link><br/><br/>
        <button type = "submit" v-on:click = "AddSentence()">Добавить</button><br/><br/>
        <select class = "select-form" v-model = "category">
            <option v-for = "category in categories" :key = "category">
                {{category}}
            </option>
        </select>
        <div v-for = 'el in sentences' :key = 'el.SentenceTaskId' class = "form-border">
            <div class = "sentences-list--element">
                <span class = "span-sentence--element">{{el.Sentence}}</span>
            </div>
            <button type = "submit" v-on:click = "editSentence(el.SentenceTaskId)">Изменить</button>
            <button type = "submit" v-on:click = "deleteSentence(el.SentenceTaskId)">Удалить</button>
        </div>
      </div>
      <div v-if = "clickSentence" style = "text-align: center">
          <br/><br/>
          <button type = "submit" v-on:click = "closeEditForm()" class = "button-close">Закрыть</button>
          <span>Категория</span>
          <textarea type = "text" v-model = "sentence.Category" class = "sentence-edit"/><br/>
          <span>Предложение</span>
          <textarea type = "text" v-model = "sentence.Sentence" class = "sentence-edit"/><br/>
          <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
          <button type = "submit" v-on:click = "saveSentence(sentence)"><span style = "color: rgb(248, 248, 248);" v-if = "!clickAddSentence">Сохранить</span><span style = "color: rgb(248, 248, 248);" v-if = "clickAddSentence">Добавить</span></button><br/><br/>
      </div>
  </div>
</template>

<script>
import api from '../api/api'

export default {
  name: 'sentences-view',
  data () {
    return {
        inProgress: false,
        errormessage: '',
        categories: [],
        category: 'none',
        clickAddSentence: false,
        clickSentence: false
        , sentence: {
            SentenceTaskId: 0,
            Sentence: '',
            Category: ''
        }
    }
  },
  methods:{
      editSentence(id){
          if(this.inProgress) return;
          this.inProgress = true;
          api.getSentence(id)
          .then(result => {
                this.inProgress = false;
                this.sentence = result;
                this.clickSentence = true;
              })
          .catch(e => {
              this.inProgress = false;
          });
      },
      saveSentence(sentence){
          if(this.inProgress) return;
          this.errormessage = '';
          this.inProgress = true;
          if(this.sentence.Sentence.length == 0 || this.sentence.Category.length == 0)
          {
              this.errormessage = 'Заполните все поля';
              this.inProgress = false;
              return;
          }
          if(!this.clickAddSentence){
            api.editSentence(sentence)
            .then(result => {
                if(result.SentenceTaskId > 0){
                    this.$store.dispatch('getSentences');
                    this.closeEditForm();
                }
            })
            .catch(e => 
            {
                this.errormessage = 'Сервер недоступен или у вас нет прав';
                this.inProgress = false;                
            })
          } else{
              api.createSentence(sentence)
              .then(result =>{
                  if(result.SentenceTaskId > 0){
                      this.$store.dispatch('getSentences');
                      this.closeEditForm();
                  }
              })
              .catch(e => {
                  this.errormessage = 'Сервер недоступен или у вас нет прав';
                  this.inProgress = false;
              })
          }
      },
      deleteSentence(id){
          if(this.inProgress) return;
          this.inProgress = true;
          if(confirm("Вы уверены?") == true){
            api.deleteSentence(id)
            .then(result =>{
                this.inProgress = false;
                this.$store.dispatch('getSentences');
            })
          } else{
              this.inProgress = false;
              alert("Вы отменили удаление!");
          }
          this.inProgress = false;
      },
      AddSentence(){
          this.clickSentence = true;
          this.clickAddSentence = true;
      },
      closeEditForm(){
        this.inProgress = false;
        this.errormessage = '';
        this.category = 'none';
        this.clickAddSentence = false;
        this.clickSentence = false;
        this.sentence = {
            SentenceTaskId: 0,
            Sentence: '',
            Category: ''
        }
      }
  },
  computed: 
  {
    sentences()
    {
      var vue = this;
      if(this.category == 'none' || this.category == '')
      {
          return this.$store.getters.sentences;
      }
      return this.$store.getters.sentences.filter(function(sentence){
          return sentence.Category.toLowerCase().indexOf(vue.category.toLowerCase()) > -1;
      });
    }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      api.getAllCategoriesSentences()
      .then(res => {
          this.categories = res;
          this.categories.push('none');
      })
      this.$store.dispatch('getSentences');
      this.inProgress = false;
  }
}
</script>