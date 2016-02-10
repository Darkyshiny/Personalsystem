

    var app = angular.module('PersonalSystemAngular', ['ngAnimate'])
                app.controller('scheduleController', ['$scope', '$http', function ($scope, $http) {

                    $scope.displayByInt = 0;

                    $scope.hourSlotToggle = function (x) {
                        if ($scope.displayByInt != 0) {
                            if ($scope.displayByInt != x)
                                $scope.displayByInt = x;
                            else {
                                $scope.displayByInt = 0;
                            }
                        }
                        else {
                            $scope.displayByInt = x;
                        }
                    }

                }]);
