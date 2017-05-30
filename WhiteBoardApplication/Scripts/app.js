
//var app = angular.module("myApp", [])

////app.controller("HomeController", function ($scope) {

////    $scope.Name = 'Pravin Khalase';
////})


//app.controller('HomeController', function ($scope, $http, $window) {
//    $scope.ButtonClick = function () {
//        var post = $http({
//            method: "POST",
//            url: "/Home/AjaxMethod",
//            dataType: 'json',
//            data: { name: $scope.Name },
//            headers: { "Content-Type": "application/json" }
//        });

//        post.success(function (data, status) {
//            $window.alert("Hello: " + data.Name + " .\nCurrent Date and Time: " + data.DateTime);
//        });

//        post.error(function (data, status) {
//            $window.alert(data.Message);
//        });
//    }
//});


$(document).ready(function () {

    var backgroundImage = new Image()
    backgroundImage.src = '/_static/sample_image.png';
    LC.setDefaultImageURLPrefix('/_static/lib/img');
    var lc = LC.init(
        document.getElementsByClassName('my-drawing')[0],
        {});

    var myscreenshot = $("#myscreenshot").text();
   
    if ($.trim(myscreenshot) != "") {
        lc.loadSnapshot(JSON.parse(myscreenshot));
    }
    //lc.on('drawingChange', function () {
    //    localStorage.setItem(localStorageKey, lc.getSnapshotJSON());
    //});


    $("#btnCrate").click(function (e) {

        var myscreenshot = lc.getSnapshotJSON();
       
        e.preventDefault(); // <------------------ stop default behaviour of button
        var element = this;
        $.ajax({
            url: "/Home/AjaxMethod",
            type: "POST",
            data: JSON.stringify({ 'myscreenshot': myscreenshot }),
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#linkURL").val(data);
            },
            error: function () {
                alert("An error has occured!!!");
            }
        });
    })
});
