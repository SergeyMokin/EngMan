import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-word-translate-page',
  templateUrl: './word-translate-page.component.html'
})
export class WordTranslatePageComponent implements OnInit {

  private Categories: string[] = [];
  private ErrorMessage: string = ``;
  private ChoosenCategory: string = ``;

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

  private Start(category: string): void
  {
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

    console.log(this.ChoosenCategory);
  }

}
