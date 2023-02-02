import Vue from 'vue'
import VueRouter, { NavigationGuardNext, Route } from 'vue-router';
import { RouteConfig } from 'vue-router'
import store from "@/store/index"
import pipeline from './pipeline';

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
                    title: 'پنل مدیریت انبار آشپزخانه',
                    route
                })
            },
            {
                path: 'groups',
                name: 'Groups',
                component: () => import("@/pages/home/GroupsPage.vue"),
                meta: (route: Route) => ({
                    title: 'گروه ها',
                    route,
                    hasBack: true
                }),
            },
        ]
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