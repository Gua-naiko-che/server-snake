import { game, BOARD_SIZE } from "./game";
import { Direction } from "./directions";
import { isPointInArray } from "./geometry";

it('snake moving right with no next direction keeps moving right', () => {
  const oldState = {
    snake: [[0, 1], [1, 1], [1, 2]],
    direction: Direction.right,
    nextDirection: null,
  };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.snake).toEqual([[1, 1], [1, 2], [1, 3]]);
  expect(newState.direction).toEqual(oldState.direction);
});

it('nextDirection is reset to null after moving', () => {
  const oldState = {
    snake: [[0, 1]],
    direction: Direction.right,
    nextDirection: Direction.up,
  };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.nextDirection).toEqual(null);
});

it('snake moves right', () => {
  const oldState = {
    snake: [[0, 1], [1, 1], [1, 2]],
    direction: Direction.up,
    nextDirection: Direction.right,
  };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.snake).toEqual([[1, 1], [1, 2], [1, 3]]);
  expect(newState.direction).toEqual(oldState.nextDirection);
});

it('snake moves down', () => {
  const oldState = { snake: [[0, 1], [1, 1], [1, 2]], direction: Direction.left, nextDirection: Direction.down };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.snake).toEqual([[1, 1], [1, 2], [2, 2]]);
  expect(newState.direction).toEqual(oldState.nextDirection);
});

it('snake moves up', () => {
  const oldState = { snake: [[0, 1], [1, 1], [1, 2]], direction: Direction.right, nextDirection: Direction.up };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.snake).toEqual([[1, 1], [1, 2], [0, 2]]);
  expect(newState.direction).toEqual(oldState.nextDirection);
});

it('snake moves left', () => {
  const oldState = { snake: [[1, 1], [2, 1], [3, 1]], direction: Direction.down, nextDirection: Direction.left };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.snake).toEqual([[2, 1], [3, 1], [3, 0]]);
  expect(newState.direction).toEqual(oldState.nextDirection);
});

it('snake moving down does not go up', () => {
  const oldState = { snake: [[1, 1], [2, 1], [3, 1]], direction: Direction.down, nextDirection: Direction.up };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.direction).toEqual(oldState.direction);
});

it('snake moving up does not go down', () => {
  const oldState = { snake: [[1, 1], [2, 1], [3, 1]], direction: Direction.up, nextDirection: Direction.down };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.direction).toEqual(oldState.direction);
});

it('snake moving right does not go left', () => {
  const oldState = { snake: [[1, 1], [2, 1], [3, 1]], direction: Direction.right, nextDirection: Direction.left };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.direction).toEqual(oldState.direction);
});

it('snake moving left does not go right', () => {
  const oldState = { snake: [[1, 1], [2, 1], [3, 1]], direction: Direction.left, nextDirection: Direction.right };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.direction).toEqual(oldState.direction);
});

it('game over against the left wall', () => {
  const oldState = { snake: [[0, 0]], direction: Direction.left, boardSize: 3 };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.isOver).toEqual(true);
});

it('game over against the right wall', () => {
  const oldState = { snake: [[2, 2]], direction: Direction.right, boardSize: 3 };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.isOver).toEqual(true);
});

it('game over against the upper wall', () => {
  const oldState = { snake: [[0, 0]], direction: Direction.up, boardSize: 3 };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.isOver).toEqual(true);
});

it('game over against the bottom wall', () => {
  const oldState = { snake: [[2, 2]], direction: Direction.down, boardSize: 3 };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.isOver).toEqual(true);
});

it('not game over', () => {
  const oldState = { snake: [[2, 1]], direction: Direction.left, boardSize: 3 };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.isOver).toEqual(false);
});

it('game over against itself', () => {
  const oldState = { snake: [[0, 0], [0, 1], [0, 2], [1, 2], [1, 1]], direction: Direction.up, boardSize: 3 };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.isOver).toEqual(true);
});

it('next direction is different to current one: is is stored', () => {
  const oldState = {};
  const newState = game(oldState, { type: "SET_NEXT_DIRECTION", nextDirection: Direction.up });
  expect(newState.nextDirection).toEqual(Direction.up);
});

it('starts with food at random location', () => {
  const newState = game(undefined, { type: "SET_NEXT_DIRECTION", nextDirection: Direction.up });
  expect(newState.food[0]).toBeGreaterThanOrEqual(0);
  expect(newState.food[1]).toBeGreaterThanOrEqual(0);
  expect(newState.food[0]).toBeLessThanOrEqual(BOARD_SIZE - 1);
  expect(newState.food[1]).toBeLessThanOrEqual(BOARD_SIZE - 1);
});

it('snake grows when eating food', () => {
  const oldState = {
    snake: [[0, 1], [1, 1], [1, 2]],
    direction: Direction.up,
    nextDirection: Direction.right,
    food: [1, 3]
  };
  const newState = game(oldState, { type: "UPDATE" });
  expect(newState.snake).toEqual([[0, 1], [1, 1], [1, 2], [1, 3]]);
});

it('new food appears at random location when it is eated', () => {
  const oldState = {
    snake: [[0, 1], [1, 1], [1, 2]],
    direction: Direction.up,
    nextDirection: Direction.right,
    food: [1, 3]
  };
  const newState = game(oldState, { type: "UPDATE" });
  const foodIsInSnake = isPointInArray(newState.food, newState.snake)
  expect(foodIsInSnake).toEqual(false);
});