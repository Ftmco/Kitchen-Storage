export const themeDetect = () => {
    return new Promise((resolve, reject) => {
        try {
            const themeMq = window.matchMedia("(prefers-color-scheme: dark)")
            if (themeMq.matches) {
                resolve({ dark: true, light: false })
            } else {
                resolve({ dark: false, light: true })
            }
        } catch (e) {
            reject(e)
        }
    })
}

export const mobileDetect = () => {
    return new Promise((resolve, reject) => {
        try {
            const mobileDetect = window.matchMedia("(max-width: 796px)")
            if (mobileDetect.matches) {
                resolve({ mobile: true })
            } else {
                resolve({ mobile: false })
            }
        } catch (e) {
            reject(e)
        }
    })
}