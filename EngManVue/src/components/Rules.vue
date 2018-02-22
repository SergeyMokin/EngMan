<template>
<div>
  <div v-if = "clickRule">
        <rule :rule.sync = 'rule' v-on:close-rule = "clickRule = false"/>
  </div>
  <div class="view-list">
      <div v-if = "!clickRule" class = "icon-close"><router-link to="/grammar"><img src = "../assets/arrow-up.png" title="Назад" style = "margin: 5px; width: 20px; height: 20px;"></router-link></div>
      <span style = "font-size: 30px" v-if = "!clickRule">Правила</span><br/>
      <select v-model = "category" v-on:change = "downloadRules()">
          <option v-for = "category in categories" :key="category">
              {{category}}
          </option>
      </select>
      <div class = "list--element pointer" v-for = 'el in rules' :key = 'el.id' v-if = "!clickRule" v-on:click = "openRule(el.RuleId)">
          <span class = "span--element">{{el.Title}}</span> 
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
        inProgress: false,
        category: '',
        categories: [],
        clickRule: false
        , rule: {}
    }
  },
  components: {
      rule
  },
  methods:{
      downloadRules(){
        if(this.inProgress) return;
        this.inProgress = true;
        api.getRulesByCategory(this.category)
            .then(res => 
            {
                this.$store.state.rules = res;
                this.inProgress = false;
            })
            .catch(e => 
            {
                if(e.message)
                {
                    allert(e.message);
                }
                this.inProgress = false;
            })
      },
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
      if(this.category)
      {
        return this.$store.getters.rules;
      }
    }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      api.getAllCategoriesRules()
      .then(res => 
      {
          this.categories = res;
          this.inProgress = false;
      })
      .catch(e => 
      {
          if(e.message)
          {
              allert(e.message);
          }
          this.inProgress = false;
      })
  }
}
</script>