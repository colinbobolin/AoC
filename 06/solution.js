"use strict";
exports.__esModule = true;
var fs = require("fs");
var filepath = "./data/test.txt";
var input = fs.readFileSync(filepath, 'utf-8')
    .toString()
    .split(",")
    .map(function (ele) { return parseInt(ele, 10); });
var grp = [0, 0, 0, 0, 0, 0, 0, 0, 0];
input.forEach(function (fish) {
    grp[fish]++;
});
for (var i = 1; i < 256; i++) {
    var parents = grp.shift();
    grp.push(parents);
    grp[6] += parents;
}
var sum = 0;
grp.forEach(function (cnt) {
    sum += cnt;
});
console.log(sum);
