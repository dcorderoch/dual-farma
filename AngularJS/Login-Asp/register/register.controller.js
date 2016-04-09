(function () {
    'use strict';

    angular
        .module('app')
        .controller('RegisterController', RegisterController);

    RegisterController.$inject = ['ClientService', '$location', '$rootScope', 'FlashService'];
    function RegisterController(ClientService, $location, $rootScope, FlashService) {
        var vm = this;

        vm.register = register;

        function register() {
            vm.dataLoading = true;
            ClientService.Create(vm.client)
                .then(function (response) {
                    if (response.success) {
                        FlashService.Success('Registro exitoso', true);
                        $location.path('/login');
                    } else {
                        FlashService.Error(response.message);
                        vm.dataLoading = false;
                    }
                });
        }
    }

} ) ();
