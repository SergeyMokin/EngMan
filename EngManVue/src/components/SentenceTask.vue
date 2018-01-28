<template>
  <div class="sentence-task">
      <h1>Задания по предложениям</h1><br/>
      <div v-if = "!show" class = "form-border">
          <select class = "select-form" v-model = "category">
            <option v-for = "category in categories" :key = "category">
                {{category}}
            </option>
        </select>
        <button v-on:click = "downloadSentenceTask">Старт</button><br/>
        <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
        <span v-if = "completemessage" class = "span-complete-message">{{completemessage}}<br/></span>
      </div>
      <div v-if = "show" class = "form-border">
        <h2>{{sentence.Sentence}}</h2>
        <br/>
        <input type = "text" v-model = "returnSentence.Sentence" class = "sentence-input">
        <button v-on:click = "verificationCorrectness">Проверить</button><br/>
        <button v-on:click = "closeForm">Завершить</button><br/>
        <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span><br/>
      </div>
  </div>
</template>

<script>
import api from '../api/api'

export default {
  name: 'sentence-task',
  data () {
    return {
        countOfSentences: 0,
        completemessage: '',
        id: -1,
        goodAnswer: 0,
        attempt: 0,
        inProgress: false,
        errormessage: '',
        categories: [],
        category: 'none',
        show: false,
        sentence: {},
        returnSentence: {
            SentenceTaskId: -1,
            Sentence: '',
            Category: ''
        }
    }
  },
  methods:{
      downloadSentenceTask(){
          if(this.inProgress) return;
          this.inProgress = true;
          this.errormessage = '';
          this.completemessage = '';
          if(this.category != 'none')
          {
            api.getSentenceTask(this.category, this.id)
            .then(result => {
                    if(result.Sentence)
                    {
                        this.countOfSentences++;
                        this.id = this.id + 1;
                        this.sentence = result;
                        this.returnSentence.SentenceTaskId = this.sentence.SentenceTaskId;
                        this.returnSentence.Category = this.sentence.Category;
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
          api.verificationSentenceTask(this.returnSentence)
          .then(result =>
          {
              if(result.data){
                if(this.attempt == 0) this.goodAnswer++;
                this.attempt = 0;
                this.inProgress = false;
                this.returnSentence.SentenceTaskId = -1;
                this.returnSentence.Sentence = '';
                this.returnSentence.Category = '';
                alert('Правильный ответ');
                this.downloadSentenceTask();
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
        this.completemessage = 'Вы успешно закончили! Правильных ответов: ' + this.goodAnswer + '/' + this.countOfSentences;
        this.countOfSentences = 0;
        this.id = -1;
        this.goodAnswer = 0;
        this.attempt = 0;
        this.inProgress = false;
        this.errormessage = '';
        this.category = 'none';
        this.show = false;
        this.sentence = {};
        this.returnSentence = {
            SentenceTaskId: -1,
            Sentence: '',
            Category: ''
        }
      }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      api.getAllCategoriesSentences()
      .then(res => {
          this.inProgress = false;
          this.categories = res;
          this.categories.push('none');
      })
      this.inProgress = false;
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.sentence-task{
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
.sentence-input{
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
</style>