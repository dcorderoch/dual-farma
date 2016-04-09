(function () {
    'use strict';

    angular
        .module('app')          //servicio para el cliente
        .factory('ClientService', ClientService);

    ClientService.$inject = ['$http'];
    function ClientService($http) {
        var service = {};

        service.GetAll = GetAll; //diferentes funciones que tiene el servicio
        service.Create = Create;    //de esta forma se pueden utilizar en los controladores
        service.Update = Update;
        service.Delete = Delete;

        return service;

        function GetAll() {
            return $http.get("http://farma-tica.azurewebsites.net/api/client/getallclients")
            .then(handleSuccess, handleError('Error getting all clients'));  //esta no se utiliza  
        }

        function Create(client) {   //para registrar un cliente
            var request = $http({
            method: "post",
            url: "http://farma-tica.azurewebsites.net/api/client/new",
            data: client
        });
            return request;
        };

        function Update(client) {
            return $http.put('http://farma-tica.azurewebsites.net/api/Client/Update' , client)
            .then(handleSuccess, handleError('Error updating client')); //importante para el crud
        }

        function Delete(client) {
            return $http.delete('http://farma-tica.azurewebsites.net/api/Client/Delete' ,client)
            .then(handleSuccess, handleError('Error deleting client')); //importante para el crud
        }

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
