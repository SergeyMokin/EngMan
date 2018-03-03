<!--edit-->
<template>
  <div v-if = "$store.state.user.Role == 'admin'">
    <div class="loading" v-if = "inProgress">Loading&#8230;</div>
    <div class="view-list">
      <router-link to="/admin/rules" class = "routes-admin">Rules </router-link>
      <router-link to="/admin/sentences" class = "routes-admin">Sentences </router-link>
      <router-link to="/admin/words" class = "routes-admin">Words </router-link>
      <router-link to="/admin/users" class = "routes-admin">Users </router-link>
      <router-link to="/admin/guessestheimages" class = "routes-admin" style = "background-color: #ddd; cursor: default;">Guesses the images</router-link><br/><br/>
      <span style = "cursor: pointer;" v-on:click = "add()"><img title="Add" style = "width: 30px; height: auto" type = "img" src = "../assets/add-icon.png"></span><br/><br/>
      <select class = "select-form" style = "width: 250px" v-model = "category">
          <option v-for = "category in categories" :key = "category">
              {{category}}
          </option>
      </select><br/>
      <input style = "width: 250px" v-if = "category.length > 0" type = "text" v-model="searchKey" class = "select-form" placeholder = "Search..."><br/>
      <div v-if = "category.length > 0" v-for = 'el in tasks' :key = 'el.Id'>
      <div class = "list--element">
        <span class = "span--element">
            {{el.Word.Original}}
            <span style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "deletetask(el.Id)"><img title="Delete" style = "width: 20px; height: auto" type = "img" src = "../assets/close-icon.png"></span>
            <span style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "edit(el.Id)"><img title="Change" style = "margin-right: 5px; width: 18px; height: auto" type = "img" src = "../assets/edit-icon.png"></span>
        </span>
      </div>
    </div>
  </div>
  <div v-if = "click" class = "b-popup">
      <div class = "b-popup-content">
          <span style = "float: right; font-size:10px; cursor: pointer" v-on:click = "closeEditForm()"><img title="Close" style = "width: 20px; height: auto;" type = "img" src = "../assets/close-icon.png"></span>
          <span style = "float: right; font-size:10px; cursor: pointer" v-on:click = "save()"><img title="Save" style = "margin-top: 2px; width: 16px; height: auto" type = "img" src = "../assets/save-icon.png"></span>
          
          <div>
          <span>Category: </span>
          <select style = "width: 250px" id = "guessestheimage_category" class = "select-form" v-model = "wordcategory" v-on:change = "downloadWordsByCategory()">
            <option v-for = "el in wordcategories" :key = "el">
                {{el}}
            </option>
          </select><br/><br/>
          <span>English word: </span>
          <select id = "guessestheimage_word" class = "select-form" style = "width: 250px" v-model = "choosenWord">
              <option v-for = "word in downloadwords" :key = "word.WordId">
                  {{word.Original}}
              </option>
          </select><br/><br/>
          </div>
          <div v-if = "clickAdd"><input type="file" accept="image/*" @change="onFileChange"><br/></div>
          <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span><br/><br/>
      </div>
  </div>
  </div>
</template>

<script>
import api from '../api/api'

