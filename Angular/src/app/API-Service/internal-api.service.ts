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
  private apiLoginURL : string = 'https://localhost:7202/Authentication/Login';
  // Using localhost for testing purpose
  Login(data: any) : Observable<any> {
    return this.http.post(this.apiLoginURL, data);
  }

  private apiRegisterURL : string = 'https://localhost:7202/Authentication/Register';
  Register(data: any): Observable<any> {
    return this.http.post(this.apiRegisterURL,data)
  }


  private apiTransactionURL : string = "https://localhost:7202/Transaction/transaction";
  Transaction(data : any) : Observable<any> {
    return this.http.post(this.apiTransactionURL, data)
  }

  private apiAdminAddMoney : string = "https://localhost:7202/Admin/addmoney";
  AdminAddMoney(data : any) : Observable<any> {
    return this.http.post(this.apiAdminAddMoney, data);
  }

  private apiAdminRemoveMoney : string = "https://localhost:7202/Admin/removemoney";
  AdminRemoveMoney(data : any) : Observable<any> {
    return this.http.post(this.apiAdminRemoveMoney, data);
  }
}
