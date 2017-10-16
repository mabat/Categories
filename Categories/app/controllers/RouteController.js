app.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "Templates/categories.html",
            controller: "CategoriesCtrl"
        })
        .otherwise({ redirectTo: '/' });
});