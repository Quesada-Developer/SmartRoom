'use strict'

var app = angular.module('dragDrop', ['ui.sortable']);
app.controller('MainCtrl', function ($scope) {

    $scope.init = function (name) {
        $scope.courses = name;
        console.log(name); // prints value of name
    }
    $scope.items = [{
        name: 'item 1'
    }, {
        name: 'item 2'
    }, {
        name: 'item 3'
    }, {
        name: 'item 4'
    }, {
        name: 'item 5'
    }, {
        name: 'item 6'
    }, {
        name: 'item 7'
    }, {
        name: 'item 8'
    }]

    $scope.sortableOptions = {
        containment: '#sortable-container'
    };
});

app.directive('myDirective', function ($compile) {
    return {
        restrict: 'E',
        //scope: {
        //  item: '='
        //},
        template: '<div><p data-ng-bind="item.name"></p></div>',
        replace: true,
    };
});

