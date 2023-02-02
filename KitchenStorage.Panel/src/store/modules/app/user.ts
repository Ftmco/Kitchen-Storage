import Account from '@/services/account'

export const user = {
    namespaced: true,
    state: {
        user: {
            isLogin: Account.isAuthenticated(),
        },
    },
    getters: {
        auth(state: any) {
            return state.user
        }
    },
    mutations: {},
    actions: {}
}