(function () {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['PedidoService', '$rootScope', 'FlashService'];
    function HomeController(PedidoService,$rootScope, FlashService) {
       
        var vm = this;
        vm.createPedido= createPedido;

        function createPedido(){
            vm.dataLoading=true;
            PedidoService.Create(vm.pedido)
            .then(function(response) {
                if ( response.success){
                    $rootScope.sucursal = vm.pedido.pickupOfficeId;
                    console.log($rootScope.sucursal);
                    FlashService.Success('Pedido Creado',true);
                } else{
                    FlashService.Error(response.message);
                    vm.dataLoading= false;
                }
            });
        }
     }
} ) ();
