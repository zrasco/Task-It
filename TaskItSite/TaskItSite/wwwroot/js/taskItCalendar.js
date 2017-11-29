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
    $scope.tasksSources = [$scope.tasks];
    //list of presented tasks via calendar (events)
    $scope.events = [];

    //Load events from server
    $scope.tasks = @Html.Raw(Json.Serialize(Model));

    //go through each event and set calendar location for each
    $scope.events.slice(0, $scope.tasks.length);
    angular.forEach(data.data, function (value) {
        $scope.events.push({
            title: value.title,
            description: value.description,
            start: value.createdDate,
            end: value.dueDate,
            allDay: value.isActive
        });
    });

    //configure calendar
    /* config object */
    $scope.uiConfig = {
        calendar: {
            height: 450,
            editable: true,
            header: {
                left: 'title',
                center: '',
                right: 'today prev,next'
            },
            //scope events
            eventClick: $scope.alertOnEventClick,
            eventDrop: $scope.alertOnDrop,
            eventResize: $scope.alertOnResize,
            eventRender: $scope.eventRender
        }
    };

    /*Add event button*/
    $scope.addEvent = function () {
        //get event info form user
        //false data for now
        var today = new Date()
        var CreatedDate = today.getFullYear() + '-' + today.getMonth() + '-' + today.getDay()
        CreatedDate += ' ' + today.getHours() + ':' + today.getMinutes() + '-' + today.getSeconds()
        var DueDate = '2017-11-20 19:00:00'
        var Summary = "This is a summary"
        var Description = "This is a description"
        var isPin = false

        //get User Id
        
        //call as JSON object
        var JSONObj = {
            CreatedDate: CreatedDate,
            DueDate: DueDate,
            Summary: Summary,
            Description: Description,
            IsPin: isPin,
            UserId: $scope.userId
        }
        
    }

    /*Edit button (will be merged with add later on)*/
}])

