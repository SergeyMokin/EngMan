<!--edit-->
<template>
  <div class="tasks-align">
      <div class="loading" v-if = "inProgress">Loading&#8230;</div>
      <h2>Угадай что на картинке</h2><br/>
      <div v-if = "!show">
        <div class = "icon-close"><router-link to="/trainings"><img src = "../assets/arrow-up.png" title="Назад" style = "margin: 5px; width: 20px; height: 20px;"></router-link></div>
        <div v-on:click = "downloadTask()"><img title="Старт" style = "width: 20px; height: auto; margin-right: 35px; margin-top: 5px" class = "icon-close" type = "img" src = "../assets/start-icon.png"></div>
        <select v-model = "category">
            <option v-for = "category in categories" :key = "category">
                {{category}}
            </option>
        </select>
        <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
      </div>
      <div v-if = "show">
        <div class = "icon-close" v-on:click = "closeForm"><img src = "../assets/close-icon.png" title="Завершить" style = "margin: 5px; width: 20px; height: 20px;"></div>
        <div v-on:click = "downloadTask()"><img title="Следующий" style = "width: 20px; height: auto; margin-right: 35px; margin-top: 5px" class = "icon-close" type = "img" src = "../assets/arrow-right.png"></div>
        <div v-on:click = "verificationCorrectness()"><img title="Проверить" style = "width: 20px; height: auto; margin-right: 50px; margin-top: 5px" class = "icon-close" type = "img" src = "../assets/start-icon.png"></div>
        <img title="Угадай кто на картинке" :src = "task.Path" style = "width: 50%">
        <br/>
        <span>{{task.Word.Translate}}</span><br/>
        <input type = "text" v-model = "task.Word.Original" class = "sentence-input">
        <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span><br/>
      </div>
  </div>
</template>

<script>
import api from '../api/api'

export default {
  name: 'guessestheimages-task',
  data () {
    return {
        indexes: '',
        countOfTasks: 0,
        completemessage: '',
        goodAnswer: 0,
        attempt: 0,
        category: '',
        categories: [],
        inProgress: false,
        errormessage: '',
        show: false,
        task: {}
    }
  },
  methods:{
      downloadTask(){
          if(this.inProgress) return;
          this.inProgress = true;
          this.attempt = 0;
          this.inProgress = false;
          this.errormessage = '';
          this.completemessage = '';
          if(this.category.length == 0)
          {
              return;
          }
          api.getGuessesTheImage(this.category, this.indexes)
          .then(result => {
                if(result.Path)
                {
                    this.indexes += result.Id + ',';
                    this.countOfTasks++;
                    this.task = result;
                    this.task.Word.Original = '';
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
     },
     verificationCorrectness(){
          if(this.inProgress) return;
          this.inProgress = true;
          this.errormessage = '';
          if(this.task.Word.Original == '')
          {
              this.inProgress = false;
              this.errormessage = 'Напишите ответ';
              return;
          }
          api.verificationGuessesTheImage(this.task)
          .then(result =>
          {
              if(result){
                if(this.attempt == 0) this.goodAnswer++;
                this.inProgress = false;
                alert('Правильный ответ');
                this.downloadTask();
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
        this.completemessage = 'Вы успешно закончили! Правильных ответов: ' + this.goodAnswer + '/' + this.countOfTasks;
        alert(this.completemessage);
        this.countOfTasks = 0;
        this.id = 0;
        this.goodAnswer = 0;
        this.attempt = 0;
        this.inProgress = false;
        this.errormessage = '';
        this.show = false;
        this.task = {};
        this.category = '';
        this.categories = [];
      }
  },
  created: function(){
      if(this.inProgress) return;
      this.inProgress = true;
      api.getAllCategoriesGuessesTheImages()
            .then(res => {
                this.categories = res;
                this.inProgress = false;
            })
            .catch(e => {
                if(e.message)
                {
                    allert(e.message);
                }
                this.inProgress = false;
            })
  }
}
</script>