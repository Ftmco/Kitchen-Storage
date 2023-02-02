export const baseURLs = (isPublic: boolean) => '';

export const group = {
    groups: (page: number, count: number) => `/Group/Groups?page=${page}&count=${count}`,
    upsert: "/Group/Upsert",
    delete: (id: string) => `/Group/Delete?id=${id}`
}