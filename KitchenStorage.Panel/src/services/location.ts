export const currentLocation = (): Promise<GeolocationPosition> => {
    return new Promise((resolve, reject) => {
        navigator.geolocation.getCurrentPosition((position) => {
            resolve(position)
        }, (error) => {
            console.log(error);

            reject(error)
        })
    })
}