/**
* TaskIt Calendar application
* Exclusively the current user's calendar
*/
var calendarDemoApp = angular.module('TaskItApp', ['ui.calendar', 'ui.bootstrap']);

//controller to pull information from the database
calendarDemoApp.controller('CalendarCtrl', ['$scope', '$http', 'uiCalendarConfig', function ($scope, $http, uiCalendarConfig) {
    //journal page default view
    $scope.viewType = true;
    //journal page delete view
    $scope.DelView = false;

    //selected calendar
    $scope.SelectedEvent = null;
    var isFirstTime = true;

    //list of tasks
    $scope.tasks = [];

    //list of presented tasks via calendar (events)
    $scope.events = [];

    //Load events from server
    $scope.tasks = personalRawData;

    //go through each event and set calendar location for each
    $scope.events.slice(0, $scope.tasks.length);

    //push personal events onto the actual calendar
    angular.forEach($scope.tasks, function (value) {
        $scope.events.push({
            title: value.summary,
            description: value.description,
            start: new Date(value.createdDate),
            end: new Date(value.dueDate),
            allDay: true
        });
    });

    //configure calendar
    /* config object */
    $scope.uiConfig = {
        calendar: {
            height: 450,
            editable: true,
            displayEventTime: true,
            header: {
                left: 'title',
                center: '',
                right: 'today prev,next'
            },
            //scope events
            eventClick: function (event) {
                $scope.SelectedEvent = event;
            },
            eventClick: $scope.alertOnEventClick
        }
    };

    /*Alert on click*/
    $scope.alertOnEventClick = function (date, jsEvent, view) {
        console.log(date.title + 'was clicked');
    };

    /*push data to calendar*/
    $scope.eventSources = [$scope.events];
}])