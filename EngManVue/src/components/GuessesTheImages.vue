<!--edit-->
<template>
<div>
  <div class="loading" v-if = "inProgress">Loading&#8230;</div>
  <div v-if = "show" class = "b-popup">
      <div class = "b-popup-content">
        <span class = "routes-admin pointer" v-if = "!showWordTranslate" style = "padding: 0px; font-size: 14px; width: 50%; margin-left: 25%;" v-on:click = "showWordTranslate = true" title = "Show translate of word">Show</span>
        <span class = "routes-admin pointer" v-else style = "padding: 0px; font-size: 14px; width: 50%; margin-left: 25%;" v-on:click = "showWordTranslate = false" title = "Hide translate of word">{{task.Word.Translate}}</span>
        <span style = "float: right; font-size:10px; cursor: pointer" v-on:click = "closeForm(false); showWordTranslate = false;"><img title="End" style = "width: 20px; height: auto;" type = "img" src = "../assets/close-icon.png"></span>
        <span style = "float: right; font-size:10px; cursor: pointer" v-on:click = "downloadTask(); showWordTranslate = false"><img title="Next" style = "width: 20px; height: auto" type = "img" src = "../assets/arrow-right.png"></span>
        <span style = "float: right; font-size:10px; cursor: pointer" v-on:click = "verificationCorrectness(); showWordTranslate = false"><img title="Verification" style = "width: 20px; height: auto" type = "img" src = "../assets/start-icon.png"></span>
        <br/><br/>
        <img title="Guess what's on the picture" :src = "task.Path" style = "width: 80%; margin-left: 10%; margin-bottom: 5px;">
        <!--div v-if = "showWordTranslate" style = "width: 50%; text-align: center; margin-left: 25%">{{task.Word.Translate}}</div>-->
        <input style = "border: none; border-radius:5px;box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.5); margin-bottom: 5px" type = "text" v-model = "task.Word.Original" class = "sentence-input">
        <div style = "width: 50%; text-align: center; margin-left: 25%" v-if = "errormessage" class = "span-error-message">{{errormessage}}</div>
      </div>
  </div>
  <div class="tasks-align">
      <span style = "font-size: 30px">Guess what's on the picture</span><br/>
      <div>
        <div class = "icon-close"><router-link to="/trainings"><img src = "../assets/arrow-up.png" title="Назад" style = "margin: 5px; width: 20px; height: 20px;"></router-link></div>
        <div v-on:click = "downloadTask()"><img title="Старт" style = "width: 20px; height: auto; margin-right: 35px; margin-top: 5px" class = "icon-close" type = "img" src = "../assets/start-icon.png"></div>
        <select v-model = "category" class = "select-form" style = "width: 250px !important;">
            <option v-for = "category in categories" :key = "category">
                {{category}}
            </option>
        </select>
        <br/>
        <span v-if = "errormessage && !show" class = "span-error-message">{{errormessage}}<br/></span>
      </div>
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
        showWordTranslate: false,
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
          this.errormessage = '';
          this.completemessage = '';
          if(this.category.length == 0)
          {
              this.errormessage = "Select a category";
              this.inProgress = false;
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
                    this.closeForm(true);
                }
            })
           .catch(e => {
                this.inProgress = false;
                this.errormessage = 'Server is not available';
            });
     },
     verificationCorrectness(){
          if(this.inProgress) return;
          this.inProgress = true;
          this.errormessage = '';
          if(this.task.Word.Original == '')
          {
              this.inProgress = false;
              this.errormessage = 'Write the answer';
              return;
          }
          api.verificationGuessesTheImage(this.task)
          .then(result =>
          {
              if(result){
                if(this.attempt == 0) this.goodAnswer++;
                this.inProgress = false;
                alert('Correct answer');
                this.downloadTask();
              }
              else{
                this.attempt++;
                this.errormessage = 'Incorrect answer';
                this.inProgress = false;
              }
          })
          .catch(e => {
              this.inProgress = false;
              this.errormessage = 'Server is not available';
          })
      },
      closeForm(endoftasks){
        if(endoftasks)
        {
            this.completemessage = 'You have successfully completed! Correct answers: ' + this.goodAnswer + '/' + this.countOfTasks;
        }
        else
        {
            this.completemessage = 'You have successfully completed! Correct answers: ' + this.goodAnswer + '/' + (this.countOfTasks-1);
        }
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
        this.showWordTranslate = false;
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