import { createClient } from '@supabase/supabase-js';

import { Database } from './database.types';

const supabaseUrl = process.env.REACT_APP_SUPABASE_URL;
const supabaseKey = process.env.REACT_APP_SUPABASE_ANON_KEY;

if (!supabaseUrl || !supabaseKey) {
  throw new Error('Your supabase client stuff is undefined!!!');
}

export const supabase = createClient<Database>(supabaseUrl, supabaseKey);
