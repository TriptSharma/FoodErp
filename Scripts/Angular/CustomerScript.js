app.controller("customerController", function ($scope, $http, $log) {

    $scope.getCustomer = function () {

        $http.get("https://localhost:44345/api/customers")
            .then(function (response) {
                $scope.customers = response.data;
                $log.info;
            }, function (response) {
                $scope.error = response.data;
                $log.info;
            }
            );

    }

    $scope.create = function () {
        $http({
            method: "POST",
            url: "https://localhost:44345/api/customers",
            data: $scope.newCustomer
        }).success(function (response) {
            $scope.getCustomer();                
                $log.info;
            }).error(function(response) {
                $scope.error = response.data;
                $log.info;
            })
            
    };

    $scope.delete = function (id) {
        $http.delete("https://localhost:44345/api/customers/" + id)
            .success(function (response) {
                $scope.getCustomer();
                $log.info;
            }).error(function (response) {
                $log.info;
            })
    };

    $scope.getCustomer();

});