<!--edit-->
<template>
  <div class="sentence-task">
      <div class="loading" v-if = "inProgress">Loading&#8230;</div>
      <h2>Угадай что на картинке</h2><br/>
      <div v-if = "!show" class = "form-border">
        <div class = "button-close"><router-link to="/trainings"><img src = "../assets/arrow-up.png" title="Назад" style = "margin: 5px; width: 20px; height: 20px;"></router-link></div>
        <div v-on:click = "downloadTask()"><img title="Старт" style = "width: 20px; height: auto; margin-right: 35px; margin-top: 5px" class = "button-close" type = "img" src = "../assets/start-icon.png"></div>
        <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
      </div>
      <div v-if = "show" class = "form-border">
        <div class = "button-close" v-on:click = "closeForm"><img src = "../assets/close-icon.png" title="Завершить" style = "margin: 5px; width: 20px; height: 20px;"></div>
        <div v-on:click = "downloadTask()"><img title="Следующий" style = "width: 20px; height: auto; margin-right: 35px; margin-top: 5px" class = "button-close" type = "img" src = "../assets/arrow-right.png"></div>
        <div v-on:click = "verificationCorrectness()"><img title="Проверить" style = "width: 20px; height: auto; margin-right: 50px; margin-top: 5px" class = "button-close" type = "img" src = "../assets/start-icon.png"></div>
        <img title="Угадай кто на картинке" :src = "task.Path" style = "width: 50%">
        <br/>
        <span>{{task.Word.Translate}}</span><br/>
        <input type = "text" v-model = "returnTask.Word" class = "sentence-input">
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
        id: 1,
        countOfTasks: 0,
        completemessage: '',
        goodAnswer: 0,
        attempt: 0,
        inProgress: false,
        errormessage: '',
        show: false,
        task: {},
        returnTask: {
            Id: 1,
            Word: '',
            Path: ''
        }
    }
  },
  methods:{
      downloadTask(){
          if(this.inProgress) return;
          this.inProgress = true;
          this.attempt = 0;
          this.inProgress = false;
          this.returnTask.Id = -1;
          this.returnTask.Word = '';
          this.returnTask.Path = '';
          this.errormessage = '';
          this.completemessage = '';
          api.getGuessesTheImage(this.id)
          .then(result => {
                if(result.Path)
                {
                    this.id++;
                    this.countOfTasks++;
                    this.task = result;
                    this.returnTask.Id = result.Id;
                    this.returnTask.Path = result.Path;
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
          if(this.returnTask.Word == '')
          {
              this.inProgress = false;
              this.errormessage = 'Напишите ответ';
          }
          api.verificationGuessesTheImage(this.returnTask)
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
        this.id = 1;
        this.goodAnswer = 0;
        this.attempt = 0;
        this.inProgress = false;
        this.errormessage = '';
        this.show = false;
        this.task = {};
        this.returnTask = {
            Id: 1,
            Word: '',
            Path: ''
        }
      }
  }
}
</script>