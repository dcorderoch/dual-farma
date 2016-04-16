(function () {
    'use strict';

    angular
        .module('app')
        .controller('ClientController', ClientController);

    ClientController.$inject = ['ClientService', '$rootScope','$scope'];
    function ClientController(ClientService, $rootScope, $scope) {
        var vm = this;

        //CRUD
        $scope.allClients = [];
        $scope.deleteClient = deleteClient;
        $scope.editClient = editClient;
        $scope.createClient= createClient;
        
        //Crear
         $scope.mostrarCrear = false;
        $scope.newClient={};
        
        //Editar
        $scope.editarClient ={};
        $scope.mostrarEditar = false;
        $scope.clienteEditID;
        
        $scope.toggle = function() {
        
            $scope.mostrarCrear = !$scope.mostrarCrear;
        };
        $scope.toggle2 = function(numCed){
            
            $scope.mostrarEditar = !$scope.mostrarEditar;
            $scope.clienteEditID = numCed;
            console.log($scope.clienteEditID);
        }
        
        initController();

        function initController() {
            loadAllClients();
            console.log($scope.allClients);
        }


        function loadAllClients() {
            ClientService.GetAll()
                .then(function (clients) {
                    $scope.allClients = clients.data;
                console.log($scope.allClients);
                });
        }

        function deleteClient(id) {
            ClientService.Delete(id)
            .then(function () {
                loadAllClients();
            });
        }
        
        function editClient(){
            $scope.editarClient.NumCed = $scope.clienteEditID
            $scope.toggle2();
            ClientService.Update($scope.editarClient)
                .then(function() {
                    loadAllClients();  
                $scope.editarClient = {};
                console.log($scope.editarClient);
            });
        }
        
        function createClient(){
            
            $scope.toggle();
            console.log($scope.newClient);
            ClientService.Create($scope.newClient)
                .then(function() {
                    loadAllClients();
                    $scope.newClient ={};   
            });
        }
        
    }

})();