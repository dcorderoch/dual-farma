(function () {
    'use strict';

    angular
        .module('app')
        .controller('StatsController', StatsController);

    StatsController.$inject = ['UserService', 'StatsService','$rootScope','$scope'];
    function StatsController(UserService, StatsService, $rootScope ,$scope) {
        var vm = this;


        vm.salesPerCompany = [];
        vm.newSales=[];
        vm.TotSalesPerComp=[];
        vm.globalsSales=[];

        initController();

        function initController() {

            console.log( { compID : $rootScope.company}    );
            loadPerCompany( { compID : $rootScope.company}  );
            loadNewCompany( { compID : $rootScope.company} );
            loadTotCompany( { compID : $rootScope.company} );
            loadGlobalSales();
        };

        function loadPerCompany( Company  ) {
            StatsService.GetPerCompany(Company)
                .then(function (response) {
                    vm.salesPerCompany = response.data;
                    console.log(vm.salesPerCompany);
                });
        }

        function loadNewCompany( Company ) {
            StatsService.GetNewCompany(Company)
                .then(function (response) {
                    vm.newSales = response.data;
                    console.log(vm.newSales);
                });
        }
        function loadTotCompany( Company ) {
            StatsService.GetTotCompany(Company)
                .then(function (response) {
                    vm.TotSalesPerComp = response.data;
                     console.log(vm.TotSalesPerComp);
                });
        }
        function loadGlobalSales() {
            StatsService.GetGlobalSales()
                .then(function (response) {
                    vm.globalsSales = response.data;
                    console.log(vm.globalsSales);
                });
        }
    }    
} ) ();