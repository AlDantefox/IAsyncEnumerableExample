//for c# like await sleep(...)
const sleep = (ms: number) => new Promise((r) => setTimeout(r, ms));
export { sleep };
