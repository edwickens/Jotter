import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { PasswordService } from '../core/services/password.service';
import { Password } from '../interfaces/password';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-password-editor',
  templateUrl: './password-editor.component.html',
  styleUrls: ['./password-editor.component.scss']
})
export class PasswordEditorComponent implements OnInit {
  password: Password = {} as Password;
  passwordForm: FormGroup;
  passwordService: PasswordService;
  router: Router;

  constructor(
    passwordService: PasswordService,
    router: Router,
    private formBuilder: FormBuilder
  ) {
    this.passwordService = passwordService;
    this.router = router;
    this.passwordForm = this.formBuilder.group({
      url: '',
      title: '',
      username: '',
      secret: '',
      description: '',
      userId: Guid.create().toString(),
    });
  }

  ngOnInit(): void {
  }

  submitForm(): void {
    Object.assign(this.password, this.passwordForm.value)
    this.passwordService.createPassword(this.password).subscribe(data => this.router.navigateByUrl(''));
  }

}
