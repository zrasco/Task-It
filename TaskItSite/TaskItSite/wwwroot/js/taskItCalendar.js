/**
        * TaskIt Calendar application
        * Exclusively the current user's calendar
        */
var calendarDemoApp = angular.module('TaskItApp', ['ui.calendar', 'ui.bootstrap']);

//controller to pull information from the database
calendarDemoApp.controller('CalendarCtrl', ['$scope', '$http', 'uiCalendarConfig', function ($scope, $http, uiCalendarConfig) {

    //selected calendar
    $scope.SelectedEvent = null;
    var isFirstTime = true;

    //list of tasks
    $scope.tasks = [];

    //list of presented tasks via calendar (events)
    $scope.events = [];

    //Load events from server
    $scope.tasks = personalRawData;
    console.log("in calendar", $scope.tasks);

    //go through each event and set calendar location for each
    $scope.events.slice(0, $scope.tasks.length);
    //push events onto the actual calendar
    angular.forEach($scope.tasks, function (value) {
        $scope.events.push({
            title: value.id,
            description: value.description,
            start: new Date(value.dueDate),
            allDay: true
        });
        console.log(new Date(value.createdDate));
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
            eventDrop: $scope.alertOnDrop,
            eventResize: $scope.alertOnResize,
            eventRender: $scope.eventRender
        }
    };
    /*push data to calendar*/
    $scope.eventSources = [$scope.events];
}])