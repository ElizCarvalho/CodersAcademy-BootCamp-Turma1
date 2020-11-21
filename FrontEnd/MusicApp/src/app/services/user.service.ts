import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from "environments/environment";
import SignIn from 'app/model/signIn';
import User from 'app/model/user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  public authenticate(payload: SignIn) : Observable<User>{
    return this.http.post<User>(`${environment.baseUrl}User/authenticate`, payload);
  }
}
