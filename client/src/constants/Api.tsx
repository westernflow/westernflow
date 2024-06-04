/* Global IDs */
export const GOOGLE_ANALYTICS_ID = 'UA-35073503-1';
export const GOOGLE_APP_ID = '292230821846-cogmasv1s0rbvhp0dr886vik2c73etb3';
export const FACEBOOK_APP_ID = '219309734863464';

/* Base endpoints */
const LOCAL_GRAPHQL_ENDPOINT = 'http://localhost:5095/graphql';
const LOCAL_BACKEND_ENDPOINT = 'http://localhost:5095';

export const GRAPHQL_ENDPOINT =
  process.env.NODE_ENV === 'development' ? LOCAL_GRAPHQL_ENDPOINT : '/graphql';

export const BACKEND_ENDPOINT =
  process.env.NODE_ENV === 'development' ? LOCAL_BACKEND_ENDPOINT : '/api';

/* Auth */
export const EMAIL_AUTH_LOGIN_ENDPOINT = '/auth/email/login';
export const EMAIL_AUTH_REGISTER_ENDPOINT = '/auth/email/register';
export const GOOGLE_AUTH_ENDPOINT = '/auth/google/login';
export const FACEBOOK_AUTH_ENDPOINT = '/auth/facebook/login';
export const AUTH_REFRESH_ENDPOINT = '/auth/refresh';

/* Reset password */
export const RESET_PASSWORD_KEY_EMAIL_ENDPOINT =
  '/auth/forgot-password/send-email';
export const RESET_PASSWORD_VERIFY_KEY_ENDPOINT =
  '/auth/forgot-password/verify';
export const RESET_PASSWORD_RESET_PASSWORD_ENDPOINT =
  '/auth/forgot-password/reset';

/* Data upload */
export const SCHEDULE_PARSE_ENDPOINT = '/parse/schedule';
export const TRANSCRIPT_PARSE_ENDPOINT = '/parse/transcript';

/* Search */
export const SEARCH_DATA_ENDPOINT = '/courses';

/* Calendar */
export const CALENDAR_EXPORT_ENDPOINT = (secretId: string) =>
  `/calendar/${secretId}.ics`;
export const GOOGLE_CALENDAR_URL = `https://calendar.google.com/calendar/r?cid=`;
