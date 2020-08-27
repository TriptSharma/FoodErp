app.controller("customerController", function ($scope, $http, $log) {
    $http.get("https://localhost:44345/api/customers")
        .then(function (response) {
            $scope.customers = response.data;
            $log.info;
        }, function (response) {
                $scope.error = response.data;
                $log.info;
            }
        );

    $scope.create = function () {
        $http({
            method: "POST",
            url: "https://localhost:44345/api/customers",
            data: $scope.newCustomer
            }).success(function (response) {
                $scope.register = null;
                $log.info;
            }).error(function(response) {
                $scope.error = response.data;
                $log.info;
            })
            
    };

});