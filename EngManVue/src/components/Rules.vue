<template>
<div>
  <div class="loading" v-if = "inProgress">Loading&#8230;</div>
  <div v-if = "clickRule">
        <rule :rule.sync = 'rule' v-on:close-rule = "clickRule = false"/>
  </div>
  <div class="view-list">
      <div v-if = "!clickRule" class = "icon-close"><router-link to="/grammar"><img src = "../assets/arrow-up.png" title="Back" style = "margin: 5px; width: 20px; height: 20px;"></router-link></div>
      <span style = "font-size: 30px" v-if = "!clickRule">Rules</span><br/>
      <div title = "Open" class = "list--element pointer" v-for = 'el in rules' :key = 'el.id' v-if = "!clickRule" v-on:click = "openRule(el.RuleId); scrollToTop()">
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
        clickRule: false
        , rule: {}
        , timeout: {}
        , rules: []
        , inProgress: false
    }
  },
  components: {
      rule
  },
  methods:{
      openRule(id){
          if(this.inProgress) return;
          this.inProgress = true;
          api.getRule(id)
          .then(result => {
                if(result.response)
                {
                    this.inProgress = false;
                    console.log(result.response.data.Message);
                    return;
                }
                this.rule = result;
                this.clickRule = true;
                this.inProgress = false;
              })
          .catch(e => {
              this.inProgress = false;
              console.log(e);
          });
      },
      scrollToTop(){
            if (document.body.scrollTop!=0 || document.documentElement.scrollTop!=0){
                window.scrollBy(0,-50);
                this.timeOut=setTimeout(this.scrollToTop(),10);
            }
            else 
            {
                clearTimeout(this.timeOut);
            }
      }
  },
  created: function()
  {
      if(this.inProgress) return;
      this.inProgress = true;
      api.getRules()
      .then(result => {
          if(result.response)
          {
            this.inProgress = false;
            console.log(result.response.data.Message);
            return;
          }
          this.rules = result;
          this.inProgress = false;
      })
      .catch(e => {
          this.inProgress = false;
          console.log(e);
      })
  }
}
</script>