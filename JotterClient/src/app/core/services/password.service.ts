import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.prod';
import { Password } from '../../interfaces/password';

@Injectable({
  providedIn: 'root'
})
export class PasswordService {
  constructor(private http: HttpClient) { }

  private apiBaseUrl: string = environment.apiBaseUrl;
  private passwordsUrl: string = "/Password";

  getPasswords() {
    return this.http.get<Password[]>(this.apiBaseUrl + this.passwordsUrl);
  }

}


