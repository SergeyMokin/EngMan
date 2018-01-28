// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import store from './store/store'
import Routes from './routes'
import VueRouter from 'vue-router'
import VueCookie from 'vue-cookie'
import Vuelidate from 'vuelidate'

Vue.use(VueRouter);
Vue.use(VueCookie);
Vue.use(Vuelidate);

const router = new VueRouter({
  mode: 'history',
  routes: Routes
});

new Vue({
  el: '#app',
  render: h => h(App),
  router: router,
  store
})
