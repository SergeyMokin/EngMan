<template>
  <div v-if = "$store.state.user.Role == 'admin'">
    <div class="loading" v-if = "inProgress">Loading&#8230;</div>
    <div v-if = "!clickRule" class="view-list">
      <router-link to="/admin/rules" class = "routes-admin">Правила </router-link>
      <router-link to="/admin/sentences" class = "routes-admin">Предложения </router-link>
      <router-link to="/admin/words" class = "routes-admin">Словарь </router-link>
      <router-link to="/admin/users" class = "routes-admin">Пользователи </router-link>
      <router-link to="/admin/guessestheimages" class = "routes-admin">Задания по картинкам</router-link><br/><br/>
      <span style = "cursor: pointer;" v-on:click = "AddRule()"><img title="Добавить" style = "width: 30px; height: auto" type = "img" src = "../assets/add-icon.png"></span><br/><br/>
      <input type = "text" v-model="searchKey" class = "select-form" placeholder = "Поиск..." v-on:click = "searchKey = ''"><br/>
      <div v-for = 'el in rules' :key = 'el.RuleId'>
      <div class = "list--element">
        <span class = "span--element">
            {{el.Title}}
            <span style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "deleteRule(el.RuleId);"><img title="Удалить" style = "width: 20px; height: auto" type = "img" src = "../assets/close-icon.png"></span>
            <span style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "editRule(el.RuleId);"><img title="Изменить" style = "margin-right: 5px; width: 18px; height: auto" type = "img" src = "../assets/edit-icon.png"></span>
        </span>
      </div>
    </div>
  </div>  
  <div v-if = "clickRule" style = "text-align: center">
          <br/><br/>
          <span v-on:click = "closeEditForm()"><img title="Закрыть" style = "width: 20px; height: auto;" class = "icon-close" type = "img" src = "../assets/close-icon.png"></span>
          <span v-on:click = "saveRule(rule)"><img title="Сохранить" style = "width: 18px; height: auto; margin-right: 30px; margin-top: 2px" class = "icon-close" type = "img" src = "../assets/save-icon.png"></span>
          <span>Название</span>
          <textarea class = "admin-edit" type = "text" v-model = "rule.Title"/><br/>
          <span>Категория</span>
          <textarea class = "admin-edit" type = "text" v-model = "rule.Category"/><br/>
          <span>Текст</span>
          <textarea class = "admin-edit" type = "text" v-model = "rule.Text" style = "height: 450px"/><br/>
          <span style = "float: left; margin-right: 10px; margin-left: 20%;" v-on:click = "downloadOnServer()"><img title="Загрузить на сервер" style = "cursor:pointer;width: 25px; height: auto;" type = "img" src = "../assets/upload-icon.png"></span>
          <span style = "float: left; text-align: left;"><input type="file" accept="image/*" @change="onFileChange" multiple><br/></span><br/><br/>
          <div v-if = 'pathesOfImages.length > 0' v-for = 'el in pathesOfImages' :key = 'el'>
              <span>
                  {{el}}
              </span>
          </div><br/><br/>
          <span v-if = "errormessage" class = "span-error-message">{{errormessage}}<br/></span><br/><br/>
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
        clickAddRule: false,
        clickRule: false,
        pathesOfImages: [],
        images: [],
        rule: {
            RuleId: 0,
            Title: '',
            Text: '',
            Category: ''
        }
    }
  },
  methods:{
      onFileChange(e) {
        this.images = [];
        var check = 0;
        var arr = e.target.files || e.dataTransfer.files;
        if (!arr.length)
            return;
        var vue = this;
        var load = (e) => {
            var image = {
                Name: arr[check].name,
                Data: e.target.result
            }
            check++;
            this.images.push(image);
        };
        for(var i = 0; i < arr.length; i++)
        {
            var reader = new FileReader();
            reader.onload = load;
            reader.readAsBinaryString(arr[i]);
        }
      },
      downloadOnServer(){
          if(this.inProgress) return;
          this.inProgress = true;
          this.errormessage = '';
          if(this.images.length == 0){
              this.errormessage = 'Для загрузки изображений выберите их';
              this.inProgress = false;
              return;
          }
          api.addImages(this.images)
          .then(result => {
              for(var i = 0; i < result.length; i++)
              {
                  this.pathesOfImages.push("<img src=\"" + result[i] + "\" style = \"width: 70%;margin: 0 15% 0 15%; border: none; border-radius: 10px;\">");
              }
              this.inProgress = false;
          })
          .catch(e => {
              this.inProgress = false;
              this.errormessage = 'Не удалось загрузить изображения';
          })
      },
      editRule(id){
        if(this.inProgress) return;
        this.inProgress = true;
        api.getRule(id)
        .then(result => {
                this.inProgress = false;
                this.rule = result;
                this.clickRule = true;
        })
        .catch(e => {
            this.inProgress = false;
        });
        this.inProgress = false;
      },
      saveRule(rule){
          if(this.inProgress) return;
          this.inProgress = true;
          this.errormessage = '';
          if(this.rule.Title.length == 0 || this.rule.Category.length == 0 || this.rule.Text.length == 0)
          {
              this.errormessage = 'Заполните все поля';
              this.inProgress = false;
              return;
          }
          if(!this.clickAddRule){
            api.editRule(rule)
            .then(result => {
                if(result.RuleId > 0){
                    this.inProgress = false;
                    this.$store.dispatch('getRules');
                    this.closeEditForm();
                }
            })
            .catch(e => {
                this.inProgress = false;
                this.errormessage = 'Сервер недоступен или у вас нет прав';
            })
          } else{
              api.createRule(rule)
              .then(result =>{
                  if(result.RuleId > 0){
                      this.inProgress = false;
                      this.$store.dispatch('getRules');
                      this.closeEditForm();
                  }
                  this.inProgress = false;
              })
              .catch(e => {
                  this.inProgress = false;
                  this.errormessage = 'Сервер недоступен или у вас нет прав';
              })
          }
          this.inProgress = false;
      },
      deleteRule(id){
          if(this.inProgress) return;
          this.inProgress = true;
          if(confirm("Вы уверены?") == true){
            api.deleteRule(id)
            .then(result =>{
                this.inProgress = false;
                this.$store.dispatch('getRules');
            })
          } else{
              this.inProgress = false;
              alert("Вы отменили удаление!");
          }
          this.inProgress = false;
      },
      AddRule(){
          this.clickRule = true;
          this.clickAddRule = true;
      },
      closeEditForm(){
        this.inProgress = false;
        this.searchKey = '';
        this.errormessage = '';
        this.clickAddRule = false;
        this.clickRule = false;
        this.pathesOfImages = [];
        this.images = [];
        this.rule = {
            RuleId: 0,
            Title: '',
            Text: '',
            Category: ''
        }
      }
  },
  computed: 
  {
    rules()
    {
      var vue = this;
      return this.$store.getters.rules.filter(function(rule){
          return rule.Title.toLowerCase().indexOf(vue.searchKey.toLowerCase()) > -1;
      });
    }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      this.$store.dispatch('getRules');
      this.inProgress = false;
  }
}
</script>