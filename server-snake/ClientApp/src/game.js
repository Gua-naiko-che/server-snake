import { Direction } from "./directions";
import { isPointInArray, isSamePoint } from "./geometry";

const START_SNAKE = [
  [0, 0],
  [0, 1],
  [1, 1],
  [1, 2],
  [2, 2],
];
export const BOARD_SIZE = 10;

const defaultGame = {
  snake: START_SNAKE,
  direction: Direction.right,
  nextDirection: null,
  food: getRandomFood(BOARD_SIZE, START_SNAKE),
  boardSize: BOARD_SIZE,
  isOver: false,
};

export function game(state = defaultGame, action) {
  if (action.type === "UPDATE") {
    const newDirection = getNewDirection(state.nextDirection, state.direction);
    const newSnake = getNewSnake(state.snake, newDirection, state.food);
    const isOver = isGameOver(newSnake, state.boardSize);

    return {
      ...state,
      snake: isOver ? state.snake : newSnake,
      isOver: isOver,
      direction: newDirection,
      nextDirection: null,
      food: isPointInArray(state.food, newSnake) ? getRandomFood(state.boardSize, newSnake) : state.food
    };
  } else if (action.type === "SET_NEXT_DIRECTION") {
    return { ...state, nextDirection: action.nextDirection };
  }

  return state;
}

function getNewSnake(snake, direction, food) {
  const oldHead = snake[snake.length - 1];

  let newHead;
  if (direction === Direction.up) {
    newHead = [oldHead[0] - 1, oldHead[1]];
  }
  else if (direction === Direction.down) {
    newHead = [oldHead[0] + 1, oldHead[1]];
  }
  else if (direction === Direction.left) {
    newHead = [oldHead[0], oldHead[1] - 1];
  }
  else if (direction === Direction.right) {
    newHead = [oldHead[0], oldHead[1] + 1];
  }

  const hasEaten = isSamePoint(food, newHead);

  return [...snake.slice(hasEaten ? 0 : 1), newHead];
}

function getNewDirection(nextDirection, oldDirection) {
  if (!nextDirection) return oldDirection;

  const isOpositeDirection =
    (nextDirection === Direction.up && oldDirection === Direction.down)
    || (nextDirection === Direction.down && oldDirection === Direction.up)
    || (nextDirection === Direction.left && oldDirection === Direction.right)
    || (nextDirection === Direction.right && oldDirection === Direction.left);

  return isOpositeDirection ? oldDirection : nextDirection;
}

export function isGameOver(snake, boardSize) {
  const head = snake[snake.length - 1];

  return head[0] < 0
    || head[1] < 0
    || head[0] > boardSize - 1
    || head[1] > boardSize - 1
    || isPointInArray(head, snake.slice(0, -1));
}

function getRandomFood(boardSize, snake) {
  const food = [Math.floor(Math.random() * boardSize), Math.floor(Math.random() * boardSize)];

  return isPointInArray(food, snake)
    ? getRandomFood(boardSize, snake)
    : food;
}