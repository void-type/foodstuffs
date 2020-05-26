import store from '../store';
import webApi from '../webApi';

export default function () {
  webApi.app.getInfo(
    data => store.dispatch('app/setApplicationInfo', data),
    response => store.dispatch('app/setApiFailureMessages', response),
  );
}
