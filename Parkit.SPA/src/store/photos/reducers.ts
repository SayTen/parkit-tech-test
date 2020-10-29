import { PhotosState, PhotoActionTypes, SET_PHOTOS } from './types';

const initialState: PhotosState = {
    photos: [],
}

const processAuthor = (author: string): string => {
    const authorMatches = /^.*\("(.*)"\)/.exec(author);

    if (!authorMatches || authorMatches.length < 2) {
        return 'Unknown';
    }

    return authorMatches[1];
}

const processDescription = (description: string): string => {
    const div = document.createElement('div');
    div.innerHTML = description;
    const lastP = div.querySelector('p:last-child');

    if (!lastP || lastP.querySelector('img')) {
        return '';
    }

    return lastP.innerHTML;
}

export const photos = (
    state = initialState,
    action: PhotoActionTypes
): PhotosState => {
    switch(action.type) {
        case SET_PHOTOS:
            var photos = action.photos.map(photo => ({
                ...photo,
                author: processAuthor(photo.author),
                description: processDescription(photo.description),
            }));

            return {...state, photos};
        default:
            return state;
    }
}