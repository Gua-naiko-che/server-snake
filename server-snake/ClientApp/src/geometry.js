export function isPointInArray(point, array) {
  return array.some(s => isSamePoint(s, point));
}

export function isSamePoint(p1, p2) {
  if (!p1 || !p2) return false;
  return p1[0] === p2[0] && p1[1] === p2[1];
}