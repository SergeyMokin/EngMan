<template>
  <div v-if = "$store.state.user.Role == 'admin'">
      <div class="loading" v-if = "inProgress">Loading&#8230;</div>
      <div v-if = "!clickSentence" class = "sentences-view">
        <router-link to="/admin/rules" class = "routes-admin">Правила </router-link>
        <router-link to="/admin/sentences" class = "routes-admin">Предложения </router-link>
        <router-link to="/admin/words" class = "routes-admin">Словарь </router-link>
        <router-link to="/admin/users" class = "routes-admin">Пользователи </router-link>
        <router-link to="/admin/guessestheimages" class = "routes-admin">Задания по картинкам</router-link><br/><br/>
        <span style = "cursor: pointer;" v-on:click = "AddSentence()"><img title="Добавить" style = "width: 30px; height: auto" type = "img" src = "../assets/add-icon.png"></span><br/><br/>
        <input placeholder="Выберите..." type="text" class = "select-form" list="sentence_category" v-model = "category" v-on:click = "category = ''"/>
        <datalist id = "sentence_category">
            <option v-for = "category in categories" :key = "category">
                {{category}}
            </option>
        </datalist>
        <div v-for = 'el in sentences' :key = 'el.SentenceTaskId' class = "form-border">
            <div class = "sentences-list--element">
                <span class = "span-sentence--element">
                    {{el.Sentence}}
                    <span style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "deleteSentence(el.SentenceTaskId)"><img title="Удалить" style = "width: 20px; height: auto" type = "img" src = "../assets/close-icon.png"></span>
                    <span style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "editSentence(el.SentenceTaskId)"><img title="Изменить" style = "margin-right: 5px; width: 18px; height: auto" type = "img" src = "../assets/edit-icon.png"></span>
                </span>
            </div>
        </div>
        <br/><br/>
      </div>
      <div v-if = "clickSentence" style = "text-align: center">
          <br/><br/>
          <span v-on:click = "closeEditForm()"><img title="Закрыть" style = "width: 20px; height: auto;" class = "button-close" type = "img" src = "../assets/close-icon.png"></span>
          <span v-on:click = "saveSentence(sentence)"><img title="Сохранить" style = "width: 18px; height: auto; margin-right: 30px; margin-top: 2px" class = "button-close" type = "img" src = "../assets/save-icon.png"></span>
          <span>Категория</span>
          <textarea type = "text" v-model = "sentence.Category" class = "sentence-edit"/><br/>
          <span>Предложение на английском</span>
          <textarea type = "text" v-model = "sentence.Sentence" class = "sentence-edit"/><br/>
          <span>Перевод на русском</span>
          <textarea type = "text" v-model = "sentence.Translate" class = "sentence-edit"/><br/>
          <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span><br/><br/>
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
        category: '',
        clickAddSentence: false,
        clickSentence: false
        , sentence: {
            SentenceTaskId: 0,
            Sentence: '',
            Category: '',
            Translate: ''
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
          if(this.sentence.Sentence.length == 0 || this.sentence.Category.length == 0 || this.sentence.Translate.length == 0)
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
        this.category = '';
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
      })
      this.$store.dispatch('getSentences');
      this.inProgress = false;
  }
}
</script>