export default function ({ store, next }: any) {       
    if (!store.getters["user/auth"].isLogin) {
        return next({
            name: "Login"
        })
    }
    return next();
}