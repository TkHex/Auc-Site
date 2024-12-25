async function Authorization(){
    var test = await fetch("/user/test", {
            "credentials": "same-origin",
            "headers": {
                "Authorization": document.cookie.split(';')[0].substr(9)
            }
        }
    ).then(d => d.text())
}