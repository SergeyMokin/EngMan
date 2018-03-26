<template>
<div>

    <div class="loading" v-if = "inProgress">Loading&#8230;</div>

    <div class="main-content-center">
        <div class = "view-list">
            
            <span style = "font-size: 30px">Dictionary</span><br/>
            
            <select 
                style = "width: 250px" 
                v-model = "category" 
                class = "select-form" 
                v-on:change = "getWordsByCategory()"
                v-if="categories.length > 0">
                <option v-for = "category in categories" :key = "category">
                    {{category}}
                </option>
            </select>
            <br/>
            
            <input 
                style = "width: 250px" 
                v-if = "!errorMessage && category.length > 0" 
                placeholder="Поиск..." 
                type="text" 
                class = "select-form" 
                v-model = "keyWord" 
                v-on:click = "keyWord = ''"/>
            
            <div 
                v-if = "errorMessage" 
                class = "span-error-message">
                {{errorMessage}}
            </div>
            
            <div v-for = 'el in sortWords' :key = 'el.Id'>
                <div class = "list--element" style = "cursor: default">
                    <a>{{el.Original}} - {{el.Translate}}</a>
                    <span 
                        style = "float: right; font-size:10px; cursor: pointer;" 
                        v-on:click = "deleteUserWord(el.WordId);">
                            <img 
                                title="Удалить" 
                                style = "width: 20px; height: auto" 
                                type = "img" 
                                src = "../assets/close-icon.png">
                    </span>
                </div>
            </div>

            <div
                v-if="categories.length == 0">
                <span>You have no words in your dictionary</span>
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
                category: '',
                categories: [],
                errorMessage: '',
                keyWord: ''
            }
        },
        methods: {
            deleteUserWord(id){
                if(this.inProgress) return;
                this.inProgress = true;
                if(confirm("Are you sure you want to delete this word?") == true){
                    api.deleteUserWord(id)
                    .then(res => {
                        if(res === "Delete completed successful")
                        {
                            alert('Delete completed successful');
                            this.inProgress = false;
                            this.getWordsByCategory();
                        }
                        else
                        {
                            alert('Delete canceled');
                            this.inProgress = false;
                        }
                    })
                    .catch(e => {
                        this.inProgress = false;
                    })
                }
                else{
                    alert('Delete canceled');
                    this.inProgress = false;
                }
            },
            getWordsByCategory()
            {
                if(this.inProgress) return;
                this.inProgress = true;
                api.getUserDictionaryByCategory(this.category)
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
                    if(res.Words.length > 0)
                    {
                        this.dictionary = res;
                        this.inProgress = false;
                        return;
                    }
                    else
                    {
                        this.inProgress = false;
                        console.log(res);
                    }
                })
                .catch(e => {
                    this.inProgress = false;
                    console.log(e);
                })
            }
        },
        computed: {
            sortWords(){
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
                        return this.dictionary.Words;
                    }
                    return this.dictionary.Words.filter(function(word){
                        return word.Category.toLowerCase().indexOf(vue.keyWord.toLowerCase()) > -1 
                        || word.Original.toLowerCase().indexOf(vue.keyWord.toLowerCase()) > -1 
                        || word.Translate.toLowerCase().indexOf(vue.keyWord.toLowerCase()) > -1;
                    });
                }
                else{
                    if(this.category.length > 0)
                    {
                        this.errorMessage = 'Dictionary is empty';
                    }
                }
            }
        },
        created: function() {
            if(this.inProgress) return;
            this.inProgress = true;
            api.getAllCategoriesOfUserDictionary()
            .then(res =>
            {
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
                    this.inProgress = false;
                    this.categories = res;
                    return;
                }
                else
                {
                    console.log(res);
                    this.inProgress = false;
                }
            })
            .catch(e => 
            {
                console.log(e);
                this.inProgress = false;
            })
        }
    }
</script>