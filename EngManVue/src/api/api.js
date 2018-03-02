import axios from 'axios';

const url = 'http://localhost:58099/api'

export default {
  addToken(token){
    axios.defaults.headers.common['Authorization'] ='bearer ' + token;
  },
  deleteToken(){
    delete axios.defaults.headers.common['Authorization'];
  },
  login(model){
    return axios.post(url + '/account/login', model)
    .then(response => {
      return response.data;
    })
    .catch(e => e)
  },
  registration(model){
    return axios.post(url + '/account/registration', model)
    .then(res => res.data)
    .catch(e => e)
  },
  signout(){
    var vue = this;
    return axios.post(url + '/account/logout')
    .then(response => {
      return response.data;
    })
    .catch(e => e);
  },
  getUserData(){
    return axios.get(url + '/account/getuserdata')
    .then(res => res.data)
    .catch(e => e)
  },
  getUserDictionary(){
    return axios.get(url + '/account/getuserdictionary')
    .then(res => res.data)
    .catch(e => e)
  },
  deleteUserWord(id){
    return axios.delete(url + '/account/deletewordfromdictionary/' + id)
    .then(res => res.data)
    .catch(e => e)
  },
  addUserWord(word){
    return axios.post(url + '/account/addwordtodictionary/', word)
    .then(res => res.data)
    .catch(e => e)
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
  },
  getAllCategoriesOfUserDictionary(){
    return axios.get(url + '/account/GetAllCategoriesOfDictionary')
    .then(res => res.data)
    .catch(e => e)
  },
  getUserDictionaryByCategory(category) {
    return axios.get(url + '/account/GetByCategoryDictionary?category=' + category)
    .then(response => response.data)
    .catch(e => e);
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
  getAllCategoriesRules() {
    return axios.get(url + '/rule/GetAllCategories')
    .then(response => response.data)
    .catch(e => e);
  },
  getRulesByCategory(category) {
    return axios.get(url + '/rule/GetByCategory?category=' + category)
    .then(response => response.data)
    .catch(e => e);
  },
  addImages(images){
    return axios.post(url + '/rule/addimages', images)
    .then(response => response.data)
    .catch(e => e)
  },
  getSentenceTask(category, indexes){
    return axios.get(url + '/sentencetask/gettask?category=' + category + '&indexes=' + indexes)
    .then(response => response.data)
    .catch(e => e);
  },
  verificationSentenceTask(sentence){
    return axios.post(url + '/sentencetask/verificationcorrectness', sentence)
  },
  getWordMap(category, indexes, translate){
    return axios.get(url + '/word/getwordmap?category=' + category + '&indexes=' + indexes + '&translate=' + translate)
    .then(response => response.data)
    .catch(e => e);
  },
  verificationWordMap(wordmap, translate){
    return axios.post(url + '/word/VerificationCorrectness?translate=' + translate, wordmap)
  },
  getAllCategoriesSentences(){
    return axios.get(url + '/sentencetask/GetAllCategories')
    .then(res => res.data)
    .catch(e => e)
  },
  getSentences() {
    return axios.get(url + '/sentencetask/GetAllTasks')
    .then(response => response.data)
    .catch(e => e);
  },
  getSentence(id) {
    return axios.get(url + '/sentencetask/GetTaskById/' + id)
    .then(response => response.data)
    .catch(e => e);
  },
  createSentence(sentence) {
    return axios.post(url + '/sentencetask/AddTask', sentence)
    .then(response => response.data)
    .catch(e => e);
  },
  editSentence(sentence) {
    return axios.put(url + '/sentencetask/EditTask', sentence)
    .then(response => response.data)
    .catch(e => e);
  },
  deleteSentence(id) {
    return axios.delete(url + '/sentencetask/DeleteTask/' + id)
    .then(response => response.data)
    .catch(e => e);
  },
  getSentencesByCategory(category) {
    return axios.get(url + '/sentencetask/GetByCategory?category=' + category)
    .then(response => response.data)
    .catch(e => e);
  },
  getAllCategoriesWords(){
    return axios.get(url + '/word/GetAllCategories')
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
  getWordsByCategory(category) {
    return axios.get(url + '/word/GetByCategory?category=' + category)
    .then(response => response.data)
    .catch(e => e);
  },
  ReadMessages(messages){
    return axios.post(url + '/message/readmessages/', messages)
    .then(response => response.data)
    .catch(e => e)
  },
  getMessages(){
    return axios.get(url + '/message/getallmessages/')
    .then(response => response.data)
    .catch(e => e);
  },
  deleteMessage(id){
    return axios.delete(url + '/message/deletemessage/' + id)
    .then(response => response.data)
    .catch(e => e);
  },
  sendMessage(mes){
    return axios.post(url + '/message/SendMessage', mes)
    .then(response => response.data)
    .catch(e => e);
  },
  getGuessesTheImage(category, indexes){
    return axios.get(url + '/guessestheimage/gettask?category=' + category + "&indexes=" + indexes)
    .then(response => response.data)
    .catch(e => e);
  },
  verificationGuessesTheImage(task){
    return axios.post(url + '/guessestheimage/verificationcorrectness/', task)
    .then(response => response.data)
    .catch(e => e)
  },
  getGuessesTheImages(){
    return axios.get(url + '/guessestheimage/getalltasks/')
    .then(response => response.data)
    .catch(e => e)
  },
  getGuessesTheImageById(id){
    return axios.get(url + '/guessestheimage/gettaskbyid/' + id)
    .then(response => response.data)
    .catch(e => e)
  },
  editGuessesTheImage(task){
    return axios.put(url + '/guessestheimage/edittask/', task)
    .then(response => response.data)
    .catch(e => e)
  },
  addGuessesTheImage(task){
    return axios.post(url + '/guessestheimage/addtask/', task)
    .then(response => response.data)
    .catch(e => e)
  },
  deleteGuessesTheImage(id){
    return axios.delete(url + '/guessestheimage/deletetask/' + id)
    .then(response => response.data)
    .catch(e => e)
  },
  getAllCategoriesGuessesTheImages(){
    return axios.get(url + '/guessestheimage/GetAllCategories')
    .then(res => res.data)
    .catch(e => e)
  },
  getGuessesTheImagesByCategory(category) {
    return axios.get(url + '/guessestheimage/GetByCategory?category=' + category)
    .then(response => response.data)
    .catch(e => e);
  }
};