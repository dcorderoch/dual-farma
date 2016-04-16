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

        $scope.salesPerCompany = [];    //Estas funciones estan disponibles en las vistas con vm. ...
        $scope.newSales=[];
        $scope.TotSalesPerComp={};
        $scope.globalsSales=[];

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
                    $scope.salesPerCompany = response.data;  //se obtiene los datos del rest
                         console.log(" sales per comp ");
                    console.log($scope.salesPerCompany);    //se imprime en consola para debug
                });
        }

        function loadNewCompany( Company ) {
            StatsService.GetNewCompany(Company)
                .then(function (response) {
                    $scope.newSales = response.data;    //igual al de arriba pero para new sales
                         console.log("new sales ");
                    console.log($scope.newSales);
                });
        }
        function loadTotCompany( Company ) {
            StatsService.GetTotCompany(Company)         //igual pero para tot sales per company
                .then(function (response) {
                    $scope.TotSalesPerComp = response.data;
                console.log("total sales per comp ");
                     console.log($scope.TotSalesPerComp);
                });
        }
        function loadGlobalSales() {
            StatsService.GetGlobalSales()
                .then(function (response) {     //igual pero para  global sales
                    $scope.globalsSales = response.data;
                         console.log("global sales ");
                    console.log($scope.globalsSales);
                });
        }
    }    
} ) ();