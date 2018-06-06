import { Component, OnInit } from '@angular/core';
import * as M from 'materialize-css';

@Component({
  selector: 'app-messages-component',
  templateUrl: './messages-component.component.html'
})
export class MessagesComponentComponent implements OnInit {

  constructor()
  { }

  public ngOnInit(): void
  {
    document.addEventListener('DOMContentLoaded', function() {
      var elems = document.querySelectorAll('.modal');
      var instances = M.Modal.init(elems, {});
    });
  }

  private UpdateUser(): void
  {

  }

  private OpenMessages(): void
  {
    document.getElementById("message-icon").className = "medium material-icons";
  }

}
