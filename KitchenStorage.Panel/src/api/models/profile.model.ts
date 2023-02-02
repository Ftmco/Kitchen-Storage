export interface UpdateProfile {
    fileId?: string;
    fileToken?: string;
    firstName: string;
    lastName: string;
    email: string;
}

export const ServantStatus = {
    User: 0,
    Servant: 1,
    Reject: 2,
    Requested: 3
}