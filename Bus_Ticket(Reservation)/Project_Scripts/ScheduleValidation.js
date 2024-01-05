document.addEventListener("DOMContentLoaded", function () {
    const ScheduleButton = document.getElementById("ScheduleButton");
    ScheduleButton.addEventListener("click", function (event) {
        const selectedSchedule = document.querySelector("input[name='scheduleid']:checked");
        if (!selectedSchedule) {
            alert("Please select a Schedule.");
            event.preventDefault(); // Prevent form submission
        }
    });
});
