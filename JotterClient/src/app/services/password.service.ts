import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { Password } from '../interfaces/password';

@Injectable({
  providedIn: 'root'
})
export class PasswordService {
  constructor(private http: HttpClient) { }

  apiBaseUrl: string = environment.apiBaseUrl;
  passwordsUrl: string = "/Password";

  getPasswords() {
    return this.http.get<Password[]>(this.apiBaseUrl + this.passwordsUrl);
  }

}


