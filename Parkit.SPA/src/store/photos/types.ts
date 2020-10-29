export const SET_PHOTOS = 'SET_PHOTOS';

export interface SetPhotosAction {
    type: typeof SET_PHOTOS,
    photos: Photo[],
}

export interface Photo {
    author: string;
    authorId: string;
    dateTaken: Date;
    description: string;
    link: string;
    media: {
        m: string;
    };
    published: Date;
    tags: string[];
    title: string;
}

export interface PhotosState {
    photos: Photo[],
}

export type PhotoActionTypes = SetPhotosAction;