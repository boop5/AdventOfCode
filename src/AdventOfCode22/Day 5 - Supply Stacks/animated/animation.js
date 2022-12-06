import { ChunkableArray } from './modules/ChunkableArray.js';
import { input } from './input.js';

const MoveBehavior = {
  MoveCrate: 'MoveCrate',
  MoveStack: 'MoveStack',
};

class MoveInstruction {
  move = 0;
  from = 0;
  to = 0;

  constructor(move, from, to) {
    this.move = move;
    this.from = from;
    this.to = to;
  }
}

// --------------------------

const el = document.getElementById('console');

// --------------------------

Animate(MoveBehavior.MoveCrate);

// // part 1
// const result1 = Solve(MoveBehavior.MoveCrate);
// console.assert('QPJPLMNNR' == result1);

// // part 2
// const result2 = Solve(MoveBehavior.MoveStack);
// console.assert('BQDNWJPVJ' == result2);

function Animate(moveBehavior) {
  const splitInput = input.split(/\r?\n\r?\n/).filter((p) => p.trim());
  const schemeText = splitInput[0];
  const instructionsText = splitInput[1];

  const columns = BuildColumns(schemeText);
  const instructions = BuildInstructions(instructionsText);

  // ExecuteInstructions(columns, instructions, moveBehavior);
  AnimateInstructions(columns, instructions, moveBehavior);

  const result = BuildResultString(columns);

  return result;
}

async function AnimateInstructions(columns, instructions, moveBehavior) {
  if (moveBehavior == MoveBehavior.MoveCrate) {
    let cols = [];
    Object.keys(columns)
      .map((x) => columns[x])
      .forEach((x) => cols.push(x));

    // rotate 2d array
    const rows = cols[0].map((_, colIndex) =>
      cols.map((row) => row[colIndex] ?? ' ')
    );

    const highestCol = cols
      .slice()
      .sort((x, y) => x.length - y.length)
      .pop().length;
    // const totalRows = 15;
    // const blankRows = new Array(totalRows - highestCol).fill(
    const blankRows = new Array(2).fill(
      new Array(cols.length).fill().map((_) => ' ')
    );
    const grid = blankRows.concat(rows);

    RenderGrid(grid);
    for (let j = 0; j < instructions.length; j++) {
      const instruction = instructions[j];

      for (let i = 0; i < instruction.move; i++) {
        const from = getPos(grid, instruction.from - 1);
        const to = getPos(grid, instruction.to - 1);

        // RenderGrid(grid);
        await calculateSteps(grid, from, to);
      }
    }
  }
}

function getHighestPos(grid, col) {
  for (let i = 0; i < grid.length; i++) {
    const row = grid[i];
    if (row[col] != ' ') {
      return i;
      // return grid.length - i;
    }
  }
}

function getPos(grid, col) {
  return { x: col, y: getHighestPos(grid, col) };
}

function sleep(ms) {
  return new Promise((r) => setTimeout(r, ms));
}

async function calculateSteps(grid, from, to) {
  // from 2 to 3 -> right
  // from 3 to 2 -> left
  const goLeft = from.x > to.x;
  const goRight = from.x < to.x;
  let steps = 0;
  let curPos = { x: from.x, y: from.y };
  let i = 0;

  do {
    if (i++ > 1000) {
      // safety break
      return -1;
    }

    const timeout = 500;
    async function moveLeft() {
      console.log('move left');
      const value = grid[curPos.y][curPos.x];
      grid[curPos.y][curPos.x] = ' ';
      grid[curPos.y][curPos.x - 1] = value;
      curPos.x--;
      steps++;
      await sleep(timeout);
      RenderGrid(grid);
    }
    async function moveUp() {
      console.log('move up');
      const value = grid[curPos.y][curPos.x];
      grid[curPos.y - 1][curPos.x] = value;
      grid[curPos.y][curPos.x] = ' ';
      curPos.y--;
      steps++;
      await sleep(timeout);
      RenderGrid(grid);
    }
    async function moveRight() {
      console.log('move right');
      const value = grid[curPos.y][curPos.x];
      grid[curPos.y][curPos.x] = ' ';
      grid[curPos.y][curPos.x + 1] = value;
      curPos.x++;
      steps++;
      await sleep(timeout);
      RenderGrid(grid);
    }
    async function moveDown() {
      console.log('move down');
      const value = grid[curPos.y][curPos.x];
      grid[curPos.y][curPos.x] = ' ';
      grid[curPos.y + 1][curPos.x] = value;
      curPos.y++;
      steps++;
      await sleep(timeout);
      RenderGrid(grid);
    }
    const needToGoLeft = () => goLeft && curPos.x > to.x;
    const canGoLeft = () => grid[curPos.y][curPos.x - 1] == ' '; // && grid[curPos.y+1][curPos.x] == ' ';
    const needToGoRight = () => goRight && curPos.x < to.x;
    const canGoRight = () => grid[curPos.y][curPos.x + 1] == ' ';

    if (needToGoLeft()) {
      console.log('if');
      if (canGoLeft()) {
        await moveLeft();
      } else {
        await moveUp();
      }
    } else if (needToGoRight()) {
      console.log('else if');
      if (canGoRight()) {
        await moveRight();
      } else {
        await moveUp();
      }
    } else {
      console.log('else');
      await moveDown();
    }
  } while ((curPos.x == to.x && curPos.y == to.y - 1) == false);

  return steps;
}

