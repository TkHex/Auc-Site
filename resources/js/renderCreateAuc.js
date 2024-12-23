async function loadLotsForCreateAuc() {
    try {
        const response = await fetch('/lot/all');
        if (!response.ok) {
            throw new Error('Ошибка при загрузке лотов');
        }
        const allLots = await response.json();
        
        const availableLots = allLots.filter(lot => lot.id_auction === null);

        renderLots(availableLots);
    } catch (error) {
        console.error('Ошибка:', error);
    }
}

function renderLots(lots) {
    const lotSelect = document.querySelector('#create-lot-box select[name="lot_ids"]');
    lotSelect.innerHTML = '';

    lots.forEach(lot => {
        const option = document.createElement('option');
        option.value = lot.id_lot; 
        option.textContent = lot.lot_name; 
        lotSelect.appendChild(option);
    });
}