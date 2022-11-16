import { Component, OnInit } from '@angular/core';
import { InternalAPIService } from '../API-Service/internal-api.service';
//import { LoginUser } from 'src/Model/LoginUser';
import { FormControl, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


  // dependency injection
  constructor(private api : InternalAPIService ) { }

  ngOnInit(): void {}

  loginFrom: FormGroup = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  });

  loginProcess(){
    if(this.loginFrom.valid){
        console.log(this.loginFrom.controls['username'].value);
        console.log(this.loginFrom.controls['password'].value);
        this.api.Login({
          username : this.loginFrom.controls['username'].value,
          password : this.loginFrom.controls['password'].value
        }).subscribe((res) => {sessionStorage.setItem("token", res['token'])});
    }
  }
 
}
