<template>
  <div class="wordmap-task">
      <div class="loading" v-if = "inProgress">Loading&#8230;</div>
      <h2>Карты слов</h2><br/>
      <div v-if = "!show" class = "form-border">
        <div class = "button-close"><router-link to="/trainings"><img src = "../assets/arrow-up.png" title="Назад" style = "margin: 5px; width: 20px; height: 20px;"></router-link></div>
        <div v-on:click = "downloadWordMap()"><img title="Старт" style = "width: 20px; height: auto; margin-right: 35px; margin-top: 5px" class = "button-close" type = "img" src = "../assets/start-icon.png"></div>
        <input placeholder="Выберите..." type="text" class = "select-form" list="task_word_category" v-model = "category" v-on:click = "category = ''"/><br/>
        <datalist id = "task_word_category">
            <option v-for = "category in categories" :key = "category">
                {{category}}
            </option>
        </datalist>
        <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
        <br/>
        <div v-for = 'el in words' :key = 'el.WordId'>
            <div title = "Добавить себе в словарь" class = "wordmap-list--element" v-on:click = "addWordToDictionary(el)">
                <a>{{el.Original}} {{el.Transcription}} - {{el.Translate}}</a>
            </div>
        </div>
      </div>
      <div v-if = "show" class = "form-border">
        <div class = "button-close" v-on:click = "closeForm()"><img src = "../assets/close-icon.png" title="Завершить" style = "margin: 5px; width: 20px; height: 20px;"></div>
        <div v-on:click = "downloadWordMap()"><img title="Следующий" style = "width: 20px; height: auto; margin-right: 35px; margin-top: 5px" class = "button-close" type = "img" src = "../assets/arrow-right.png"></div>
        <div v-on:click = "verificationCorrectness()"><img title="Проверить" style = "width: 20px; height: auto; margin-right: 50px; margin-top: 5px" class = "button-close" type = "img" src = "../assets/start-icon.png"></div>
        <span style = "font-size: larger;">{{word.Original}}</span>
        <br/>
        <div v-for = "el in word.Translate" :key = "el" v-on:click = "returnWord.Translate = el">
            <div v-bind:class = "{'selected-wordmap': el == returnWord.Translate}" class = "wordmap-list--element">
                {{el}}
            </div>
        </div>
        <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span><br/>
      </div>
  </div>
</template>

<script>
import api from '../api/api'

export default {
  name: 'wordmap-task',
  data () {
    return {
        choose: false,
        countOfWords: 0,
        completemessage: '',
        id: -1,
        goodAnswer: 0,
        attempt: 0,
        inProgress: false,
        errormessage: '',
        categories: [],
        category: '',
        show: false,
        word: {},
        returnWord: {
            WordId: -1,
            Original: '',
            Translate: '',
            Category: ''
        }
    }
  },
  methods:{
      addWordToDictionary(word){
          if(this.inProgress) return;
          this.inProgress = true;
          api.addUserWord({
              UserId: this.$store.state.user.Id,
              WordId: word.WordId
          })
          .then(res => {
              if(res.Id > 0)
              {
                  alert('Успешно добавлено');
              }
              else{
                  alert('Уже добавлен');
              } 
              this.inProgress = false;
          })
          .catch(e => {
              alert('Сервер недоступен');
          })
      },
      downloadWordMap(){
          if(this.inProgress) return;
          this.inProgress = true;
          this.attempt = 0;
          this.inProgress = false;
          this.returnWord.WordId = -1;
          this.returnWord.Original = '';
          this.returnWord.Translate = '';
          this.returnWord.Category = '';
          this.errormessage = '';
          this.completemessage = '';
          if(this.categories.indexOf(this.category) != -1)
          {
            api.getWordMap(this.category, this.id)
            .then(result => {
                    if(result.Original)
                    {
                        this.countOfWords++;
                        this.id = this.id + 1;
                        this.word = result;
                        this.returnWord.WordId = this.word.WordId;
                        this.returnWord.Original = this.word.Original;
                        this.returnWord.Category = this.word.Category;
                        this.show = true;
                        this.inProgress = false;
                    }
                    else{
                        this.closeForm();
                    }
                })
            .catch(e => {
                this.inProgress = false;
                this.errormessage = 'Сервер недоступен';
            });
          }
          else{
              this.inProgress = false;
              this.errormessage = 'Выберите категорию';
          }
      },
      verificationCorrectness(){
          if(this.inProgress) return;
          this.inProgress = true;
          this.errormessage = '';
          api.verificationWordMap(this.returnWord)
          .then(result =>
          {
              if(result.data){
                if(this.attempt == 0) this.goodAnswer++;
                this.inProgress = false;
                alert('Правильный ответ');
                this.downloadWordMap();
              }
              else{
                this.attempt++;
                this.errormessage = 'Неверный вариант';
                this.inProgress = false;
              }
          })
          .catch(e => {
              this.inProgress = false;
              this.errormessage = 'Сервер недоступен';
          })
      },
      closeForm(){
        this.completemessage = 'Вы успешно закончили! Правильных ответов: ' + this.goodAnswer + '/' + this.countOfWords;
        alert(this.completemessage);
        this.countOfWords = 0;
        this.id = -1;
        this.goodAnswer = 0;
        this.attempt = 0;
        this.inProgress = false;
        this.errormessage = '';
        this.category = '';
        this.show = false;
        this.word = {};
        this.returnWord = {
            WordId: -1,
            Original: '',
            Translate: '',
            Category: ''
        }
      }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      api.getAllCategoriesWords()
      .then(res => {
          this.inProgress = false;
          this.categories = res;
      })
      this.inProgress = false;
      this.$store.dispatch('getWords');
  },
  computed:{
    words()
    {
      var vue = this;
      if(this.category == 'none' || this.category == '')
      {
          return;
      }
      return this.$store.getters.words.filter(function(sentence){
          return sentence.Category.toLowerCase().indexOf(vue.category.toLowerCase()) > -1;
      });
    }
  }
}
</script>