export default {
  name: 'rules-view',
  data () {
    return {
        inProgress: false,
        searchKey: '',
        errormessage: '',
        wordcategory: '',
        wordcategories: [],
        categories: [],
        category: '',
        downloadwords: [],
        clickAdd: false,
        click: false,
        choosenWord: '',
        image: {
            Name: '',
            Data: ''
        },
        task: {
            Id: 0,
            WordId: 0,
            Path: ''
        }
    }
  },
  methods:{
      onFileChange(e) {
        var arr = e.target.files || e.dataTransfer.files;
        if (!arr.length)
            return;
        var vue = this;
        this.image = {
            Name: arr[0].name,
            Data: ''
        }
        var load = (e) => {
            vue.image.Data = e.target.result;
        };
        for(var i = 0; i < arr.length; i++)
        {
            var reader = new FileReader();
            reader.onload = load;
            reader.readAsBinaryString(arr[i]);
        }
      },
      edit(id){
        if(this.inProgress) return;
        this.inProgress = true;
        api.getGuessesTheImageById(id)
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
                if(result.Id)
                {
                    this.inProgress = false;
                    this.task = result;
                    this.click = true;
                    document.body.style = "overflow: hidden";
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
        });
      },
      save(task){
          if(this.inProgress) return;
          this.inProgress = true;
          this.errormessage = '';
          if(this.image.Data.length == 0 && this.clickAdd){
              this.errormessage = 'Select an image';
              this.inProgress = false;
              return;
          }
          if(this.choosenWord.length == 0)
          {
              this.errormessage = 'Choose the word';
              this.inProgress = false;
              return;
          }
          this.task.WordId = this.getWordIdFromChoosenWord();
          if(this.task.WordId == 0)
          {
              this.errormessage = 'Unreal word';
              this.inProgress = false;
              return;
          }
          if(!this.clickAdd){
            api.editGuessesTheImage(this.task)
            .then(result => {
                if(result.response)
                {
                    if(result.response.data.Message)
                    {
                        this.inProgress = false;
                        this.errormessage = result.response.data.Message;
                        return;
                    }
                }
                if(result === true){
                    this.inProgress = false;
                    this.$store.dispatch('getGuessesTheImages');
                    this.closeEditForm();
                }
                else{
                    this.inProgress = false;
                    console.log(result);
                }
            })
            .catch(e => {
                this.inProgress = false;
                this.errormessage = 'The server is unavailable or you do not have the rights';
            })
          } else{
              api.addGuessesTheImage({
                  Id: 0,
                  WordId: this.task.WordId,
                  Image: this.image
              })
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
                  if(result === true){
                      this.inProgress = false;
                      this.$store.dispatch('getGuessesTheImages');
                      this.closeEditForm();
                      return;
                  }
                  else
                  {
                    this.errormessage = 'The server is unavailable or you do not have the rights';
                    this.inProgress = false;
                  }
              })
              .catch(e => {
                  this.inProgress = false;
                  this.errormessage = 'The server is unavailable or you do not have the rights';
              })
          }
      },
      deletetask(id){
          if(this.inProgress) return;
          this.inProgress = true;
          if(confirm("Are you sure?") == true){
            api.deleteGuessesTheImage(id)
            .then(result =>{
                if(result.response)
                {
                    if(result.response.data.Message)
                    {
                        console.log(result.response.data.Message);
                        this.inProgress = false;
                        return;
                    }
                }
                if(result === "Delete completed successful")
                {
                    this.inProgress = false;
                    this.$store.dispatch('getGuessesTheImages');
                    return;
                }
            })
          } else{
              this.inProgress = false;
              alert("You canceled the deletion!");
          }
      },
      add(){
          this.click = true;
          this.clickAdd = true;
      },
      closeEditForm(){
        this.inProgress = false;
        document.body.style = "overflow: auto";
        this.searchKey = '';
        this.errormessage = '',
        this.wordcategory = '',
        this.clickAdd = false,
        this.click = false,
        this.choosenWord = '',
        this.image = {
            Name: '',
            Data: ''
        },
        this.task = {
            Id: 0,
            WordId: 0,
            Path: ''
        }
      },
      downloadWordsByCategory()
      {
          if(this.inProgress) return;
          this.inProgress = true;
          api.getWordsByCategory(this.wordcategory)
          .then(result =>
          {
              if(result.response)
              {
                  if(result.response.data.Message)
                  {
                      console.log(result.response.data.Message);
                      this.inProgress = false;
                      return;
                  }
              }
              if(result.length > 0)
                {
                    this.downloadwords = result;
                    this.inProgress = false;
                    return;
                }
                else
                {
                    console.log(result);
                    this.inProgress = false;
                    return;
                }
            })
          .catch(e => 
          {
              console.log(e);
              this.inProgress = false;
          })
      },
      getWordIdFromChoosenWord()
      {
        var vue = this;
        var word = this.downloadwords.filter(function(word){
            return word.Original.toLowerCase() === vue.choosenWord.toLowerCase();
        });
        return word[0].WordId;
      }
  },
  computed: 
  {
    tasks()
    {
      var vue = this;
      return this.$store.getters.guessestheimages.filter(function(task){
          return task.Word.Original.toLowerCase().indexOf(vue.searchKey.toLowerCase()) > -1 
          && task.Word.Category.toLowerCase().indexOf(vue.category.toLowerCase()) > -1;
      });
    }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      this.$store.dispatch('getGuessesTheImages');
      api.getAllCategoriesWords()
      .then(res => {
          if(res.response)
          {
              if(res.response.data.Message)
              {
                  this.inProgress = false;
                  console.log(res.response.data.Message);
                  return;
              }
          }
          if(res.length > 0)
          {
            this.wordcategories = res;
            this.inProgress = false;
            return;
          }
          else
          {
              console.log(res);
              this.inProgress = false;
              return;
          }
      })
      .catch(e => {
          console.log(e);
          this.inProgress = false;
      })
      api.getAllCategoriesGuessesTheImages()
      .then(res => {
          if(res.response)
          {
              if(res.response.data.Message)
              {
                  this.inProgress = false;
                  console.log(res.response.data.Message);
                  return;
              }
          }
          if(res.length > 0)
          {
            this.categories = res;
            this.inProgress = false;
            return;
          }
          else
          {
              console.log(res);
              this.inProgress = false;
              return;
          }
      })
      .catch(e => {
          console.log(e);
          this.inProgress = false;
      })
  }
}
</script>