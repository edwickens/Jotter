import { Component, OnInit } from '@angular/core';
import { PasswordService } from '../core/services/password.service';
import { Password } from '../interfaces/password';

@Component({
  selector: 'app-passwords-list',
  templateUrl: './passwords-list.component.html',
  styleUrls: ['./passwords-list.component.scss']
})
export class PasswordsListComponent implements OnInit {

  passwordService: PasswordService;

  passwords: Password[] = [];
  displayedColumns: string[] = ['title', 'url', 'username', 'secret'];
  edit: string = 'edit';

  constructor(passwordService: PasswordService) {
    this.passwordService = passwordService
  }

  ngOnInit() {
    this.passwordService.getPasswords().subscribe(data => this.passwords = data);
  }

}
