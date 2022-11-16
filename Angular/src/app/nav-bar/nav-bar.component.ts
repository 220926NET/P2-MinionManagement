import { Component, OnInit } from '@angular/core';
import { InternalAPIService } from '../API-Service/internal-api.service';


@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  myData: any;
  constructor(private api : InternalAPIService) { }

  ngOnInit(): void {

  }

  logout(){
    sessionStorage.removeItem('token');
    console.log(sessionStorage.getItem('token'))
  }

  // onRedirect(): void {
  //   this.api.getData().subscribe((data) => {
  //     this.myData = data;
  //     });
  //     console.log("is this thing on?");
  //   }

}
