import { apiCall } from "..";
import { ApiResponse } from "../models/api.model";
import { UpsertProduct } from "../models/product.model";
import { baseURLs, product } from "../urls";


const _axios = apiCall(baseURLs(false).coffee_admin);

export const getProducts = (page: number, count: number): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await _axios.get(product.products(page, count))
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })

export const upsertProduct = (upsertProduct: UpsertProduct): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await _axios.post(product.upsert, upsertProduct)
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })

export const deleteProduct = (id: string): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await _axios.delete(product.delete(id))
            const response = await request.data;
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })

export const getSellers = (id: string, page: number, count: number): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await _axios.get(product.sellers(id, page, count))
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })