var app = angular.module('syllabusApp', []);
app.controller('formCtrl', function ($scope) {
    $scope.master = { Date: "", Title: "", Info: "" };
    $scope.reset = function () {
        $scope.user = angular.copy($scope.master);
    };
    $scope.reset();
    $scope.addFields = function () { $scope.master.push({Date:"", Title:"", Info:""}); };
});