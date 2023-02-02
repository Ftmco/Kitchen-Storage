export const baseUrl = "https://localhost:7017/"

export const navigationItems = [
    {
        id: 0,
        title: 'خانه',
        icon: 'mdi-home',
        to: '/tabs/home'
    },
    {
        id: 2,
        title: 'غذا',
        icon: 'mdi-group',
        to: '/tabs/groups',
        active: false,
    },
    {
        id: 3,
        title: 'انبار',
        icon: 'mdi-group',
        to: '/tabs/groups',
        children:[

        ],
        active: false,
    },
]

export const messages = {
    netWorkError: (message: string) => ({
        status: false,
        code: 500,
        title: 'Connection to server faild',
        message: message
    }),
    invalidForm: 'Please fill in all fields'
}

export const rules = {
    require: (value: string) => !!value || 'Required.',
    password: (value: string) => !!value && (value.length > 5 || 'Password required more than 6 characters'),
    requireSelect:(items:any)=> items.length > 0 || 'Required.',
}