<template>
    <div class="main-content-center"><div class="loading" v-if = "inProgress">Loading&#8230;</div>
    <div class = "icon-close"><router-link to="/"><img src = "../assets/arrow-up.png" title="Назад" style = "margin: 5px; width: 20px; height: 20px;"></router-link></div> 
        <div class = "view-list">
            <input v-if = "!errorMessage" placeholder="Поиск..." type="text" class = "select-form" v-model = "keyWord" v-on:click = "keyWord = ''"/>
            <div v-if = "errorMessage" class = "span-error-message">{{errorMessage}}</div>
            <div v-for = 'el in sortWords' :key = 'el.Id'>
                <div class = "list--element" style = "cursor: default">
                    <a>{{el.Original}} - {{el.Translate}}</a>
                    <span style = "float: right; font-size:10px; cursor: pointer;" v-on:click = "deleteUserWord(el.WordId);"><img title="Удалить" style = "width: 20px; height: auto" type = "img" src = "../assets/close-icon.png"></span>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import api from '../api/api';
    export default {
        data () {
            return {
                inProgress: false,
                dictionary: {},
                errorMessage: '',
                keyWord: ''
            }
        },
        methods: {
            deleteUserWord(id){
                if(this.inProgress) return;
                this.inProgress = true;
                if(confirm("Вы уверены, что хотите удалить это слово?") == true){
                    api.deleteUserWord(id)
                    .then(res => {
                        if(res > 0)
                        {
                            alert('Успешно удалён');
                            this.inProgress = true;
                            api.getUserDictionary()
                            .then(res => {
                                if(res.User)
                                {
                                    this.dictionary = res;
                                }
                                else
                                {
                                    this.errorMessage = 'Словарь пуст';
                                }
                                this.inProgress = false;
                            })
                            .catch(e => {
                                this.errorMessage = 'Сервер недоступен';
                                this.inProgress = false;
                            })  
                            }
                            else
                            {
                                alert('В базе данных не существует такого слова, обновите страницу');
                        }
                    })
                    .catch(e => {
                        this.inProgress = false;
                    })
                }
                else{
                    alert('Удаление отменено');
                    this.inProgress = false;
                }
            },
        },
        computed: {
            sortWords(){
                if(this.inProgress) return;
                this.inProgress = true;
                this.errorMessage = '';
                if(this.dictionary.Words != undefined)
                {
                    this.dictionary.Words.sort((lword, rword) => {
                        if(lword.Category > rword.Category) return 1;
                        if(lword.Category < rword.Category) return -1;
                        return 0;
                    });
                    var vue = this;
                    if(this.keyWord == '')
                    {
                        this.inProgress = false;
                        return this.dictionary.Words;
                    }
                    this.inProgress = false;
                    return this.dictionary.Words.filter(function(word){
                        return word.Category.toLowerCase().indexOf(vue.keyWord.toLowerCase()) > -1 
                        || word.Original.toLowerCase().indexOf(vue.keyWord.toLowerCase()) > -1 
                        || word.Translate.toLowerCase().indexOf(vue.keyWord.toLowerCase()) > -1;
                    });
                }
                else{
                    this.errorMessage = 'Словарь пуст';
                    this.inProgress = false;
                }
            }
        },
        created: function() {
            if(this.inProgress) return;
            this.inProgress = true;
            api.getUserDictionary()
            .then(res => {
                if(res.User)
                {
                    this.dictionary = res;
                }
                else
                {
                    this.errorMessage = 'Словарь пуст';
                }
                this.inProgress = false;
            })
            .catch(e => {
                this.errorMessage = 'Сервер недоступен';
                this.inProgress = false;
            })
        }
    }
</script>