(function () {
    'use strict';

    angular
        .module('app')   //Pertenece al modulo app de app.js
        .controller('LoginController', LoginController);//Nombre del controlador, se debe poner 2 veces.


//Injeccion de dependencias: Location: para URL, AuthenServ.. $scope se utiliza para variables locales que se pueden 
//usar en la vista tambien. http es lo de rest. 
    LoginController.$inject = ['$location', 'AuthenticationService', 'FlashService','UserService','$scope','$http', '$rootScope'];
    function LoginController($location, AuthenticationService, FlashService, UserService,$scope,$http,$rootScope) {

        var vm = this; //se pueden utilizar objetos y variables de las inyecciones en la vista

        vm.login = login;
        vm.userInfo=[]; //variable para obtener los valores retornados del login, almacenado temporal

        (function initController() {
// Se inicializa el servicio de limpiar credenciales en AuthenticationService
            AuthenticationService.ClearCredentials();
        }) ();//Estos parentesis del final indican que esto se inicia automaticamente

        function login() {
            vm.dataLoading = true;  //Aqui se hace el rest sin llamar a authentication service porque fue mas facil para mi
            $http.post("http://farma-tica.azurewebsites.net/api/Login/Login", { ID: vm.ID, Pass: vm.Pass })
                .then(function(response)  {
                    AuthenticationService.SetCredentials(vm.ID, vm.Pass);
                    vm.userInfo = response.data; 
                    $rootScope.company = vm.userInfo[6];//la compania para una variable global
                    $rootScope.sucursal = vm.userInfo[8];//la sucursal para una variable global
                    console.log($rootScope.sucursal);
                    if ( vm.userInfo[7] ==="2" ){ //se determina si es un gerente o dependiente
                        $location.path('/stats'); //vista de stats
                    }
                    if (vm.userInfo[7] ==="1"){
                        $location.path('/'); //vista de pedidos
                    }  
                    else{
                                          FlashService.Error(response.message);//errores
                    vm.dataLoading = false;  
                    } 
                }, 
                function(response) {             
                  FlashService.Error(response.message);//errores
                    vm.dataLoading = false;   
                });
            }

        }
}) ();
