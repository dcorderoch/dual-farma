(function () {
    'use strict';

    angular
        .module('app')
        .factory('PedidoService', PedidoService);

    PedidoService.$inject = ['$http'];
    function PedidoService($http) {
        var service = {};

        service.GetAll = GetAll;
        service.Create = Create;
        service.CreateP = CreateP;

        return service;

        function GetAll() {
            return $http.get("http://farma-tica.azurewebsites.net/Pedido/GetAllByBranchOffice").then(handleSuccess, handleError('Error obteniendo los pedidos por cadena'));
        }

        function Create(theOrder) {
            var request =$http({
            method: "post",
            url: "http://farma-tica.azurewebsites.net/api/Pedido/Create",
            data: theOrder         
        });
            return request;
        };

        function CreateP(theOrder) {
            return $http.post("http://farma-tica.azurewebsites.net/Pedido/CreateWPrescription", theOrder).then(handleSuccess, handleError('Error creating pedido'));
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
