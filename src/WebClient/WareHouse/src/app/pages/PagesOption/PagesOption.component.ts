import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/extension/Authentication.service';

@Component({
  selector: 'app-PagesOption',
  templateUrl: './PagesOption.component.html',
  styleUrls: ['./PagesOption.component.scss']
})
export class PagesOptionComponent implements OnInit {
userName: string |undefined;
  constructor(        private authenticationService: AuthenticationService
    ) { }

  ngOnInit() {
    this.userName=this.authenticationService.userValue.username;
  }

}
