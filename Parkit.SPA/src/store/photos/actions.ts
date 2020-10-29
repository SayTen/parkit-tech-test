import { Photo, SET_PHOTOS, PhotoActionTypes } from './types'

export const setPhotos = (photos: Photo[]): PhotoActionTypes => ({
    type: SET_PHOTOS,
    photos,
});