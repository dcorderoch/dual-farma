(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$location', 'AuthenticationService', 'FlashService','UserService','$scope','$http', '$rootScope'];
    function LoginController($location, AuthenticationService, FlashService, UserService,$scope,$http,$rootScope) {
        var vm = this;

        vm.login = login;
        vm.userInfo=[];

        (function initController() {
            // reset login status
            AuthenticationService.ClearCredentials();
        }) ();

        function login() {
            vm.dataLoading = true;
            $http.post("http://farma-tica.azurewebsites.net/api/Login/Login", { ID: vm.ID, Pass: vm.Pass })
                .then(function(response)  {
                    AuthenticationService.SetCredentials(vm.ID, vm.Pass);
                    vm.userInfo = response.data; 
                    $rootScope.company = vm.userInfo[6];
                    if ( vm.userInfo[7] ==="2"){
                        $location.path('/stats');
                    }
                    else{
                        $location.path('/');
                    }   
                }, 
                function(response) {             
                  FlashService.Error(response.message);
                    vm.dataLoading = false;   
                });
            }

        }
}) ();
