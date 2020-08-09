import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { NgxPaginationModule } from 'ngx-pagination';

import { AuthService } from "../services/auth.service";
import { ContactRow } from '../services/contact.service';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LogInComponent } from './log-in/log-in.component';
import { LogInFormComponent } from './log-in-form/log-in-form.component';
import { RegisterComponent } from './register/register.component';
import { ManageAccountComponent } from './manage-account/manage-account.component';
import { ContactListComponent } from './contact-list/contact-list.component';
import { ContactEditComponent } from './contact-edit/contact-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LogInFormComponent,
    HomeComponent,
    LogInComponent,
    RegisterComponent,
    ManageAccountComponent,
    ContactListComponent,
    ContactEditComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgxPaginationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'log-in', component: LogInComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'manage-account', component: ManageAccountComponent },
      { path: 'contact-list', component: ContactListComponent },
      { path: 'contact-add', component: ContactEditComponent },
      { path: 'contact-edit', component: ContactEditComponent },
    ])
  ],
  providers: [AuthService, ContactRow],
  bootstrap: [AppComponent]
})
export class AppModule { }
