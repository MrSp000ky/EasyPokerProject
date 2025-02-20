<template>
  <div class="container">
    <!-- Registration Form View -->
    <div v-if="!gameResult">
      <h2 class="title">Register Players</h2>
      <div v-for="(player, index) in players" :key="index" class="player-card">
        <div class="card p-3">
          <label :for="'player' + index" class="form-label player-label">
            Player {{ index + 1 }}
          </label>
          <input
            type="text"
            class="form-control custom-input"
            :id="'player' + index"
            v-model="players[index]"
            placeholder="Enter player name"
            required
          />
        </div>
      </div>
      <div class="d-flex justify-content-center mt-4">
        <button
          type="submit"
          class="btn btn-lg btn-success start-button"
          :disabled="!allPlayersEntered"
          @click="startGame"
        >
          <i class="bi bi-play-circle"></i> Start Game
        </button>
      </div>
    </div>

    <!-- Game Results View -->
    <div v-else>
      <h2 class="title">Game Results</h2>
      <p class="text-center" style="font-size: 20px;">
        Winner: <strong>{{ gameResult.winner }}</strong>
      </p>
      <div v-for="hand in gameResult.hands" :key="hand.playerName" class="player-card">
        <div class="card p-3">
          <label class="form-label player-label">Player: {{ hand.playerName }}</label>
          <p>Cards: {{ hand.card1 }}, {{ hand.card2 }}, {{ hand.card3 }}, {{ hand.card4 }}, {{ hand.card5 }}</p>
        </div>
      </div>
      <div class="d-flex justify-content-between mt-4">
        <button class="btn btn-secondary" @click="goBack">Back</button>
        <button class="btn btn-warning" @click="startGame">Reset</button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      players: ["", "", "", ""],
      gameResult: null,
    };
  },
  computed: {
    allPlayersEntered() {
      return this.players.every((name) => name.trim() !== "");
    },
  },
  methods: {
    async startGame() {
      try {
        const response = await axios.post("http://localhost:5131/api/Game/start", this.players);
        
        this.gameResult = response.data;
        console.log("Game Started:", this.gameResult);
      } catch (error) {
        console.error("Error starting the game:", error);
        alert("Failed to start the game.");
      }
    },
    goBack() {
      // Go back to the registration form so players can be edited
      this.gameResult = null;
    },
  },
};
</script>
  
  <style scoped>
  /* Container */
  .container {
    max-width: 600px;
    margin: auto;
    padding-top: 5vh;
  }
  
  /* Title */
  .title {
    text-align: center;
    font-weight: bold;
    margin-bottom: 15px;
  }
  
  /* Player Card */
  .player-card {
    margin-bottom: 20px;
  }
  
  .card {
    border-radius: 15px;
    background: #363636;
    border: none;
    padding: 15px;
    box-shadow: 2px 2px 10px rgba(255, 255, 255, 0.1);
  }
  
  /* Label Styling */
  .player-label {
    margin-right: 10px;
    font-size: 20px;
    color: white;
  }
  
  /* Input Field - Blending with Card */
  .custom-input {
    font-size: large;
    border: none;
    background: transparent;
    color: white;
    outline: none;
    box-shadow: none;
    border-bottom: 2px solid rgba(255, 255, 255, 0.5);
    transition: border-color 0.3s ease-in-out;
  }
  
  /* Focus Effect on Input */
  .custom-input:focus {
    border-bottom: 2px solid white;
  }
  
  /* Start Game Button */
  .start-button {
    width: 80%;
    font-size: 1.5rem;
    font-weight: bold;
  }
  </style>
  