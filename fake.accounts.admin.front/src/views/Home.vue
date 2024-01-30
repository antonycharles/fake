<template>
    <section class="hero is-small mb-5">
        <h3 class="title">Home</h3>
        <p class="subtitle">
            <progress class="progress is-small" value="0" max="100"></progress>
        </p>
    </section>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    export default defineComponent({
        name: "HomePage"
    });
</script>