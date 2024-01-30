<template>
    <nav class="menu has-background-white-bis">
        <p class="menu-label">General</p>
        <ul class="menu-list">
            <li>
                <router-link to="/" class="link">- Home</router-link>
            </li>
            <li>
                <router-link to="/" class="link">- User</router-link>
            </li>
        </ul>
    </nav>
</template>

<script lang="ts">
import { defineComponent } from 'vue';


export default defineComponent({
    name:"SideBar"
})
</script>

<style>
    .menu{
        min-height: 100vh;
        padding: 10px;
        padding-top: 30px;
    }
</style>