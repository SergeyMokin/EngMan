import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import * as M from 'materialize-css';
import { SentenceTaskModel } from '../app.models';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-practice-rules-page',
  templateUrl: './practice-rules-page.component.html'
})
export class PracticeRulesPageComponent implements OnInit {

  private Categories: string[];
  private ErrorMessage: string;

  private Indexes: string = `_`;
  private Task: SentenceTaskModel;
  private SplitedSentence: string[] = [];

  private ResultMessageOfTask: string;
  private AnswerTask: string = ``;
  private CountOfCorrectAnswers: number = 0;
  private CountOftrying: number = 0;
  private TotalCountOfTask: number = 0;

  private ChoosenCategory: string = "";

  constructor(private apiService: ApiService, private activateRoute: ActivatedRoute) 
  { 
    this.Categories = [];
  }

  ngOnInit(): void
  {
    this.apiService.GetSentenceTaskCategories().subscribe(
      obj => 
      {
        for(var i = 0; i < obj.length; i++)
        {
          var option = document.createElement("option");
          option.text = obj[i];
          document.getElementById("selectCategoryOfTask").appendChild(option);
        }
        M.FormSelect.init(document.querySelectorAll('select'), null);        
        this.activateRoute.queryParams.subscribe(
          (queryParam: any) => {
            if(queryParam['category'] != undefined)
            {
              let doc:any;
              doc = document.getElementById("selectCategoryOfTask");
              doc.options[doc.selectedIndex].text = queryParam['category']; 
              M.FormSelect.init(document.querySelectorAll('select'), null);   
            }
          }
        )
      },
      error => console.log(error));
  }

  ChooseWord(word:string): void
  {
    document.getElementById(word).className += " disabled"
    this.AnswerTask += word.toUpperCase() + ` `;
  }

  Start(category:string): void
  {
    this.ErrorMessage = ``;
    this.CountOftrying = 0;
    this.Indexes = `_`;
    this.AnswerTask = ``;
    this.SplitedSentence = [];
    this.ResultMessageOfTask = ``;
    this.CountOfCorrectAnswers = 0;
    this.TotalCountOfTask = 0;
    document.getElementById("next-btn").className = "btn";
    document.getElementById("refresh-btn").className = "btn";
    document.getElementById("check-btn").className = "btn";
    this.ErrorMessage = ``;
    if(category == null)
    {
      let doc:any;
      doc = document.getElementById("selectCategoryOfTask");
      category = doc.options[doc.selectedIndex].text;
    }
    if(category === `Choose your option`)
    {
      this.ErrorMessage = `Choose the category`;
      return;
    }

    this.ChoosenCategory = category;

    this.Next();
  }

  Refresh(): void
  {
    for(let i = 0; i < this.SplitedSentence.length; i++)
    {
      document.getElementById(this.SplitedSentence[i]).className = "btn grey";
    }
    this.AnswerTask = "";
    this.ResultMessageOfTask = ``;
  }

  Check(): void
  {
    if(this.AnswerTask.length != this.Task.Sentence.length + 1)
    {
      this.ResultMessageOfTask = `Make a fuul sentence.`;
      document.getElementById("msg-result-form").className = "red-text";
      return;
    }
    let resAnsw = new SentenceTaskModel();
    resAnsw.Sentence = this.AnswerTask;
    resAnsw.Category = this.Task.Category;
    resAnsw.SentenceTaskId = this.Task.SentenceTaskId;
    resAnsw.Translate = this.Task.Translate;
    this.apiService.CheckTheAnswerOfSentenceTask(resAnsw).subscribe(
      obj => 
      {
        if(obj ===true)
        {
          this.ResultMessageOfTask = `Correct answer.`;
          document.getElementById("msg-result-form").className = "green-text";
          if(this.CountOftrying == 0)
          {
            this.CountOfCorrectAnswers++;
          }
          this.CountOftrying = 0;
          document.getElementById("check-btn").className = "btn disabled";
          document.getElementById("refresh-btn").className = "btn disabled";
          if(this.TotalCountOfTask == 10)
          {
            this.EndOfTask();
            return;
          }
        }
        else
        {
          this.CountOftrying++;
          this.ResultMessageOfTask = `Incorrect answer.`;
          document.getElementById("msg-result-form").className = "red-text";
        }
      },
      error => console.log(error)
    )
    this.ResultMessageOfTask = "";
  }

  Next(): void
  {
    if(this.TotalCountOfTask == 10)
    {
      this.EndOfTask();
      return;
    }
    this.Refresh();
    document.getElementById("check-btn").className = "btn";
    document.getElementById("refresh-btn").className = "btn";
    this.apiService.GetSenteceTask(this.ChoosenCategory, this.Indexes).subscribe(
      obj => 
      {
        this.Task = obj;
        this.SplitedSentence = this.Task.Sentence.split(" ");
        this.Indexes += this.Task.SentenceTaskId + ",";
        this.TotalCountOfTask++;
        this.CountOftrying = 0;
      },
      error => 
      {
        this.ErrorMessage = error.message;
        this.ResultMessageOfTask = error.message;
        document.getElementById("msg-result-form").className = "red-text";
      }
    );
  }

  EndOfTask(): void
  {
    document.getElementById("next-btn").className += " disabled";
    document.getElementById("refresh-btn").className += " disabled";
    document.getElementById("check-btn").className += " disabled";
    this.ResultMessageOfTask = `You have successfully completed! Correct answers: ${this.CountOfCorrectAnswers}/${this.TotalCountOfTask}`;
    document.getElementById("msg-result-form").className = "green-text";
    document.getElementById("close-btn").className = "btn";
    document.getElementById("end-btn").className = "btn disabled";
  }

  Close(): void
  {
    document.getElementById("check-btn").className = "btn";
    document.getElementById("next-btn").className = "btn";
    document.getElementById("refresh-btn").className = "btn";
    document.getElementById("close-btn").className = "btn disabled";
    document.getElementById("end-btn").className = "btn";
    this.Task = null;
    this.ErrorMessage = ``;
    this.CountOftrying = 0;
    this.Indexes = `_`;
    this.AnswerTask = ``;
    this.SplitedSentence = [];
    this.ResultMessageOfTask = ``;
    this.CountOfCorrectAnswers = 0;
    this.TotalCountOfTask = 0;
  }

}
