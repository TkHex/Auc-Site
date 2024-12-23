async function loadUsers() {
    try {
        const response = await fetch('/users/all');
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const users = await response.json();
        renderUsers(users);
    } catch (error) {
        console.error('Ошибка при загрузке пользователей:', error);
    }
}

function renderUsers(users) {
    const userContainer = document.getElementById('user-block');
    userContainer.innerHTML = '';

    users.forEach(user => {
        const userForm = document.createElement('form');
        userForm.className = 'user-info';
        userForm.action = '/user/update';
        userForm.method = 'post';
        userForm.setAttribute('data-user-id', user.id_user);

        userForm.innerHTML = `
            <input readonly value="${user.id_user}" name="id" class="user-id">
            <input readonly value="${user.lastname}" class="user-lname">
            <input readonly value="${user.name}" class="user-name">
            <input readonly value="${user.middle_name}" class="user-mname">
            <input readonly value="${user.bank_info}" class="user-bank">
            <select name="id_role" class="user-role" onchange="updateUser(${user.id_user})">
                <option value="1" ${user.id_role == '1' ? 'selected' : ''}>Администратор</option>
                <option value="2" ${user.id_role == '2' ? 'selected' : ''}>Аукционер</option>
                <option value="3" ${user.id_role == '3' ? 'selected' : ''}>Участник</option>
            </select>
        `;

        userContainer.appendChild(userForm);
    });
}

// Вызываем функцию loadUsers при загрузке страницы
document.addEventListener('DOMContentLoaded', loadUsers);