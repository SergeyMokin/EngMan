import Vue from 'vue';
import Vuex from 'vuex';
import api from '../api/api'
import $ from 'jquery'
window.jQuery = $
require('ms-signalr-client')

Vue.use(Vuex);

const state = {
    rules: []
    , sentences: []
    , words: []
    , users: []
    , messages: []
    , newmess: false
    , newmessUsers: []
    , guessestheimages: []
    , connectionSignalR: {}
    , proxySignalR: {}
    , user: {
        Logined: false,
        Id: '',
        FirstName: '',
        LastName: '',
        Role: '',
        Email: ''
    }
};

const getters = {
    rules: state => state.rules
    , sentences: state => state.sentences
    , words: state => state.words
    , users: state => state.users
    , messages: state => state.messages
    , guessestheimages: state => state.guessestheimages
};


const mutations = {
    getMessages(state, result)
    {
        Array.prototype.unique = function() {
            return this.filter(function(value, index, _this) {
                return _this.indexOf(value) === index;
        })}
        state.newmessUsers = [];
        state.newmess = false;
        state.messages = result;
        state.messages = state.messages.reverse();
        for(var i = 0; i < state.messages.length; i++)
        {
            if(!state.messages[i].CheckReadMes && state.messages[i].Sender.Id != state.user.Id)
            {
                state.newmess = true;
                state.newmessUsers.push(state.messages[i].Sender).unique();
            }
        }
    },
    getGuessesTheImages(state, result)
    {
        state.guessestheimages = result;
    },
    createGuessesTheImage(state, result)
    {
        state.guessestheimages.unshift(result);
    },
    getRules(state, result)
    {
        state.rules = result;
    }
    , createRule(state, result)
    {
        state.posts.unshift(result);
    }
    , getSentences(state, result)
    {
        state.sentences = result;
    }
    , createSentence(state, result)
    {
        state.sentences.unshift(result);
    }
    , getWords(state, result)
    {
        state.words = result;
    }
    , createWord(state, result)
    {
        state.words.unshift(result);
    }
    , getUsers(state, result)
    {
        state.users = result;
    }
    , connectToServ(state, con, proxy)
    {
        state.connectionSignalR = con;
        state.proxySignalR = proxy;
        console.log(state.connectionSignalR);
    }
};

const actions = {
    getMessages: ({ commit }) => {
        api.getMessages()
        .then(res => {
            commit('getMessages', res)
        })
        .catch(e => console.log(e));
    },
    getGuessesTheImages: ({ commit }) => {
      api.getGuessesTheImages()
      .then(rules => {
        commit('getGuessesTheImages', rules);
      })
      .catch(e => {console.log(e)});
    }
    , createGuessesTheImage: ({ commit }, rule) => {
      api.addGuessesTheImage(rule)
      .then(data => commit('createGuessesTheImage', data))
      .catch(e => console.log(e));
    },
    getRules: ({ commit }) => {
      api.getRules()
      .then(rules => {
        commit('getRules', rules);
      })
      .catch(e => {console.log(e)});
    }
    , createRule: ({ commit }, rule) => {
      api.createRule(rule)
      .then(data => commit('createRule', data))
      .catch(e => console.log(e));
    }
    , getSentences: ({ commit }) => {
        api.getSentences()
        .then(sentences => {
          commit('getSentences', sentences);
        })
        .catch(e => {console.log(e)});
    }
    , createSentence: ({ commit }, sentence) => {
        api.createRule(rule)
        .then(data => commit('createSentence', data))
        .catch(e => console.log(e));
      }
    , getWords: ({ commit }) => {
        api.getWords()
        .then(words => {
          commit('getWords', words);
        })
        .catch(e => console.log(e));
    }
    , createWord: ({ commit }, word) => {
        api.createWord(word)
        .then(data => commit('createWord', data))
        .catch(e => console.log(e));
    }
    , getUsers: ({commit}) => {
        api.getUsers()
        .then(res => commit('getUsers', res))
        .catch(e => console.log(e))
    }
    , connectToServ: ( {commit} ) => {
        var vue = this;
        var connection = $.hubConnection('http://localhost:58099'); 
        var proxy = connection.createHubProxy('chat');
        proxy.on('onUpdateMessages', function(){
          vue.getMessages;
        })
        connection.start()
        .done(() => commit('connectToServ', connection, proxy))
        .fail(() => console.log('not connected'));
    }
};

export default new Vuex.Store({
    state
    , getters
    , mutations
    , actions
});