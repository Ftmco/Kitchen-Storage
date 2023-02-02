export type Login = {
    userName: string;
    password: string;
}

export type SignUp = {
    userName: string;
    fullName: string;
    password: string;
    email: string;
    phoneNumber: string;
}

export type Activation = {
    userName: string;
    activeCode: string;
}

export interface ServantRequest {
    nationalCode: string;
    address: string;
    lat: number;
    lon: number;
    groups: []
}