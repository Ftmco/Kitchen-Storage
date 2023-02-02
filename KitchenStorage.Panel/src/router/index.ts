import Vue from 'vue'
import VueRouter, { NavigationGuardNext, Route } from 'vue-router';
import { RouteConfig } from 'vue-router'
import guest from './middleware/guest';
import store from "@/store/index"
import pipeline from './pipeline';
import auth from './middleware/auth';
import { changeTitle } from '@/services/title';

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
    {
        path: '/',
        redirect: '/tabs/home'
    },
    {
        path: '/account/',
        redirect: '/account/login'
    },
    {
        path: '/tabs/',
        component: () => import("@/pages/home/TabsPage.vue"),
        children: [
            {
                path: 'home',
                name: 'Home',
                component: () => import("@/pages/home/HomePage.vue"),
                meta: (route: Route) => ({
                    title: 'پنل مدیریت کافی شاپ',
                    middleware: auth,
                    route
                })
            },

            {
                path: 'comments',
                name: 'Comments',
                component: () => import("@/pages/home/CommentsPage.vue"),
                meta: (route: Route) => ({
                    title: 'نظرات',
                    middleware: auth,
                    route,
                    hasBack: true
                }),
            },
            {
                path: 'tickets',
                name: 'Tickets',
                component: () => import("@/pages/home/TicketsPage.vue"),
                meta: (route: Route) => ({
                    title: 'تیکت ها',
                    middleware: auth,
                    route,
                    hasBack: true
                }),
            },
            {
                path: 'settings',
                name: 'Settings',
                component: () => import("@/pages/home/SettingsPage.vue"),
                meta: (route: Route) => ({
                    title: 'تنظیمات',
                    middleware: auth,
                    route,
                    hasBack: true
                })
            }, {
                path: 'groups',
                name: 'Groups',
                component: () => import("@/pages/home/GroupsPage.vue"),
                meta: (route: Route) => ({
                    title: 'گروه ها',
                    middleware: auth,
                    route,
                    hasBack: true
                }),
            },
            {
                path: 'stores',
                name: 'Stores',
                component: () => import('@/pages/home/CoffeeShopsPage.vue'),
                meta: (route: Route) => ({
                    title: 'کافی شاپ ها',
                    middleware: auth,
                    route,
                    hasBack: true,
                }),
            },
            {
                path: 'products',
                name: 'Products',
                component: () => import('@/pages/home/ProductsPage.vue'),
                meta: (route: Route) => ({
                    title: 'محصولات',
                    middleware: auth,
                    route,
                    hasBack: true,
                }),
            },
            {
                path: 'productSellers',
                name: 'ProductSellers',
                component: () => import('@/pages/product/Sellers.vue'),
                meta: (route: Route) => ({
                    title: `فروشندگان ${route.query.name}`,
                    middleware: auth,
                    route,
                    hasBack: true,
                }),
            },
            {
                path: '/store/detail',
                name: 'StoreDetail',
                component: () => import("@/pages/store/detail.vue"),
                meta: (route: Route) => ({
                    title: `جزئیات ${route.query.name}`,
                    middleware: auth,
                    route,
                    hasBack: true,
                })
            },
        ]
    },

    {
        path: '/account/login',
        name: 'Login',
        component: () => import("@/pages/account/LoginPage.vue"),
        meta: (route: Route) => ({
            title: 'Login',
            middleware: guest,
            route
        })
    },
    {
        path: '/account/register',
        name: 'SignUp',
        component: () => import("@/pages/account/SignUp.vue"),
        meta: (route: Route) => ({
            title: 'SignUp',
            middleware: guest,
            route
        })
    },
    {
        path: '/account/activation',
        name: 'ActiveAccount',
        component: () => import("@/pages/account/AccountActivation.vue"),
        meta: (route: Route) => ({
            title: 'Account Activation',
            middleware: guest,
            route
        })
    },
];

const router = new VueRouter({
    routes: routes,
    mode: 'history',
})

router.beforeEach((to: any, from: Route, next: NavigationGuardNext<Vue>) => {
    let meta = to.meta(to)
    document.title = meta.title
    if (!meta.middleware) {
        return next()
    }

    const middleware = meta.middleware
    const context = {
        to,
        from,
        next,
        store
    }

    return middleware({
        ...context,
        next: pipeline(context, middleware, 1)
    })
})

export default router;