import { apiCall } from "..";
import { Activation, Login, ServantRequest } from "../models/account.model";
import { ApiResponse } from "../models/api.model";
import { UpdateProfile } from "../models/profile.model";
import { account, baseURLs } from "../urls";


const axios = apiCall(baseURLs(false).identity, [
    {
        key: "application",
        value: `{"Key":"zjAIv/NGBb5ftidP6BfGTg==","ApiKey":"6c1ad3ee5a002ab3f28994493688f9225b993a8700f7825af65bd8eea9d88fc2"}`
    }
]);


export const otpLogin = (mobileNo?: string): Promise<ApiResponse> => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.post(account.otpLogin(), { mobileNo: mobileNo })
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })
}

export const login = (login: Login): Promise<ApiResponse> => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.post(account.login(), login)
            const response = await request.data
            const json = response as ApiResponse
            if (json.status) {
                localStorage.setItem("tokenKey", json.result.key)
                localStorage.setItem(json.result.key, json.result.value)
            }
            resolve(json)
        } catch (error) {
            reject(error)
        }
    })
}

export const activation = (activation: Activation): Promise<ApiResponse> => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.post(account.activation(), activation)
            const response = await request.data
            const json = response as ApiResponse
            if (json.status) {
                localStorage.setItem("tokenKey", json.result.session.key)
                localStorage.setItem(json.result.session.key, json.result.session.value)
            }
            resolve(json)
        } catch (error) {
            reject(error)
        }
    })
}

export const logout = async (): Promise<ApiResponse> => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.get(account.logout())
            const response = await request.data
            const apiResult = response as ApiResponse
            if (apiResult.status) {
                localStorage.clear()
            }
            resolve(apiResult)
        } catch (error) {
            reject(error)
        }
    })
}

export const getProfile = (userId?: string): Promise<ApiResponse> => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.get(account.profile.get())
            const response = await request.data
            localStorage.setItem("profile", JSON.stringify(response))
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })
}

export const updateProfile = (profile: UpdateProfile): Promise<ApiResponse> => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.post(account.profile.update(), profile)
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })
}

export const checkAccess = (pageName: string): Promise<ApiResponse> => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.post(account.checkAccess(), { pageName: pageName })
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })
}