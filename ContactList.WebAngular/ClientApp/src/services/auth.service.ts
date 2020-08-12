import { Injectable, NgZone } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from "@angular/router";
import { environment } from '../environments/environment';
import { ApiDto } from './api.dto';

@Injectable()

export class AuthService {
  userData: User;
  success: boolean;
  message: string;

  constructor(
    public router: Router,
    public ngZone: NgZone, // NgZone service to remove outside scope warning
    private http: HttpClient
  ) {
    this.userData = new User();
    if (this.isLoggedIn()) {
      this.userData = JSON.parse(sessionStorage.getItem('user'));
    }    
  }

  isLoggedIn() {
    return ('user' in sessionStorage);
  }

  // Sign in with username/password
  signIn(username, password) {
    this.success = false;
    this.message = "Processing...";
    var utcOffset = this.getUtcOffset();
    var loginParams = new ApiDto.UserInputLogin(username, password);

    this.http.post<ApiDto.UserOutputBase>(
      environment.apiUrl + 'AppUser/Login',
      loginParams
    ).subscribe(res => {
      this.success = !res.hasErrors;
      if (this.success) {
        this.userData = new User();
        this.userData.userId = res.returnObject.userId;
        this.userData.userName = res.returnObject.userName.toLowerCase();
        this.userData.email = res.returnObject.emailAddress;
        this.userData.firstName = res.returnObject.firstName;
        this.userData.lastName = res.returnObject.lastName;
        this.userData.utcOffset = utcOffset;
 
        sessionStorage.setItem('user', JSON.stringify(this.userData));
        this.router.navigate(['/']);
      } else {
        this.message = res.dtoMessage;
      }
    },
      err => {
        console.log(err);
        this.message = err;
      });
  }

  // Sign out 
  signOut() {
    sessionStorage.clear();
    this.userData = new User();
    this.userData.firstName = "Unknown User";
    this.router.navigate(['/']);
  }


  // Register an account
  register(username, password, email, firstname, lastname) {
    this.success = false;
    this.message = "Processing...";
    var utcOffset = this.getUtcOffset();

    var registerParams = new ApiDto.UserInputRegistration(username, email, firstname, lastname, password);

    this.http.post<ApiDto.UserOutputBase>(
      environment.apiUrl + 'AppUser/Register',
      registerParams
    ).subscribe(res => {
      this.success = !res.hasErrors;
      if (this.success) {
        this.userData = new User();
        this.userData.userId = res.returnObject.userId;
        this.userData.userName = res.returnObject.userName.toLowerCase();
        this.userData.email = res.returnObject.emailAddress;
        this.userData.firstName = res.returnObject.firstName;
        this.userData.lastName = res.returnObject.lastName;
        this.userData.utcOffset = utcOffset;
        sessionStorage.setItem('user', JSON.stringify(this.userData));
        this.router.navigate(['/']);
      } else {
        this.message = res.dtoMessage;
      }
    },
      err => {
        console.log(err);
        this.message = err;
      });
  }

  // Update an account
  update(email, firstname, lastname, oldpassword, newpassword) {
    this.success = false;
    this.message = "Processing...";

    var updateParams = new ApiDto.UserInputUpdate(this.userData.userId, email, firstname, lastname, oldpassword, newpassword);

    this.http.post<ApiDto.OutputBase>(
      environment.apiUrl + 'AppUser/Update',
      updateParams
    ).subscribe(res => {
      this.success = !res.hasErrors;
      if (this.success) {
        this.userData.firstName = firstname;
        this.userData.lastName = lastname;
        sessionStorage.setItem('user', JSON.stringify(this.userData));
      } else {
        this.message = res.dtoMessage;
      }
    },
      err => {
        console.log(err);
        this.message = err;
      });
  }

  // Get the UTC offset for a user in minutes
  getUtcOffset() {
    return 0 - (new Date()).getTimezoneOffset();
  }
}

export class User {
  public userId: string;
  public userName: string;
  public firstName: string;
  public lastName: string;
  public email: string;
  public utcOffset: number;

  constructor() {
    this.userId = null;
    this.userName = '';
    this.firstName = 'Unknown User';
    this.lastName = '';
    this.email = '';
    this.utcOffset = 0;
  }
}
