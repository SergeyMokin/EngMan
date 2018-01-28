<template>
<div style = "text-align: center;">
  <br/><br/>
  <button type = "submit" v-on:click = "$emit('close-rule')">Закрыть</button>
  <span class = "span-view">{{rule.Title}}</span>
  <div class="rule-view">
        <p  v-html = "rule.Text" id = "Text" style="white-space: pre-line; text-align: left">
            {{rule.Text}}
        </p>
  </div>
  </div>
</template>

<script>
import api from '../api/api'

export default {
  props: ['rule'],
  name: 'rules-view',
  data () {
    return {

    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.rule-view{
    text-align: left;
    width: 60%;
    margin: 2% 20% 0% 20%;
    padding: 0.5%;
    background: rgb(248, 248, 248);
    border: none;
    outline:none;
    border-radius: 10px;
}
.span-view{
    font-size: 30px;
}
button{
    position: absolute;
    right: 18.65%;
    top: 75px;
}
</style>