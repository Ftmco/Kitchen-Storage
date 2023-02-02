import { apiCall } from "..";
import { ApiResponse } from "../models/api.model";
import { UpsertTicket } from "../models/ticket.model";
import { baseURLs, ticket } from "../urls";


const axios = apiCall(baseURLs(false).ticket)

export const upsertTicket = (upTicket: UpsertTicket): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await axios.post(ticket.upsert, upTicket)
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })

export const userTickets = (): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await axios.get(ticket.userTickets())
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })

export const addAttachments = (attachments: any): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await axios.post(ticket.addAttachments(), attachments)
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })

export const deleteTicket = (id: string): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await axios.delete(ticket.delete(id))
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })

export const closeTicket = (id: string): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await axios.delete(ticket.close(id))
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })

export const getTickets = (): Promise<ApiResponse> =>
    new Promise(async (resolve, reject) => {
        try {
            const request = await axios.get(ticket.allTickets())
            const response = await request.data
            resolve(response as ApiResponse)
        } catch (error) {
            reject(error)
        }
    })