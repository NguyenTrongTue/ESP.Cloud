/**
 * Uppercase first letter of each word in a string
 * @param {string} value - String to modify
 * @returns {string} String with first letter of each word capitalized
 */
export function upperCaseName(value = "") {
  return value
    .split(" ")
    .map((word) => word.charAt(0).toUpperCase() + word.slice(1))
    .join(" ");
}
export function convertDate(day) {
  let result = "";
  switch (day) {
    case 1:
      result = "Thứ hai";
      break;
    case 2:
      result = "Thứ ba";
      break;
    case 3:
      result = "Thứ tư";
      break;
    case 4:
      result = "Thứ năm";
      break;
    case 5:
      result = "Thứ sáu";
      break;
    case 7:
      result = "Thứ bảy";
      break;
    default:
      result = "Chủ nhật";
      break;
  }

  return result;
}
/**
 * Builds and returns a text representation of the open time based on the current time and the provided open and close times.
 *
 * @param {string} timeOpen - The opening time in HH:mm format
 * @param {string} timeClose - The closing time in HH:mm format
 * @return {string} The text representation of the open time
 */
export function buildOpenTimeText(timeOpen, timeClose) {
  if (timeOpen && timeClose) {
    const current = new Date();
    const open = new Date(
      current.getFullYear(),
      current.getMonth(),
      current.getDate(),
      timeOpen.split(":")[0],
      timeOpen.split(":")[1]
    );
    const close = new Date(
      current.getFullYear(),
      current.getMonth(),
      current.getDate(),
      timeClose.split(":")[0],
      timeClose.split(":")[1]
    );

    if (current >= open && current <= close) {
      const [house, minutes] = timeClose.split(":");
      return `Đang mở · Đóng cửa lúc ${house}h${minutes}`;
    } else if (current <= open) {
      const [house, minutes] = timeOpen.split(":");
      return `Đóng cửa · Mở cửa lúc ${house}h${minutes}`;
    } else {
      const [house, minutes] = timeOpen.split(":");
      return `Đóng cửa · Mở cửa vào ${house}h${minutes} ${convertDate(
        current.getDay() + 1
      )}`;
    }
  }
  return "";
}
