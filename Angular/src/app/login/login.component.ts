import { Component, OnInit } from '@angular/core';
import { InternalAPIService } from '../API-Service/internal-api.service';
//import { LoginUser } from 'src/Model/LoginUser';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { firstValueFrom, observable, Observable } from 'rxjs';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  myData: any;

  // dependency injection
  constructor(private api : InternalAPIService, private router: Router ) { }

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
        }).subscribe((res) => {sessionStorage.setItem("token", res['token'])
        alert("login successful");
        this.router.navigate(['/']);
        console.log("bearer : " + `Bearer ${sessionStorage.getItem('token')}`);});
    // Place Await Here

    }
  }

  // onLogin(): void {
  //   this.api.getData().subscribe((data) => {
  //     this.myData = data;
  //     });
  //     console.log("is this thing on?");
  //   }


     

}
