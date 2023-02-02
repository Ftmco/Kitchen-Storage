
export const snackBar = {
    namespaced: true,
    state: {
        message: "",
        openSnackbar: false,
    },
    mutations: {
        showSnackbar(state: any, message: string) {
            if (message.trim() != "") {
                state.message = message
                state.openSnackbar = true
            } else {
                state.message = ""
                state.openSnackbar = false
            }
        },
        hideSnackbar(state: any) {
            state.openSnackbar = false
        },
    },
    getters: {},
    actions: {}
}