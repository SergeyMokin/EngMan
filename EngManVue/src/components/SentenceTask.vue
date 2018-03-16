<template>
<div>
    <div class="loading" v-if = "inProgress">Loading&#8230;</div>
    
    <div v-if = "show" class = "b-popup">
        <div class = "b-popup-content">
            <div class = "height: 300px">
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
                    v-on:click = "downloadSentenceTask()">
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
                
                <span 
                    style = "float: right; font-size:10px; cursor: pointer" 
                    v-on:click = "rebuildsentence()">
                        <img 
                            title="Refresh" 
                            style = "width: 18px; height: auto; margin-right:5px; margin-top:1px" 
                            type = "img" 
                            src = "../assets/refresh.png">
                </span>
                <br/><br/>
                
                <div style = "border-bottom: solid 1px; width: 300px; height: 44px">
                    {{returnSentence.Translate}}
                </div>
                <br/><br/>
                
                <div style = "border-bottom: solid 1px; width: 300px; height: 44px">
                    {{returnSentence.Sentence}}
                </div>
                <br/><br/>
                
                <div 
                    class = "routes-admin pointer" 
                    v-for = "el in splitSentence" 
                    :ref = "el" 
                    :key = "el" 
                    v-on:click="clickWord(el)">
                    {{el}}
                </div><br/><br/>
                
                <span v-if = "errorMessage" class = "span-error-message">
                    {{errorMessage}}<br/>
                </span>
            </div>
        </div>
    </div>

    <div class="tasks-align">
        <span style = "font-size: 30px">Practice rules</span><br/>
        <div>
            <div class = "icon-close">
                <router-link to="/grammar">
                    <img 
                        src = "../assets/arrow-up.png" 
                        title="Back" 
                        style = "margin: 5px; width: 20px; height: 20px;">
                </router-link>
            </div>
            
            <div v-on:click = "downloadSentenceTask()">
                <img 
                    title="Start" 
                    style = "width: 20px; height: auto; margin-right: 35px; margin-top: 5px" 
                    class = "icon-close" 
                    type = "img" 
                    src = "../assets/start-icon.png">
            </div>
            
            <select id = "task_sent_category" class = "select-form" v-model = "category">
                <option v-for = "category in categories" :key = "category">
                    {{category}}
                </option>
            
            </select><br/>
            <span v-if = "errorMessage && !show" class = "span-error-message">
                {{errorMessage}}
            </span>
        </div>
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
        completeMessage: '',
        indexes: '',
        goodAnswer: 0,
        attempt: 0,
        inProgress: false,
        errorMessage: '',
        categories: [],
        category: '',
        show: false,
        sentence: {},
        splitSentence: [],
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
          this.errorMessage = '';
          this.completeMessage = '';
          if(this.categories.indexOf(this.category) != -1)
          {
            api.getSentenceTask(this.category, this.indexes)
            .then(result => {
                    if(result.Sentence)
                    {
                        this.rebuildsentence();
                        this.countOfSentences++;
                        this.indexes += result.SentenceTaskId + ',';
                        this.sentence = result;
                        this.splitSentence = this.sentence.Sentence.split(" ");
                        this.returnSentence.SentenceTaskId = this.sentence.SentenceTaskId;
                        this.returnSentence.Category = this.sentence.Category;
                        this.returnSentence.Translate = this.sentence.Translate;
                        this.show = true;
                        this.inProgress = false;
                    }
                    else{
                        console.log(result);
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

          //last element ''
          if(this.returnSentence.Sentence.split(" ").length < (this.splitSentence.length + 1))
          {
              this.errorMessage = "Make a sentence";
              this.inProgress = false;
              return;
          }
          api.verificationSentenceTask(this.returnSentence)
          .then(result =>
          {
              if(result.data){
                if(this.attempt == 0) this.goodAnswer++;
                this.inProgress = false;
                alert('Correct answer');
                this.downloadSentenceTask();
              }
              else{
                this.attempt++;
                this.errorMessage = 'Incorrect answer';
                this.inProgress = false;
                console.log(result);
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
            this.completeMessage = 'You have successfully completed! Correct answers: ' + this.goodAnswer + '/' + this.countOfSentences;
        }
        else
        {
            this.completeMessage = 'You have successfully completed! Correct answers: ' + this.goodAnswer + '/' + (this.countOfSentences-1);
        }
        alert(this.completeMessage);
        this.countOfSentences = 0
        this.completeMessage = ''
        this.indexes = ''
        this.goodAnswer = 0
        this.attempt = 0
        this.inProgress = false
        this.errorMessage = ''
        this.category = ''
        this.show = false
        this.sentence = {}
        this.splitSentence = []
        this.returnSentence = {
            SentenceTaskId: -1,
            Sentence: '',
            Category: '',
            Translate: ''
        }
      },
      clickWord(el){
          this.returnSentence.Sentence += el += ' ';
          this.$refs[el.replace(/ +/g, '')][0].className += " disabled";
      },
      rebuildsentence()
      {
        this.returnSentence.Sentence = '';
        if(this.splitSentence)
        {
            for(var i = 0; i < this.splitSentence.length; i++)
            {
                this.$refs[this.splitSentence[i].replace(/ +/g, '')][0].className = "routes-admin pointer";
            }
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