import { input } from "./input.js";
// import {AdventDirectory, AdventFile, BuildTree} from './node.js'
// const lines = input.split(/\r?\n/).filter((p) => p.trim());
// const root = BuildTree(lines);

const lines = input.split(/\r?\n/).filter((p) => p.trim());
let output = new Array();
let lastLineWasInstruction = false;

let curPath = "/";

for (let i = 0; i < lines.length; i++) {
  let line = lines[i];
  const lineClone = line + "";

  if (line.startsWith("$")) {
    if (lastLineWasInstruction == false && output.length > 1) {
      let last = output.pop();
      last += "</span>";
      last += "`";
      output.push(last);
    }

    line = line.substring(1);
    if (line == " ls"){
        line = " ls -al"
    }
    line = `\`<span class="user">[boop4198@reddit]</span> <span class="path">${curPath}</span>\n<span class="cmd">$</span>\`${line}`;

    if (line.startsWith("$ cd")) {
      var rng = Math.floor(Math.random() * 750) + 100;
      line += `^${rng}`;
    }

    output.push(line);
    lastLineWasInstruction = true;

    if (lineClone.startsWith("$ cd ")) {
      const p = lineClone.split(" ")[2];

      if (p === "..") {
        var split = curPath.split("/");
        split.pop();
        curPath = split.join("/");
      } else if (p === "/") {
        curPath = "/";
      } else {
        if (curPath !== "/") curPath += "/";
        curPath += p;
      }
    }
  } else {
    let last = output.pop();
    last += "\n";

    if (lastLineWasInstruction == true) {
      last += "<span class='output'>";
      last += "`";
    }

    const split = line.split(" ");
    let split0 = split[0];
    let split1 = split[1];

    if (split[0] == "dir") {
      split0 = "drwxr-xr-x\t0";
      split1 = '<span class="dir">' + split1 + "</span>/";
    } else {

      split0 = "-rw-r--r--\t" + split0;
    }

    line = split0 + "\t" + split1;

    last += line;
    output.push(last);
    lastLineWasInstruction = false;
  }
}

let strings = new Array();
strings.push(output.join("\n"));

var typed = new Typed("body", {
  strings: strings,
  typeSpeed: 60,
  fadeOut: true,
  fadeOutClass: "typed-fade-out",
  fadeOutDelay: 50,
});

setInterval(() => {
  window.scrollTo(0, document.body.scrollHeight);
}, 50);
