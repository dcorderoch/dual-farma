(function () {
    'use strict';

    angular
        .module('app')   // pertenece a app y se llama PedidoService
        .factory('PedidoService', PedidoService);  //Una factory es un servicio que tiene valores de retorno

    PedidoService.$inject = ['$http'];
    function PedidoService($http) {       //se inyecta http para rest.
        var service = {};   //variable de servicio es un objeto js y se retorna en las funciones 

        service.GetAll = GetAll;  
        service.Create = Create;      //funcion de getall esto se usa para poder usarlo en los controladores.
        service.CreateP = CreateP;  //igual para estos

        return service;      //es una fabrica entonces se retorna el service.

        function GetAll(boID) {
            var request=$http({
                method:"post",       //esto no se ocupa en el caso de clientes.
                url: "http://farma-tica.azurewebsites.net/Pedido/GetAllByBranchOffice",
                data: boID      //mejor tipo de rest, hacer de esta forma los necesarios
            });
                return request;
        };

        function Create(theOrder) {  //MEJOR HACER LOS rest de esta forma
            var request =$http({ //Se crea un pedido sin prescripcion
            method: "post",//tipo metodo, se puede usar get o put
            url: "http://farma-tica.azurewebsites.net/api/Pedido/Create",
            data: theOrder          //parametros que pide el rest api
        });
            return request;
        };

        function CreateP(theOrder) {        
            var request = $http({
                method:"post",
                url:  "http://farma-tica.azurewebsites.net/api/Pedido/CreateWPrescription",
                dara: theOrder

            });
            return request;
        };
        // private functions

        function handleSuccess(res) {
            return res.data;
        }
 //manejo de errores
        function handleError(error) {
            return function () {
                return { success: false, message: error };
            };
        }
    }

})();

