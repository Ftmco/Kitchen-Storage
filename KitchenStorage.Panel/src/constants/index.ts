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
        icon: 'mdi-food',
        active: false,
        children: [
            {
                id: 21,
                title: 'غذا ها',
                icon: 'mdi-food',
                to: '/tabs/foods',
            },
            {
                id: 22,
                title: 'گزارش غذا',
                icon: 'mdi-newspaper',
                to: '/tabs/foods/report',
            }
        ]
    },
    {
        id: 3,
        title: 'انبار',
        icon: 'mdi-store',
        active: false,
        children: [
            {
                id: 31,
                title: 'گروه ها',
                icon: 'mdi-group',
                to: '/tabs/groups'
            },
            {
                id: 32,
                title: 'موجودی',
                icon: 'mdi-list-box-outline',
                to: '/tabs/inventory'
            }
        ],
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
    requireSelect: (items: any) => items.length > 0 || 'Required.',
}