(function () {
   var app = angular.module('PersonalSystemAngular', ['ngAnimate'])
     var PMController = function ($scope, $http, PMService) {
         getUsers();
         function getUsers() {
             PMService.getUsers().success(function (test) {
                 $scope.users = test;
             }).error(function (error) {
                 $scope.status = 'Unable to load user data' + error.message;
             });
         }
         $scope.Name = "";
         $scope.SearchFill = function (_name) {
             $scope.searchUser = _name;
         }
     };

     app.controller("PMController", PMController);
     app.factory('PMService', ['$http', function ($http) {
         var PMService = {};
         PMService.getUsers = function () {
             return $http.get('/Users/GetUsers');
         };
         return PMService;
     }]);
    
}());