
'use strict'
// Configure routes for App
app.config(function ($routeProvider, $locationProvider) {
    // Added line to remove ! mark from url
    $locationProvider.hashPrefix('');
    $routeProvider

        // Routes for the Date and Email
        .when('/', {
            templateUrl: 'Assessment/DateDifference/dateDifference.html',
            controller: 'dateController'
        })
        .when('/emailProvider', {
            templateUrl: 'Assessment/EmailProvider/emailProvider.html',
            controller: 'emailProviderController'
        })
});