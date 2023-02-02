import Vue from 'vue'
import Vuex from 'vuex'
import { dialog } from './modules/dialog'
import { snackBar } from './modules/snackBar'
import { loading } from './modules/loading'
import { stepper } from './modules/stepper'
Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        dialog,
        snackBar,
        loading,
        stepper,
        
    }
})