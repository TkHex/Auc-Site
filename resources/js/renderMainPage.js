async function loadLots(auction_id) {
    /* Загрузка лотов для конкретного аукциона и возврат их массивом */
    let response = fetch(`/lots/get/auc_id=${auction_id}`)
    if(response.ok) {
        let json = await response.json()
        return JSON.parse(json)
    } else
        throw new Error(`Ошибка запроса лотов для аукциона с id ${auction_id}`)
}

function loadAuctions() {
    /* Загрузка аукционов и возврат их массивом */
}

function renderMainPage() {
    /* Сама отрисовка элементов */
}