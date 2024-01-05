
    window.onload = function() {
      var today = new Date().toISOString().split('T')[0];
    document.getElementById('dateInput').setAttribute('min', today);
}

function check(event) {
        let x1 = document.getElementById("from");
    let x2 = document.getElementById("to");
    if (x1.value == x2.value) {
        alert("From and To Cannot be Same");
    event.preventDefault();
        }
 }
