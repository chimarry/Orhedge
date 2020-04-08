function setDataTable(tableId) {

    var baseUrl = getBaseURL();
    
    $(tableId).dataTable({
        "aaSorting": [],
        "language": {
            "url": baseUrl + "/Scripts/datatable/Serbian.json"
        }
    });
}

function getBaseURL() {
    var getUrl = window.location;
    var baseUrl = getUrl.protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1];
    return baseUrl;
}