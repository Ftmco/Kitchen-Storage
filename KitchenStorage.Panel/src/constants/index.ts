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
        title: 'گروه ها',
        icon: 'mdi-group',
        to: '/tabs/groups',
        active: false,
    },
    {
        id: 3,
        title: 'کافی شاپ ها',
        icon: 'mdi-store',
        to: '/tabs/stores',
        active: false,
    },
    {
        id: 4,
        title: 'محصولات',
        icon: 'mdi-shopping',
        to: '/tabs/products',
        active: false,
    },
    {
        id: 5,
        title: 'نظرات',
        icon: 'mdi-comment-multiple',
        to: '/tabs/comments'
    },
    {
        id: 6,
        title: 'تیکت ها',
        icon: 'mdi-ticket',
        to: '/tabs/tickets'
    },
    {
        id: 7,
        title: 'تنظیمات',
        icon: 'mdi-cog',
        to: '/tabs/settings'
    }
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

export const application = {
    apiKey: "31fddd91da9d9170151e5b5eb524dbee1e7381e7564a0b9e0ff64ac2ef24aee1",
    password: "1G14ijWA1401",
}

export const defaultLocation = [35.71089, 51.334968]; 