/// <reference path="E:\Research\Interview\TechnicalAssignment\TechnicalAssignment\Scripts/angular.js" />

'use strict'

// Controller for Date operations
var dateController = function ($scope, $http) {

    // Scope variables
    $scope.date = {
        fromDate: "",
        toDate: ""
    }

    // Error scope details
    $scope.error = {
        hasError: "",
        errorMessage: ""
    }

    // Scope to hold response
    $scope.response = {
        days: "",
        showResponse: false
    }

    $scope.showSpinner = false;

    // Function to calculate difference in dates		
    $scope.calculateDateDiff = function (isValid) {

        // Remove error details and response obj
        setErrorDetails(false, "");
        setResponseDetails("", "", false);

        // Check whether the form is valid, if yes do service call and calculate difference in dates
        if (isValid) {

            // Show Spinner
            showHideSpinner(true);

            // Request model
            var requestModel = {
                FromDate: constructDateObject($scope.date.fromDate),
                ToDate: constructDateObject($scope.date.toDate)
            };

            // Call service to get the amount in words
            $http.post('/api/Date/GetDateDifference', requestModel).then(successCallback, errorCallback);
        }
        else {
            setErrorDetails(true, "Please fill all required details and correct validation errors if any.");
        }
    };

    // Success Callback
    var successCallback = function (response) {

        // Set success and error flag to false
        setErrorDetails(false, "");

        // Set Response object
        if (response != null || response != "") {
            setResponseDetails(response.data, true);
        }

        // Hide Spinner
        showHideSpinner(false);
    }

    // Error Callback
    var errorCallback = function (response) {
        // Set error details
        setErrorDetails(true, "Oops!! Exception occurred while calculating Date difference.");
        setResponseDetails("", false);

        // Hide Spinner
        showHideSpinner(false);
    }

    // Function to handle spinner functionality
    var showHideSpinner = function (flag) {
        $scope.showSpinner = flag;
    };

    // Function to set Error details
    var setErrorDetails = function (flag, message) {
        $scope.error.hasError = flag;
        $scope.error.errorMessage = message;
    }

    // Function to set Response details object
    var setResponseDetails = function (data, showResponse) {
        $scope.response.days = data;
        $scope.response.showResponse = showResponse;
    }

    // Function to construct date object
    var constructDateObject = function (date) {
        // Split the date string
        var dateArray = date.split("/");

        // If date array is not undefined create date object and return
        if (dateArray != undefined) {
            var dateObj = {
                day: dateArray[0],
                month: dateArray[1],
                year: dateArray[2]
            }
        }
    
        // Date object
        return dateObj;
    }
}

// Register Controller
app.controller('dateController', dateController);
