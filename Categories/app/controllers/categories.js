app.controller("CategoriesCtrl", ["$scope", "$http", function ($scope, $http) {

    $scope.model = {};

    //dohvat kategorija
    $scope.getData = function () {
        $http.get("/Home/Index").then(function (data) {
            $scope.model = data.data;
        });
    };

    $scope.getData();

    // file info
    $scope.getFileDetails = function (e) {

        $scope.files = [];
        $scope.$apply(function () {
            // spremanje file objekta u niz
            for (var i = 0; i < e.files.length; i++) {
                $scope.files.push(e.files[i]);
            }
        });
    };

    // upload
    $scope.uploadFiles = function () {

        var data = new FormData();

        for (var i in $scope.files) {
            data.append("uploadedFile", $scope.files[i]);
        }

        var objXhr = new XMLHttpRequest();
        // dohvati podatke za prikaz nakon ucitavanja datoteke
        objXhr.onreadystatechange = function () {
            if (this.readyState === 4 && objXhr.status === 200) {
                $scope.getData();
            }
        };
        // posalji datoteku
        objXhr.open("POST", "/Home/UploadFile/");
        objXhr.send(data);
    };

}]);