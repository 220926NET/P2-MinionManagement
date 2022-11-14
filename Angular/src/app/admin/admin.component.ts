import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validator, Validators } from '@angular/forms';
import { InternalAPIService } from '../API-Service/internal-api.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  constructor(private api : InternalAPIService) { }

  ngOnInit(): void {
  }

  adminAddMoneyForm: FormGroup = new FormGroup({
    amount : new FormControl('', [Validators.required])
  })

  adminRemoveMoneyForm: FormGroup = new FormGroup({
    amount : new FormControl('', [Validators.required])
  })


  adminAddMoneyProcess(){
    if(this.adminAddMoneyForm){
      console.log(this.adminAddMoneyForm.controls['amount'].value);

      this.api.AdminAddMoney({
        amount : this.adminAddMoneyForm.controls['amount'].value
      }).subscribe( (res) => alert(res))
    }
  }


  adminRemoveMoneyProcess(){
    if(this.adminRemoveMoneyForm){
      console.log(this.adminRemoveMoneyForm.controls['amount'].value);

      this.api.AdminRemoveMoney({
        amount : this.adminRemoveMoneyForm.controls['amount'].value
      }).subscribe( (res) => alert(res))
    }
  }
}
