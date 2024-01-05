document.addEventListener("DOMContentLoaded", function () {
    const Busbtn = document.getElementById("Busbtn");
    Busbtn.addEventListener("click", function (event) {
        const selectedBus = document.querySelector("input[name='busid']:checked");
        if (!selectedBus) {
            alert("Please select a bus.");
            event.preventDefault(); // Prevent form submission
        }
    });
});