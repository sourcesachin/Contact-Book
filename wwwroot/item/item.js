(function () {
    'use strict';

    angular
        .module('app')
        .controller('item', item);

    item.$inject = ['dataService', '$routeParams', '$location','$scope'];

    function item(dataService, $routeParams, $location, $scope) {
        var vm = this;
        vm.editMode = false;
        vm.newContact = false;
        vm.Addresses = [];
        vm.add = add;
        vm.addAddress = addAddress;
        vm.deleteAddress = deleteAddress;
        if ($routeParams.id) {
            dataService.getContact($routeParams.id).then(function (data) {
                vm.contact = data;
                vm.Addresses = data.addresses;
            })
        }
        if (!$routeParams.id) {
            vm.editMode = true;
            vm.newContact = true;
        }

        function addAddress() {
            debugger;
            var em = {
                line1: vm.line1,
                line2: vm.line2,
                country: vm.country,
                postcode: vm.postcode,
                contactId: $routeParams.id
            };
            vm.Addresses.push(em);
            vm.line1 = null;
            vm.line2 = null;
            vm.country = null;
            vm.postcode = null;
        }
        function deleteAddress(selectedAddress) {
            vm.Addresses = vm.Addresses.filter(function (address) {
                return address.id !== selectedAddress.id;
            });
        }
        function add() {
            vm.contact.addresses = vm.Addresses;
            dataService.addContact(vm.contact).then(function (data) {
                $location.path('/');
            })
        }
    }

})();
