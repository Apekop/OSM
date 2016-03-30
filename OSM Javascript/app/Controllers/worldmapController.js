app.controller('worldmapController', ['$scope', '$routeParams', 'worldmapService', function($scope, $routeParams, worldmapService) {
	 
	$scope.getWorldmap = function() {
		console.log('bla' + $routeParams.manager);
		worldmapService.getCountries($routeParams.manager).success(function(data) {
			$scope.countries = data;
		});
	};
	if($routeParams.manager) {
		console.log('waarde van Manager = ' + $routeParams.manager);
		$scope.getWorldmap();
	}
}
]);