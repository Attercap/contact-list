import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../services/auth.service";
import { HttpClient } from '@angular/common/http';
import { ApiResult } from '../../services/api.result';
import { ContactRow } from '../../services/contact.service';
import { environment } from '../../environments/environment';
import { Router } from "@angular/router";

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html'
})
export class ContactListComponent implements OnInit {
  public contactCount: number;
  public contacts: ContactRow[];
  public config: any;

  constructor(
    public authService: AuthService,
    public router: Router,
    private http: HttpClient
  ) {
    this.authService = authService;
    this.contactCount = 0;
    this.initContacts();
    this.config = {
      itemsPerPage: 25,
      currentPage: 1,
      totalItems: 0
    };
  }

  ngOnInit() { }

  initContacts() {
    if (this.authService.userData !== null && this.authService.userData.userId !== null) {
      var params = { userName: this.authService.userData.userName };
      this.http.post<ApiResult.ApiReturn>(environment.apiUrl + 'Contact/Count', params).subscribe(res => {
          if (!res.hasErrors) {
            this.contactCount = parseInt(res.dtoMessage);
            this.config.totalItems = this.contactCount;
            this.loadPagedContacts();
          } else {
            alert(res.dtoMessage);
          }
        },
        error => console.error(error)
      );
    }
  }

  loadPagedContacts() {
    var params = { userName: this.authService.userData.userName, utcOffset: this.authService.userData.utcOffset, rowsPerPage: this.config.itemsPerPage, pageNumber: this.config.currentPage };
    this.http.post<ContactRow[]>(environment.apiUrl + 'Contact/List', params).subscribe(res => {
        this.contacts = res;
    },
      error => console.error(error)
    );
  }

  pageChanged(event) {
    this.config.currentPage = event;
    this.loadPagedContacts();
  }

  editContact(i) {
    var contact = this.contacts[i];
    sessionStorage.setItem('currentContact', JSON.stringify(contact));
    this.router.navigate(['/contact-edit']);
  }

  addContact() {
    sessionStorage.setItem('currentContact', JSON.stringify(new ContactRow()));
    this.router.navigate(['/contact-add']);
  }

  deleteContact(i) {
    var contact = this.contacts[i];
    contact.userName = this.authService.userData.userName;
    this.http.post<ApiResult.ApiReturn>(environment.apiUrl + 'Contact/Delete', contact).subscribe(res => {
      if (res.hasErrors) {
        alert(res.dtoMessage);
      } else {
        this.router.navigate(['/contact-list']);
      }
    },
      error => console.error(error)
    );
  }
}

export class ContactGetter {
  userName: string;
  utcOffset: number;
  pageNumber: number;
  rowsPerPage: number;
}
