<template>
  <div class="footer">
      <!--<a href = "https://telegram.org/" target="_blank"><img src = "../assets/telegram.png" style = "width: 30px; height: 30px; margin: 5px;"></a>
      <a href = "https://vk.com/" target="_blank"><img src = "../assets/vkgrey.png" style = "width: 30px; height: 30px; margin: 5px;"></a>-->
      <a><a href = "https://lingualeo.com/" target="_blank">lingualeo.com</a> - с данного источника брался материал</a>
      <span style = "position: fixed; bottom: 0.5%; right: 0.5%; cursor: pointer" v-on:click = "show = !show">messages</span>
      <messages v-show = "show" v-on:closeMessages = "closeMessageForm()"/>
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

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.footer img:hover{
    cursor: pointer;
}
.footer{
    background-color: rgba(205, 205, 205, 0.9);
    position: fixed;
    bottom: 0;
    width: 100%;
    text-align: center;
}
</style>
