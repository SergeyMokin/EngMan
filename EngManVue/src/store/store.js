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
    , endOfMessages: false
    , newmessages: []
    , guessestheimages: []
    , connectionSignalR: {}
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
    SET_MESSAGES(state, result)
    {
        state.messages = result;
    },
    SET_NEW_MESSAGES(state, result)
    {
        state.newmessages = [];
        state.newmess = false;
        if(result.length > 0)
        {
            state.newmessages = result;
            state.newmess = true;
        }
    },
    SET_MESSAGES_BY_USER_ID(state, result)
    {
        state.endOfMessages = false;
        if(result.length == 0)
        {
            state.endOfMessages = true;
        }
        state.messages = state.messages.concat(result)
    },
    SET_GUESSES_THE_IMAGES(state, result)
    {
        state.guessestheimages = result;
    },
    SET_RULES(state, result)
    {
        state.rules = result;
    },
    SET_SENTENCES(state, result)
    {
        state.sentences = result;
    },
    SET_WORDS(state, result)
    {
        state.words = result;
    },
    SET_USERS(state, result)
    {
        state.users = result;
    },
    SET_CONNECTONS(state, con)
    {
        state.connectionSignalR = con;
    }
};

const actions = {
    getMessages: ({ commit }) => {
        api.getMessages()
        .then(res => {
            commit('SET_MESSAGES', res)
        })
        .catch(e => console.log(e));
    },
    getNewMessages: ({ commit }) => {
        api.getNewMessages()
        .then(res => {
            commit('SET_NEW_MESSAGES', res)
        })
        .catch(e => console.log(e));
    },
    getMessagesByUserId: ({ commit }, params) => {
        api.getMessagesByUserId(params.otherUserId, params.lastReceivedMessageId)
        .then(res => {
            commit('SET_MESSAGES_BY_USER_ID', res)
        })
        .catch(e => console.log(e));
    },
    getGuessesTheImages: ({ commit }) => {
      api.getGuessesTheImages()
      .then(rules => {
        commit('SET_GUESSES_THE_IMAGES', rules);
      })
      .catch(e => {console.log(e)});
    },
    getRules: ({ commit }) => {
      api.getRules()
      .then(rules => {
        commit('SET_RULES', rules);
      })
      .catch(e => {console.log(e)});
    }
    , getSentences: ({ commit }) => {
        api.getSentences()
        .then(sentences => {
          commit('SET_SENTENCES', sentences);
        })
        .catch(e => {console.log(e)});
    }
    , getWords: ({ commit }) => {
        api.getWords()
        .then(words => {
          commit('SET_WORDS', words);
        })
        .catch(e => console.log(e));
    }
    , getUsers: ({commit}) => {
        api.getUsers()
        .then(res => commit('SET_USERS', res))
        .catch(e => console.log(e))
    }
    , connectToServ: ( {dispatch, commit}, userview ) => {
        var connection = $.hubConnection('http://localhost:58099'); 
        var proxy = connection.createHubProxy('chat');
        proxy.on('onUpdateMessages', function(){
          dispatch('getNewMessages');
        })
        connection.start()
        .done(() => 
        {
            proxy.invoke('Connect', userview);
            commit('SET_CONNECTONS', connection)
        })
        .fail(() => console.log('Невозможно подключиться к серверу'));
    }
};

export default new Vuex.Store({
    state
    , getters
    , mutations
    , actions
});