const showToast = (message) => {
    let x = document.getElementById("snackbar");
    x.innerHTML = message;
    x.className = "show";
    setTimeout(() => { x.className = x.className.replace("show", ""); }, 3000);
}

const getHtmlApiCallOptions = {
    method: 'GET',
    headers: {
        'content-type': 'text/html'
    }
}


const showLoading = () => {
    let ulLoaderul = document.getElementById('ulLoaderul');
    ulLoaderul.style.display = 'flex';
}

const hideLoading = () => {
    let ulLoaderul = document.getElementById('ulLoaderul');
    ulLoaderul.style.display = 'none';
}
