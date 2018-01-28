import Vue from 'vue';
import Vuex from 'vuex';
import api from '../api/api'

Vue.use(Vuex);

const state = {
    rules: []
    , sentences: []
    , words: []
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
};


const mutations = {
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
};

const actions = {
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
};

export default new Vuex.Store({
    state
    , getters
    , mutations
    , actions
});