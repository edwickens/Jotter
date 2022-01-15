import { Component } from '@angular/core';
import { Password } from './interfaces/password';
import { PasswordService } from './services/password.service' ;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  passwordService: PasswordService;
  title = 'Jotter';
  dataSource: Password[] = [];
  displayedColumns: string[] = ['title', 'url', 'username', 'secret'];

  constructor(passwordService: PasswordService) {
    this.passwordService = passwordService
  }

  ngOnInit() {
    this.passwordService.getPasswords().subscribe(data => this.dataSource = data);
  }
}
