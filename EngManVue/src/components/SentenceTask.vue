<template>
  <div class="tasks-align">
      <div class="loading" v-if = "inProgress">Loading&#8230;</div>
      <span style = "font-size: 25px">Practice rules</span><br/><br/>
      <div v-if = "!show">
        <div class = "icon-close"><router-link to="/grammar"><img src = "../assets/arrow-up.png" title="Back" style = "margin: 5px; width: 20px; height: 20px;"></router-link></div>
        <div v-on:click = "downloadSentenceTask()"><img title="Start" style = "width: 20px; height: auto; margin-right: 35px; margin-top: 5px" class = "icon-close" type = "img" src = "../assets/start-icon.png"></div>
        <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
        <select id = "task_sent_category" placeholder="Выберите..." class = "select-form-mes" v-model = "category">
            <option v-for = "category in categories" :key = "category">
                {{category}}
            </option>
        </select>
      </div>
      <div v-if = "show">
        <div class = "icon-close" v-on:click = "closeForm"><img src = "../assets/close-icon.png" title="End" style = "margin: 5px; width: 20px; height: 20px;"></div>
        <div v-on:click = "downloadSentenceTask()"><img title="Next" style = "width: 20px; height: auto; margin-right: 35px; margin-top: 5px" class = "icon-close" type = "img" src = "../assets/arrow-right.png"></div>
        <div v-on:click = "verificationCorrectness()"><img title="Verification" style = "width: 20px; height: auto; margin-right: 50px; margin-top: 5px" class = "icon-close" type = "img" src = "../assets/start-icon.png"></div>
        <div style = "display:inline; background: white; cursor:pointer; margin: 5px" v-for = "el in splitsentence" :key = "el" v-on:click="returnSentence.Sentence += el += ' '">
            {{el}}
        </div>
        {{returnSentence.Sentence}}
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
        indexes: '',
        goodAnswer: 0,
        attempt: 0,
        inProgress: false,
        errormessage: '',
        categories: [],
        category: '',
        show: false,
        sentence: {},
        splitsentence: [],
        returnSentence: {
            SentenceTaskId: -1,
            Sentence: '',
            Category: '',
            Translate: ''
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
          this.returnSentence.Translate = '';
          this.errormessage = '';
          this.completemessage = '';
          if(this.categories.indexOf(this.category) != -1)
          {
            api.getSentenceTask(this.category, this.indexes)
            .then(result => {
                    if(result.Sentence)
                    {
                        this.countOfSentences++;
                        this.indexes += result.SentenceTaskId + ',';
                        this.sentence = result;
                        this.splitsentence = this.sentence.Sentence.split(" ");
                        this.returnSentence.SentenceTaskId = this.sentence.SentenceTaskId;
                        this.returnSentence.Category = this.sentence.Category;
                        this.returnSentence.Translate = this.sentence.Translate;
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
        this.indexes = '';
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