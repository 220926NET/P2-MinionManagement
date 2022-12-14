import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { InternalAPIService } from '../API-Service/internal-api.service';


@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {
  
  constructor(private api : InternalAPIService ) { }

  ngOnInit(): void {
  }

  transactionForm: FormGroup = new FormGroup({
    from: new FormControl('', [Validators.required]),
    to: new FormControl('', [Validators.required]),
    amount: new FormControl('', [Validators.required])
  })

  transacationProcess(){
    if(this.transactionForm.valid){
      console.log(this.transactionForm.controls['from'].value);
      console.log(this.transactionForm.controls['to'].value);
      console.log(this.transactionForm.controls['amount'].value);
      
      this.api.Transaction({
        from : this.transactionForm.controls['from'].value,
        to : this.transactionForm.controls['to'].value,
        amount : this.transactionForm.controls['amount'].value
      }).subscribe( (res) => alert(res)
      )
    }
  }


  buyTroopForm : FormGroup = new FormGroup({
    userID : new FormControl('', [Validators.required]),
    numOfTroop : new FormControl('', [Validators.required])
  })

  buyTroopProcess(){
    if(this.buyTroopForm.valid){
      ///console.log(this.buyTroopForm.controls['userID'].value);
      console.log(this.buyTroopForm.controls['numOfTroop'].value);
    }

    this.api.BuyTroop({
      //userID : this.buyTroopForm.controls['userID'].value, // need to replace with current user ID
      numOfTroop : this.buyTroopForm.controls['numOfTroop'].value
    }).subscribe((res) => alert(res))
  }

 records : any[] = [] ;

 recordForm : FormGroup = new FormGroup({
  accountNum : new FormControl('', [Validators.required])
 })

 showTransactionRecord(){
    // need to replace with currect user account number
     this.api.TransactionRecords(this.recordForm.controls['accountNum'].value).subscribe((res) => this.records = res
    );
    console.log(this.records);
  }
}
