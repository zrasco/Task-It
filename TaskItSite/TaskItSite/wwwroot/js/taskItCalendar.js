/**
* TaskIt Calendar application
* Exclusively the current user's calendar
*/
var calendarDemoApp = angular.module('TaskItApp', ['ui.calendar', 'ui.bootstrap']);

//controller to pull information from the database
calendarDemoApp.controller('CalendarCtrl', ['$scope', '$http', 'uiCalendarConfig', function ($scope, $http, uiCalendarConfig, $sce, $sanitize) {
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

    //Edit flag
    $scope.editFlag = false;

    //go through each event and set calendar location for each
    $scope.events.slice(0, $scope.tasks.length);

    //push personal events onto the actual calendar
    angular.forEach($scope.tasks, function (value) {
        var newEndDate = new Date(value.dueDate);
        newEndDate.setDate(newEndDate.getDate() + 1);

        var event = {
            title: value.summary,
            description: value.description,
            start: new Date(value.startDate),
            end: new Date(newEndDate),
            allDay: true,
            id: value.id,
            isPrivate: value.isPrivate
        };
        if (event.isPrivate) event.className = 'privateEvent';

        $scope.events.push(event);
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
            eventClick: function (date, event) {
                console.log(date.id + ' was clicked');
                editEvent(date.id);

            }
        }
    };

    /*push data to calendar*/
    $scope.eventSources = [$scope.events]; 
}])

//jQuery event for creating an event
//called only on button is clicked while 
//calendar is in view 
function createEvent () {
    console.log("createEvent called")
    var $detailDiv = $('#calPage'),
        url = "/Tasks/GetCreate";

    $.get(url, function (data) {
        $detailDiv.replaceWith(data);
    }).fail(function (error) {
        console.log(error)
    });
}

//jQuery event for editting the event
//used for personal events exclusively
function editEvent(id) {
    console.log("editEvent called " + id)
    var $detailDiv = $('#calPage'),
        url = "/Tasks/GetEdit/" + id;

    $.get(url, function (data) {
        $detailDiv.replaceWith(data);
    }).fail(function (error) {
        console.log(error)
        });
    
}

//jQuery event for detailing the event
//used  for non-personal event 
function detailEvent(id) {
    console.log("detailEvent called")
    var $detailDiv = $('#calPage'),
        url = "/Tasks/GetDetails",
        data = { id: id };

    $.get(url, data, function (data) {
        $detailDiv.replaceWith(data);
    }).fail(function (error) {
        console.log(error)
    });
}

//jQuery call for deleting the event
function deleteEvent(id) {
    console.log("deleteEvent called")
    var $detailDiv = $('#calPage'),
        url = "/Tasks/GetDelete";

    $.get(url, function (data) {
        $detailDiv.replaceWith(data);
    }).fail(function (error) {
        console.log(error)
    });
}
