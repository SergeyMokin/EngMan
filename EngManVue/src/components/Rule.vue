<template>
<div style = "text-align: center;">
  <br/><br/>
  <button class = "button-close" type = "submit" v-on:click = "$emit('close-rule')">Закрыть</button>
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