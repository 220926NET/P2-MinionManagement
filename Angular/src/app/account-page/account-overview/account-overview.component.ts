import { Component, OnInit } from '@angular/core';
import { InternalAPIService } from '../../API-Service/internal-api.service';

@Component({
  selector: 'app-account-overview',
  templateUrl: './account-overview.component.html',
  styleUrls: ['./account-overview.component.css']
})
export class AccountOverviewComponent implements OnInit {
  
  myData: any;
  // objectKeys: any;
  // jsonObj: any;
  constructor(private api : InternalAPIService) { }

  ngOnInit(): void {
      this.api.getData().subscribe((data) => {
      this.myData = data;
      console.log(this.myData.CheckingAccounts.Item1);
      console.log(this.myData.CheckingAccounts.Item2);
      console.log(this.myData.SavingAccounts.Item1);
      console.log(this.myData.SavingAccounts.Item2);
      console.log(this.myData.CheckingAccounts);
      console.log(this.myData.SavingAccounts);
      // this.objectKeys = Object.keys;
      // this.jsonObj = this.myData.SavingAccounts;
      // console.log(this.objectKeys.keys)
      });

      
  }

}
