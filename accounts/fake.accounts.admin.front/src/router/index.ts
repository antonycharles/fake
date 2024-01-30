import { RouteRecordRaw, createRouter, createWebHashHistory } from "vue-router"
import Home from '../views/Home.vue'

const routes: RouteRecordRaw[] = [
    {
        path: '/',
        name:'Home',
        component:Home
    }
]


const router = createRouter({
    history:createWebHashHistory(),
    routes:routes
});


export default router;