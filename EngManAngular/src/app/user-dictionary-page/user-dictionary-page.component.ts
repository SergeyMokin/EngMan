import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { UserDictionaryModel, UserViewModel, WordModel } from '../app.models';
import { SearchUserDictionaryPipe } from './user-dictionary-page.pipe';

@Component({
  selector: 'app-user-dictionary-page',
  templateUrl: './user-dictionary-page.component.html'
})
export class UserDictionaryPageComponent implements OnInit {

  private ErrorMessage: string = ``;
  private ChoosenCategory: string = ``;
  private UserDictionary: UserDictionaryModel = new UserDictionaryModel();
  private SearchString: string = ``;
  private Categories: string[] = [];

  constructor(private apiService: ApiService) 
  {
  }

  public ngOnInit(): void
  {
    this.apiService
    .GetUserDictionaryCategories()
    .subscribe(
      obj => 
      {
        this.Categories = obj;
        this.ChoosenCategory = ``;
      },
      error => console.log(error)
    )
  }

  private ChangeSelector(): void
  {
    let doc:any;
    doc = document.getElementById("selectCategoryOfDictionary");
    this.ChoosenCategory = doc.options[doc.selectedIndex].text
    this.apiService
    .GetUserDictionaryByCategory(this.ChoosenCategory)
    .subscribe(
      obj => this.UserDictionary = obj,
      error => console.log(error)
    )
  }

  private DeleteWord(word: WordModel): void
  {
    this.apiService
    .DeleteUserDictionaryWord(word.WordId)
    .subscribe(
      obj =>
      {
        if(obj === `Delete completed successful`)
        {
          let index = this.UserDictionary.Words.indexOf(word);
          if(index > -1)
          {
            this.UserDictionary.Words.splice(index, 1);
          }
        }
      },
      error => console.log(error)
    )
  }

}
