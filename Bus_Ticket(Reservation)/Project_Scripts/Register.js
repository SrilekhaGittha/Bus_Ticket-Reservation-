
$(function () {
    $("#stateId").append("<option value=-1>select</option>");
    $("#countryId").append("<option value='-1''>select</option>");
    $.ajax(
        {
            type: "GET",
            url: "/Customer/Countries",
            dateTypes: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                for (var i = 0; i < data.length; i++) {
                    $("#countryId").append("<option value='" + data[i].countryid + "'>" + data[i].countryname + "</option>");
                }
            },
            error: function (x, err) {
                alert(x.readyState);

                alert(x.ResponseText)
            }
        }
    )
    $("#countryId").click(function () {
        $.ajax({
            type: "POST",
            url: "/Customer/States/",
            data: "{'CiD':'" + $("#countryId").val() + "'}",
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                $("#stateId").empty();
                $("#stateId").append("<option value=-1>select</option>");
                for (var i = 0; i < data.length; i++) {
                    $("#stateId").append("<option value='" + data[i].statename + "'>" + data[i].statename + "</option>");
                }
            },
            error: function (x, err) {
                alert("errror");
                alert(x.responseText);
            }
        })
    })
    $("#btn").click(function (event) {
        let v1 = $("#password").val();
        let v2 = $("#confirm").val();
        if (v1 != v2) {
            alert("Enter Same Password");
            event.preventDefault();
        }
    });
})