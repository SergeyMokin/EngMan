<template>
<div>
  <div v-if = "clickRule">
        <rule :rule.sync = 'rule' v-on:close-rule = "clickRule = false"/>
  </div>
  <div class="rules-view-list">
      <span style = "font-size: 30px" v-if = "!clickRule">Правила</span>
      <div class = "rules-list--element" v-for = 'el in rules' :key = 'el.id' v-if = "!clickRule" v-on:click = "openRule(el.RuleId)">
          <span class = "span-rule--element">{{el.Title}}</span> 
      </div>
  </div>
</div>
</template>

<script>
import api from '../api/api'
import rule from './Rule';
export default {
  name: 'rules-view',
  data () {
    return {
        clickRule: false
        , rule: {}
    }
  },
  components: {
      rule
  },
  methods:{
      openRule(id){
          api.getRule(id)
          .then(result => {
                this.rule = result;
                this.clickRule = true;
              })
          .catch(e => console.log(e));
      }
  },
  computed: 
  {
    rules()
    {
      return this.$store.getters.rules;
    }
  },
  created: function()
  {
      this.$store.dispatch('getRules');
  }
}
</script>