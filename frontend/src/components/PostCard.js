import React from 'react';
import '../styles/PostCard.css';

export default function PostCard({title, content, author, likes, type, comments}) {
    return (
        <div className="post-card">
            <h2 className="title">{title}</h2>
            <p className="post-meta">
                <span className="post-author">{author}</span>, <span className="post-likes">{likes}</span>, <span className="post-type">{type}</span>
            </p>
            <p>
                {content}
            </p>
        </div>
    );
}