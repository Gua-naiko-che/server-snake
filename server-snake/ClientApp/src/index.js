import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import Board from './Board';
import { game } from "./game";
import { createStore } from "redux";
import { directionByKeyCode } from "./directions";

const SNAKE_SPEED = 150;

const store = createStore(game);
store.subscribe(() => ReactDOM.render(
  <Board {...store.getState()} />,
  document.getElementById('root'))
);

document.onkeydown = e => {
  e = e || window.event;
  const nextDirection = directionByKeyCode[e.keyCode];
  if (nextDirection) {
    store.dispatch({ type: "SET_NEXT_DIRECTION", nextDirection })
    e.preventDefault();
  }
}

(function gameLoop() {
  store.dispatch({ type: "UPDATE" });

  if (!store.getState().isOver) {
    setTimeout(gameLoop, SNAKE_SPEED);
  }
})();