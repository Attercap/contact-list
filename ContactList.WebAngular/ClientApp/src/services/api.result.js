"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ApiResult;
(function (ApiResult) {
    var ApiReturn = /** @class */ (function () {
        function ApiReturn() {
            this.hasErrors = true;
            this.dtoMessage = 'Unknown error.';
        }
        return ApiReturn;
    }());
    ApiResult.ApiReturn = ApiReturn;
    var ApiReturnUser = /** @class */ (function () {
        function ApiReturnUser() {
            this.hasErrors = true;
            this.dtoMessage = 'Unknown error.';
            this.responseObject = new ApiUser();
        }
        return ApiReturnUser;
    }());
    ApiResult.ApiReturnUser = ApiReturnUser;
    var ApiUser = /** @class */ (function () {
        function ApiUser() {
            this.userId = null;
            this.userName = '';
            this.emailAddress = '';
            this.firstName = 'Unknown User';
            this.lastName = '';
        }
        return ApiUser;
    }());
    ApiResult.ApiUser = ApiUser;
})(ApiResult = exports.ApiResult || (exports.ApiResult = {}));
//# sourceMappingURL=api.result.js.map