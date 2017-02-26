/// <reference path="E:\Research\Interview\TechnicalAssignment\TechnicalAssignment\Scripts/angular.js" />

'use strict'

// Controller for Email provider
var emailProviderController = function ($scope, $http) {

    // Scope variables
    $scope.user = {
        name: "",
        email: ""
    }

    // Error scope details
    $scope.error = {
        hasError: "",
        errorMessage: ""
    }

    // Scope to hold response
    $scope.response = {
        status: "",
        showResponse: false
    }

    $scope.showSpinner = false;

    // Function to send email to user		
    $scope.sendEmail = function (isValid) {

        // Remove error details and response obj
        setErrorDetails(false, "");
        setResponseDetails("", "", false);

        // Check whether the form is valid, if yes do service call and send email
        if (isValid) {

            // Show Spinner
            showHideSpinner(true);

            // Request model
            var requestModel = {
                name: $scope.user.name,
                email: $scope.user.email
            };

            // Call service to send email
            $http.post('/api/Email/SendEmail', requestModel).then(successCallback, errorCallback);
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
        setErrorDetails(true, "Oops!! Exception occurred while sending email.");
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
        $scope.response.status = data;
        $scope.response.showResponse = showResponse;
    }
}

// Register Controller
app.controller('emailProviderController', emailProviderController);