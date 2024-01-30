<template>
  <main>
    <Header/>
    <div class="columns is-gapless is-multiline">
      <div class="column is-one-fifth">
        <SideBar/>
      </div>
      <div class="column">
        <div class="p-3">
          <router-view></router-view>
        </div>
      </div>
    </div>
  </main>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import SideBar from './components/SideBar.vue';
import Header from './components/Header.vue';

export default defineComponent({
  name: 'App',
  components: {
    SideBar,
    Header
}
});
</script>

<style>
body,#app, #app main{
  min-height: 100vh;
}
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}
</style>
