$(document).ready(function () {

    $("#Username").change(function() {
        username = $(this).val();

        if (username.indexOf("@") > 0) {
            $("#rememberMeDiv").hide();
        } else {
            $("#rememberMeDiv").show();
        }
    });
    

    $("#btnAcessar").click(function (e) {

        e.preventDefault();

        $("#loginform").validate();

        if (!$("#loginform").valid()) return;
    
        var $btn = $(this).button("loading");
        var l = Ladda.create(this);
        l.start();
        
        $("#loginform").submit();
    });
});