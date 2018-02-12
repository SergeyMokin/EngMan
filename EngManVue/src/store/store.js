import Vue from 'vue';
import Vuex from 'vuex';
import api from '../api/api'

Vue.use(Vuex);

const state = {
    rules: []
    , sentences: []
    , words: []
    , users: []
    , messages: []
    , guessestheimages: []
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
        state.messages = result;
        state.messages = state.messages.reverse();
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
};

export default new Vuex.Store({
    state
    , getters
    , mutations
    , actions
});