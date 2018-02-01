import axios from 'axios';

const url = 'http://localhost:58099/api'

export default {
  addToken(token){
    axios.defaults.headers.common['Authorization'] ='bearer ' + token;
  },
  deleteToken(){
    delete axios.defaults.headers.common['Authorization'];
  },
  getRules() {
    return axios.get(url + '/rule/getallrules')
    .then(response => response.data)
    .catch(e => e);
  },
  getRule(id) {
    return axios.get(url + '/rule/getrule/' + id)
    .then(response => response.data)
    .catch(e => e);
  },
  createRule(rule) {
    return axios.post(url + '/rule/addrule', rule)
    .then(response => response.data)
    .catch(e => e);
  },
  editRule(rule) {
    return axios.put(url + '/rule/editrule', rule)
    .then(response => response.data)
    .catch(e => e);
  },
  deleteRule(id) {
    return axios.delete(url + '/rule/deleterule/' + id)
    .then(response => response.data)
    .catch(e => e);
  },
  addImages(images){
    return axios.post(url + '/rule/addimages', images)
    .then(response => response.data)
    .catch(e => e)
  },
  login(model){
    return axios.post('http://localhost:58099/token', require('querystring').stringify(model))
    .then(response => {
      axios.defaults.headers.common['Authorization'] ='bearer ' + response.data.access_token;
      return response.data;
    })
    .catch(e => e)
  },
  registration(model){
    return axios.post(url + '/account/registration', model)
    .then(repsonse => response.data)
    .catch(e => e)
  },
  signout(){
    this.deleteToken();
    return axios.post(url + '/account/logout')
    .then(response => response.data)
    .catch(e => e);
  },
  getUserData(){
    return axios.get(url + '/account/getuserdata')
    .then(res => res.data)
    .catch(e => e)
  },
  getSentenceTask(category, id){
    return axios.get(url + '/sentencetask/gettask?category=' + category + '&id=' + id)
    .then(response => response.data)
    .catch(e => e);
  },
  verificationSentenceTask(sentence){
    return axios.post(url + '/sentencetask/verificationcorrectness', sentence)
  },
  getWordMap(category, id){
    return axios.get(url + '/wordmap/getword?category=' + category + '&id=' + id)
    .then(response => response.data)
    .catch(e => e);
  },
  verificationWordMap(wordmap){
    return axios.post(url + '/wordmap/VerificationCorrectness', wordmap)
  },
  getAllCategoriesSentences(){
    return axios.get(url + '/sentencetask/getallcategories')
    .then(res => res.data)
    .catch(e => e)
  },
  getSentences() {
    return axios.get(url + '/adminsentencetask/GetAllTasks')
    .then(response => response.data)
    .catch(e => e);
  },
  getSentence(id) {
    return axios.get(url + '/adminsentencetask/GetTaskById/' + id)
    .then(response => response.data)
    .catch(e => e);
  },
  createSentence(sentence) {
    return axios.post(url + '/adminsentencetask/AddTask', sentence)
    .then(response => response.data)
    .catch(e => e);
  },
  editSentence(sentence) {
    return axios.put(url + '/adminsentencetask/EditTask', sentence)
    .then(response => response.data)
    .catch(e => e);
  },
  deleteSentence(id) {
    return axios.delete(url + '/adminsentencetask/DeleteTask/' + id)
    .then(response => response.data)
    .catch(e => e);
  },
  getAllCategoriesWords(){
    return axios.get(url + '/wordmap/getallcategories')
    .then(res => res.data)
    .catch(e => e)
  },
  getWords() {
    return axios.get(url + '/word/GetAllWords')
    .then(response => response.data)
    .catch(e => e);
  },
  getWord(id) {
    return axios.get(url + '/word/GetWordById/' + id)
    .then(response => response.data)
    .catch(e => e);
  },
  createWord(word) {
    return axios.post(url + '/word/AddWord', word)
    .then(response => response.data)
    .catch(e => e);
  },
  editWord(word) {
    return axios.put(url + '/word/EditWord', word)
    .then(response => response.data)
    .catch(e => e);
  },
  deleteWord(id) {
    return axios.delete(url + '/word/DeleteWord/' + id)
    .then(response => response.data)
    .catch(e => e);
  },
  getUsers(){
    return axios.get(url + '/account/getallusers')
    .then(res => res.data)
    .catch(e => e)
  },
  deleteUser(id){
    return axios.delete(url + '/account/deleteuser/' + id)
    .then(response => response.data)
    .catch(e => e);
  },
  changeRole(user){
    return axios.put(url + '/account/changerole', user)
    .then(response => response.data)
    .catch(e => e);
  },
  changePassword(id, oldpassword, newpassword){
    return axios.put(url + '/account/ChangePassword?id=' + id + '&oldpassword=' + oldpassword + '&newpassword=' + newpassword)
    .then(response => response.data)
    .catch(e => e);
  },
  editUser(user){
    return axios.put(url + '/account/edituser', user)
    .then(response => response.data)
    .catch(e => e);
  }
};