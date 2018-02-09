<template>
  <div class="sentence-task">
      <div class="loading" v-if = "inProgress">Loading&#8230;</div>
      <h1>Угадай что на картинке</h1><br/>
      <div v-if = "!show" class = "form-border">
        <button v-on:click = "downloadTask">Старт</button><br/>
        <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span>
        <span v-if = "completemessage" class = "span-complete-message">{{completemessage}}<br/></span>
      </div>
      <div v-if = "show" class = "form-border">
        <img :src = "task.Path" style = "width: 50%">
        <br/>
        <input type = "text" v-model = "returnTask.Word" class = "sentence-input">
        <button v-on:click = "verificationCorrectness">Проверить</button><br/>
        <button v-on:click = "closeForm">Завершить</button><br/>
        <button v-on:click = "downloadTask">Следующий</button><br/>
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
        id: 0,
        countOfTasks: 0,
        completemessage: '',
        goodAnswer: 0,
        attempt: 0,
        inProgress: false,
        errormessage: '',
        show: false,
        task: {},
        returnTask: {
            Id: 0,
            Word: '',
            Path: ''
        }
    }
  },
  methods:{
      downloadTask(){
          if(this.inProgress) return;
          this.inProgress = true;
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
                this.attempt = 0;
                this.inProgress = false;
                this.returnTask.Id = -1;
                this.returnTask.Word = '';
                this.returnTask.Path = '';
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
        this.countOfTasks = 0;
        this.id = 0;
        this.goodAnswer = 0;
        this.attempt = 0;
        this.inProgress = false;
        this.errormessage = '';
        this.show = false;
        this.task = {};
        this.returnTask = {
            Id: -1,
            Word: '',
            Path: ''
        }
      }
  }
}
</script>