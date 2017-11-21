/**
 * TaskIt Calendar application
 * Exclusively the current user's calendar
 */
var calendarDemoApp = angular.module('TaskItApp', ['ui.calendar', 'ui.bootstrap']);

//controller to pull information from the database 
calendarDemoApp.controller('CalendarCtrl', ['$scope', '$http', 'uiCalendarConfig', function ($scope, $http, uiCalendarConfig) {

    $scope.init = function (id) {

        $scope.userId = id
        $scope.test = "tentative test"
        console.log(id)
    }
    //selected calendar
    $scope.SelectedEvent = null;
    var isFirstTime = true;

    //list of displayed events
    $scope.events = [];
    $scope.eventSources = [$scope.events];

    //Load events from server
    /*
    $http.post('Tasks/Details/5', {
        cache: true,
        params: {}
    }).then(function (data) {
        $scope.events.slice(0, $scope.events.length);
        //for each event personal event, call data
        angular.forEach(data.data, function (value) {
            $scope.events.push({
                ApplicationUser: value.ApplicationUser,
                CreatedDate: value.CreatedDate,
                Description: value.Description,
                DueDate: value.DueDate,
                IsPin: value.IsPin,
                Summary: value.Summary,
                UserId: value.UserId

            });
        //for each subscribed calendar, call data
        });
        });
    */
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
        
        //push events to database
        console.log("button clicked")
        //ajax post call
        $.ajax({
            type: 'POST',
            url: '../Controllers/TasksController/Tasks/Create',
            data: JSON.stringify(JSONObj),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        })
    }

    /*Edit button (will be merged with add later on)*/
}])

