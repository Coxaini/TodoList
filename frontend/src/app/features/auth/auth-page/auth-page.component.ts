import {Component} from '@angular/core';
import {faTableList} from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-auth-page',
  templateUrl: './auth-page.component.html',
  styleUrls: ['./auth-page.component.scss']
})
export class AuthPageComponent {

  protected readonly faTableList = faTableList;
}

