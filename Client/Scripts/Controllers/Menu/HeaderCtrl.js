'use strict'
//companionApp.controller('HeaderCtrl', ['$scope', '$location', function ($scope, $location) {
companionApp.controller('HeaderCtrl', ['$scope', '$rootScope', '$location', 'forceLogOut', 'changesAlert', function ($scope, $rootScope, $location, forceLogOut, changesAlert) {
    $scope.userName = $rootScope.user.nvName;
    $scope.logOut = function () {
        changesAlert.alert(function () {
            forceLogOut.doLogOut();
        });
    }
}]);