import { Component, OnInit } from '@angular/core';
import { InternalAPIService } from '../API-Service/internal-api.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  myData: any = {"Username":"username","ProfilePic":"../../assets/minion-master-placeholder-bk.png","FirstName":"Name","LastName":"Loading","TroopCount":10,"CheckingAccounts":{"Item1":123,"Item2":123.0000},"SavingAccounts":{"Item1":321,"Item2":321.0000}};
  constructor(private api : InternalAPIService) { }

  ngOnInit(): void {
    this.api.getData().subscribe((data) => {
      this.myData = data;
      });
  }
  
}
