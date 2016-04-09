(function () {
    'use strict';

    angular
        .module('app')
        .factory('StatsService', StatsService);

    StatsService.$inject = ['$http'];
    function StatsService($http) {
        var service = {};

        service.GetPerCompany = GetPerCompany;
        service.GetNewCompany = GetNewCompany;
        service.GetTotCompany = GetTotCompany;
        service.GetGlobalSales = GetGlobalSales;

        return service;

        function Create(login) {
            var request = $http({
            method: "post",
            url: "http://farma-tica.azurewebsites.net/api/Login/Login",
            data: login
        });
            return request;
        };

        function GetPerCompany(CompanyID) {
            var response=$http({
                method: "post",
                url: "http://farma-tica.azurewebsites.net/api/Stats/SalesPerComp",
                data : CompanyID
            });
            return response;
        };


        function GetNewCompany(CompanyID) {
            var response=$http({
            method: "post",
            url: "http://farma-tica.azurewebsites.net/api/Stats/NewSales",
            data : CompanyID
        });
        return response;
    };

        function GetTotCompany(CompanyID) {
            var response=$http({
                method:"post",
                url:"http://farma-tica.azurewebsites.net/api/Stats/TotSalesPerComp",
                data: CompanyID
            });
            return response;
        };


        function GetGlobalSales() {
            var response=$http({
                method:"get",
                url: "http://farma-tica.azurewebsites.net/api//Stats/GlobalSales"
            });
            return response;
        };
        // private functions

        function handleSuccess(res) {
            return res.data;
        }

        function handleError(error) {
            return function () {
                return { success: false, message: error };
            };
        }
    }

})();
