function setCache(key, value) {
  try {
    localStorage.setItem(key, JSON.stringify(value));
  } catch (e) {
    console.log(e);
  }
}

function getCache(key) {
  return JSON.parse(localStorage.getItem(key));
}
function deleteCache(key) {
  try {
    localStorage.removeItem(key);
  } catch (e) {
    console.log(e);
  }
}
export const cache = { setCache, getCache, deleteCache };
