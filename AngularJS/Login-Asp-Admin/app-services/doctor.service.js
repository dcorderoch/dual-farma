(function () {
    'use strict';

    angular
        .module('app')          //servicio para el cliente
        .factory('DoctorService', DoctorService);

    DoctorService.$inject = ['$http'];
    function DoctorService($http) {
        var service = {};

        service.GetAll = GetAll; //diferentes funciones que tiene el servicio
        service.Create = Create;    //de esta forma se pueden utilizar en los controladores
        service.Update = Update;
        service.Delete = Delete;

        return service;

        function GetAll() {
            var response=$http({
                method:"get",
                url:"http://farma-tica.azurewebsites.net/api/Doctor/GetAll"
            });
            return response;    
        }

        function Create(doctor) {   //para registrar un cliente
            var request = $http({
            method: "post",
            url: "http://farma-tica.azurewebsites.net/api/Doctor/Create",
            data: doctor
        });
            return request;
        }

        function Update(doctor) {
            var request =$http({
                method:"post",
                url: "http://farma-tica.azurewebsites.net/api/Doctor/Update",
                data: doctor
            });
                return request;
        }
        
        function Delete(docID){
            var response =$http({
            method:"post",
                url: "http://farma-tica.azurewebsites.net/api/Doctor/Delete",
                data :{"docID":docID}
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
