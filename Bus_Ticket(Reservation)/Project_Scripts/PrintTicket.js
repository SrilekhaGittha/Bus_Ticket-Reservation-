
        // JavaScript code for printing the ticket
    document.getElementById("printButton").addEventListener("click", function () {
            var ticketDetails = document.getElementById("ticketDetails");
    var printWindow = window.open('', '_blank');
    printWindow.document.write('<html><head><title>Ticket Details</title></head><body>');
        printWindow.document.write(ticketDetails.innerHTML);
        printWindow.document.write('</body></html>');
    printWindow.document.close();
    printWindow.print();
        });
