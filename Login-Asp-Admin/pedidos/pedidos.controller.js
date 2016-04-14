(function () {
    'use strict';

    angular 
        .module('app')      //modulo a donde pertenece
        .controller('PedidosController', PedidosController); //nombre controlador

    PedidosController.$inject = ['$location', 'FlashService','$scope','$http', '$rootScope'];
    function PedidosController($location, FlashService ,$scope,$http,$rootScope) {
        var vm = this;

        vm.pedidosV = pedidosV;
        vm.datosPedidos = [];

        (function initController() {        // el init por los parentesis de abajo
            // reset login status           //se autollama
            pedidosV();
        }) ();


        function pedidosV()  {      //esta funcion se encarga del rest de ver pedidos
                console.log($rootScope.sucursal );                                          //esta es una variable global
            $http.post("http://farma-tica.azurewebsites.net/api/Pedido/GetAllByBranchOffice", {  boID: $rootScope.sucursal  })
                .then(function(response)  {         //notar si hay un then o success en el rest     //se obtuvo en login
                    vm.datosPedidos = response.data;  //se guarda los valores
                }, 
                function(response) {                //si no funciono
                  FlashService.Error(response.message);
                    vm.dataLoading = false;   
                });
            }

        }
}) ();
