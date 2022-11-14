import { Component, OnInit } from '@angular/core';
import { InternalAPIService } from '../API-Service/internal-api.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {



  constructor(private api:InternalAPIService) { }

  ngOnInit(): void {
  }

  registerFrom: FormGroup = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  });

  registerProcess(){
    if(this.registerFrom.valid){
      this.api.Register({
        username: this.registerFrom.controls['username'].value,
        password: this.registerFrom.controls['password'].value
      }).subscribe( //(res) => console.log(res) always return http error message
        {error(err){if(err.status == 201){alert("new user created ssuccessfully")}}}
      )
    }
  }
}
