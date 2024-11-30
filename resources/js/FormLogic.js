function openForm(id) {
    document.getElementById(id).showModal()
    document.body.style.overflowY = "hidden"
}

function closeForm(id) {
    document.getElementById(id).close()
    document.body.style.overflowY = "scroll"
}