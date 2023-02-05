

export const loading = {
    namespaced:true,
    state: {
        loading: false,
    },
    mutations: {
        loading(state: any, open: boolean) {
            state.loading = open
        },
    },
    getters: {},
    actions: {}
}