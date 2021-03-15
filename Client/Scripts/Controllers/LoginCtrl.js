/// <reference path="../Libraries/angular.min.js" />
companionApp.controller('LoginCtrl', ['$scope', '$location', '$rootScope', 'alerts', 'connect', function ($scope, $location, $rootScope, alerts, connect) {
    $rootScope.isLogin = true;
    $rootScope.isWait = false;
    $scope.isDisableBtn = false;
    //if guide expired
    if ($rootScope.user == null || $rootScope.user == undefined) {
        $rootScope.user = JSON.parse(localStorage.getItem('AyeletHashacharCompanionship'));
    }

    $scope.login = function () {
        if ($scope.checkRequiredFields()) {
            $scope.isDisableBtn = true;
            //connect.post(false, connect.functions.Login, {userName: $scope.userName, password: $scope.userPassword }, function (result) {
            connect.login({
                userName: $scope.userName,
                password: $scope.userPassword
            }, function (result) {
                if (result == "" || result == null || result == undefined || result.iUserId && result.iUserId == -1) {
                    alerts.alert('שם משתמש או סיסמה אינם תקינים', 'אופס...');
                } else {
                    $rootScope.user = result;
                    $rootScope.userName = $scope.userName;
                    localStorage.setItem('AyeletHashacharCompanionship', JSON.stringify($rootScope.user));
                    $rootScope.isLogin = false;
                    $location.path('/ABCBook');
                }
            });
        }
    };

    $scope.checkRequiredFields = function () {
        if ((!$scope.userName) || (!$scope.userPassword)) {
            alert("לא הכנסת שם ו/או סיסמה!");
            return false;
        }
        return true;
    };

}]);