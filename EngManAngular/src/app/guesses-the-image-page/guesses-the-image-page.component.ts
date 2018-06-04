import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { WordTaskModel, WordModel, GuessesTheImageTaskModel } from '../app.models';

@Component({
  selector: 'app-guesses-the-image-page',
  templateUrl: './guesses-the-image-page.component.html'
})
export class GuessesTheImagePageComponent implements OnInit {

  private AnswerOriginal: string = ``;
  private ShowTranslate: boolean = false;

  private Categories: string[] = [];
  private ErrorMessage: string = ``;

  private Indexes: string = `_`;
  private Task: GuessesTheImageTaskModel = null;


  private ResultMessageOfTask: string;
  private CountOfCorrectAnswers: number = 0;
  private CountOftrying: number = 0;
  private TotalCountOfTask: number = 0;

  private ChoosenCategory: string = ``;

  constructor(private apiService: ApiService) 
  {
    
  }

  public ngOnInit(): void
  {
    this.apiService
    .GetGuessesTheImageTaskCategories()
    .subscribe(
      obj => this.Categories = obj,
      error => console.log(error)
    )
  }

  public UpdateCategory()
  {
    let doc:any;
    doc = document.getElementById("selectCategoryOfTask");
    this.ChoosenCategory = doc.options[doc.selectedIndex].text;
  }

  private Start(): void
  {
    this.ErrorMessage = ``;
    this.CountOftrying = 0;
    this.Indexes = `_`;
    this.ResultMessageOfTask = ``;
    this.CountOfCorrectAnswers = 0;
    this.TotalCountOfTask = 0;
    this.AnswerOriginal = ``;
    this.ShowTranslate = false;
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

  private Check(): void
  {
    if(this.AnswerOriginal.length == 0)
    {
      this.ResultMessageOfTask = `Write answer.`;
      document.getElementById("msg-result-form").className = "red-text";
      return;
    }
    this.Task.Word.Original = this.AnswerOriginal;
    this.apiService.CheckTheAnswerOfGuessesTheImageTask(this.Task).subscribe(
      obj => 
      {
        if(obj === true)
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
    this.AnswerOriginal = ``;
    this.ShowTranslate = false;
    document.getElementById("check-btn").className = "btn";
    this.apiService.GetGuessesTheImageTask(this.ChoosenCategory, this.Indexes).subscribe(
      obj => 
      {
        this.Task = obj;
        this.Indexes += this.Task.Id + ",";
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
    document.getElementById("msg-result-form").className = "green-text";
    document.getElementById("close-btn").className = "btn";
    document.getElementById("end-btn").className = "btn disabled";
    this.ResultMessageOfTask = `You have successfully completed! Correct answers: ${this.CountOfCorrectAnswers}/${this.TotalCountOfTask}`;
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
    this.ResultMessageOfTask = ``;
    this.CountOfCorrectAnswers = 0;
    this.TotalCountOfTask = 0;
    this.AnswerOriginal = ``;
    this.ShowTranslate = false;
  }

}
