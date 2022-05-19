import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { AuthenticationService } from 'src/app/extension/Authentication.service';
import { NotifierService } from 'angular-notifier';
@Component({
  selector: 'app-PagesForbie',
  templateUrl: './PagesForbie.component.html',
  styleUrls: ['./PagesForbie.component.scss']
})
export class PagesForbieComponent implements OnInit {

  constructor(private _location: Location, private router: Router,        private service: AuthenticationService,
    private notife: NotifierService

  ) { }

  ngOnInit() {
  }
  back() {
    this.router.navigate(['/page']);
  }
  logout() {
    this.service.logout();
    this.notife.notify('success', 'Đăng xuất thành công !');
  }
}
