import { Component, OnInit } from '@angular/core';
import { InternalAPIService } from '../API-Service/internal-api.service';

@Component({
  selector: 'app-account-page',
  templateUrl: './account-page.component.html',
  styleUrls: ['./account-page.component.css']
})
export class AccountPageComponent implements OnInit {
  myData: any;
  constructor(private api : InternalAPIService) { }

  ngOnInit(): void {
    this.api.getData().subscribe((data) => {
      this.myData = data;
      });
  }

}
