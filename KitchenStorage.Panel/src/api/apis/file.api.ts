import { apiCall } from "..";
import { ApiResponse } from "../models/api.model";
import { FileUpload } from "../models/file.model";
import { baseURLs, file } from "../urls";


const axios = apiCall(baseURLs(false).file);

export const uploadFile = (fileuploads: Array<FileUpload>): Promise<ApiResponse> => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.post(file.uploadFile(), fileuploads)
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })
}

export const getImage = (token: string,width:Number,hieght:Number): Promise<ApiResponse> => {
    return new Promise(async (resolve, reject) => {
        try {
            const request = await axios.get(file.getImage(token,width,hieght))
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })
}