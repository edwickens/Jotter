import { HttpClient } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { PasswordService } from '../core/services/password.service';
import { Password } from '../interfaces/password';
import { PasswordsListComponent } from './passwords-list.component';

describe('PasswordsListComponent', () => {
  let component: PasswordsListComponent;
  let fixture: ComponentFixture<PasswordsListComponent>;
  const passwordServiceSpy = jasmine.createSpyObj('PasswordService', ['getPasswords']);
  const testPasswords: Password[] = []

  beforeEach(async () => {
    passwordServiceSpy.getPasswords.and.returnValue(of(testPasswords));
    await TestBed.configureTestingModule({
      declarations: [PasswordsListComponent],
      providers: [{ provide: PasswordService, useValue: passwordServiceSpy }]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PasswordsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
