
var button = document.getElementById('Confirm-Delete')
button.addEventListener('click', function () {

    var Confirm = confirm('Are you sure you want to delete your account?\nNOTE, ALL data we have about you will also be deleted from our system')
    if (Confirm == true) {
        alert('Your account is successfully deleted!')
        window.location.href = '/Account/DeleteAccount'
    }
    else {
        window.location.href = '/Identity/Account/Manage'
    }
})
