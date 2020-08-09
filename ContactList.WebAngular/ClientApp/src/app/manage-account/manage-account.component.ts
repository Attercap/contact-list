import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../services/auth.service";

 @Component({
  selector: 'app-manage-account',
   templateUrl: './manage-account.component.html',
   styleUrls: ['../form-overrides.css']
})
 export class ManageAccountComponent implements OnInit {
  constructor(
    public authService: AuthService
  ) {
    this.authService = authService;
  }

  ngOnInit() { }

  model = new AccountFields(this.authService.userData.email, this.authService.userData.firstName, this.authService.userData.lastName);

  submitted = false;

  onSubmit() {
    this.submitted = true;    
    this.authService.update(this.model.email, this.model.firstname, this.model.lastname, this.model.oldpassword, this.model.newpassword);
   }

   isOldPasswordRequired() {
     return (this.model.newpassword.length > 0 && this.model.oldpassword.length == 0);
   }

   isValidNewPassword() {
     return (this.model.newpassword.length > 0 && this.model.oldpassword != this.model.newpassword);
   }

   isValidConfirmationPassword() {
     return this.model.newpassword === this.model.confirmpassword;
   }

}

export class AccountFields {
  public email: string;
  public firstname: string;
  public lastname: string;
  public oldpassword: string;
  public newpassword: string;
  public confirmpassword: string;

  constructor(email: string, firstname: string, lastname: string) {
    this.email = email;
    this.firstname = firstname;
    this.lastname = lastname;
    this.oldpassword = '';
    this.newpassword = '';
    this.confirmpassword = '';
  }

}
