(function () {
    'use strict';

    angular   ///modulo al que pertenece
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['PedidoService', '$rootScope', 'FlashService', '$scope'];
    function HomeController(PedidoService,$rootScope, FlashService, $scope) {
        $scope.pedido={};

        var vm = this;
        vm.createPedido= createPedido;  //variables locales y para vistas
        vm.createWPedido = createWPedido;

        function createPedido(){   //pedido sin prescripcion
            console.log(vm.pedido);
            vm.dataLoading=true;      //se muestra un data loading mientras se hace el pedido
            PedidoService.Create(vm.pedido)
            .then(function(response) {
                if ( response.success){
                    FlashService.Success('Pedido Creado',true);  //si funco se muestra un approve
                } else{
                    FlashService.Error(response.message);
                    vm.dataLoading= false;
                }
            });
        }

        function createWPedido(){
            $scope.pedido.prescriptionImage = $scope.pedido.prescriptionImage.base64;
            console.log(vm);
            vm.dataLoading=true;   //pedido sin prescripcion
            console.log($scope.pedido);
            PedidoService.CreateP($scope.pedido)
            .then(function(response) {  //notar el .then es diferente al .success
                if ( response.success){
                    FlashService.Success('Pedido Creado',true);
                } else{
                    FlashService.Error(response.message); //manejo errpres
                    vm.dataLoading= false;
                }
            });
        }
        function add(){


        }
     }
} ) ();  // La funcion se auto llama
///importante no se pueden hacer dos pedidos (con y sin prescripcion al mismo tiempo)
//
//
//