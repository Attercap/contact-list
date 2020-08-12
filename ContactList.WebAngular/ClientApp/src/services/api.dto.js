"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ApiDto;
(function (ApiDto) {
    var OutputBase = /** @class */ (function () {
        function OutputBase() {
            this.hasErrors = true;
            this.dtoMessage = 'Unknown error.';
        }
        return OutputBase;
    }());
    ApiDto.OutputBase = OutputBase;
    var UserOutputData = /** @class */ (function () {
        function UserOutputData() {
            this.userId = null;
            this.userName = '';
            this.emailAddress = '';
            this.firstName = 'Unknown User';
            this.lastName = '';
        }
        return UserOutputData;
    }());
    ApiDto.UserOutputData = UserOutputData;
    var UserOutputBase = /** @class */ (function () {
        function UserOutputBase() {
            this.hasErrors = true;
            this.dtoMessage = 'Unknown error.';
            this.returnObject = new UserOutputData();
        }
        return UserOutputBase;
    }());
    ApiDto.UserOutputBase = UserOutputBase;
    var UserInputLogin = /** @class */ (function () {
        function UserInputLogin(username, password) {
            this.userName = username;
            this.password = password;
        }
        return UserInputLogin;
    }());
    ApiDto.UserInputLogin = UserInputLogin;
    var UserInputRegistration = /** @class */ (function () {
        function UserInputRegistration(username, emailaddress, firstname, lastname, password) {
            this.userName = username;
            this.emailAddress = emailaddress;
            this.firstName = firstname;
            this.lastName = lastname;
            this.password = password;
        }
        return UserInputRegistration;
    }());
    ApiDto.UserInputRegistration = UserInputRegistration;
    var UserInputUpdate = /** @class */ (function () {
        function UserInputUpdate(userid, emailaddress, firstname, lastname, oldpassword, newpassword) {
            this.userId = userid;
            this.emailAddress = emailaddress;
            this.firstName = firstname;
            this.lastName = lastname;
            this.oldPassword = oldpassword;
            this.newPassword = newpassword;
        }
        return UserInputUpdate;
    }());
    ApiDto.UserInputUpdate = UserInputUpdate;
    var ContactOutputBase = /** @class */ (function () {
        function ContactOutputBase() {
            this.hasErrors = true;
            this.dtoMessage = 'Unknown error.';
            this.returnObject = new ContactOutputData[0];
        }
        return ContactOutputBase;
    }());
    ApiDto.ContactOutputBase = ContactOutputBase;
    var ContactOutputData = /** @class */ (function () {
        function ContactOutputData() {
            this.contactId = null;
            this.userId = '';
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
        return ContactOutputData;
    }());
    ApiDto.ContactOutputData = ContactOutputData;
    var ContactInputGetCount = /** @class */ (function () {
        function ContactInputGetCount(userid) {
            this.userId = userid;
        }
        return ContactInputGetCount;
    }());
    ApiDto.ContactInputGetCount = ContactInputGetCount;
    var ContactInputGetList = /** @class */ (function () {
        function ContactInputGetList(userid, utcoffset, pagenumber, rowsperpage) {
            this.userId = userid;
            this.utcOffset = utcoffset;
            this.pageNumber = pagenumber;
            this.rowsPerPage = rowsperpage;
        }
        return ContactInputGetList;
    }());
    ApiDto.ContactInputGetList = ContactInputGetList;
    var ContactInputUpdate = /** @class */ (function () {
        function ContactInputUpdate(contatid, userid, firstname, lastname, emailaddress, streetaddress1, streetaddress2, city, stateprovince, postalcode, country) {
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
        return ContactInputUpdate;
    }());
    ApiDto.ContactInputUpdate = ContactInputUpdate;
    var ContactInputDelete = /** @class */ (function () {
        function ContactInputDelete(contactid, userid) {
            this.contactId = contactid;
            this.userId = userid;
        }
        return ContactInputDelete;
    }());
    ApiDto.ContactInputDelete = ContactInputDelete;
})(ApiDto = exports.ApiDto || (exports.ApiDto = {}));
//# sourceMappingURL=api.dto.js.map