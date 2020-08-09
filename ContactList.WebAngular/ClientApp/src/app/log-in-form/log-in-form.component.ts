import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../services/auth.service";

 @Component({
  selector: 'app-log-in-form',
   templateUrl: './log-in-form.component.html',
   styleUrls: ['../form-overrides.css']
})
 export class LogInFormComponent implements OnInit {
  constructor(
    public authService: AuthService
  ) {
    this.authService = authService;
  }

  ngOnInit() { }

  model = new LoginFields('', '');

  submitted = false;

  onSubmit() {
    this.submitted = true;    
    this.authService.signIn(this.model.username, this.model.password);
  }

}

export class LoginFields {

  constructor(
    public username: string,
    public password: string
  ) { }

}
