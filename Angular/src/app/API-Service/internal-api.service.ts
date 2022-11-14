import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http'
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InternalAPIService {

  

  // dependency injection
  constructor(private http:HttpClient) { }

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
}
