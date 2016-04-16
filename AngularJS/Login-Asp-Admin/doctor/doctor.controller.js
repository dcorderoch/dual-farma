(function () {
    'use strict';

    angular
        .module('app')
        .controller('DoctorController', DoctorController);

    DoctorController.$inject = ['DoctorService', '$rootScope','$scope'];
    function DoctorController(DoctorService, $rootScope, $scope) {
        var vm = this;

        //CRUD
        $scope.allDoctors = [];
        $scope.deleteDoctor = deleteDoctor;
        $scope.editDoctor = editDoctor;
        $scope.createDoctor= createDoctor;
        
        //Crear
         $scope.mostrarCrear = false;
        $scope.newDoctor={};
        
        //Editar
        $scope.editarDoctor ={};
        $scope.mostrarEditar = false;
        $scope.doctorEditID;
        
        $scope.toggle = function() {
        
            $scope.mostrarCrear = !$scope.mostrarCrear;
        };
        $scope.toggle2 = function(docID){
            
            $scope.mostrarEditar = !$scope.mostrarEditar;
            $scope.doctorEditID = docID;
            console.log($scope.doctorEditID);
        }
        
        initController();

        function initController() {
            loadAllDoctors();
            console.log($scope.alldoctors);
        }


        function loadAllDoctors() {
            DoctorService.GetAll()
                .then(function (doctors) {
                    $scope.allDoctors = doctors.data;
                console.log($scope.allDoctors);
                });
        }

        function deleteDoctor(id) {
            console.log(id);
            DoctorService.Delete(id)
            .then(function () {
                loadAllDoctors();
            });
        }
        
        function editDoctor(doctor){
            $scope.editarDoctor.DoctorId = $scope.doctorEditID;
            $scope.toggle2();
            console.log(doctor);
            console.log($scope.doctorEditID);
            console.log($scope.editarDoctor);
            DoctorService.Update($scope.editarDoctor)
                .then(function() {
                    loadAllDoctors();  
                $scope.editarDoctor = {};
                console.log($scope.editarDoctor);
            });
        }
        
        function createDoctor(){
            
            $scope.toggle();
            console.log($scope.newDoctor);
            DoctorService.Create($scope.newDoctor)
                .then(function() {
                    loadAllDoctors();
                    $scope.newDoctor ={};   
            });
        }
        
    }

})();