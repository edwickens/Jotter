import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.prod';
import { Password } from '../../interfaces/password';



const httpOptions = {
  headers: new HttpHeaders()
    .set('content-type', 'application/json')
};

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

  createPassword(password: Password) {
    return this.http.post<Password>(this.apiBaseUrl + this.passwordsUrl, password, httpOptions);
  }

}


