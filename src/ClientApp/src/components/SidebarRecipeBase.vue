<script lang="ts" setup>
import type { PropType } from 'vue';
import type { SearchRecipesResultItem } from '@/api/data-contracts';
import RouterHelper from '@/models/RouterHelper';

defineProps({
  recipes: {
    type: Array as PropType<Array<SearchRecipesResultItem>>,
    required: true,
  },
  title: {
    type: String,
    required: false,
    default: '',
  },
  routeName: {
    type: String,
    required: true,
  },
});
</script>

<template>
  <div class="card">
    <h5 class="card-header">
      {{ title }}
    </h5>
    <div class="list-group list-group-flush">
      <router-link
        v-for="recipe in recipes"
        :key="recipe.id"
        class="list-group-item card-hover"
        :to="RouterHelper.viewRecipe(recipe)"
      >
        {{ recipe.name }}
      </router-link>
      <div v-if="recipes.length < 1" class="list-group-item p-4 text-center">No results.</div>
    </div>
  </div>
</template>

<style lang="scss" scoped></style>
