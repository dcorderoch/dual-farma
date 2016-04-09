(function () {
    'use strict';

    angular
        .module('app')
        .factory('StatsService', StatsService);//servicio para stats

    StatsService.$inject = ['$http'];
    function StatsService($http) {
        var service = {};

        service.GetPerCompany = GetPerCompany;
        service.GetNewCompany = GetNewCompany; //todos los servicios que expone los stats
        service.GetTotCompany = GetTotCompany;  //disponible donde sea que se inyecte el servicio 
        service.GetGlobalSales = GetGlobalSales; //mediante el controlleras = this

        return service;
                    //servicios rest


        function GetPerCompany(CompanyID) {
            var response=$http({
                method: "post",         //obtener el sales per compnay
                url: "http://farma-tica.azurewebsites.net/api/Stats/SalesPerComp",
                data : CompanyID
            });
            return response;
        };


        function GetNewCompany(CompanyID) {
            var response=$http({
            method: "post",         //new sales
            url: "http://farma-tica.azurewebsites.net/api/Stats/NewSales",
            data : CompanyID
        });
        return response;
    };

        function GetTotCompany(CompanyID) {
            var response=$http({
                method:"post",          //slaes per company
                url:"http://farma-tica.azurewebsites.net/api/Stats/TotSalesPerComp",
                data: CompanyID
            });
            return response;
        };


        function GetGlobalSales() {
            var response=$http({        //global saless
                method:"get",
                url: "http://farma-tica.azurewebsites.net/api//Stats/GlobalSales"
            });
            return response;
        };
        // private functions

        function handleSuccess(res) {       //manejo de errores
            return res.data;
        }

        function handleError(error) {
            return function () {
                return { success: false, message: error };
            };
        }
    }

})();
