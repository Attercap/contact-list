export module ApiResult {
  export class ApiReturn {
    public hasErrors: boolean;
    public dtoMessage: string;

    constructor() {
      this.hasErrors = true;
      this.dtoMessage = 'Unknown error.';
    }
  }

  export class ApiUser {
    public userId: string;
    public userName: string;
    public emailAddress: string;
    public firstName: string;
    public lastName: string;
    public hasErrors: boolean;
    public dtoMessage: string;

    constructor() {
      this.userId = null;
      this.userName = '';
      this.emailAddress = '';
      this.firstName = 'Unknown User';
      this.lastName = '';
      this.hasErrors = true;
      this.dtoMessage = 'Unknown error.';
    }
  }

}
