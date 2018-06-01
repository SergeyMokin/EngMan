import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
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

  public ngOnInit(): void
  {
    this.activateRoute.queryParams.subscribe(
      (queryParam: any) => {
        if(queryParam['category'] != undefined)
        {
          let doc:any;
          doc = document.getElementById("selectCategoryOfTask");
          doc.options[doc.selectedIndex].text = queryParam['category']; 
        }
      }
    )
    this.apiService.GetSentenceTaskCategories().subscribe(
      obj => this.Categories = obj,
      error => console.log(error));
  }

  private ChooseWord(word:string): void
  {
    document.getElementById(word).className += " disabled"
    this.AnswerTask += word.toUpperCase() + ` `;
  }

  private Start(category:string): void
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

  private Refresh(): void
  {
    for(let i = 0; i < this.SplitedSentence.length; i++)
    {
      document.getElementById(this.SplitedSentence[i]).className = "btn grey";
    }
    this.AnswerTask = "";
    this.ResultMessageOfTask = ``;
  }

  private Check(): void
  {
    if(this.AnswerTask.length != this.Task.Sentence.length + 1)
    {
      this.ResultMessageOfTask = `Make a full sentence.`;
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

  private Next(): void
  {
    if(this.TotalCountOfTask == 10)
    {
      this.EndOfTask();
      return;
    }
    if(this.Task != null)
    {
      this.Refresh();
    }
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

  private EndOfTask(): void
  {
    document.getElementById("next-btn").className += " disabled";
    document.getElementById("refresh-btn").className += " disabled";
    document.getElementById("check-btn").className += " disabled";
    this.ResultMessageOfTask = `You have successfully completed! Correct answers: ${this.CountOfCorrectAnswers}/${this.TotalCountOfTask}`;
    document.getElementById("msg-result-form").className = "green-text";
    document.getElementById("close-btn").className = "btn";
    document.getElementById("end-btn").className = "btn disabled";
  }

  private Close(): void
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
