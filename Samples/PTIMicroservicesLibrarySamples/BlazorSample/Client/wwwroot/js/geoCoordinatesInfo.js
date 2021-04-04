export function getCurrentLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(ongetCurrentPositionSuccess, ongetCurrentPositionError)
    }
}

export function ongetCurrentPositionSuccess(position) {
    DotNet.invokeMethodAsync('BlazorSample.Client', 'ongetCurrentLocationSuccess',
        position.coords.latitude, position.coords.longitude);
}

export function ongetCurrentPositionError(error) {
}