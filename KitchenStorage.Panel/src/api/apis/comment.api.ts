import { apiCall } from "..";
import { ApiResponse } from "../models/api.model";
import { baseURLs, comment } from "../urls";


const axios = apiCall(baseURLs(false).commentLikeRate)

export const getAdminComments = (page: number, count: number, q?: string): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await axios.get(comment.getAdminComments(page, count, q))
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })