

    $(document).ready(function () {
        $(".eye-icon").click(function (e) {
            var x = $(this).prev().val();
            $("#myModal").modal("show");
            $('.modal-body').html(x);
        });
    });
    
var modal = document.getElementById("myModal");
var btn = document.getElementById("myBtn");
var span = document.getElementsByClassName("close")[0];

span.onclick = function () {
    modal.style.display = "none";
}
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}