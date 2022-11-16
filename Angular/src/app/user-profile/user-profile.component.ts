import { Component, OnInit } from '@angular/core';
import { InternalAPIService } from '../API-Service/internal-api.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  myData: any;

  constructor(private api : InternalAPIService) { }

  ngOnInit(): void {
    this.api.getData().subscribe((data) => {
      console.log(data);
      this.myData = data;
      });
      console.log("is this thing on?");
  }
  
}
