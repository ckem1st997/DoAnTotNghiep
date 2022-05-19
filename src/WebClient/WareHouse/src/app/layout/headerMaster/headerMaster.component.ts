import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/extension/Authentication.service';

@Component({
  selector: 'app-headerMaster',
  templateUrl: './headerMaster.component.html',
  styleUrls: ['./headerMaster.component.scss']
})
export class HeaderMasterComponent implements OnInit {

  constructor(private service:AuthenticationService) { }

  ngOnInit() {
  }

  /// active to click
  changActive(e: any): number {

    var activeMenu=e.target.parentElement.getAttribute("data-active");
    if(activeMenu !=null && activeMenu !=undefined && activeMenu.length >0 )
    {
      var elementA = DeleteActiveAll();
      document.getElementById(activeMenu)?.classList.add("active");
      return 1;
    }
    var check = e.target.className;
    if (check.includes("mat-icon")) {
      check=e.target.parentElement.className;
      if (check !== undefined && !check.includes("active")) {
        var elementA = DeleteActiveAll();
        e.target.parentElement.classList.add("active");
      }
    }
    else
      if (check !== undefined && !check.includes("active")) {
        var elementA = DeleteActiveAll();
        e.target.classList.add("active");
      }
      return 1;

    function DeleteActiveAll() {
      var elementA = document.getElementsByClassName("header-a");
      for (let index = 0; index < elementA.length; index++) {
        const element = elementA[index];
        element.classList.remove("active");
      }
      return elementA;
    }
  }
  logout(){
    this.service.logout();
  }
}
