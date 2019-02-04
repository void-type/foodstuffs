import router from '../../../router';
import webApi from '../../../webApi';

export default {
  fetchList(context) {
    webApi.recipes.list(
      context.getters.listRequest,
      data => context.commit('SET_LIST_RESPONSE', data),
      response => context.dispatch('app/setApiFailureMessage', response, { root: true }),
    );
  },
  save(context, recipe) {
    webApi.recipes.save(
      recipe,
      (data) => {
        router.push({ name: 'edit', params: { id: data.id } });
        context.dispatch('app/setSuccessMessage', data.message, { root: true });
      },
      response => context.dispatch('app/setApiFailureMessage', response, { root: true }),
    );
  },
  delete(context, id) {
    webApi.recipes.delete(
      id,
      (data) => {
        context.dispatch('fetchList');
        router.push({ name: 'search' });
        context.dispatch('app/setSuccessMessage', data.message, { root: true });
      },
      response => context.dispatch('setApiFailureMessage', response),
    );
  },
  resetListRequest(context) {
    context.commit('RESET_LIST_REQUEST');
  },
  setListPage(context, page) {
    context.commit('SET_LIST_REQUEST_PAGE', page);
  },
  setListTake(context, take) {
    context.commit('SET_LIST_REQUEST_TAKE', take);
  },
  setListCategorySearch(context, categorySearch) {
    context.commit('SET_LIST_REQUEST_CATEGORY_SEARCH', categorySearch);
  },
  setListNameSearch(context, nameSearch) {
    context.commit('SET_LIST_REQUEST_NAME_SEARCH', nameSearch);
  },
  setListNameSort(context, sortName) {
    context.commit('SET_LIST_REQUEST_NAME_SORT', sortName);
  },
  addToRecent(context, recipe) {
    const recentRecipes = context.state.recentRecipes.slice();

    const indexOfCurrentInRecents = recentRecipes
      .map(recentRecipe => recentRecipe.id)
      .indexOf(recipe.id);

    const recipeListItem = {
      id: recipe.id,
      name: recipe.name,
      categories: recipe.categories,
    };

    if (indexOfCurrentInRecents > -1) {
      recentRecipes.splice(indexOfCurrentInRecents, 1);
    }
    if (recipe.id > 0) {
      recentRecipes.unshift(recipeListItem);
    }
    if (recentRecipes.length > 3) {
      recentRecipes.pop();
    }
    context.commit('SET_RECENT_RECIPES', recentRecipes);
  },
};
