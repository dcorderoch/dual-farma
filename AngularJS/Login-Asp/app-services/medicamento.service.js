(function () {
    'use strict';

    angular
        .module('app')
        .factory('MedicamentoService', MedicamentoService);

    MedicamentoService.$inject = ['$http'];
    function MedicamentoService($http) {
        var service = {};

        service.GetAll = GetAll;
        service.Create = Create;
        service.Update = Update;
        service.Delete = Delete;

        return service;

        function GetAll() {
            return $http.get("http://farma-tica.azurewebsites.net/api/Medicine/AllMeds").then(handleSuccess, handleError('Error obteniendo los pedidos por cadena'));
        }

        function Create(newMed) {
            return $http.post("http://farma-tica.azurewebsites.net/Pedido/Create", newMed).then(handleSuccess, handleError('Error creando pedido'));
        
        }

        function Update(newMed) {
            return $http.put('http://farma-tica.azurewebsites.net/home/Medicine/Update', newMed).then(handleSuccess, handleError('Error actualizando pedido'));
        }

        function Delete(medToDel) {
            return $http.delete('http://farma-tica.azurewebsites.net//home/Medicine/Delete',medToDel).then(handleSuccess, handleError('Error eliminando pedido'));
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
