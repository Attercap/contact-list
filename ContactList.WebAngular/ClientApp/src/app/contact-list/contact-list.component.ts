import { Component, OnInit } from '@angular/core';
import { AuthService } from "../../services/auth.service";
import { HttpClient } from '@angular/common/http';
import { ApiDto } from '../../services/api.dto';
import { environment } from '../../environments/environment';
import { Router } from "@angular/router";

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html'
})
export class ContactListComponent implements OnInit {
  public contactCount: number;
  public contacts: ApiDto.ContactOutputData[];
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
      var getCountParams = new ApiDto.ContactInputGetCount(this.authService.userData.userId);

      this.http.post<ApiDto.OutputBase>(environment.apiUrl + 'Contact/Count', getCountParams).subscribe(res => {
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
    var pagedContactParams = new ApiDto.ContactInputGetList(this.authService.userData.userId, this.authService.userData.utcOffset, this.config.currentPage, this.config.itemsPerPage);
    this.http.post<ApiDto.ContactOutputBase>(environment.apiUrl + 'Contact/List', pagedContactParams).subscribe(res => {
      if (!res.hasErrors) {
        this.contacts = res.contactList;
      }
      else {
        alert(res.dtoMessage);
      }

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
    sessionStorage.setItem('currentContact', JSON.stringify(new ApiDto.ContactInputUpdate(null, this.authService.userData.userId,'','','','','','','','','')));
    this.router.navigate(['/contact-add']);
  }

  deleteContact(i) {
    var contact = this.contacts[i];
    var deleteParams = new ApiDto.ContactInputDelete(contact.contactId, this.authService.userData.userId);
    this.http.post<ApiDto.OutputBase>(environment.apiUrl + 'Contact/Delete', deleteParams).subscribe(res => {
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
