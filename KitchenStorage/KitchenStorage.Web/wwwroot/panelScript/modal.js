const createUniqId = () => {
    const myString = "1234567890qwertyuioplkjhgfdsazxcvbnm";
    let uniqueId = [];
    for (let i = 0; i < 30; i++) {
        uniqueId.push(myString[Math.floor(Math.random() * myString.length)]);
    }
    return uniqueId.join("");
}



const createModal = () => {
    let id = createUniqId()
    let modalHtml = `<div id="${id}" class="w3-modal modal-container" style="backdrop-filter: blur(5px);z-index:1400">
		<div id="modalmain-${id}" class="w3-modal-content w3-card-4 w3-animate-zoom mymodal">
			<div id="modalmainheader-${id}" class="w3-center">
				<br>
				<span onclick="closeMainModal('${id}')" class="w3-button w3-xlarge w3-hover-red w3-display-topright" title="Close Modal">&times;</span>
				<h5 id="Title-${id}"></h5>
			</div>
			<div id="modalBody-${id}">
			    			     </div>

			<div class="w3-container w3-border-top w3-padding-16 w3-light-grey">
				<button onclick="closeMainModal('${id}')" type="button" class="btn btn-dark btn-block">بازگشت</button>
			</div>
		</div>
	</div>`
	var modalsDiv = document.getElementById('modals').innerHTML
    modalsDiv.innerHTML += modalHtml

    return id
}
