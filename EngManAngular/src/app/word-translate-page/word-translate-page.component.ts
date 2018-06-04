import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { WordTaskModel, WordModel } from '../app.models';
import * as M from 'materialize-css';

@Component({
  selector: 'app-word-translate-page',
  templateUrl: './word-translate-page.component.html'
})
export class WordTranslatePageComponent implements OnInit {

  private Categories: string[] = [];
  private ErrorMessage: string = ``;

  private Indexes: string = `_`;
  private Task: WordTaskModel = null;


  private ResultMessageOfTask: string;
  private AnswerTask: string = ``;
  private CountOfCorrectAnswers: number = 0;
  private CountOftrying: number = 0;
  private TotalCountOfTask: number = 0;

  private ChoosenCategory: string = ``;

  private WordsByCategory: WordModel[] = [];

  constructor(private apiService: ApiService) 
  {
    
  }

  public ngOnInit(): void
  {
    this.apiService
    .GetWordTaskCategories()
    .subscribe(
      obj => this.Categories = obj,
      error => console.log(error)
    )
  }

  public AddToDictionary(word: WordModel): void
  {
    this.apiService
    .AddWordToDictionaryOfCurrentUser(word.WordId)
    .subscribe(
      obj => M.toast({html: obj ? "Successfully." : "Already added.", classes: 'rounded'}),
      error => console.log(error)
    )
  }

  public UpdateCategory()
  {
    let doc:any;
    doc = document.getElementById("selectCategoryOfTask");
    this.ChoosenCategory = doc.options[doc.selectedIndex].text;
    this.apiService
    .GetWordsByCategory(this.ChoosenCategory)
    .subscribe(
      obj => this.WordsByCategory = obj,
      error => console.log(error)
    );
  }

  private ChooseWord(word:string): void
  {
    this.Refresh();
    document.getElementById(word).className += " disabled"
    this.AnswerTask = word;
  }

  private Start(): void
  {
    this.ErrorMessage = ``;
    this.CountOftrying = 0;
    this.Indexes = `_`;
    this.AnswerTask = ``;
    this.ResultMessageOfTask = ``;
    this.CountOfCorrectAnswers = 0;
    this.TotalCountOfTask = 0;
    document.getElementById("next-btn").className = "btn";
    document.getElementById("check-btn").className = "btn";
    this.ErrorMessage = ``;
    if(this.ChoosenCategory === ``)
    {
      this.ErrorMessage = `Choose the category`;
      return;
    }

    this.Next();
  }

  private Refresh(): void
  {
    for(let i = 0; i < this.Task.Answers.length; i++)
    {
      document.getElementById(this.Task.Answers[i]).className = "btn grey";
    }
    this.AnswerTask = "";
    this.ResultMessageOfTask = ``;
  }

  private Check(): void
  {
    if(this.AnswerTask.length == 0)
    {
      this.ResultMessageOfTask = `Choose word.`;
      document.getElementById("msg-result-form").className = "red-text";
      return;
    }
    let resAnsw = new WordModel();
    resAnsw.Original = this.Task.Word;
    resAnsw.Category = this.Task.Category;
    resAnsw.WordId = this.Task.WordId;
    resAnsw.Translate = this.AnswerTask;
    this.apiService.CheckTheAnswerOfWordTask(resAnsw, true).subscribe(
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
    this.apiService.GetWordTask(this.ChoosenCategory, this.Indexes, true).subscribe(
      obj => 
      {
        this.Task = obj;
        this.Indexes += this.Task.WordId + ",";
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
    document.getElementById("close-btn").className = "btn disabled";
    document.getElementById("end-btn").className = "btn";
    this.Task = null;
    this.ErrorMessage = ``;
    this.CountOftrying = 0;
    this.Indexes = `_`;
    this.AnswerTask = ``;
    this.ResultMessageOfTask = ``;
    this.CountOfCorrectAnswers = 0;
    this.TotalCountOfTask = 0;
  }
  
}
