import { Component, OnInit } from '@angular/core';
import { InternalAPIService } from '../API-Service/internal-api.service';
import { LoginUser } from 'src/Model/LoginUser';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  // dependency injection
  constructor(private api : InternalAPIService ) { }

  ngOnInit(): void {
  }

  // Create a user object for testing
  loginUser : LoginUser = {
    username : 'testforReg',
    password : '000'
  }

  token : any = '';
  
  LoginFunction() : void{
    console.log('LoginFunction invoked');
    this.api.Login(this.loginUser).subscribe(
      (data) => console.log(data)
    )
  }
}
