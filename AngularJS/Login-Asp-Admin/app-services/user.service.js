(function () {
    'use strict';

    angular
        .module('app')
        .factory('UserService',UserService);

   UserService.$inject = ['$http'];
    function UserService($http) {
        
        var service = {};

        return service;

        service.Create = Create;

        function Create(login) {
            var request = $http({
            method: "post",
            url: "http://farma-tica.azurewebsites.net/api/Login/Login",
            data: login
        });
            return request;
        };
        // private functions

        function handleSuccess(res) {
            return res.data;
        }

        function handleError(error) {
            return function () {
                return { success: false, message: error };
            };
        }
    }

})();
