import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import Album from 'app/model/album';
import { environment } from "environments/environment";

@Injectable({
  providedIn: 'root'
})
export class MusicService {

  constructor(private http: HttpClient) { }

  public getAlbuns(): Observable<Album[]>{
    return this.http.get<Album[]>(`${environment.baseUrl}album`);
  }
}
