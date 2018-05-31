import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import * as M from 'materialize-css';
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

  constructor(private apiService: ApiService) 
  {
  }

  ngOnInit(): void
  {
    this.apiService
    .GetUserDictionaryCategories()
    .subscribe(
      obj => 
      {
        for(var i = 0; i < obj.length; i++)
        {
          var option = document.createElement("option");
          option.text = obj[i];
          document.getElementById("selectCategoryOfDictionary").appendChild(option);
        }
        M.FormSelect.init(document.querySelectorAll('select'), null);
        this.ChoosenCategory = ``;
      },
      error => console.log(error)
    )
  }

  ChangeSelector(): void
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

  DeleteWord(word: WordModel): void
  {
    this.apiService
    .DeleteUserDictionaryWord(word.WordId)
    .subscribe(
      obj =>
      {
        if(obj)
        {
          console.log(obj)
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
