
        // Client-side form validation
document.querySelector("form").addEventListener("submit", function (event)
{
    alert("form submiited");
    const busid = document.querySelector("#busid").value.trim();
    const busname = document.querySelector("#busname").value.trim();
    const bustype = document.querySelector("#bustype").value.trim();
    const no_ofseats = document.querySelector("#no_ofseats").value.trim();

    if (!busid || !busname || !bustype || !no_ofseats) {
        event.preventDefault();
    alert("Please fill out all fields.");
            }
        });

    const bustypeSelect = document.querySelector("#bustype");
    const noOfSeatsInput = document.querySelector("#no_ofseats");
    const form = document.querySelector("form");
    form.addEventListener("submit", function (event) {
            const selectedBusType = bustypeSelect.value;
    const enteredSeats = parseInt(noOfSeatsInput.value);

            if (selectedBusType === "A/C Sleeper" && enteredSeats > 25) {
        event.preventDefault();
    alert("Maximum number of seats for A/C Sleeper is 25.");
            }
            else if (selectedBusType === "A/C Semi-Sleeper" && enteredSeats > 35) {
        event.preventDefault();
    alert("Maximum number of seats for A/C Semi-Sleeper is 35.");
            }
            else if (selectedBusType === "Non A/C Seater" && enteredSeats > 45) {
        event.preventDefault();
    alert("Maximum number of seats for Non A/C Seater is 45.");
            }
        });


