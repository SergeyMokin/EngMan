<template>
<div>
  <div class="loading" v-if = "inProgress">Loading&#8230;</div>

  <div v-if = "show" class = "b-popup">
      <div class = "b-popup-content">
        
        <span 
            style = "float: right; font-size:10px; cursor: pointer" 
            v-on:click = "closeForm(false)">
                <img 
                    title="End" 
                    style = "width: 20px; height: auto;" 
                    type = "img" 
                    src = "../assets/close-icon.png">
        </span>
        
        <span 
            style = "float: right; font-size:10px; cursor: pointer" 
            v-on:click = "downloadWordMap()">
                <img 
                    title="Next" 
                    style = "width: 20px; height: auto" 
                    type = "img" 
                    src = "../assets/arrow-right.png">
        </span>
        
        <span 
            style = "float: right; font-size:10px; cursor: pointer" 
            v-on:click = "verificationCorrectness()">
                <img 
                    title="Verification" 
                    style = "width: 20px; height: auto" 
                    type = "img" 
                    src = "../assets/start-icon.png">
        </span>
        
        <div style = "font-size: larger; margin-left: 10px">
            {{word.Word}}
        </div>
        
        <div 
            title="Choose" 
            v-for = "el in word.Answers" 
            :key = "el.WordId" 
            v-on:click = "returnWord.Translate = el">
            <div 
                v-bind:class = "{'selected-wordmap': el == returnWord.Translate}" 
                class = "list--element pointer">
                {{el}}
            </div>
        </div>
        
        <span v-if = "errorMessage" class = "span-error-message">{{errorMessage}}</span>
      </div>
  </div>

  <div class="tasks-align">
      
      <span style = "font-size: 30px">Word-translate</span><br/>
      
      <div>
        <div class = "icon-close">
            <router-link to="/trainings">
                <img 
                    src = "../assets/arrow-up.png" 
                    title="Back" 
                    style = "margin: 5px; width: 20px; height: 20px;">
            </router-link>
        </div>
        
        <div v-on:click = "downloadWordMap()">
            <img 
                title="Start" 
                style = "width: 20px; height: auto; margin-right: 35px; margin-top: 5px" 
                class = "icon-close" 
                type = "img" 
                src = "../assets/start-icon.png">
        </div>
        
        <select 
            id = "task_word_category" 
            class = "select-form" 
            style = "width: 250px !important;" 
            v-model = "category">
            
            <option v-for = "category in categories" :key = "category">
                {{category}}
            </option>
        </select>
        <br/>
        
        <span 
            v-if = "errorMessage && !show" 
            class = "span-error-message">
            {{errorMessage}}<br/>
        </span>
        <br/>
        
        <div v-if = "!show" v-for = 'el in words' :key = 'el.WordId'>
            <div 
                title = "Add to dictionary" 
                class = "list--element pointer" 
                v-on:click = "addWordToDictionary(el)">
                <a>{{el.Original}} {{el.Transcription}} - {{el.Translate}}</a>
            </div>
        </div>
      </div>
  </div>

</div>
</template>

<script>
import api from '../api/api'

export default {
  name: 'Word-translate-task',
  data () {
    return {
        choose: false,
        countOfWords: 0,
        completeMessage: '',
        indexes: '',
        goodAnswer: 0,
        attempt: 0,
        inProgress: false,
        errorMessage: '',
        categories: [],
        category: '',
        show: false,
        word: {},
        returnWord: {
            WordId: -1,
            Original: '',
            Translate: '',
            Category: '',
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
              if(res === true)
              {
                  alert('Successfully added');
              }
              else{
                  if(res.response)
                  {
                      if(res.response.data.Message)
                      {
                          console.log(res.response.data.Message);
                          this.inProgress = false;
                          return;
                      }
                  }
                  alert('Already added');
              } 
              this.inProgress = false;
          })
          .catch(e => {
              alert('Server is not available');
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
          this.errorMessage = '';
          this.completeMessage = '';
          if(this.categories.indexOf(this.category) != -1)
          {
            api.getWordMap(this.category, this.indexes, true)
            .then(result => {
                    if(result.Word)
                    {
                        this.countOfWords++;
                        this.indexes += result.WordId + ',';
                        this.word = result;
                        this.returnWord.WordId = this.word.WordId;
                        this.returnWord.Original = this.word.Word;
                        this.returnWord.Category = this.word.Category;
                        this.show = true;
                        this.inProgress = false;
                    }
                    else{
                        this.closeForm(true);
                    }
                })
            .catch(e => {
                this.inProgress = false;
                this.errorMessage = 'Server is not available';
            });
          }
          else{
              this.inProgress = false;
              this.errorMessage = 'Select a category';
          }
      },
      verificationCorrectness(){
          if(this.inProgress) return;
          this.inProgress = true;
          this.errorMessage = '';
          api.verificationWordMap(this.returnWord, true)
          .then(result =>
          {
              if(result.data){
                if(this.attempt == 0) this.goodAnswer++;
                this.inProgress = false;
                alert('Correct answer');
                this.downloadWordMap();
              }
              else{
                this.attempt++;
                this.errorMessage = 'Incorrect answer';
                this.inProgress = false;
              }
          })
          .catch(e => {
              this.inProgress = false;
              this.errorMessage = 'Server is not available';
          })
      },
      closeForm(endoftasks){
        if(endoftasks)
        {
            this.completeMessage = 'You have successfully completed! Correct answers: ' + this.goodAnswer + '/' + this.countOfWords;
        }
        else
        {
            this.completeMessage = 'You have successfully completed! Correct answers: ' + this.goodAnswer + '/' + (this.countOfWords-1);
        }
        alert(this.completeMessage);
        this.choose = false
        this.countOfWords = 0
        this.completeMessage = ''
        this.indexes = ''
        this.goodAnswer = 0
        this.attempt = 0
        this.inProgress = false
        this.errorMessage = ''
        this.category = ''
        this.show = false
        this.word = {}
        this.returnWord = {
            WordId: -1,
            Original: '',
            Translate: '',
            Category: '',
        }
      }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      api.getAllCategoriesWords()
      .then(res => {
          if(res.response)
          {
              if(res.response.data.Message)
              {
                  this.inProgress = false;
                  this.errorMessage = res.response.data.Message;
                  return;
              }
          }
          if(res.length > 0)
          {
            this.inProgress = false;
            this.categories = res;
          }
          else
          {
              this.inProgress = false;
              console.log(res);
          }
      })
      .catch(e => 
      {
          this.inProgress = false;
          console.log(e);
      })
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