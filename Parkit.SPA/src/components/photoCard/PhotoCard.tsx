import React, { FC } from 'react';
import { Photo } from '../../store/photos/types';

import './PhotoCard.css';

interface PhotoCardProps {
    photo: Photo,
}

const PhotoCard: FC<PhotoCardProps> = ({ photo }) => {
    return (
        <div className="photo-card">
            <div className="photo-card_inner">
                <div className="photo-card_image">
                    <img src={photo.media.m} alt={photo.title} />
                </div>

                <div className="photo-card_meta">
                    <a href={photo.link} target="_blank" rel="noreferrer" className="photo-card_title">{photo.title}</a>
                    {' '}by{' '}
                    <a href={`https://www.flickr.com/photos/${photo.authorId}/`} target="_blank" rel="noreferrer" className="photo-card_author">{photo.author}</a>
                </div>

                <div className="photo-card_description" dangerouslySetInnerHTML={{ __html: photo.description}}></div>

                {photo.tags.length > 0 && (
                    <div className="photo-card_tags">
                        <span>Tags: </span>
                        <ul>
                            {photo.tags.map(tag => (
                                <li key={tag}>{tag}</li>
                            ))}
                        </ul>
                    </div>
                )}
            </div>
        </div>
    );
}

export default PhotoCard;