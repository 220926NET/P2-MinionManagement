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

  // Using localhost for testing purpose
  Login(loginUser : LoginUser) : Observable<any> {
    return this.http.post('https://localhost:7202/Authentication/Login', loginUser);
  }
}
