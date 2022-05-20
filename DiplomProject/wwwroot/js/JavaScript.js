<script>
    function ShowCreate() {
    var checkBox = document.getElementById("isCreated");
    var form = document.getElemenetById("AddingStreet");
    if (checkBox.checked == false){
        form.style.display = "block";
        
  } else {
        form.style.display = "none";
    location.reload();
  }
}
</script>