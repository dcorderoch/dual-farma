(function () {
    'use strict';

    angular
        .module('app')
        .factory('MedicamentoService', MedicamentoService);  //servicio de medicamentos

    MedicamentoService.$inject = ['$http'];
    function MedicamentoService($http) {
        var service = {};

        service.GetAll = GetAll; //las funciones que son expuestas
        service.Create = Create; //todas estas son para crud del mantenimiento
        service.Update = Update; 
        service.Delete = Delete;

        return service;

        function GetAll(SucursalID) {
            var response=$http({
                method:"post",
                url:"http://farma-tica.azurewebsites.net/api/Medicine/AllMeds",
                data :{"mID":SucursalID}
            });
            return response;    
        }
        
        function Create(Medicine) {
            var response=$http({
                method:"post",
                url:"http://farma-tica.azurewebsites.net/api/Medicine/Create",
                data : Medicine
            });
            return response;    
        }
        
        function Update(newMed) {
            var response=$http({
                method:"post",
                url:"http://farma-tica.azurewebsites.net/api/Medicine/Update",
                data : newMed
            });
            return response;    
        }

        function Delete(medicineId) {
            var response=$http({
                method:"post",
                url:"http://farma-tica.azurewebsites.net/api/Medicine/Delete",
                data : {"medicineId":medicineId}
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

        //     function GetAll(boID) {   como este tipo 
        //     var request=$http({
        //         method:"post",
        //         url: "http://farma-tica.azurewebsites.net/Pedido/GetAllByBranchOffice",
        //         data: boID
        //     });
        //         return request;
        // };

})();
