import React from 'react';
import { isSamePoint } from "./geometry";
import './Board.css';

function Board({ boardSize, snake, food }) {
  const indexes = [...Array(boardSize).keys()];

  return (
    <table className="board"><tbody>{renderRows()}</tbody></table>
  );

  function renderRows() {
    return indexes.map(renderRow);
  }

  function renderRow(rowIndex) {
    const cells = indexes.map(colIndex => renderCell(rowIndex, colIndex));
    return <tr key={rowIndex}>{cells}</tr>;
  }

  function renderCell(rowIndex, colIndex) {
    return <td className={getClass(rowIndex, colIndex)} key={colIndex}></td>;
  }

  function getClass(rowIndex, colIndex) {
    const currentCell = [rowIndex, colIndex];
    const isSnakeCell = snake.some(point => isSamePoint(point, currentCell));
    const isFoodCell = isSamePoint(food, currentCell)

    if (isSnakeCell) {
      return "snake";
    } else if (isFoodCell) {
      return "food";
    } else {
      return "";
    }
  }
}

export default Board;
