(function () {
    'use strict';

    angular
        .module('app')
        .factory('UserService',UserService);//servicio para el usuario

   UserService.$inject = ['$http'];
    function UserService($http) {
        
        var service = {}; //de esta forma se pueden utilizar en los controladores

        return service;

        service.Create = Create;//diferentes funciones que tiene el servicio

        function Create(login) {
            var request = $http({   //esta no se utiliza  
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
