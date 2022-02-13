import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatTableModule } from '@angular/material/table';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { PasswordsListComponent } from './passwords-list/passwords-list.component';
import { SharedModule } from './shared/shared.module';
import { HeaderComponent } from './shared/header.component';
import { PasswordEditorComponent } from './password-editor/password-editor.component';
import { MatIconModule } from '@angular/material/icon';



@NgModule({
  declarations: [
    AppComponent,
    PasswordsListComponent,
    HeaderComponent,
    PasswordEditorComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatIconModule,
    BrowserAnimationsModule,
    MatTableModule,
    HttpClientModule,
    SharedModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
