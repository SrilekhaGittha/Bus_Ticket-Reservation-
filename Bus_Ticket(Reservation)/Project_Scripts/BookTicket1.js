function selectBus(busId, busTime, card) {
    var selectedBusIdField = document.getElementById("selectedBusId");
    selectedBusIdField.value = busId;
    var busTimeInput = document.getElementById('selectedBusTime');
    busTimeInput.value = busTime;
    var allCards = document.querySelectorAll('.bus-card');
    allCards.forEach((c) => {
        c.classList.remove('selected');
    });

    card.classList.add('selected');

    var bookTicketBtn = document.getElementById("bookTicketBtn");
    bookTicketBtn.removeAttribute("disabled");
}
