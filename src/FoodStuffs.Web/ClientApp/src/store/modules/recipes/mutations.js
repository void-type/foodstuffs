/* eslint-disable no-param-reassign */
export default {
  SET_LIST_RESPONSE(state, listResponse) {
    state.listResponse = listResponse;
  },
  SET_LIST_REQUEST(state, listRequest) {
    state.listRequest = listRequest;
  },
  SET_RECENT_RECIPES(state, recent) {
    state.recent = recent;
  },
};
/* eslint-enable no-param-reassign */
