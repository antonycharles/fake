<template>
    <header id="header">
        <h1><span>Fake.</span>Accounts.admin</h1>
    </header>
</template>

<script lang="ts">
import { defineComponent } from 'vue';


export default defineComponent({
    name:"WebsiteHeader"
});

</script>

<style>
    #header{
        background-color: #403D58;
        padding: 20px;
    }
    #header h1{
        font-family: BlinkMacSystemFont,-apple-system,"Segoe UI",Roboto,Oxygen,Ubuntu,Cantarell,"Fira Sans","Droid Sans","Helvetica Neue",Helvetica,Arial,sans-serif;
        font-size: 1.75rem;
        font-weight: 500;
        color:#fff;
    }
    #header h1 span{
        color: #9E95E3;
    }
</style>