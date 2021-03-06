import React from 'react';
import ReactDOM from 'react-dom';
import Board from './Board';
import { game } from "./game";
import { createStore } from "redux";
import { directionByKeyCode } from "./directions";
import { Bodies } from './Bodies';
import * as signalR from "@microsoft/signalr";

const SNAKE_SPEED = 150;

const store = createStore(game);
store.subscribe(() => {
  const state = store.getState();

  ReactDOM.render(
    <div>
      <Board {...state} />
      <Bodies {...state} />
    </div>,
    document.getElementById('root'))
});

const connection = new signalR.HubConnectionBuilder()
  .withUrl("/hub")
  .build();
connection.on("GameState", body => {
  store.dispatch({ type: "ADD_BODY", body })
});
connection.start();

document.onkeydown = async e => {
  e = e || window.event;
  const nextDirection = directionByKeyCode[e.keyCode];
  if (nextDirection) {
    store.dispatch({ type: "SET_NEXT_DIRECTION", nextDirection })

    await fetch("snake", {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: nextDirection
    });

    e.preventDefault();
  }
}

(function gameLoop() {
  store.dispatch({ type: "UPDATE" });

  if (!store.getState().isOver) {
    setTimeout(gameLoop, SNAKE_SPEED);
  }
})();