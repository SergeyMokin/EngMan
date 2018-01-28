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

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.rules-view-list{
    margin: 5% 25% 5% 25%;
    width: 50%;
    text-align: center;
    display: table;
}
.rules-list--element{
    margin: 10px;
    padding: 3px;
    text-align: left;
    cursor: pointer;
    background: rgb(248, 248, 248);
    height: 35px;
    border: none;
    outline:none;
    border-radius: 10px;
}
.span-rule--element{
    margin: 15px;
    font-size: 18px;
}
</style>