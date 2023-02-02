export const stepper = {
    namespaced: true,
    state: {
        step: 1
    },
    mutations: {
        changeStep(state: any, step: any) {
            state.step = step
        }
    },
    actions: {},
    getters: {}
}