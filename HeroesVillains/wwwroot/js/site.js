// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function setLocalSuggest() {

    //$.ajax({
    //    url: '/HeroVillain/Index',
    //    type: 'POST',
    //    // we set cache: false because GET requests are often cached by browsers
    //    // IE is particularly aggressive in that respect
    //    cache: false,
    //    data: { search: $("#txtSearch").val().toLowerCase() },
    //    success: function (result) {
    //    }
    //});

    if (localStorage.getItem("record")) {

        var rec = JSON.parse(localStorage.getItem("record"));
        if (!rec.includes($("#txtSearch").val().toLowerCase())) {
            rec.push($("#txtSearch").val().toLowerCase());
            localStorage.setItem("record", JSON.stringify(rec));
        }
    } else {
        var val = $("#txtSearch").val();
        console.log(val);
        localStorage.setItem("record", JSON.stringify([val.toLowerCase()]));
    }

    $(document).ready(function () {
        $("#txtSearch").autocomplete({
            source: function (request, response) {
                var rec = JSON.parse(localStorage.getItem("record"));
                if (rec.length == 0) {
                    response();
                }
                else {
                    var data = rec.filter(a => a.includes(request.term.toLowerCase()));
                    response($.map(data, function (item) {
                        return {
                            label: item,
                            value: item
                        }
                    }));
                }
            },
            minLength: 2,
            select: function (event, ui) {

            }
        });
    });
}



