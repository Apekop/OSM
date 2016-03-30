app.service('worldmapService', ['$http', function($http) {
	this.getCountries = function(manager) {
		return $http ({
			method: "get",
			url: "http://localhost:51009/api/Worldmap?managerID="+manager
		});
	};
}]);