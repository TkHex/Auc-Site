function loadUsers() {}

function renderUserInfo() {
    let users = loadUsers()
    for(let i = 0; i < 0; i++) {
        let form = document.createElement("form")
        form.classList.add("user-info")
        form.action = "/users/update"
        form.innerHTML = `<input readonly value="${users[i].id}" name="id" class="user-id">
                        <input readonly value="${users[i].lastname}" class="user-lname">
                        <input readonly value="${users[i].name}" class="user-name">
                        <input readonly value="${users[i].middle_name}" class="user-mname">
                        <input readonly value="${users[i].bank}" class="user-bank">
                        <select name="role_id" class="user-role" onchange="updateUser(${i})">
                            <option value="1">Администратор</option>
                            <option value="2">Аукционер</option>
                            <option value="3" selected>Участник</option>
                        </select>`
        document.getElementById("user-block").appendChild(form)
    }
}

function updateUser(idx) {
    document.getElementsByClassName("user-info")[idx].submit();
}