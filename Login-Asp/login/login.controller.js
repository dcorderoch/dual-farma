(function () {
    'use strict';

    angular
        .module('app')          //Pertenece al modulo app de app.js
        .controller('LoginController', LoginController);    //Nombre del controlador, se debe poner 2 veces.

//Injeccion de dependencias: Location: para URL, AuthenServ.. $scope se utiliza para variables locales que se pueden 
//usar en la vista tambien. http es lo de rest. 
//
    LoginController.$inject = ['$location', 'AuthenticationService', 'FlashService','ClientService','$scope','$http'];  
    function LoginController($location, AuthenticationService, FlashService, ClientService,$scope,$http) {
        
        var vm = this; //se pueden utilizar objetos y variables de las inyecciones en la vista

        vm.login = login; //se puede usar la funcion login en la vista

        (function initController() {
            // Se inicializa el servicio de limpiar credenciales en AuthenticationService
            AuthenticationService.ClearCredentials();
        })();//Estos parentesis del final indican que esto se inicia automaticamente


        function login(){
            vm.dataLoading = true; //Aqui se hace el rest sin llamar a authentication service porque fue mas facil para mi
            $http.post('http://farma-tica.azurewebsites.net/api/Client/Login', { cID: vm.cID, pPass: vm.pPass })
                .then(function(response) {
                    AuthenticationService.SetCredentials(vm.cID, vm.pPass); //Se agregan las credenciales del usuario
                    $location.path('/'); //Se envia a home
                }, function(response) {// es como un else
                    FlashService.Error(response.message);
                    vm.dataLoading = false;  //No funciono
            });
        }
    }

})();
