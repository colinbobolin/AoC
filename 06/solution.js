"use strict";
exports.__esModule = true;
var fs = require("fs");
var filepath = "./data/data.txt";
var input = fs.readFileSync(filepath, 'utf-8').toString().split(",");
var log = function (ele) {
    //console.log(ele);
    return ele;
};
var parseDecimal = function (ele) { return parseInt(ele, 10); };
var proliferate = function (fish) {
    return fish === 0
        ? [6, 8]
        : [fish - 1];
};
var processNextDay = function (fishes) {
    var nextDay = fishes.flatMap(proliferate);
    return nextDay;
};
var fishes = input.map(parseDecimal);
var recurse = function (fn, value, n) {
    return n === 0
        ? value
        : recurse(fn, fn(value), n - 1);
};
var count = recurse(processNextDay, fishes, 80);
console.log(count.length);
var convertArrayToFishDict = function (fishes) {
    var dict = {};
    dict[0] = 0;
    dict[1] = 0;
    dict[2] = 0;
    dict[3] = 0;
    dict[4] = 0;
    dict[5] = 0;
    dict[6] = 0;
    dict[7] = 0;
    dict[8] = 0;
    fishes.forEach(function (fish) {
        dict[fish]++;
    });
    return dict;
};
var recurseDict = function (fn, value, n) {
    return n === 0
        ? value
        : recurseDict(fn, fn(value), n - 1);
};
var proliferateDict = function (fishDict) {
    var newDict = {};
    newDict[0] = fishDict[1];
    newDict[1] = fishDict[2];
    newDict[2] = fishDict[3];
    newDict[3] = fishDict[4];
    newDict[4] = fishDict[5];
    newDict[5] = fishDict[6];
    newDict[6] = fishDict[7] + fishDict[0];
    newDict[7] = fishDict[8];
    newDict[8] = fishDict[0];
    return newDict;
};
var initDict = convertArrayToFishDict(fishes);
var finalDict = recurseDict(proliferateDict, initDict, 256);
var sum = 0;
Object.keys(finalDict).forEach(function (key) {
    sum += finalDict[key];
});
console.log(sum);
