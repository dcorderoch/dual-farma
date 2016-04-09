(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$location', 'AuthenticationService', 'FlashService','ClientService','$scope','$http'];
    function LoginController($location, AuthenticationService, FlashService, ClientService,$scope,$http) {
        var vm = this;

        vm.login = login;

        (function initController() {
            // reset login status
            AuthenticationService.ClearCredentials();
        })();


        function login(){
            vm.dataLoading = true;
            $http.post('http://farma-tica.azurewebsites.net/api/Client/Login', { cID: vm.cID, pPass: vm.pPass })
                .then(function(response) {
                    AuthenticationService.SetCredentials(vm.cID, vm.pPass);
                    $location.path('/');
                }, function(response) {
                    FlashService.Error(response.message);
                    vm.dataLoading = false;  
            });
        }
    }

})();
