var app = angular
        .module("CustomerApp", [])
        .controller("customerController", function ($scope, $http, $log) {
            $http.get("https://localhost:44345/api/customers")
                .then(function (response) {
                    $scope.customers = response.data;
                    $log.info;
                },
                    function (response) {
                        $scope.error = response.data;
                        $log.info;
                    }
                )
});