 function check(event) {
        let x1 = document.getElementById("pickup");
    let x2 = document.getElementById("dropout");
    if (x1.value == x2.value) {
        alert("From and To Cannot be Same");
    event.preventDefault();
        }
    }
