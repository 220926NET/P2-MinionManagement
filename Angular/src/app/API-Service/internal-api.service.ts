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
  //header = new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`);

  //Need to change to API deployment string // tested
  private apiLoginURL : string = 'https://minionmanagement.azurewebsites.net/Authentication/Login';
  Login(data: any) : Observable<any> {
    return this.http.post(this.apiLoginURL, data);
  }
  // tested
  private apiRegisterURL : string = 'https://minionmanagement.azurewebsites.net/Authentication/Register';
  Register(data: any): Observable<any> {
    return this.http.post(this.apiRegisterURL,data)
  }
  // tested
  private apiTransactionURL : string = "https://minionmanagement.azurewebsites.net/Account/Transaction";
  Transaction(data : any) : Observable<any> {
    return this.http.post(this.apiTransactionURL, data, { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)})
  }
  // tested
  private apiAdminAddMoney : string = "https://minionmanagement.azurewebsites.net/Admin/addmoney";
  AdminAddMoney(data : any) : Observable<any> {
    return this.http.post(this.apiAdminAddMoney, data, { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)});
  }
  // tested
  private apiAdminRemoveMoney : string = "https://minionmanagement.azurewebsites.net/Admin/removemoney";
  AdminRemoveMoney(data : any) : Observable<any> {
    return this.http.post(this.apiAdminRemoveMoney, data, { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)});
  }

  // tested // failing
  getData() : Observable<any> {
    console.log(`Bearer ${sessionStorage.getItem('token')}`)
    return this.http.get('https://minionmanagement.azurewebsites.net/Profile/', { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)});
  }
  // tested
  private apiBuyTroop :  string = "https://minionmanagement.azurewebsites.net/Account/buytroop";
  BuyTroop(data : any) : Observable<any> {
    return this.http.post(this.apiBuyTroop, data, { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)});
  }
  
  TransactionRecords(accountNum : number) : Observable<any>{
    console.log(sessionStorage.getItem('token'));
    return this.http.get(`https://minionmanagement.azurewebsites.net/account/${accountNum}`, { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)});
  }
  // tested
  Raid() : Observable<any> {
    console.log(sessionStorage.getItem('token'));
    return this.http.get("https://minionmanagement.azurewebsites.net/Account/Raid", { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)});
   }
  // tested 
  RaidResult(raidOpponentID : number) : Observable<any> {
    return this.http.put(`https://minionmanagement.azurewebsites.net/Account/Raid/${raidOpponentID}`,raidOpponentID ,  { headers : new HttpHeaders().set('Authorization', `Bearer ${sessionStorage.getItem('token')}`)});
  }

}
