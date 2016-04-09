(function () {
    'use strict';

    angular
        .module('app')  //El modulo al que pertenece todo
        .controller('RegisterController', RegisterController); //este es un controller
                    //ClientService provee el servicio de rest, rootScope son para variables globales
                    //FlashService es para mensajes para el usuario de alerta y demas
    RegisterController.$inject = ['ClientService', '$location', '$rootScope', 'FlashService']; //la injeccion de dependencias
    function RegisterController(ClientService, $location, $rootScope, FlashService) {
      
        var vm = this; //vm es el controllerAS que se definio en app.js, se puede usar vm. 
                                    //de cualquier servicio injectado en la vista

        vm.register = register; //se puede usar vm.register en la vista

        function register() {
            vm.dataLoading = true;      //Parte de FlashService
            ClientService.Create(vm.client) //Se llama al servicio ClientService para el rest
                .then(function (response) {     //Si funciono entonces, 
                    if (response.success) {
                        FlashService.Success('Registro exitoso', true);// Un mensaje al usuario
                        $location.path('/login');   //Lo devuleve a la pagina de login
                    } else {        //Sino funco
                        FlashService.Error(response.message);
                        vm.dataLoading = false;
                    }
                });
        }
    }

} ) ();
