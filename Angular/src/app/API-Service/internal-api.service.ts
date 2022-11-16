import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http'
import { catchError, Observable, throwError } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class InternalAPIService {

  

  // dependency injection
  constructor(private http: HttpClient) { }

  // create new header with token
  header = new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)

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
    return this.http.post(this.apiTransactionURL, data)
  }

  private apiAdminAddMoney : string = "https://minionmanagement.azurewebsites.net/Admin/addmoney";
  AdminAddMoney(data : any) : Observable<any> {
    return this.http.post(this.apiAdminAddMoney, data);
  }

  private apiAdminRemoveMoney : string = "https://minionmanagement.azurewebsites.net/Admin/removemoney";
  AdminRemoveMoney(data : any) : Observable<any> {
    return this.http.post(this.apiAdminRemoveMoney, data);
  }

  private apiGetProfile : string = "https://localhost:7202/Profile";
  GetUserProfile(data : any) : Observable<any> {
  return this.http.get(this.apiGetProfile, data);
  }

  private apiGetAccounts : string = "https://localhost:7202/Account";
  GetUserAccounts(data : any) : Observable<any> {
  return this.http.get(this.apiGetAccounts, data);
  }

  private apiGetAccountDetails : string = "https://minionmanagement.azurewebsites.net/Accounts/accountdetails";
  GetAccountDetails(data : any) : Observable<any> {
  return this.http.get(this.apiGetAccountDetails, data);
  }

getData() {
  return this.http.get('https://localhost:7202/Profile')
}

  private apiBuyTroop :  string = "https://minionmanagement.azurewebsites.net/Transaction/buytroop";
  BuyTroop(data : any) : Observable<any> {
    return this.http.post(this.apiBuyTroop, data);
  }

  //private apiTransactionRecord : string = `https://localhost:7202/account/${accountNum}`
  TransactionRecords() : Observable<any>{
    console.log(sessionStorage.getItem('token'));
    return this.http.get(`https://localhost:7202/account/43`, { headers : this.header});
    
  }
}
