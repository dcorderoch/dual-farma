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
            var response=$http({
                method:"get",
                url:"http://farma-tica.azurewebsites.net/api/Client/GetAllClients"
            });
            return response;    
        }

        function Create(client) {   //para registrar un cliente
            var request = $http({
            method: "post",
            url: "http://farma-tica.azurewebsites.net/api/Client/New",
            data: client
        });
            return request;
        }

        function Update(client) {
            var request =$http({
                method:"post",
                url: "http://farma-tica.azurewebsites.net/api/Client/Update",
                data: client
            });
                return request;
        }
        
        function Delete(ced){
            var response =$http({
            method:"post",
                url: "http://farma-tica.azurewebsites.net/api/Client/Delete",
                data :{"cID":ced}
            });
            return response;
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
