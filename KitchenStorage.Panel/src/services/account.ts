
export default {
    authenticationToken() {
        return localStorage.getItem("Auth-Token") ?? "";
    },
    isAuthenticated() {
        let token = this.authenticationToken()
        return token != ""
    },
};
