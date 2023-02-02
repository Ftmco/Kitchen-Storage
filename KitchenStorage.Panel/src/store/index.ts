import Vue from 'vue'
import Vuex from 'vuex'
import { dialog } from './modules/app/dialog'
import { snackBar } from './modules/app/snackBar'
import { user } from './modules/app/user'
import { loading } from './modules/app/loading'
import { stepper } from './modules/app/stepper'
Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        dialog,
        snackBar,
        user,
        loading,
        stepper,
        
    }
})