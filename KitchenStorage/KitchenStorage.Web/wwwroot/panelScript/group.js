
const newGroup = () => {
    fetch('/Group/Create', getHtmlApiCallOptions).then(async (res) => {
        createModal('افزودن گروه جدید', await res.text())
    })
}

const createGroup = () => {
    let name = document.getElementById('group-name')
    upsertGroup({ id: null, name: name.value })
}

const editGroup = ({ id, name }) => {
    fetch(`/Group/Edit?id=${id}`, getHtmlApiCallOptions).then(async (res) => {
        createModal(`ویرایش گروه ${name}`, await res.text())
    })
}

const updateGroup = (id) => {
    let name = document.getElementById('group-name')
    upsertGroup({ id: id, name: name.value })
}

const upsertGroup = ({ id, name }) => {
    fetch('/api/Group/Upsert', postJsonApiCallOptions({ 'id': id, 'name': name })).then(async (rest) => {
        const body = await rest.json()
        if (body.status) {
            closeModal()
            addGroupToList(body.result.group)
        }
        showToast(body.title)
    })
}

const deleteGroup = ({ id, name }) => {
    fetch(`/Group/Delete?id=${id}`, getHtmlApiCallOptions).then(async (res) => {
        createModal(`حذف گروه ${name}`, await res.text())
    })
}

const deleteGroupConfirm = (id) => {
    fetch(`/api/Group/Delete?id=${id}`, deleteJsonApiCallOptions).then(async (rest) => {
        const body = await rest.json()
        if (body.status) {
            closeModal()
            addGroupToList(body.result.group)
        }
        showToast(body.title)
    })
}

const addGroupToList = (group) => {
    removeFromTable(group.id)
    let html = `<tr id="${group.id}">
                                        <td>
                                            ${group.name}
                                        </td>
                                        <td>
                                            ${group.createDate}
                                        </td>
                                        <td>
                                            ${group.status == 0 ? '<h6 class="text-success">فعال</h6>' : ''}
                                            ${group.status == 1 ? '<h6 class="text-warning">غیر فعال</h6>' : ''}
                                            ${group.status == 2 ? '<h6 class="text-warning">حذف شده</h6>' : ''}
                                        </td>

                                        <td>
                                           <a onclick="editGroup({id:'${group.id}',name:'${group.name}'})">
                                                <i class="badge-circle badge-circle-light-secondary bx bxs-pencil font-medium-1"></i>
                                            </a>
                                        </td>
                                       ${group.status != 2 ? ` <td>
                                            <a asp-action="Delete" asp-route-id="@item.Id" data-toggle="tooltip" class="delete-confirm" title="حذف">
                                                <i class="badge-circle badge-circle-light-danger bx bxs-trash-alt font-medium-1"></i>
                                            </a>
                                        </td>` : ''}
                                    </tr>`

    addItemToTable('groups-table-body', html)
}