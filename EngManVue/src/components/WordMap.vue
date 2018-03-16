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
            v-on:click = "nextWord()">
                <img 
                    title="Next" 
                    style = "width: 20px; height: auto" 
                    type = "img" 
                    src = "../assets/arrow-right.png">
        </span>
        <br/><br/>
        
        <div style = "font-size: larger; width: 70%; text-align: center; margin-left: 15%;">
            {{words[countOfWords].Original}}
        </div>
        <br/>
        
        <div style = "font-size: larger; width: 70%; text-align: center; margin-left: 15%;">
            {{words[countOfWords].Transcription}}
        </div>
        <br/>
        
        <div 
            v-if = "clickIKnow || clickIDoNotKnow" 
            style = "font-size: 17px; width: 70%; text-align: center; margin-left: 15%;">
            {{words[countOfWords].Translate}}
        </div>
        <br/>
        
        <div 
            v-if = "!clickIKnow && !clickIDoNotKnow" 
            style = "font-size: larger; width: 70%; text-align: center; margin-left: 15%;">
            <span 
                class = "routes-admin pointer" 
                style = "font-size: 17px" 
                v-on:click = "clickIDoNotKnow = true; addWordToDictionary(words[countOfWords]); badAnswer++">
                I don't know
            </span>
            
            <span 
                class = "routes-admin pointer" 
                style = "font-size: 17px" 
                v-on:click = "clickIKnow = true;">
                I know
            </span>
            <br/>
        </div>
        
        <div 
            v-else 
            style = "font-size: larger; width: 70%; text-align: center; margin-left: 15%;">
                <span 
                    class = "routes-admin pointer" 
                    style = "font-size: 17px" 
                    v-on:click = "nextWord()">
                    Next
                </span>
        </div>
        <br/>
        
        <span v-if = "errorMessage" class = "span-error-message">{{errorMessage}}</span>
      </div>
  </div>

  <div class="tasks-align">
      <span style = "font-size: 30px">Word Cards</span><br/>
      
      <div>
        <div class = "icon-close">
            <router-link to="/trainings">
                <img 
                    src = "../assets/arrow-up.png" 
                    title="Back" 
                    style = "margin: 5px; width: 20px; height: 20px;">
            </router-link>
        </div>
        
        <div v-on:click = "show = true">
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
        </select><br/>
        
        <span 
            v-if = "errorMessage && !show" 
            class = "span-error-message">
            {{errorMessage}}<br/>
        </span>
        <br/>
        
        <div 
            v-if = "!show" 
            v-for = 'el in words' 
            :key = 'el.WordId'>
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
  name: 'Word-cards-task',
  data () {
    return {
        choose: false,
        countOfWords: 0,
        completeMessage: '',
        indexes: '',
        badAnswer: 0,
        attempt: 0,
        inProgress: false,
        errorMessage: '',
        categories: [],
        category: '',
        show: false,
        clickIKnow: false,
        clickIDoNotKnow: false
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
      closeForm(endoftasks){
        if(endoftasks)
        {
            this.completeMessage = 'Added: ' + this.badAnswer + ' words to your dictionary';
        }
        else
        {
            this.completeMessage = 'Added: ' + this.badAnswer + ' words to your dictionary';
        }
        alert(this.completeMessage);
        this.choose = false
        this.countOfWords = 0
        this.completeMessage = ''
        this.indexes = ''
        this.badAnswer = 0
        this.attempt = 0
        this.inProgress = false
        this.errorMessage = ''
        this.category = ''
        this.show = false
      },
      nextWord()
      {
          this.countOfWords++;
          this.clickIKnow = false;
          this.clickIDoNotKnow = false;
          if(this.countOfWords >= this.words.length)
          {
              this.closeForm(true);
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