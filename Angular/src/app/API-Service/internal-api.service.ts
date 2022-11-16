import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http'
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InternalAPIService {

  

  // dependency injection
  constructor(private http:HttpClient) { }

  // create new header with token
  //header = new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`);

  //Need to change to API deployment string
  private apiLoginURL : string = 'https://minionmanagement.azurewebsites.net/Authentication/Login';
  // Using localhost for testing purpose
  Login(data: any) : Observable<any> {
    return this.http.post(this.apiLoginURL, data);
  }

  private apiRegisterURL : string = 'https://minionmanagement.azurewebsites.net/Authentication/Register';
  Register(data: any): Observable<any> {
    return this.http.post(this.apiRegisterURL,data)
  }


  private apiTransactionURL : string = "https://minionmanagement.azurewebsites.net/Transaction/transaction";
  Transaction(data : any) : Observable<any> {
    return this.http.post(this.apiTransactionURL, data, { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)})
  }

  private apiAdminAddMoney : string = "https://minionmanagement.azurewebsites.net/Admin/addmoney";
  AdminAddMoney(data : any) : Observable<any> {
    return this.http.post(this.apiAdminAddMoney, data, { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)});
  }

  private apiAdminRemoveMoney : string = "https://minionmanagement.azurewebsites.net/Admin/removemoney";
  AdminRemoveMoney(data : any) : Observable<any> {
    return this.http.post(this.apiAdminRemoveMoney, data, { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)});
  }

  private apiBuyTroop :  string = "https://minionmanagement.azurewebsites.net/Transaction/buytroop";
  BuyTroop(data : any) : Observable<any> {
    return this.http.post(this.apiBuyTroop, data, { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)});
  }

  
  TransactionRecords(accountNum : number) : Observable<any>{
    console.log(sessionStorage.getItem('token'));
    return this.http.get(`https://localhost:7202/account/${accountNum}`, { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)});
  }

  Raid() : Observable<any> {
    console.log(sessionStorage.getItem('token'));
    return this.http.get("https://minionmanagement.azurewebsites.net/Account/Raid", { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)});
   }

  RaidResult(raidOpponentID : number) : Observable<any> {
    return this.http.put(`https://minionmanagement.azurewebsites.net/Account/Raid/${raidOpponentID}`,raidOpponentID ,  { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)});
  }
}
