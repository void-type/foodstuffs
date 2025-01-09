/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

import type {
  AppVersion,
  CategoriesListParams,
  EntityMessageOfInteger,
  EntityMessageOfString,
  GetCategoryResponse,
  GetMealPlanResponse,
  GetRecipeResponse,
  GetShoppingItemResponse,
  IItemSetOfIFailure,
  IItemSetOfListCategoriesResponse,
  IItemSetOfListMealPlansResponse,
  IItemSetOfListShoppingItemsResponse,
  IItemSetOfSuggestRecipesResultItem,
  ImagesUploadParams,
  MealPlansListParams,
  RecipesSearchParams,
  RecipesSuggestParams,
  SaveCategoryRequest,
  SaveMealPlanRequest,
  SaveRecipeRequest,
  SaveShoppingItemRequest,
  SearchRecipesResponse,
  ShoppingItemsListParams,
  UserMessage,
  WebClientInfo,
} from './data-contracts';
import { ContentType, HttpClient, type RequestParams } from './http-client';

export class Api<SecurityDataType = unknown> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags App
   * @name AppGetInfo
   * @summary Get information to bootstrap the SPA client like application name and user data.
   * @request GET:/api/app/info
   * @response `200` `WebClientInfo`
   */
  appGetInfo = (params: RequestParams = {}) =>
    this.request<WebClientInfo, any>({
      path: `/api/app/info`,
      method: 'GET',
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags App
   * @name AppGetVersion
   * @summary Get the version of the application.
   * @request GET:/api/app/version
   * @response `200` `AppVersion`
   */
  appGetVersion = (params: RequestParams = {}) =>
    this.request<AppVersion, any>({
      path: `/api/app/version`,
      method: 'GET',
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags Categories
   * @name CategoriesList
   * @summary List categories. All parameters are optional and some have defaults.
   * @request GET:/api/categories
   * @response `200` `IItemSetOfListCategoriesResponse`
   * @response `400` `IItemSetOfIFailure`
   */
  categoriesList = (query: CategoriesListParams, params: RequestParams = {}) =>
    this.request<IItemSetOfListCategoriesResponse, IItemSetOfIFailure>({
      path: `/api/categories`,
      method: 'GET',
      query: query,
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags Categories
   * @name CategoriesSave
   * @summary Save a shopping item. Will update if found, otherwise a new item will be created.
   * @request POST:/api/categories
   * @response `200` `EntityMessageOfInteger`
   * @response `400` `IItemSetOfIFailure`
   */
  categoriesSave = (data: SaveCategoryRequest, params: RequestParams = {}) =>
    this.request<EntityMessageOfInteger, IItemSetOfIFailure>({
      path: `/api/categories`,
      method: 'POST',
      body: data,
      type: ContentType.Json,
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags Categories
   * @name CategoriesGet
   * @summary Get a shopping item.
   * @request GET:/api/categories/{id}
   * @response `200` `GetCategoryResponse`
   * @response `400` `IItemSetOfIFailure`
   */
  categoriesGet = (id: number, params: RequestParams = {}) =>
    this.request<GetCategoryResponse, IItemSetOfIFailure>({
      path: `/api/categories/${id}`,
      method: 'GET',
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags Categories
   * @name CategoriesDelete
   * @summary Delete a shopping item.
   * @request DELETE:/api/categories/{id}
   * @response `200` `EntityMessageOfInteger`
   * @response `400` `IItemSetOfIFailure`
   */
  categoriesDelete = (id: number, params: RequestParams = {}) =>
    this.request<EntityMessageOfInteger, IItemSetOfIFailure>({
      path: `/api/categories/${id}`,
      method: 'DELETE',
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags Images
   * @name ImagesGet
   * @summary Get an image.
   * @request GET:/api/images/{name}
   * @response `200` `File`
   * @response `400` `IItemSetOfIFailure`
   */
  imagesGet = (name: string, params: RequestParams = {}) =>
    this.request<File, IItemSetOfIFailure>({
      path: `/api/images/${name}`,
      method: 'GET',
      ...params,
    });
  /**
   * No description
   *
   * @tags Images
   * @name ImagesDelete
   * @summary Delete an image.
   * @request DELETE:/api/images/{name}
   * @response `200` `EntityMessageOfString`
   * @response `400` `IItemSetOfIFailure`
   */
  imagesDelete = (name: string, params: RequestParams = {}) =>
    this.request<EntityMessageOfString, IItemSetOfIFailure>({
      path: `/api/images/${name}`,
      method: 'DELETE',
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags Images
   * @name ImagesUpload
   * @summary Upload an image using a multi-part form file.
   * @request POST:/api/images
   * @response `200` `EntityMessageOfString`
   * @response `400` `IItemSetOfIFailure`
   */
  imagesUpload = (
    query: ImagesUploadParams,
    data: {
      /** @format binary */
      file?: File | null;
    },
    params: RequestParams = {}
  ) =>
    this.request<EntityMessageOfString, IItemSetOfIFailure>({
      path: `/api/images`,
      method: 'POST',
      query: query,
      body: data,
      type: ContentType.FormData,
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags Images
   * @name ImagesPin
   * @summary Pin an image for a recipe. This image will be the default image for the recipe.
   * @request POST:/api/images/pin/{name}
   * @response `200` `EntityMessageOfString`
   * @response `400` `IItemSetOfIFailure`
   */
  imagesPin = (name: string, params: RequestParams = {}) =>
    this.request<EntityMessageOfString, IItemSetOfIFailure>({
      path: `/api/images/pin/${name}`,
      method: 'POST',
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags MealPlans
   * @name MealPlansList
   * @summary List meal plans. All parameters are optional and some have defaults.
   * @request GET:/api/meal-plans
   * @response `200` `IItemSetOfListMealPlansResponse`
   * @response `400` `IItemSetOfIFailure`
   */
  mealPlansList = (query: MealPlansListParams, params: RequestParams = {}) =>
    this.request<IItemSetOfListMealPlansResponse, IItemSetOfIFailure>({
      path: `/api/meal-plans`,
      method: 'GET',
      query: query,
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags MealPlans
   * @name MealPlansSave
   * @summary Save a meal plan. Will update if found, otherwise a new meal plan will be created.
   * @request POST:/api/meal-plans
   * @response `200` `EntityMessageOfInteger`
   * @response `400` `IItemSetOfIFailure`
   */
  mealPlansSave = (data: SaveMealPlanRequest, params: RequestParams = {}) =>
    this.request<EntityMessageOfInteger, IItemSetOfIFailure>({
      path: `/api/meal-plans`,
      method: 'POST',
      body: data,
      type: ContentType.Json,
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags MealPlans
   * @name MealPlansGet
   * @summary Get a meal plan.
   * @request GET:/api/meal-plans/{id}
   * @response `200` `GetMealPlanResponse`
   * @response `400` `IItemSetOfIFailure`
   */
  mealPlansGet = (id: number, params: RequestParams = {}) =>
    this.request<GetMealPlanResponse, IItemSetOfIFailure>({
      path: `/api/meal-plans/${id}`,
      method: 'GET',
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags MealPlans
   * @name MealPlansDelete
   * @summary Delete a meal plan.
   * @request DELETE:/api/meal-plans/{id}
   * @response `200` `EntityMessageOfInteger`
   * @response `400` `IItemSetOfIFailure`
   */
  mealPlansDelete = (id: number, params: RequestParams = {}) =>
    this.request<EntityMessageOfInteger, IItemSetOfIFailure>({
      path: `/api/meal-plans/${id}`,
      method: 'DELETE',
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags Recipes
   * @name RecipesSearch
   * @summary Search for recipes using the following criteria. All are optional and some have defaults.
   * @request GET:/api/recipes
   * @response `200` `SearchRecipesResponse`
   * @response `400` `IItemSetOfIFailure`
   */
  recipesSearch = (query: RecipesSearchParams, params: RequestParams = {}) =>
    this.request<SearchRecipesResponse, IItemSetOfIFailure>({
      path: `/api/recipes`,
      method: 'GET',
      query: query,
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags Recipes
   * @name RecipesSave
   * @summary Save a recipe. Will update if found, otherwise a new recipe will be created.
   * @request POST:/api/recipes
   * @response `200` `EntityMessageOfInteger`
   * @response `400` `IItemSetOfIFailure`
   */
  recipesSave = (data: SaveRecipeRequest, params: RequestParams = {}) =>
    this.request<EntityMessageOfInteger, IItemSetOfIFailure>({
      path: `/api/recipes`,
      method: 'POST',
      body: data,
      type: ContentType.Json,
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags Recipes
   * @name RecipesSuggest
   * @summary Suggest recipes based on search.
   * @request GET:/api/recipes/suggest
   * @response `200` `IItemSetOfSuggestRecipesResultItem`
   * @response `400` `IItemSetOfIFailure`
   */
  recipesSuggest = (query: RecipesSuggestParams, params: RequestParams = {}) =>
    this.request<IItemSetOfSuggestRecipesResultItem, IItemSetOfIFailure>({
      path: `/api/recipes/suggest`,
      method: 'GET',
      query: query,
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags Recipes
   * @name RecipesGet
   * @summary Get a recipe.
   * @request GET:/api/recipes/{id}
   * @response `200` `GetRecipeResponse`
   * @response `400` `IItemSetOfIFailure`
   */
  recipesGet = (id: number, params: RequestParams = {}) =>
    this.request<GetRecipeResponse, IItemSetOfIFailure>({
      path: `/api/recipes/${id}`,
      method: 'GET',
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags Recipes
   * @name RecipesDelete
   * @summary Delete a recipe.
   * @request DELETE:/api/recipes/{id}
   * @response `200` `EntityMessageOfInteger`
   * @response `400` `IItemSetOfIFailure`
   */
  recipesDelete = (id: number, params: RequestParams = {}) =>
    this.request<EntityMessageOfInteger, IItemSetOfIFailure>({
      path: `/api/recipes/${id}`,
      method: 'DELETE',
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags Recipes
   * @name RecipesRebuild
   * @summary Rebuild the recipe search index.
   * @request POST:/api/recipes/rebuild-index
   * @response `200` `UserMessage`
   */
  recipesRebuild = (params: RequestParams = {}) =>
    this.request<UserMessage, any>({
      path: `/api/recipes/rebuild-index`,
      method: 'POST',
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags ShoppingItems
   * @name ShoppingItemsList
   * @summary List shopping items. All parameters are optional and some have defaults.
   * @request GET:/api/shopping-items
   * @response `200` `IItemSetOfListShoppingItemsResponse`
   * @response `400` `IItemSetOfIFailure`
   */
  shoppingItemsList = (query: ShoppingItemsListParams, params: RequestParams = {}) =>
    this.request<IItemSetOfListShoppingItemsResponse, IItemSetOfIFailure>({
      path: `/api/shopping-items`,
      method: 'GET',
      query: query,
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags ShoppingItems
   * @name ShoppingItemsSave
   * @summary Save a shopping item. Will update if found, otherwise a new item will be created.
   * @request POST:/api/shopping-items
   * @response `200` `EntityMessageOfInteger`
   * @response `400` `IItemSetOfIFailure`
   */
  shoppingItemsSave = (data: SaveShoppingItemRequest, params: RequestParams = {}) =>
    this.request<EntityMessageOfInteger, IItemSetOfIFailure>({
      path: `/api/shopping-items`,
      method: 'POST',
      body: data,
      type: ContentType.Json,
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags ShoppingItems
   * @name ShoppingItemsGet
   * @summary Get a shopping item.
   * @request GET:/api/shopping-items/{id}
   * @response `200` `GetShoppingItemResponse`
   * @response `400` `IItemSetOfIFailure`
   */
  shoppingItemsGet = (id: number, params: RequestParams = {}) =>
    this.request<GetShoppingItemResponse, IItemSetOfIFailure>({
      path: `/api/shopping-items/${id}`,
      method: 'GET',
      format: 'json',
      ...params,
    });
  /**
   * No description
   *
   * @tags ShoppingItems
   * @name ShoppingItemsDelete
   * @summary Delete a shopping item.
   * @request DELETE:/api/shopping-items/{id}
   * @response `200` `EntityMessageOfInteger`
   * @response `400` `IItemSetOfIFailure`
   */
  shoppingItemsDelete = (id: number, params: RequestParams = {}) =>
    this.request<EntityMessageOfInteger, IItemSetOfIFailure>({
      path: `/api/shopping-items/${id}`,
      method: 'DELETE',
      format: 'json',
      ...params,
    });
}
