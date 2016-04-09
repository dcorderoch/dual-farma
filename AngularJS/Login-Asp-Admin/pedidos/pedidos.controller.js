(function () {
    'use strict';

    angular
        .module('app')
        .controller('PedidosController', PedidosController);

    PedidosController.$inject = ['$location', 'FlashService','$scope','$http', '$rootScope'];
    function PedidosController($location, FlashService ,$scope,$http,$rootScope) {
        var vm = this;

        vm.pedidosV = pedidosV;
        vm.datosPedidos = [];

        (function initController() {
            // reset login status
            pedidosV();
        }) ();


        function pedidosV()  {
                console.log($rootScope.sucursal );
            $http.post("http://farma-tica.azurewebsites.net/api/Pedido/GetAllByBranchOffice", {  boID: $rootScope.sucursal  })
                .then(function(response)  {
                    vm.datosPedidos = response.data;  
                }, 
                function(response) {             
                  FlashService.Error(response.message);
                    vm.dataLoading = false;   
                });
            }

        }
}) ();
