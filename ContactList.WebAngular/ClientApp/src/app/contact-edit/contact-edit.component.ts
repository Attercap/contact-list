import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../services/auth.service";
import { HttpClient } from '@angular/common/http';
import { ApiDto } from '../../services/api.dto';
import { environment } from '../../environments/environment';
import { Router } from "@angular/router";

@Component({
  selector: 'app-contact-edit',
  templateUrl: './contact-edit.component.html',
  styleUrls: ['../form-overrides.css']
})
export class ContactEditComponent implements OnInit {
  constructor(
    public authService: AuthService,
    public router: Router,
    public currentContact: ApiDto.ContactInputUpdate,
    private http: HttpClient
  ) {
    this.authService = authService;
    if (sessionStorage.getItem('currentContact') != null) {
      this.currentContact = JSON.parse(sessionStorage.getItem('currentContact'));
    } else {
      this.currentContact = new ApiDto.ContactInputUpdate(null,this.authService.userData.userId,'','','','','','','','','');
    }
  }

  model = new ContactFormFields(
    this.currentContact.firstName,
    this.currentContact.lastName,
    this.currentContact.emailAddress,
    this.currentContact.streetAddress1,
    this.currentContact.streetAddress2,
    this.currentContact.city,
    this.currentContact.stateProvince,
    this.currentContact.postalCode,
    this.currentContact.country
  );
  submitted = false;
  success = false;
  message = '';

  ngOnInit() {
    this.model = new ContactFormFields(
      this.currentContact.firstName,
      this.currentContact.lastName,
      this.currentContact.emailAddress,
      this.currentContact.streetAddress1,
      this.currentContact.streetAddress2,
      this.currentContact.city,
      this.currentContact.stateProvince,
      this.currentContact.postalCode,
      this.currentContact.country
    );
    console.log(this.currentContact);
    console.log(this.model);
  }

  onSubmit() {
    this.submitted = true;
    this.currentContact.userId = this.authService.userData.userId;
    this.currentContact.firstName = this.model.firstname;
    this.currentContact.lastName = this.model.lastname;
    this.currentContact.emailAddress = this.model.emailaddress;
    this.currentContact.streetAddress1 = this.model.streetaddress1;
    this.currentContact.streetAddress2 = this.model.streetaddress2;
    this.currentContact.city = this.model.city;
    this.currentContact.stateProvince = this.model.stateprovince;
    this.currentContact.postalCode = this.model.postalcode;
    this.currentContact.country = this.model.country;
    this.http.post<ApiDto.OutputBase>(environment.apiUrl + 'Contact/AddEdit', this.currentContact).subscribe(res => {
      this.success = !res.hasErrors;
      this.message = res.dtoMessage;

      if (this.success) {
        this.router.navigate(['/contact-list']);
      }
    },
      error => console.error(error)
    );

  }

}

export class ContactFormFields {
  public firstname: string;
  public lastname: string;
  public emailaddress: string;
  public streetaddress1: string;
  public streetaddress2: string;
  public city: string;
  public stateprovince: string;
  public postalcode: string;
  public country: string;

  constructor(
    firstName: string,
    lastName: string,
    emailAddress: string,
    streetAddress1: string,
    streetAddress2: string,
    city: string,
    stateProvince: string,
    postalCode: string,
    country: string
  ) {
    this.firstname = firstName;
    this.lastname = lastName;
    this.emailaddress = emailAddress;
    this.streetaddress1 = streetAddress1;
    this.streetaddress2 = streetAddress2;
    this.city = city;
    this.stateprovince = stateProvince;
    this.postalcode = postalCode;
    this.country = country;
  }
}
