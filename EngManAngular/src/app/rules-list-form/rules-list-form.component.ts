import { Component, OnInit } from '@angular/core';
import { RuleModel } from '../app.models';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-rules-list-form',
  templateUrl: './rules-list-form.component.html'
})
export class RulesListFormComponent implements OnInit {

  private Rules: RuleModel[]

  private OpenedRule: RuleModel;

  constructor(private apiService: ApiService, private router: Router) { }

  public ngOnInit(): void
  {
    this.OpenedRule = null;
    this.apiService.GetRules().subscribe(obj => 
      {
        this.Rules = obj;
        for(let i = 0; i < this.Rules.length; i++)
        {
          this.Rules[i].Text = this.Rules[i].Text.replace(/(?:\r\n|\r|\n)/g, '<br>');
        }
      },
      error => 
      {
        console.log(error);
        this.Rules = [];
      }
      )
  }

  private OpenModalDialog(rule:RuleModel): void
  {
    this.OpenedRule = rule;
  }

  private CloseModalDialog(): void
  {
    document.getElementById("ModalDialog").scrollTop = 0;
    this.OpenedRule = null;
  }

  private GoPractice(rule: RuleModel): void
  {
    this.router.navigate(["/practice-rules"], {queryParams: {'category': rule.Category}});
  }

}
