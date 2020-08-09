export class ContactRow {
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
