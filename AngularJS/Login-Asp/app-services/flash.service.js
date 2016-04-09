(function () {
    'use strict';       //no hay que hacer nada aqui

    angular
        .module('app')
        .factory('FlashService', FlashService);//Fabrica FlashService

    FlashService.$inject = ['$rootScope'];
    function FlashService($rootScope) {
        var service = {};

        service.Success = Success;  //mensajes error y success
        service.Error = Error;

        initService();

        return service;

        function initService() {
            $rootScope.$on('$locationChangeStart', function () {
                clearFlashMessage();
            });

            function clearFlashMessage() {
                var flash = $rootScope.flash;
                if (flash) {
                    if (!flash.keepAfterLocationChange) {
                        delete $rootScope.flash;
                    } else {
                        // Solo mantener para una localizacion
                        flash.keepAfterLocationChange = false;
                    }
                }
            }
        }

        function Success(message, keepAfterLocationChange) {
            $rootScope.flash = {                //Cosas de gui
                message: message,
                type: 'success', 
                keepAfterLocationChange: keepAfterLocationChange
            };
        }

        function Error(message, keepAfterLocationChange) {
            $rootScope.flash = {
                message: message,
                type: 'error',
                keepAfterLocationChange: keepAfterLocationChange
            };
        }
    }

})();