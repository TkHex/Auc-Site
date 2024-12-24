async function loadLots(auction_id) {
    /* Загрузка лотов для конкретного аукциона и возврат их массивом */
    let response = await fetch(`/lot/for_auc?auc_id=${auction_id}`)
    if(response.ok) {
        return await response.json()
    } else
        throw new Error(`Ошибка запроса лотов для аукциона с id ${auction_id}`)
}

async function renderMainPage() {
    const aucBox = document.getElementById('auc-box');

    const response = await fetch('/auc/all');
    if(response.ok) {
        const auctions = await response.json();
        for(const auc of auctions) {
            const lots = await loadLots(auc.id_auction);
            let lotsList = lots.map(lot => `<li>${lot.lot_name}</li>`).join('');

            const auctionElement = `
                <div class="auc-cont">
                    <button class="redact-button" id="${auc.id_auction}" onclick="openForm('redact-dialog')">&#9998;</button>
                    <h2>${auc.auct_name}</h2>
                    <p>Дата начала: ${new Date(auc.starting_date).toLocaleString()}</p>
                    <p>Дата окончания: ${new Date(auc.ending_date).toLocaleString()}</p>
                    <ol>
                        ${lotsList}
                    </ol>
                    <button onclick="alert('Ставка сделана, ставок больше нет')">Сделать ставку</button>
                </div>
            `;
            aucBox.innerHTML += auctionElement;
        }
        addEditButtonListeners();
    } else {
        throw new Error("Ошибка при загрузке аукционов");
    }
}