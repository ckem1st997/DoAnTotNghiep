import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-NotFound',
  templateUrl: './NotFound.component.html',
  styleUrls: ['./NotFound.component.scss']
})
export class NotFoundComponent implements OnInit {

  constructor(private r: Router) { }

  ngOnInit() {
  }
  toHome() {
    this.r.navigate(['/wh'])
  }
}
