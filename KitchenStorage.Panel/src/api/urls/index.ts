export const group = {
    groups: (page: number, count: number) => `/Group/Groups?page=${page}&count=${count}`,
    upsert: () => "/Group/Upsert",
    delete: (id: string) => `/Group/Delete?id=${id}`
}

export const file = {
    uploadFile: () => "/File/Upload",
    getImage: (token: string, width: Number, hieght: Number) => `/File/Image?token=${token}&width=${width}&hieght=${hieght}`
}

export const baseURLs = (isPublic: boolean) => {
    return {
        identity: isPublic ? "http://192.168.43.198:5264/api" : "http://localhost:801/api",
        file: isPublic ? "http://192.168.43.198:5032/api" : "https://localhost:7002/api",
        ticket: isPublic ? "http://192.168.43.198:5070/api" : "http://localhost:802/api",
        commentLikeRate: isPublic ? "http://192.168.43.198:5162/api" : "https://localhost:7161/api",
        coffee_admin: isPublic ? "https://localhost:7043/api" : "https://localhost:7043/api"
    }
}

export const ticket = {
    userTickets: () => "/Ticket/GetTickets",
    upsert: "/Ticket/Upsert",
    addAttachments: () => "/Ticket/AddAttachment",
    delete: (id: string) => `/Ticket/Delete?id=${id}`,
    close: (id: string) => `/Ticket/Close?id=${id}`,
    allTickets: () => "/Ticket/Tickets"
}

export const account = {
    otpLogin: () => "/OTPAccount/Login",
    login: () => "/Account/Login",
    activation: () => "/OTPAccount/Activation",
    logout: () => "/Account/Logout",
    checkAccess: () => "/Application/CheckAccess",
    profile: {
        get: () => "/Profile/Get",
        update: () => "Profile/Update"
    },
    servant: {
        request: () => "/Servant/ServantRequest",
        status: () => "/Servant/ServantStatus"
    }
}

export const comment = {
    getComments: (id: string) => `/Comment/Comments?objectId=${id}`,
    send: () => "/Comment/Send",
    getAdminComments: (page: number, count: number, q?: string) => `/Admin/Comment/Comments?q=${q}&page=${page}&count=${count}`
}

export const coffeeShop = {
    types: '/Store/Types',
    stores: (page: number, count: number) => `/Store/Stores?page=${page}&count=${count}`,
    details: (storeId: string) => `/Store/Details?storeId=${storeId}`,
    changeStatus: '/Store/ChangeStatus'
}

export const product = {
    products: (page: number, count: number) => `/Product/Products?page=${page}&count=${count}`,
    delete: (id: string) => `/Product/Delete?id=${id}`,
    upsert: '/Product/Upsert',
    sellers: (id: string, page: number, count: number) => `/Product/Sellers?productId=${id}&page=${page}&count=${count}`
}