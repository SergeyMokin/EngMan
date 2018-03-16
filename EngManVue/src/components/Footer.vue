<template>
  <div class="footer">

      <!-- This is links to telegram and vk groups -->
      <!--<a href = "https://telegram.org/" target="_blank"><img src = "../assets/telegram.png" style = "width: 30px; height: 30px; margin: 5px;"></a>
      <a href = "https://vk.com/" target="_blank"><img src = "../assets/vkgrey.png" style = "width: 30px; height: 30px; margin: 5px;"></a>-->

      <a>
        <a href = "https://lingualeo.com/" target="_blank">lingualeo.com</a> - from this source was taken material
      </a>
      <span 
        v-if = "$store.state.user.Role" 
        style = "position: fixed; bottom: 30px; right: 1%; cursor: pointer" 
        v-on:click = "show = true">
          <img 
            v-bind:class = "{'new-mes-in': $store.state.newmess}" 
            title="Messages" 
            style = "width: 50px; height: auto" 
            type = "img" 
            src = "../assets/mes-icon.png">
      </span>
      <messages v-if = "show" v-on:closeMessages = "closeMessageForm()"/>

  </div>
</template>

<script>
import messages from './Messages'

export default {
  name: 'Footer',
  data () {
    return {
      show: false
    }
  },
  components: {
    messages
  },
  methods: {
    closeMessageForm(){
      this.show = false;
    }
  }
}
</script>
