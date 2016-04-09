(function () {
    'use strict';

    angular         //El modulo principal del proyecto
        .module('app', ['ngRoute', 'ngCookies'])    //se llama a ngRoute (routeo entre paginas, y ngCookies para cookies)
        .config(config) //funcion de config se llama de primero, no hay que cambiarlo
        .run(run);      //la funcion run se llama de primero tambien, no hay que cambiarlo

    config.$inject = ['$routeProvider', '$locationProvider']; //Inyectar dependencias de routeProvider y locationProvider 
    function config($routeProvider, $locationProvider) {
        $routeProvider
            .when('/', {                //esta es la extension de url que sucede cuando se esta en el HomeController
                controller: 'HomeController',   //El controlador de este when home.controller.js
                templateUrl: 'home/home.view.html',     //Vista es el archivo home/home.view.html
                controllerAs: 'vm'                      //un alias del controlador, vm significa view model
            })

            .when('/login', {
                controller: 'LoginController',
                templateUrl: 'login/login.view.html',
                controllerAs: 'vm'
            })

            .when('/register', {
                controller: 'RegisterController',
                templateUrl: 'register/register.view.html',
                controllerAs: 'vm'
            })

            .otherwise({ redirectTo: '/login' });       //Esta es la url por defecto
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];         //cosas que se corren al comienzo del proyecto
    function run($rootScope, $location, $cookieStore, $http) {                  //no hay que modificar esto
        // keep user logged in after page refresh                               // Se mantiene el usuario
        $rootScope.globals = $cookieStore.get('globals') || {};                 // se crean las cookies
        if ($rootScope.globals.currentUser) {
            $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata; // jshint ignore:line
        }

        $rootScope.$on('$locationChangeStart', function (event, next, current) {            //Si intenta meterse en una pagina restringida
            // redirect to login page if not logged in and trying to access a restrictedPage page
            var restrictedPage = $.inArray($location.path(), ['/login', '/register']) === -1;
            var loggedIn = $rootScope.globals.currentUser;          
            if (restrictedPage && !loggedIn) {
                $location.path('/login');
            }
        });
    }

})();