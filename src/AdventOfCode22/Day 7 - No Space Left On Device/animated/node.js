export class AdventDirectory {
  #children = new Array();

  constructor(name, parent) {
    this.name = name;
    this.parent = parent;
  }

  Directories() {
    return this.#children.filter((x) => x instanceof AdventDirectory);
  }
  Files() {
    return this.#children.filter((x) => x instanceof AdventFile);
  }
  Add(node) {
    this.#children.push(node);
  }
  GetSize() {
    return this.#children
      .map((x) => x.GetSize())
      .reduce((pv, cv) => pv + cv, 0);
  }
}

export class AdventFile {
  #size = 0;

  constructor(name, size, parent) {
    this.name = name;
    this.parent = parent;
    this.#size = size;
  }

  GetSize() {
    return this.#size;
  }
}

export function BuildTree(input) {
  let root = new AdventDirectory("/", null);
  let current = root;

  for (let i = 0; i < input.length; i++) {
    const line = input[i];
    if (!line) continue;

    if (line.startsWith("$ ls")) {
      // ignore
    } else if (line.startsWith("$ cd ..")) {
      // move upwards
      current = current.parent;
    } else if (line == "$ cd /") {
      // move to root
      current = root;
    } else if (line.startsWith("$ cd /")) {
      // split and move down the path
      // wasn't required though
    } else if (line.startsWith("$ cd ")) {
      // move "down" to directory
      let dirName = line.split(" ")[2];
      let dir = current.Directories().filter((x) => x.name === dirName);
      if (dir.length == 0) {
        current.Add(new AdventDirectory(dirName, current));
      }
      current = current.Directories().filter((x) => x.name === dirName)[0];
    } else if (line.startsWith("dir ")) {
      // add dir
      let dirname = line.substring(4);
      let dir = new AdventDirectory(dirname, current);
      current.Add(dir);
    } else if (!isNaN(line[0])) {
      // add file
      let splitLine = line.split(" ");
      let filesize = parseInt(splitLine[0]);
      let filename = splitLine[1];
      let file = new AdventFile(filename, filesize, current);
      current.Add(file);
    }
  }

  return root;
}
