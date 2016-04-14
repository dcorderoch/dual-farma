(function () {
    'use strict';

    angular
        .module('app')// pertenece a app y se llama PedidoService
        .factory('PedidoService', PedidoService); //Una factory es un servicio que tiene valores de retorno

    PedidoService.$inject = ['$http']; //se inyecta http para rest.
    function PedidoService($http) {
        
        var service = {}; //variable de servicio es un objeto js y se retorna en las funciones 

        service.GetAll = GetAll; //funcion de getall esto se usa para poder usarlo en los controladores.
        service.Create = Create; //igual para estos
        service.CreateP = CreateP;

        return service; //es una fabrica entonces se retorna el service.

        function GetAll() {     //esto no se ocupa en el caso de clientes.
            return $http.get("http://farma-tica.azurewebsites.net/Pedido/GetAllByBranchOffice").then(handleSuccess, handleError('Error obteniendo los pedidos por cadena'));
        }

        function Create(theOrder) {//MEJOR HACER LOS rest de esta forma
            var request =$http({    //Se crea un pedido sin prescripcion
            method: "post", //tipo metodo, se puede usar get o put
            url: "http://farma-tica.azurewebsites.net/api/Pedido/Create",
            data: theOrder         //parametros que pide el rest api
        });
            return request; //el response
        };

        function CreateP(theOrder) {        //Falta esta parte hacerlo bien
            var request = $http({
                method:"post",
                url:"http://farma-tica.azurewebsites.net/Pedido/CreateWPrescription",
                dara: theOrder

            });
            return request;
        };
        // private functions

        function handleSuccess(res) {   //manejo de errores
            return res.data;
        }

        function handleError(error) {
            return function () {
                return { success: false, message: error };
            };
        }
    }

})();
