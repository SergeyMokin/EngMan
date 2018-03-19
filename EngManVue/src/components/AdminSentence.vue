<template>
  <div v-if = "$store.state.user.Role == 'admin'">

      <div class="loading" v-if = "inProgress">Loading&#8230;</div>
      
      <div v-if = "!clickSentence" class = "view-list">

        <router-link to="/admin/rules" class = "routes-admin">Rules </router-link>

        <router-link 
            to="/admin/sentences" 
            class = "routes-admin" 
            style = "background-color: #ddd; cursor: default;">
                Sentences 
        </router-link>

        <router-link to="/admin/words" class = "routes-admin">Words </router-link>

        <router-link to="/admin/users" class = "routes-admin">Users </router-link>
        
        <router-link to="/admin/guessestheimages" class = "routes-admin">Guesses the images</router-link>
        
        <br/><br/>

        <span 
            style = "cursor: pointer;" 
            v-on:click = "AddSentence()">
                <img 
                    title="Add" 
                    style = "width: 30px; height: auto" 
                    type = "img" 
                    src = "../assets/add-icon.png">
        </span>
        <br/><br/>
        
        <select 
            class = "select-form" 
            style = "width: 250px" 
            id = "sentence_category" 
            v-model = "category">

            <option v-for = "category in categories" :key = "category">
                {{category}}
            </option>

        </select><br/>
        
        <input 
            style = "width: 250px" 
            v-if = "category.length > 0" 
            type = "text" 
            v-model="searchKey" 
            class = "select-form" 
            placeholder = "Search...">
        <br/>
        
        <div v-if = "category.length > 0" v-for = 'el in sentences' :key = 'el.SentenceTaskId'>
            <div class = "list--element">
                <span class = "span--element">
                    {{el.Sentence}}

                    <span 
                        style = "float: right; font-size:10px; cursor: pointer;" 
                        v-on:click = "deleteSentence(el.SentenceTaskId)">
                            <img 
                                title="Delete" 
                                style = "width: 20px; height: auto" 
                                type = "img" 
                                src = "../assets/close-icon.png">
                    </span>
                    
                    <span 
                        style = "float: right; font-size:10px; cursor: pointer;" 
                        v-on:click = "editSentence(el.SentenceTaskId)">
                            <img 
                                title="Chenge" 
                                style = "margin-right: 5px; width: 18px; height: auto" 
                                type = "img" 
                                src = "../assets/edit-icon.png">
                    </span>
                </span>
            </div>
        </div>
      
      </div>

      <div v-if = "clickSentence" style = "text-align: center">
          <br/><br/>
          <span v-on:click = "closeEditForm()">
              <img 
                title="Close" 
                style = "width: 20px; height: auto;" 
                class = "icon-close" 
                type = "img" 
                src = "../assets/close-icon.png">
          </span>

          <span v-on:click = "saveSentence(sentence)">
              <img 
                title="Save" 
                style = "width: 18px; height: auto; margin-right: 30px; margin-top: 2px" 
                class = "icon-close" 
                type = "img" 
                src = "../assets/save-icon.png">
          </span>
          
          <span>Category</span>
          
          <textarea type = "text" v-model = "sentence.Category" class = "admin-edit"/><br/>
          
          <span>English sentence</span>
          
          <textarea type = "text" v-model = "sentence.Sentence" class = "admin-edit"/><br/>
          
          <span>Translate</span>
          
          <textarea type = "text" v-model = "sentence.Translate" class = "admin-edit"/><br/>
          
          <span v-if = "errorMessage" class = "span-error-message">{{errorMessage}}<br/></span>
          <br/><br/>

      </div>

  </div>
</template>

<script>
import api from '../api/api'

