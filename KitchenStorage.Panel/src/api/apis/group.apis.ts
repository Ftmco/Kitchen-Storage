import { apiCall } from "../index"
import { baseURLs, group } from "@/api/urls/index"
import { ApiResponse } from "../models/api.model";
import { UpsertGroup } from "../models/group.model";

const axios = apiCall(baseURLs(false).coffee_admin);

export const getGroups = (page:number,count:number): Promise<ApiResponse> => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.get(group.groups(page,count))
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })
}

export const upsertGroup = (upsert: UpsertGroup): Promise<ApiResponse> => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.post(group.upsert(), upsert)
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })
}

export const deleteGroup = (groupId: string): Promise<ApiResponse> => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.delete(group.delete(groupId))
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })
}