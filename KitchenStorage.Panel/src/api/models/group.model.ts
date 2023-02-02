import { FileModel } from "./file.model";

export interface UpsertGroup {
    id?: string;
    name: string;
    title: string;
    code: string;
    parentId?: string;
    avatar?:FileModel|null
}