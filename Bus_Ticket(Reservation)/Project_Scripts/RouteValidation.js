document.addEventListener("DOMContentLoaded", function () {
    const RouteButton = document.getElementById("RouteButton");
    RouteButton.addEventListener("click", function (event) {
        const selectedRoute = document.querySelector("input[name='routeid']:checked");
        if (!selectedRoute) {
            alert("Please select a Route.");
            event.preventDefault(); // Prevent form submission
        }
    });
});
