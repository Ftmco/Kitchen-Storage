import { base, store} from "./status.enum"


export const getBaseStatusObj = (status: number): Status =>
    base.find((s) => s.status == status) as Status;

export const getStoreStatusObj = (status:number): Status=>
    store.find((s)=> s.status == status) as Status;


export interface Status {
    status: number
    color: string
    title: string
}
