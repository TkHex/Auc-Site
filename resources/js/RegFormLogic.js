function openRegForm() {
    document.getElementById("reg-dialog").showModal()
    document.body.style.overflowY = "hidden"
}

function closeRegForm() {
    document.getElementById("reg-dialog").close()
    document.body.style.overflowY = "scroll"
}