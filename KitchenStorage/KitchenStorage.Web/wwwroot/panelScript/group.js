
const newGroup = () => {
    fetch('/Group/Create', getHtmlApiCallOptions).then(async (res) => {
        createModal('افزودن گروه جدید', await res.text())
    })
}