const apiUrl = "/api/tasks";

const statusNames = {
    0: "Новая",
    1: "В работе",
    2: "Готово",
    3: "Отменена"
};

async function loadTasks() {
    const response = await fetch(apiUrl);
    const tasks = await response.json();
    const tbody = document.getElementById("taskTableBody");
    tbody.innerHTML = "";

    tasks.forEach(task => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${task.title}</td>
            <td>${task.employeeName}</td>
            <td>${task.priority}</td>
            <td>${statusNames[task.status] ?? task.status}</td>
            <td>
                <button class="small" onclick="editTask('${task.id}')">Изменить</button>
                <button class="small" onclick="changeStatus('${task.id}')">Сменить статус</button>
                <button class="small" onclick="deleteTask('${task.id}')">Удалить</button>
            </td>
        `;
        tbody.appendChild(row);
    });
}

document.getElementById("taskForm").addEventListener("submit", async (e) => {
    e.preventDefault();

    const newTask = {
        title: document.getElementById("title").value,
        employeeName: document.getElementById("employeeName").value,
        description: document.getElementById("description").value,
        priority: document.getElementById("priority").value
    };

    await fetch(apiUrl, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newTask)
    });

    e.target.reset();
    loadTasks();
});

async function changeStatus(id) {
    const rows = { "0": 1, "1": 2, "2": 3, "3": 0 }; // простое переключение по кругу
    const current = await (await fetch(`${apiUrl}/${id}`)).json();
    const nextStatus = rows[current.status];

    await fetch(`${apiUrl}/${id}/status`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(nextStatus)
    });

    loadTasks();
}

async function editTask(id) {
    const current = await (await fetch(`${apiUrl}/${id}`)).json();

    const newTitle = prompt("Новая тема задачи:", current.title);
    if (newTitle === null) return; // отмена

    const newDescription = prompt("Новое описание:", current.description);
    if (newDescription === null) return;

    const updated = {
        ...current,
        title: newTitle,
        description: newDescription
    };

    await fetch(`${apiUrl}/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(updated)
    });

    loadTasks();
}

async function deleteTask(id) {
    await fetch(`${apiUrl}/${id}`, { method: "DELETE" });
    loadTasks();
}

loadTasks();