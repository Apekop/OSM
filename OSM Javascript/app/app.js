var app = angular.module('worldmap', ['ngRoute']);

app.config(function ($routeProvider) {
	$routeProvider.
	when('/', {
		templateUrl : 'app/views/worldmap.html',
		controller : 'worldmapController'
	}).
	when('/worldmap/:manager', {
		templateUrl : 'app/views/worldmap.html',
		controller : 'worldmapController'
	});
});