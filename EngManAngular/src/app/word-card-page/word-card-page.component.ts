import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { WordTaskModel, WordModel } from '../app.models';
import * as M from 'materialize-css';

@Component({
  selector: 'app-word-card-page',
  templateUrl: './word-card-page.component.html'
})
export class WordCardPageComponent implements OnInit {

  private Categories: string[] = [];
  private ErrorMessage: string = ``;

  private ResultMessageOfTask: string;
  private AnswerTask: string = ``;
  private CountOfIncorrectAnswers: number = 0;
  private TotalCountOfTask: number = 0;

  private ChoosenCategory: string = ``;

  private WordsByCategory: WordModel[] = [];

  private ShowPopup: boolean = false;
  private ShowTranslate: boolean = false;

  constructor(private apiService: ApiService) 
  {
    
  }

  public ngOnInit(): void
  {
    this.apiService
    .GetWordCategories()
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
      obj => 
      {
        if(obj)
        {
          this.CountOfIncorrectAnswers++;
        }
      },
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

  private Start(): void
  {
    this.ErrorMessage = ``;
    this.CountOfIncorrectAnswers = 0;
    this.AnswerTask = ``;
    this.ResultMessageOfTask = ``;
    this.TotalCountOfTask = 0;
    document.getElementById("next-btn").className = "btn disabled";
    document.getElementById("dknw-btn").className = "btn";
    document.getElementById("knw-btn").className = "btn";
    this.ErrorMessage = ``;
    if(this.ChoosenCategory === ``)
    {
      this.ErrorMessage = `Choose the category`;
      return;
    }
    this.ShowPopup = true;
  }
  
  private DKnow(): void
  {
    if(this.TotalCountOfTask >= this.WordsByCategory.length)
    {
      this.EndOfTask();
      return;
    }

    this.ShowTranslate = true;
    this.AddToDictionary(this.WordsByCategory[this.TotalCountOfTask]);
    document.getElementById("next-btn").className = "btn";
    document.getElementById("dknw-btn").className = "btn disabled";
    document.getElementById("knw-btn").className = "btn disabled";
  }

  private Know(): void
  {
    if(this.TotalCountOfTask >= this.WordsByCategory.length)
    {
      this.EndOfTask();
      return;
    }

    this.ShowTranslate = true;
    document.getElementById("next-btn").className = "btn";
    document.getElementById("dknw-btn").className = "btn disabled";
    document.getElementById("knw-btn").className = "btn disabled";
  }

  private Next(): void
  {
    this.ShowTranslate = false;
    this.TotalCountOfTask++;
    document.getElementById("next-btn").className = "btn disabled";
    document.getElementById("dknw-btn").className = "btn";
    document.getElementById("knw-btn").className = "btn";
  }

  private EndOfTask(): void
  {
    document.getElementById("next-btn").className += " disabled";
    document.getElementById("knw-btn").className += " disabled";
    document.getElementById("dknw-btn").className += " disabled";
    document.getElementById("end-btn").className += " disabled";
    document.getElementById("msg-result-form").className = "green-text";
    document.getElementById("close-btn").className = "btn";
    this.ResultMessageOfTask = `Added: ${this.CountOfIncorrectAnswers} words to your dictionary.`;
  }

  private Close(): void
  {
    document.getElementById("knw-btn").className = "btn";
    document.getElementById("next-btn").className = "btn disabled";
    document.getElementById("close-btn").className = "btn disabled";
    document.getElementById("dknw-btn").className = "btn";
    document.getElementById("end-btn").className = "btn";
    this.ErrorMessage = ``;
    this.AnswerTask = ``;
    this.ResultMessageOfTask = ``;
    this.CountOfIncorrectAnswers = 0;
    this.TotalCountOfTask = 0;
    this.ShowPopup = false;
    this.ShowTranslate = false;
  }

}
