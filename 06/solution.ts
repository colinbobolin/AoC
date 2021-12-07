import * as fs from 'fs';

const filepath = "./data/data.txt";

const input = fs.readFileSync(filepath, 'utf-8').toString().split(",");

const log = (ele: any): any => {
    //console.log(ele);
    return ele;
}

const parseDecimal = (ele: string): number => parseInt(ele, 10);

const proliferate = (fish: number): number[] => 
    fish === 0 
        ? [6, 8] 
        : [fish - 1]

const processNextDay = (fishes: number[]) => {
    const nextDay =
        fishes.flatMap(proliferate);
    return nextDay;
}

const fishes = input.map(parseDecimal);

const recurse = (fn: (val: number[]) => number[], value: number[], n: number): number[] =>
    n === 0
        ? value
        : recurse(fn, fn(value), n-1);
        
const count: number[] = recurse(processNextDay, fishes, 80);
console.log(count.length);

interface FishDictionary {
    [Key: number]: number;
}

const convertArrayToFishDict = (fishes: number[]): FishDictionary => {
    const dict: FishDictionary = {};
    dict[0] = 0;
    dict[1] = 0;
    dict[2] = 0;
    dict[3] = 0;
    dict[4] = 0;
    dict[5] = 0;
    dict[6] = 0;
    dict[7] = 0;
    dict[8] = 0;
    fishes.forEach(fish => {
        dict[fish]++;
    });
    return dict;
}

const recurseDict = (fn: (val: FishDictionary) => FishDictionary, value: FishDictionary, n: number): FishDictionary =>
    n === 0
        ? value
        : recurseDict(fn, fn(value), n-1);

const proliferateDict = (fishDict: FishDictionary): FishDictionary => {
    const newDict: FishDictionary = {};
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
}

const initDict = convertArrayToFishDict(fishes);
const finalDict: FishDictionary = recurseDict(proliferateDict, initDict, 256);

var sum: number = 0;
Object.keys(finalDict).forEach(key => {
    sum += finalDict[key];
})

console.log(sum);