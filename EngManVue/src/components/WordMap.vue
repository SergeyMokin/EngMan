<template>
  <div class="wordmap-task">
      <h1>Карты слов</h1><br/>
      <div v-if = "!show" class = "form-border">
          <select class = "select-form" v-model = "category">
            <option v-for = "category in categories" :key = "category">
                {{category}}
            </option>
        </select>
        <button v-on:click = "downloadWordMap">Старт</button><br/>
        <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
        <span v-if = "completemessage" class = "span-complete-message">{{completemessage}}<br/></span>
        <div v-for = 'el in words' :key = 'el.WordId'>
            <div class = "words-list--element" style = "cursor: default">
                <a>{{el.Original}} - {{el.Translate}}</a>
            </div>
        </div>
      </div>
      <div v-if = "show" class = "form-border">
        <h2>{{word.Original}}</h2>
        <br/>
        <div v-for = "el in word.Translate" :key = "el" class = "words-list--element" v-on:click = "returnWord.Translate = el">
            <div>
                {{el}}
            </div>
        </div>
        <h2><a>Ваш выбор: {{returnWord.Translate}}</a></h2><br/>
        <button v-on:click = "verificationCorrectness">Проверить</button><br/>
        <button v-on:click = "closeForm">Завершить</button><br/>
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
        countOfWords: 0,
        completemessage: '',
        id: -1,
        goodAnswer: 0,
        attempt: 0,
        inProgress: false,
        errormessage: '',
        categories: [],
        category: 'none',
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
      downloadWordMap(){
          if(this.inProgress) return;
          this.inProgress = true;
          this.errormessage = '';
          this.completemessage = '';
          if(this.category != 'none')
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
                this.attempt = 0;
                this.inProgress = false;
                this.returnWord.WordId = -1;
                this.returnWord.Original = '';
                this.returnWord.Translate = '';
                this.returnWord.Category = '';
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
        this.countOfWords = 0;
        this.id = -1;
        this.goodAnswer = 0;
        this.attempt = 0;
        this.inProgress = false;
        this.errormessage = '';
        this.category = 'none';
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
          this.categories.push('none');
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

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.wordmap-task{
    margin: 5% 30% 5% 30%;
    text-align: center;
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
.words-list--element{
    margin: 10px;
    padding: 3px;
    text-align: left;
    cursor: pointer;
    background: rgb(248, 248, 248);
    height: 35px;
    border: none;
    outline:none;
    border-radius: 10px;
}
</style>