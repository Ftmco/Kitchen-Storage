export interface FileUpload {
    base64: string;
    extension: string;
    fileName: string;
    folderToken: string;
    type:string;
}

export interface FileModel {
    id?:string;
    fileId:string;
    fileToken:string;
    type:number;
}