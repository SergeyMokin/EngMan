<template>
  <div class="sentence-task">
      <div class="loading" v-if = "inProgress">Loading&#8230;</div>
      <h2>Составь предложение</h2><br/>
      <div v-if = "!show" class = "form-border">
        <div class = "button-close"><router-link to="/grammar"><img src = "../assets/arrow-up.png" title="Назад" style = "margin: 5px; width: 20px; height: 20px;"></router-link></div>
        <div v-on:click = "downloadSentenceTask()"><img title="Старт" style = "width: 20px; height: auto; margin-right: 35px; margin-top: 5px" class = "button-close" type = "img" src = "../assets/start-icon.png"></div>
        <input placeholder="Выберите..." type="text" class = "select-form" list="task_sent_category" v-model = "category"/><br/>
        <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
        <datalist id = "task_sent_category">
            <option v-for = "category in categories" :key = "category">
                {{category}}
            </option>
        </datalist>
      </div>
      <div v-if = "show" class = "form-border">
        <div class = "button-close" v-on:click = "closeForm"><img src = "../assets/close-icon.png" title="Завершить" style = "margin: 5px; width: 20px; height: 20px;"></div>
        <div v-on:click = "downloadSentenceTask()"><img title="Следующий" style = "width: 20px; height: auto; margin-right: 35px; margin-top: 5px" class = "button-close" type = "img" src = "../assets/arrow-right.png"></div>
        <div v-on:click = "verificationCorrectness()"><img title="Проверить" style = "width: 20px; height: auto; margin-right: 50px; margin-top: 5px" class = "button-close" type = "img" src = "../assets/start-icon.png"></div>
        <span style = "font-size: larger;">{{sentence.Sentence}}</span>
        <br/>
        <input type = "text" v-model = "returnSentence.Sentence" class = "sentence-input"/>
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
        category: '',
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
          this.attempt = 0;
          this.inProgress = false;
          this.returnSentence.SentenceTaskId = -1;
          this.returnSentence.Sentence = '';
          this.returnSentence.Category = '';
          this.errormessage = '';
          this.completemessage = '';
          if(this.categories.indexOf(this.category) != -1)
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
                this.inProgress = false;
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
        alert(this.completemessage);
        this.countOfSentences = 0;
        this.id = -1;
        this.goodAnswer = 0;
        this.attempt = 0;
        this.inProgress = false;
        this.errormessage = '';
        this.category = '';
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
      })
      this.inProgress = false;
  }
}
</script>