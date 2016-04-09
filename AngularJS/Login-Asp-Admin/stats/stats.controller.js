(function () {
    'use strict';

    angular
        .module('app')
        .controller('StatsController', StatsController);
                        //inyeccion de dependencias 
    StatsController.$inject = ['UserService', 'StatsService','$rootScope','$scope'];
    function StatsController(UserService, StatsService, $rootScope ,$scope) {
        var vm = this;
        //se puede usar las funciones de los padres (dependencias del controlador)

        vm.salesPerCompany = [];    //Estas funciones estan disponibles en las vistas con vm. ...
        vm.newSales=[];
        vm.TotSalesPerComp=[];
        vm.globalsSales=[];

        initController();   // se ejecuta la funciones initcontroller

        function initController() {

            console.log( { compID : $rootScope.company}    );
            loadPerCompany( { compID : $rootScope.company}  );  //rootSccope sirve para menatener variables
            loadNewCompany( { compID : $rootScope.company} );  //globales
            loadTotCompany( { compID : $rootScope.company} );
            loadGlobalSales();
        };

        function loadPerCompany( Company  ) {   
            StatsService.GetPerCompany(Company)
                .then(function (response) {         //se copia el valor a vm.salesPerCompny para accesarlo en vistas
                    vm.salesPerCompany = response.data;  //se obtiene los datos del rest
                    console.log(vm.salesPerCompany);    //se imprime en consola para debug
                });
        }

        function loadNewCompany( Company ) {
            StatsService.GetNewCompany(Company)
                .then(function (response) {
                    vm.newSales = response.data;    //igual al de arriba pero para new sales
                    console.log(vm.newSales);
                });
        }
        function loadTotCompany( Company ) {
            StatsService.GetTotCompany(Company)         //igual pero para tot sales per company
                .then(function (response) {
                    vm.TotSalesPerComp = response.data;
                     console.log(vm.TotSalesPerComp);
                });
        }
        function loadGlobalSales() {
            StatsService.GetGlobalSales()
                .then(function (response) {     //igual pero para  global sales
                    vm.globalsSales = response.data;
                    console.log(vm.globalsSales);
                });
        }
    }    
} ) ();