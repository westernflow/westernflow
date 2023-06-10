export const isLetter = (char: string) => {
  if (char.toUpperCase() != char.toLowerCase()) return true;
  return false;
};

export const stripSuffix = (course: string) => {
  // given a course in the form of "CS3300A", return "CS3300"
  if (
    course !== null &&
    course.length > 0 &&
    isLetter(course[course.length - 1])
  ) {
    return course.substring(0, course.length - 1);
  }
  return course;
};

export const removeWhiteSpace = (str: string) => {
  return str.replace(/\s/g, "");
};
