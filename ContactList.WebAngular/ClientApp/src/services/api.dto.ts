import { User } from "./auth.service";

export module ApiDto {
  export class OutputBase {
    public hasErrors: boolean;
    public dtoMessage: string;

    constructor() {
      this.hasErrors = true;
      this.dtoMessage = 'Unknown error.';
    }
  }



  export class UserOutputData {
    public userId: string;
    public userName: string;
    public emailAddress: string;
    public firstName: string;
    public lastName: string;

    constructor() {
      this.userId = null;
      this.userName = '';
      this.emailAddress = '';
      this.firstName = 'Unknown User';
      this.lastName = '';
    }
  }

  export class UserOutputBase {
    public hasErrors: boolean;
    public dtoMessage: string;
    public userObject: OutputUser

    constructor() {
      this.hasErrors = true;
      this.dtoMessage = 'Unknown error.';
      this.userObject = new OutputUser();
    }
  }

  export class UserInputLogin {
    public userName: string;
    public password: string;

    constructor(username, password) {
      this.userName = username;
      this.password = password;
    }
  }

  export class UserInputRegistration {
    public userName: string;
    public emailAddress: string;
    public firstName: string;
    public lastName: string;
    public password: string;

    constructor(username, emailaddress, firstname, lastname, password) {
      this.userName = username;
      this.emailAddress = emailaddress;
      this.firstName = firstname;
      this.lastName = lastname;
      this.password = password;
    }
  }

  export class UserInputUpdate {
    public userId: string;
    public emailAddress: string;
    public firstName: string;
    public lastName: string;
    public oldPassword: string;
    public newPassword: string;

    constructor(userid, emailaddress, firstname, lastname, oldpassword, newpassword) {
      this.userId = userid;
      this.emailAddress = emailaddress;
      this.firstName = firstname;
      this.lastName = lastname;
      this.oldPassword = oldpassword;
      this.newPassword = newpassword;
    }
  }


  export class ContactOutputBase {
    public hasErrors: boolean;
    public dtoMessage: string;
    public contactList: OutputContactData[]

    constructor() {
      this.hasErrors = true;
      this.dtoMessage = 'Unknown error.';
      this.contactList = new OutputContactData[];
    }
  }

  export class ContactOutputData {
    public contactId: string;
    public userName: string;
    public firstName: string;
    public lastName: string;
    public emailAddress: string;
    public streetAddress1: string;
    public streetAddress2: string;
    public city: string;
    public stateProvince: string;
    public postalCode: string;
    public country: string;
    public lastModifiedFormatted: string;

    constructor() {
      this.contactId = null;
      this.userName = '';
      this.firstName = '';
      this.lastName = '';
      this.emailAddress = '';
      this.streetAddress1 = '';
      this.streetAddress2 = '';
      this.city = '';
      this.stateProvince = '';
      this.postalCode = '';
      this.country = '';
      this.lastModifiedFormatted = '';
    }
  }

  export class ContactInputGetCount {
    public userId: string;

    constructor(userid) {
      this.userId = userid;
    }
  }

  export class ContactInputGetList {
    public userId: string;
    public utcOffset: number;
    public pageNumber: number;
    public rowsPerPage: number;

    constructor(userid, utcoffset, pagenumber, rowsperpage) {
      this.userId = userid;
      this.utcOffset = utcoffset;
      this.pageNumber = pagenumber;
      this.rowsPerPage = rowsperpage;
    }
  }

  export class ContactInputUpdate {
    public contactId: string;
    public userId: string;
    public firstName: string;
    public lastName: string;
    public emailAddress: string;
    public streetAddress1: string;
    public streetAddress2: string;
    public city: string;
    public stateProvince: string;
    public postalCode: string;
    public country: string;

    constructor(contatid, userid, firstname, lastname, emailaddress, streetaddress1, streetaddress2, city, stateprovince, postalcode, country) {
      this.contactId = contatid;
      this.userId = userid;
      this.firstName = firstname;
      this.lastName = lastname;
      this.emailAddress = emailaddress;
      this.streetAddress1 = streetaddress1;
      this.streetAddress2 = streetaddress2;
      this.city = city;
      this.stateProvince = stateprovince;
      this.postalCode = postalcode;
      this.country = country;
    }
  }

  export class ContactInputDelete {
    public contactId: string;
    public userId: string;

    constructor(contactid, userid) {
      this.contactId = contactid;
      this.userId = userid;
    }
  }

}
