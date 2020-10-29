import { combineReducers } from 'redux'
import { photos } from './photos/reducers';

export const rootReducer = combineReducers({
    photos
});

export type RootState = ReturnType<typeof rootReducer>
