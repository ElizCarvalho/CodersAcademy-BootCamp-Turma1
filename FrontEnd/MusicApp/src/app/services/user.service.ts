import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from "environments/environment";
import SignIn from 'app/model/signIn';
import User from 'app/model/user';
import { Observable } from 'rxjs';
import Register from 'app/model/register';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  public authenticate(payload: SignIn) : Observable<User>{
    return this.http.post<User>(`${environment.baseUrl}User/authenticate`, payload);
  }

  public register(payload: Register) : Observable<User>{
    return this.http.post<User>(`${environment.baseUrl}User/register`, payload);
  }

  public removeFromFavorite(id: string, musicId: string) : Observable<any>{
    return this.http.delete(`${environment.baseUrl}User/${id}/favorite-music/${musicId}`)
  }

  public addToFavorite(id: string, musicId: string) : Observable<any>{
    return this.http.post<any>(`${environment.baseUrl}User/${id}/favorite-music/${musicId}`, null);
  }

  public getUser(id) : Observable<User> {
    return this.http.get<User>(`${environment.baseUrl}User/${id}`);
  }
  
}
