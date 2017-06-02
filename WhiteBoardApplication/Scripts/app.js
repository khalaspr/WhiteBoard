var lc;
$(document).ready(function () {
    var backgroundImage = new Image()
    backgroundImage.src = '/_static/sample_image.png';
    LC.setDefaultImageURLPrefix('/_static/lib/img');
    lc = LC.init(
        document.getElementsByClassName('my-drawing')[0],
        {});

    var myscreenshot = $("#myscreenshot").text();
   
    if ($.trim(myscreenshot) != "") {
        lc.loadSnapshot(JSON.parse(myscreenshot));
    }
    $("#btnSaveScreenShot").click(function (e) {
        var myscreenshot = lc.getSnapshotJSON();
      //  myscreenshot = '';
        e.preventDefault(); // <------------------ stop default behaviour of button
        $.ajax({
            url: "/Home/saveScreenShot",
            type: "POST",
            data: JSON.stringify({ 'myscreenshot': myscreenshot }),
            dataType: "json",
            traditional: true,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (!data.success)
                    alert(data.error)
                else
                    $("#linkURL").val(data.urlString);
            },
            error: function () {
                alert("An error has occured!!!");
            }
        });

    })
});
