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
  private apiURL : string = 'https://localhost:7202/Authentication/Login';
  // Using localhost for testing purpose
  Login(data: any) : Observable<any> {
    return this.http.post(this.apiURL, data);
  }
}
