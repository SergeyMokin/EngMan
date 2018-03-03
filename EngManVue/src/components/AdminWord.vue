<template>
  <div v-if = "$store.state.user.Role == 'admin'">
      <div class="loading" v-if = "inProgress">Loading&#8230;</div>
      <div v-if = "!clickWord" class="view-list" >
        <router-link to="/admin/rules" class = "routes-admin">Rules </router-link>
        <router-link to="/admin/sentences" class = "routes-admin">Sentences </router-link>
        <router-link to="/admin/words" class = "routes-admin" style = "background-color: #ddd; cursor: default;">Words </router-link>
        <router-link to="/admin/users" class = "routes-admin">Users </router-link>
        <router-link to="/admin/guessestheimages" class = "routes-admin">Guesses the images</router-link><br/><br/>
        <span style = "cursor: pointer;" v-on:click = "addWord()"><img title="Add" style = "width: 30px; height: auto" type = "img" src = "../assets/add-icon.png"></span><br/><br/>
        <select id = "word_category" class = "select-form" style = "width: 250px" v-model = "category">
            <option v-for = "category in categories" :key = "category">
                {{category}}
            </option>
        </select><br/>
        <input style = "width: 250px" v-if = "category.length > 0" type = "text" v-model="searchKey" class = "select-form" placeholder = "Search..."><br/>
        <div v-if = "category.length > 0" v-for = 'el in words' :key = 'el.WordId'>
            <div class = "list--element">
                <span class = "span--element">
                    {{el.Original}} {{el.Transcription}} - {{el.Translate}}
                    <span style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "deleteWord(el.WordId)"><img title="Delete" style = "width: 20px; height: auto" type = "img" src = "../assets/close-icon.png"></span>
                    <span style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "editWord(el.WordId)"><img title="Change" style = "margin-right: 5px; width: 18px; height: auto" type = "img" src = "../assets/edit-icon.png"></span>
                </span>
            </div>
        </div>
      </div>
      <div v-if = "clickWord" style = "text-align: center">
          <br/><br/>
          <span v-on:click = "closeEditForm()"><img title="Close" style = "width: 20px; height: auto;" class = "icon-close" type = "img" src = "../assets/close-icon.png"></span>
          <span v-on:click = "saveWord(word)"><img title="Save" style = "width: 18px; height: auto; margin-right: 30px; margin-top: 2px" class = "icon-close" type = "img" src = "../assets/save-icon.png"></span>
          <span>Category</span>
          <textarea type = "text" v-model = "word.Category" class = "admin-edit"/><br/>
          <span>English original</span>
          <textarea type = "text" v-model = "word.Original" class = "admin-edit"/><br/>
          <span>Russian translate</span>
          <textarea type = "text" v-model = "word.Translate" class = "admin-edit"/><br/>
          <span>Transcription</span>
          <textarea type = "text" v-model = "word.Transcription" class = "admin-edit"/><br/>
          <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span><br/><br/>
      </div>
  </div>
</template>

<script>
import api from '../api/api'

export default {
  name: 'words-view',
  data () {
    return {
        inProgress: false,
        errormessage: '',
        searchKey: '',
        categories: [],
        category: '',
        clickAddWord: false,
        clickWord: false
        , word: {
            WordId: 0,
            Original: '',
            Translate: '',
            Category: '',
            Transcription: ''
        }
    }
  },
  methods:{
      editWord(id){
          if(this.inProgress) return;
          this.inProgress = true;
          api.getWord(id)
          .then(result => {
                if(result.response)
                {
                    if(result.response.data.Message)
                    {
                        this.inProgress = false;
                        console.log(result.response.data.Message);
                        return;
                    }
                }
                if(result.WordId)
                {
                    this.inProgress = false;
                    this.word = result;
                    this.clickWord = true;
                }
                else
                {
                    console.log(result);
                    this.inProgress = false;
                }
              })
          .catch(e => {
              this.inProgress = false;
              console.log(e);
          });
      },
      saveWord(word){
          if(this.inProgress) return;
          this.errormessage = '';
          this.inProgress = true;
          if(this.word.Original.length == 0 || this.word.Translate.length == 0 || this.word.Category.length == 0 || this.word.Transcription.length == 0)
          {
              this.errormessage = 'Fill in all the fields';
              this.inProgress = false;
              return;
          }
          if(!this.clickAddWord){
            api.editWord(word)
            .then(result => {
                if(result.response)
                {
                    if(result.response.data.Message)
                    {
                        this.errormessage = result.response.data.Message;
                        this.inProgress = false;
                        return;
                    }
                }
                if(result == true){
                    this.$store.dispatch('getWords');
                    this.closeEditForm();
                }
                else
                {
                    console.log(result);
                    this.inProgress = false;
                    this.errormessage = "The server is unavailable or you do not have the rights";
                }
            })
            .catch(e => {
                this.errormessage = 'The server is unavailable or you do not have the rights';
                this.inProgress = false;  
            })
          } else{
              api.createWord(word)
              .then(result =>{
                if(result.response)
                {
                    if(result.response.data.Message)
                    {
                        this.errormessage = result.response.data.Message;
                        this.inProgress = false;
                        return;
                    }
                }
                if(result == true){
                    this.$store.dispatch('getWords');
                    this.closeEditForm();
                }
                else
                {
                    console.log(result);
                    this.inProgress = false;
                    this.errormessage = "The server is unavailable or you do not have the rights";
                }
              })
              .catch(e => {
                  this.errormessage = 'Сервер недоступен или у вас нет прав';
                  this.inProgress = false;
              })
          }
      },
      deleteWord(id){
          if(this.inProgress) return;
          this.inProgress = true;
          if(confirm("Вы уверены?") == true){
            api.deleteWord(id)
            .then(result =>{
                if(result.response){
                    if(result.response.data.Message)
                    {
                        this.errormessage = result.response.data.Message;
                        this.inProgress = false;
                        return;
                    }
                }
                if(result === "Delete completed successful")
                {
                    this.inProgress = false;
                    this.$store.dispatch('getWords');
                }
                else
                {
                    console.log(result);
                    this.inProgress = false;
                }
            })
          } else{
              this.inProgress = false;
              alert("Вы отменили удаление!");
          }
      },
      addWord(){
          this.clickWord = true;
          this.clickAddWord = true;
      },
      closeEditForm(){
        this.inProgress = false;
        this.errormessage = '';
        this.clickAddWord = false;
        this.clickWord = false;
        this.word = {
            WordId: 0,
            Original: '',
            Translate: '',
            Category: ''
        }
      }
  },
  computed: 
  {
    words()
    {
      var vue = this;
      if(this.category == 'none' || this.category == '')
      {
          return this.$store.getters.words;
      }
      return this.$store.getters.words.filter(function(word){
          return (word.Original.toLowerCase().indexOf(vue.searchKey.toLowerCase()) > -1 || word.Translate.toLowerCase().indexOf(vue.searchKey.toLowerCase()) > -1)  
          && word.Category.toLowerCase().indexOf(vue.category.toLowerCase()) > -1;
      });
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
                this.errormessage = res.response.data.Message;
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
            this.inProgress = false;
            console.log(res);
        }
      })
      .catch(e => 
      {
          console.log(e);
          this.inProgress = false;
      })
      this.$store.dispatch('getWords');
  }
}
</script>