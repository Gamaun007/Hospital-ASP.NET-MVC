
    $(document).ready(function () {
        $("#doctors").change(function (evt) {
           $.post("@Url.Action("ShowDoctorInfo",new { @area = "Administration" })", { doctorId: ($("#doctors").val()) }, function (data) { 
                $("#dinfo").empty();
                $("#dinfo").append(data);
            });
        });
    $("#patients").change(function (evt) {
        $.post("@Url.Action("ShowPatientInfo",new { @area = "Administration" })", { patientId: ($("#patients").val()) }, function (data) {
            $("#pinfo").empty();
            $("#pinfo").append(data);
        });
    });
});

