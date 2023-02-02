export interface UpsertTicket {
    id?: string | null
    toUser: string
    subject: string
    description: string
}