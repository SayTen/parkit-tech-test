import React, { ChangeEvent, FC, useCallback, useEffect, useState } from 'react';
import { RootState } from './store';
import { connect } from 'react-redux';
import { Dispatch } from 'redux';
import { setPhotos } from './store/photos/actions';
import { Photo } from './store/photos/types';

import PhotoCard from './components/photoCard/PhotoCard';
import Spinner from './components/spinner/Spinner';

import './App.css';

interface AppProps
{
  photos: Photo[],
  setPhotos: (photos: Photo[]) => void,
}

const debounce = (callback: (...args: any[]) => void, delay: number) => {
  let timer: NodeJS.Timer;

  return (...args: any[]) => {
    clearTimeout(timer);
    timer = setTimeout(() => callback(...args), delay);
  };
}

const loadFeed = (
  setState: (a: string) => void,
  setPhotos: (b: Photo[]) => void,
  filterTags: string[]) => {
    setState('loading');

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

    const myFilterTags = [...filterTags, 'safe'].join(',');

    const script = document.createElement('script');
    script.src = `https://api.flickr.com/services/feeds/photos_public.gne?format=json&tags=${myFilterTags}`;
    document.querySelector('head')?.appendChild(script);
}

const App: FC<AppProps> = ({ photos, setPhotos }) => {
  const [state, setState] = useState('loading');
  const [filterTags, setFilterTags] = useState<string[]>([]);

  const debouncedLoadFeed = useCallback(
    debounce((filterTags: string[]) => loadFeed(setState, setPhotos, filterTags), 500),
    [setState, setPhotos]
  );

  useEffect(() => {
    debouncedLoadFeed(filterTags);
  }, [debouncedLoadFeed, filterTags])

  const handleFilterChange = (event: ChangeEvent<HTMLInputElement>) => {
    setFilterTags(event.target.value.split(','));
  }

  return (
    <div className="App">
      <form className="search-form">
        <input type="text" placeholder="tag1,tag2,tag3" value={filterTags} onChange={handleFilterChange} />
      </form>
      {state === 'loading' && (
        <Spinner />
      )}
      {state === 'ready' && photos.length > 0 && (
        <div className="photo-cards">
          {photos.map(photo => (
            <PhotoCard key={photo.link} photo={photo} />
          ))}
        </div>
      )}
      {state === 'ready' && photos.length < 1 && (
        <h1>No photos found</h1>
      )}
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
