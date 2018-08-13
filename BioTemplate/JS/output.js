function Proses() {
    var Divisi = document.getElementById("Divisi").value;

    if (Divisi == "")
        alert("Divisi harus di pilih");
    else {
        document.form.zdivisi.value = Divisi;
    }

}