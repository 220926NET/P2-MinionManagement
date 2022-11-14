import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { InternalAPIService } from '../API-Service/internal-api.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {
  fromAccount : number = 1;
  constructor(private api : InternalAPIService ) { }

  ngOnInit(): void {
  }

  transactionForm: FormGroup = new FormGroup({
    to: new FormControl('', [Validators.required]),
    amount: new FormControl('', [Validators.required])
  })

  transacationProcess(){
    if(this.transactionForm.valid){
      console.log(this.transactionForm.controls['to'].value);
      console.log(this.transactionForm.controls['amount'].value);
      this.api.Transaction({
        from : 1,  //need to replace with current user
        to : this.transactionForm.controls['to'].value,
        amount : this.transactionForm.controls['amount'].value
      }).subscribe( //(res) => console.log(res) always return http error message
        {error(err){if(err.status == 201){alert("transaction successfully")}}}
      )
    }
  }
}
