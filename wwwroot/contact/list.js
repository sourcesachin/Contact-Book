(function () {
    'use strict';
    angular
        .module('app')
        .controller('list', list);
    list.$inject = ['dataService'];
    function list(dataService) {
        var vm = this;
        vm.contacts = [];
        vm.deleteContact = deleteContact;
        vm.search = search;
        vm.searchFirstName = "";
        vm.searchLastName = "";
        vm.selected = [];

        dataService.getContacts().then(function (data) {
            vm.contacts = data;
        })

        function deleteContact(id) {
            dataService.deleteContact(id).then(function (data) {
                vm.contacts = vm.contacts.filter(function (contact) {
                    return contact.id !== id;
                });
            })
        }
        function search() {
            vm.searchFirstName = vm.searchFirstName == '' ? '' : vm.searchFirstName;
            vm.searchLastName = vm.searchLastName == '' ? '' : vm.searchLastName;
            var search = {
                FirstName: vm.searchFirstName,
                LastName: vm.searchLastName,
            };
            dataService.searchContacts(search).then(function (data) {
                vm.contacts = data;
            })
        }

    }
})();
