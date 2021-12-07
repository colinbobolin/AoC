import * as fs from 'fs';

const filepath = "./data/test.txt";

const input = 
    fs.readFileSync(filepath, 'utf-8')
        .toString()
        .split(",")
        .map((ele) => parseInt(ele, 10));

const grp = [0, 0, 0, 0, 0, 0, 0, 0, 0];

input.forEach(fish => {
    grp[fish]++
});

for (var i=1; i<256; i++) {
    const parents = grp.shift();
    grp.push(parents);
    grp[6] += parents;
}

var sum: number = 0;
grp.forEach(cnt => {
    sum += cnt;
})

console.log(sum);