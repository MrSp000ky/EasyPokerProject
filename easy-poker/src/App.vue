<script setup>
import HelloWorld from './components/HelloWorld.vue'
import GameForm from './components/GameForm.vue';
</script>

<template>
  <h1 class="title">Easy Poker</h1>
  <div class="image-container">
    <img src="./assets/poker-cards.png" class="poker-image" />
  </div>
  <GameForm msg="Vite + Vue" />
</template>

<style scoped>
.logo {
  height: 6em;
  padding: 1.5em;
  will-change: filter;
  transition: filter 300ms;
}
.logo:hover {
  filter: drop-shadow(0 0 2em #646cffaa);
}
.logo.vue:hover {
  filter: drop-shadow(0 0 2em #42b883aa);
}

/* Container to center the image */
.image-container {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
}

/* Scalable Image */
.poker-image {
  width: clamp(150px, 40%, 400px); /* Minimum: 150px, Maximum: 400px */
  height: auto; /* Maintain aspect ratio */
  object-fit: contain; /* Ensure the full image is visible */
}

</style>
