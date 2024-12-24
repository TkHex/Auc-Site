async function loadAuctionData(auctionId) {
    try {
        const response = await fetch(`/auc?id=${auctionId}`);
        if (!response.ok) {
            throw new Error('Ошибка при загрузке данных аукциона');
        }
        const auctionData = await response.json();

        if (auctionData) {
            const dialog = document.getElementById('redact-dialog');
            dialog.querySelector('input[name="auct_name"]').value = auctionData.auct_name;
            dialog.querySelector('input[name="starting_date"]').value = auctionData.starting_date;
            dialog.querySelector('input[name="ending_date"]').value = auctionData.ending_date;
            dialog.querySelector('input[name="auct_step"]').value = auctionData.auct_step;

            loadLotsForRedactAuc(auctionId);
        }
    } catch (error) {
        console.error('Ошибка:', error);
    }
}

async function loadLotsForRedactAuc(auctionId) {
    try {
        const response = await fetch('/lot/all');
        if (!response.ok) {
            throw new Error('Ошибка при загрузке лотов');
        }
        const allLots = await response.json();
        
        const availableLots = allLots.filter(lot => lot.id_auction === null || lot.id_auction == auctionId);

        addLotsToRedactDialog(availableLots, auctionId);
    } catch (error) {
        console.error('Ошибка:', error);
    }
}

function addLotsToRedactDialog(lots, auctionId) {
    const lotSelect = document.querySelector('#create-lot-box select[name="lot_in_auc"]');
    lotSelect.innerHTML = '';

    lots.forEach(lot => {
        const option = document.createElement('option');
        option.value = lot.id_lot;
        option.textContent = lot.lot_name;

        if (lot.id_auction == auctionId) {
            option.selected = true;
        }

        lotSelect.appendChild(option);
    });
}

function addEditButtonListeners() {
    const redactButtons = document.querySelectorAll('.redact-button');
    redactButtons.forEach(button => {
        button.addEventListener('click', () => {
            const auctionId = button.id;
            loadAuctionData(auctionId);
            document.getElementById('auctionIdInput').value = auctionId;
        });
    });
}