import { ConfirmDialog, ConfirmResult, Dialog, DialogResult } from "@/components/models";

export const dialog = {
    namespaced: true,
    state: {
        modalVisible: false,
        dialog: {} as Dialog,
        confirmVisible: false,
        confirmDialog: {} as ConfirmDialog,
        confirmDialogResult: {} as ConfirmResult,
        dialogResult: {} as DialogResult
    },
    mutations: {
        showModal(state: any, config: Dialog) {
            state.modalVisible = true;
            state.dialog = config
        },
        hideModal(state: any) {
            state.modalVisible = false
        },
        setDialogResult(state: any, res: DialogResult) {
            state.dialogResult = res
        },
        showConfirm(state: any, confirm: ConfirmDialog) {
            state.confirmVisible = true
            state.confirmDialog = confirm
        },
        confirmResponse(state: any, res: ConfirmResult) {
            state.confirmDialogResult = res
        },
        hideConfirm(state: any) {
            state.confirmVisible = false
        }
    },
    getters: {},
    actions: {}
}