function RenderGrid(grid) {
  let html = '';
  grid.forEach((row, rowIndex) => {
    if (row.every((x) => x == ' ')) html += '<br />';
    else {
      if (rowIndex > 0) html += '<br />';
      row.forEach((col, colIndex) => {
        if (col == ' ') html += '    ';
        else {
          const hpos = getHighestPos(grid, colIndex);
          if (hpos == rowIndex) {
            html += `<span class=upper>[${col}]</span> `;
          } else {
            html += `[${col}] `;
          }
        }
      });
    }
  });

  el.innerHTML = html;
}

function Solve(moveBehavior) {
  const splitInput = input.split(/\r?\n\r?\n/).filter((p) => p.trim());
  const schemeText = splitInput[0];
  const instructionsText = splitInput[1];

  const columns = BuildColumns(schemeText);
  const instructions = BuildInstructions(instructionsText);

  ExecuteInstructions(columns, instructions, moveBehavior);

  const result = BuildResultString(columns);

  return result;
}

function BuildResultString(columns) {
  return Object.keys(columns)
    .map((x) => columns[x][0])
    .join('');
}

function ExecuteInstructions(columns, instructions, moveBehavior) {
  instructions.forEach((instruction) => {
    if (moveBehavior == MoveBehavior.MoveStack) {
      let stack = [];
      for (let i = 0; i < instruction.move; i++) {
        stack.push(columns[instruction.from][0]);
        columns[instruction.from].shift();
      }

      stack.reverse().forEach((crate) => {
        columns[instruction.to].unshift(crate);
      });
    } else if (moveBehavior == MoveBehavior.MoveCrate) {
      for (let i = 0; i < instruction.move; i++) {
        let x = columns[instruction.from][0];
        columns[instruction.from].shift();
        columns[instruction.to].unshift(x);
      }
    }
  });
}

function BuildInstructions(instructionsText) {
  const instructionsRegex = /move (\d+) from (\d+) to (\d+)/;
  let instructions = instructionsText.split(/\r?\n/);
  let moveInstructions = [];
  let i = 0;

  instructions.forEach((instruction) => {
    const x = instructionsRegex.exec(instruction);

    moveInstructions[i++] = new MoveInstruction(
      parseInt(x[1]),
      parseInt(x[2]),
      parseInt(x[3])
    );
  });

  return moveInstructions;
}

function BuildColumns(schemeText) {
  const rows = schemeText.split(/\r?\n/).slice(0, -1);
  let columns = {};

  rows.forEach((row) => {
    const rowColumns = ChunkableArray.from(row)
      .chunkBy(4)
      .map((x) => (x.length > 3 ? x.slice(0, -1) : x))
      .map((x) => x[1]);

    for (let colIndex = 0; colIndex < rowColumns.length; colIndex++) {
      let col = rowColumns[colIndex];
      if (columns[colIndex + 1] == null) {
        columns[colIndex + 1] = [];
      }

      columns[colIndex + 1].push(col);
    }
  });

  return columns;
}
