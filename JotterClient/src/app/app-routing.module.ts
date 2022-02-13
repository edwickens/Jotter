import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PasswordEditorComponent } from './password-editor/password-editor.component';
import { PasswordsListComponent } from './passwords-list/passwords-list.component';

const routes: Routes = [
  {
    path: 'edit',
    component: PasswordEditorComponent
  },
  {
    path: '',
    component: PasswordsListComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
