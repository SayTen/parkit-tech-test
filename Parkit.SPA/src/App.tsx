import React, { FC, useEffect, useState } from 'react';
import { RootState } from './store';
import { connect } from 'react-redux';
import { Dispatch } from 'redux';
import { setPhotos } from './store/photos/actions';
import { Photo } from './store/photos/types';

import PhotoCard from './components/photoCard/PhotoCard';

import './App.css';

interface AppProps
{
  photos: Photo[],
  setPhotos: (photos: Photo[]) => void,
}

const App: FC<AppProps> = ({ photos, setPhotos }) => {
  const [state, setState] = useState('loading');

  useEffect(() => {
    if (state !== 'loading') {
      return;
    }

    (window as any).jsonFlickrFeed = (photos: any) => {
      const photos2 = photos.items.map((photo: any) => ({
        ...photo,
        authorId: photo.author_id,
        dateTaken: photo.date_taken,
        tags: photo.tags.split(' '),
      }));
      setPhotos(photos2);
      setState('ready');
    }

    const script = document.createElement('script');
    script.src = 'https://api.flickr.com/services/feeds/photos_public.gne?format=json&tags=safe';
    document.querySelector('head')?.appendChild(script);
  }, [state, setPhotos])

  return (
    <div className="App">
      <div className="photo-cards">
        {photos.map(photo => (
          <PhotoCard key={photo.link} photo={photo} />
        ))}
      </div>
    </div>
  );
}

const mapStateToProps = (state: RootState) => ({
  photos: state.photos.photos,
});

const mapDispatchToProps = (dispatch: Dispatch) => ({
  setPhotos: (photos: Photo[]) => dispatch(setPhotos(photos)),
});

export default connect(mapStateToProps, mapDispatchToProps)(App)
