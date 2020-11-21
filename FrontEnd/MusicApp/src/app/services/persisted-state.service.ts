import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PersistedStateService {

  constructor() { }

  public LOGGEND_IN: string = "AUTHENTICATED_USER";

  public set(key: string, data: any){
    localStorage.setItem(key, JSON.stringify(data));
  }

  public get(key: string){
    return JSON.parse(localStorage.getItem(key));
  }

  public remove(key: string){
    localStorage.removeItem(key);
  }

  public exists(key: string){
    return !!localStorage.getItem(key);
  }
}
