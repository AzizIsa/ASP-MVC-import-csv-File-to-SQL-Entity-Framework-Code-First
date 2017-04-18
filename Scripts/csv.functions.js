function checkfile() {
    var FileName = $("#FileUpload").val();
    var extension = FileName.split('.');
    if (extension[1] != "csv") {
        $("#FileUpload").val('');
        alert("Please choose a .csv file");
        return;
    }
}