export default {
  name: 'sentences-view',
  data () {
    return {
        inProgress: false,
        errorMessage: '',
        categories: [],
        category: '',
        searchKey: '',
        clickAddSentence: false,
        clickSentence: false
        , sentence: {
            SentenceTaskId: 0,
            Sentence: '',
            Category: '',
            Translate: ''
        }
    }
  },
  methods:{
      editSentence(id){
          if(this.inProgress) return;
          this.inProgress = true;
          api.getSentence(id)
          .then(result => {
                if(result.response)
                {
                    if(result.response.data.Message)
                    {
                        console.log(result.response.data.Message);
                        this.inProgress = false;
                        return;
                    }
                }
                if(result.SentenceTaskId)
                {
                    this.inProgress = false;
                    this.sentence = result;
                    this.clickSentence = true;
                    return;
                }
                else
                {
                    this.inProgress = false;
                    console.log(result);
                    return;
                }
              })
          .catch(e => {
              console.log(e);
              this.inProgress = false;
              return;
          });
      },
      saveSentence(sentence){
          if(this.inProgress) return;
          this.errorMessage = '';
          this.inProgress = true;
          if(this.sentence.Sentence.length == 0 
            || this.sentence.Category.length == 0 
            || this.sentence.Translate.length == 0)
          {
              this.errorMessage = 'Fill in all the fields';
              this.inProgress = false;
              return;
          }
          if(!this.clickAddSentence){
            api.editSentence(sentence)
            .then(result => {
                if(result.response)
                {
                    if(result.response.data.Message)
                    {
                        this.errorMessage = result.response.data.Message;
                        this.inProgress = false;
                        return;
                    }
                }
                if(result === true){
                    this.$store.dispatch('getSentences');
                    this.closeEditForm();
                    return;
                }
                else{
                    this.inProgress = false;
                    console.log(result);
                    return;
                }
            })
            .catch(e => 
            {
                this.errorMessage = 'The server is unavailable or you do not have the rights';
                this.inProgress = false;                
            })
          } else{
              api.createSentence(sentence)
              .then(result =>{
                  if(result.response)
                  {
                      if(result.response.data.Message)
                      {
                          this.inProgress = false;
                          this.errorMessage = result.response.data.Message;
                          return;
                      }
                  }
                  if(result === true){
                      this.$store.dispatch('getSentences');
                      this.closeEditForm();
                      return;
                  }
                  else
                  {
                      this.inProgress = false;
                      this.errorMessage = 'The server is unavailable or you do not have the rights';  
                  }
              })
              .catch(e => {
                  this.errorMessage = 'The server is unavailable or you do not have the rights';
                  this.inProgress = false;
              })
          }
      },
      deleteSentence(id){
          if(this.inProgress) return;
          this.inProgress = true;
          if(confirm("Are you sure?") == true){
            api.deleteSentence(id)
            .then(result =>{
                if(result.response)
                {
                    if(result.response.data.Message)
                    {
                        alert(result.response.data.Message);
                        this.inProgress = false;
                    }
                }
                if(result === "Delete completed successful")
                {
                    this.inProgress = false;
                    this.$store.dispatch('getSentences');
                }
                else{
                    this.inProgress = false;
                    alert(result);
                }
            })
          } else{
              this.inProgress = false;
              alert("You canceled the deletion!");
          }
      },
      AddSentence(){
          this.clickSentence = true;
          this.clickAddSentence = true;
      },
      closeEditForm(){
        this.inProgress = false;
        this.errorMessage = '';
        this.clickAddSentence = false;
        this.clickSentence = false;
        this.sentence = {
            SentenceTaskId: 0,
            Sentence: '',
            Category: ''
        }
      }
  },
  computed: 
  {
    sentences()
    {
      var vue = this;
      if(this.category == 'none' || this.category == '')
      {
          return this.$store.getters.sentences;
      }
      return this.$store.getters.sentences.filter(function(sentence){
          return sentence.Sentence.toLowerCase().indexOf(vue.searchKey.toLowerCase()) > -1  
          && sentence.Category.toLowerCase().indexOf(vue.category.toLowerCase()) > -1;
      });
    }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      api.getAllCategoriesSentences()
      .then(res => {
          if(res.response)
          {
              if(res.response.data.Message)
              {
                  console.log(res.response.data.Message);
                  this.inProgress = false;
                  return;
              }
          }
          if(res.length > 0)
          {
            this.categories = res;
            this.inProgress = false;
          }
          else
          {
              console.log(res);
              this.inProgress = false;
          }
      })
      .catch(e => {
          console.log(e);
          this.inProgress = false;
      })
      this.$store.dispatch('getSentences');
  }
}
</script>