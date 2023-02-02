import { apiCall } from "..";
import { ApiResponse } from "../models/api.model";
import { ChangeStoreStatus } from "../models/coffeeshop.model";
import { baseURLs, coffeeShop } from "../urls";


const _axios = apiCall(baseURLs(false).coffee_admin);

export const storesTypes = (): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await _axios.get(coffeeShop.types)
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    });

export const stores = (page: number, count: number): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await _axios.get(coffeeShop.stores(page, count))
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    });

export const storeDetails = (storeId: string): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await _axios.get(coffeeShop.details(storeId))
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })

export const changeStoreStatus = (status: ChangeStoreStatus): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await _axios.post(coffeeShop.changeStatus, status)
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })