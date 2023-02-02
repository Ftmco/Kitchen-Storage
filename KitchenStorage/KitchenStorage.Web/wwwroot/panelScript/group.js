
const newGroup = () => {
    fetch('/Group/Create', getHtmlApiCallOptions).then(async (res) => {
        createModal('افزودن گروه جدید', await res.text())
    })
}

const createGroup = () => {
    let name = document.getElementById('group-name')
    upsertGroup({ id: null,name: name.value })
}

const upsertGroup = ({ id, name }) => {
    fetch('/api/Group/Upsert', postJsonApiCallOptions({ 'id': id, 'name': name })).then(async (rest) => {
        const body = await rest.json()
        if (body.status) {
            closeModal()            
        }
        showToast(body.title)
    })
}