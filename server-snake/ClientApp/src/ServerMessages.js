import React from 'react';

export const ServerMessages = ({ messages }) => (
    <div>
        {messages.map(message => <div>{message}</div>)}
    </div>
);