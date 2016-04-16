(function () {
    'use strict';

    angular
        .module('app')
        .controller('MedicamentoController', MedicamentoController);

    MedicamentoController.$inject = ['MedicamentoService', '$rootScope','$scope'];
    function MedicamentoController(MedicamentoService, $rootScope, $scope) {
        var vm = this;

        //CRUD
        $scope.allMedicamentos = []; //Read
        $scope.deleteMedicamento = deleteMedicamento; //Delete
        $scope.editMedicamento = editMedicamento;   //Update
        $scope.createMedicamento= createMedicamento;    //Create
        
        //Crear
         $scope.mostrarCrear = false;
        $scope.newMedicamento={};
        
        //Editar
        $scope.editarMedicamento ={};
        $scope.mostrarEditar = false;
        $scope.medicamentoEditID;
        
        $scope.toggle = function() {
        
            $scope.mostrarCrear = !$scope.mostrarCrear;
        };
        $scope.toggle2 = function(medID){
            
            $scope.mostrarEditar = !$scope.mostrarEditar;
            $scope.medicamentoEditID = medID;
            console.log($scope.medicamentoEditID);
        }
        
        initController();

        function initController() {
            loadAllMedicamentos();
            console.log($scope.allMedicamentos);
        }


        function loadAllMedicamentos() {
            MedicamentoService.GetAll()
                .then(function (medicamentos) {
                    $scope.allMedicamentos = medicamentos.data;
                console.log($scope.allMedicamentos);
                });
        }

        function deleteMedicamento(id) {
            console.log(id);
            MedicamentoService.Delete(id)
            .then(function () {
                loadAllMedicamentos();
            });
        }
        
        function editMedicamento(medicamento){
            $scope.editarMedicamento.medID = $scope.medicamentoEditID;
            $scope.toggle2();
            console.log(medicamento);
            console.log($scope.medicamentoEditID);
            console.log($scope.editarMedicamento);
            MedicamentoService.Update($scope.editarMedicamento)
                .then(function() {
                    loadAllMedicamentos();  
                $scope.editarMedicamento = {};
                console.log($scope.editarMedicamento);
            });
        }
        
        function createMedicamento(){
            
            $scope.toggle();
            console.log($scope.newMedicamento);
            MedicamentoService.Create($scope.newMedicamento)
                .then(function() {
                    loadAllMedicamentos();
                    $scope.newMedicamento ={};   
            });
        }
        
    }

})();