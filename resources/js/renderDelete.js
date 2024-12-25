async function deleteAuction() {
    const auctionId = document.getElementById('auctionIdInput').value;

    if (!auctionId) {
        alert("ID аукциона не найден.");
        return;
    }

    try {
        const response = await fetch('/auc/delete', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: new URLSearchParams({
                'id_auction': auctionId
            })
        });

        if (response.ok) {
            alert("Аукцион успешно удален.");
            closeForm('redact-dialog');
        } else {
            const errorText = await response.text();
            alert(`Ошибка при удалении аукциона: ${errorText}`);
        }
    } catch (error) {
        alert(`Ошибка: ${error.message}`);
        console.error("Ошибка при удалении аукциона:", error);
    }
}