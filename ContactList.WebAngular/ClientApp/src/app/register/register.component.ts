import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../services/auth.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['../form-overrides.css']
})
export class RegisterComponent implements OnInit {
  constructor(
    public authService: AuthService
  ) {
    this.authService = authService;
  }

  ngOnInit() { }

  model = new RegistrationFields();

  submitted = false;

  onSubmit() {
    this.submitted = true;
    this.authService.register(this.model.username, this.model.password, this.model.email, this.model.firstname, this.model.lastname);
  }

  isValidConfirmationPassword() {
    return this.model.password == this.model.confirmpassword;
  }

}

export class RegistrationFields {
  public username: string;
  public email: string;
  public firstname: string;
  public lastname: string;
  public password: string;
  public confirmpassword: string;

  constructor() {
    this.username = '';
    this.email = '';
    this.firstname = '';
    this.lastname = '';
    this.password = '';
    this.confirmpassword = '';
  }

}
