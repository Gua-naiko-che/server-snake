import React from 'react';

export const Bodies = (props) => {
    const bodies = props.bodies;

    return <div>
        {bodies.map((body, bodyIndex) => <div key={bodyIndex}>{<Body body={body} />}</div>)}
    </div>
}


const Body = (props) => {
    return props.body.map((point, pointIndex) => <span key={pointIndex}>{`(${point.x},${point.y})`}</span>);
}