import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http'
import { Observable } from 'rxjs';
import { LoginUser } from 'src/Model/LoginUser';

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
}
