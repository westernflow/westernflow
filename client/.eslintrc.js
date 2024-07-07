module.exports = {
  parser: '@typescript-eslint/parser',
  extends: [
    "airbnb-base",
    "airbnb-typescript/base",
    'plugin:@typescript-eslint/recommended',
    'plugin:react/recommended',
    'plugin:prettier/recommended',
    'prettier',
  ],
  parserOptions: {
    project: './tsconfig.json',
    sourceType: 'module',
    ecmaVersion: 2018,
    ecmaFeatures: {
      jsx: true,
    },
  },
  rules: {
    '@typescript-eslint/naming-convention': 'off',
    '@typescript-eslint/no-unused-vars': 'off',
    '@typescript-eslint/explicit-function-return-type': 'off',
    '@typescript-eslint/default-param-last': 'off',
    '@typescript-eslint/no-empty-interface': 'off',
    '@typescript-eslint/no-explicit-any': 'off',
    'import/extensions': 'off',
    'import/first': 'error',
    'import/newline-after-import': 'error',
    'import/no-duplicates': 'error',
    'import/prefer-default-export': 'off',
    'linebreak-style': ['error', 'unix'],
    'max-len': [0, 80, 2],
    'no-case-declarations': 'off',
    'no-console': 'off',
    'no-nested-ternary': 'off',
    'no-param-reassign': 'off',
    'no-restricted-syntax': 'off',
    'no-underscore-dangle': 'off',
    'object-curly-newline': 'off',
    'prefer-spread': 'off',
    'react/display-name': 'off',
    'react/prefer-default-export': 'off',
    'react/prop-types': 'off',
    'sort-imports': 'off',
  },
  settings: {
    'import/resolver': {
      node: {
        extensions: ['.js', '.jsx', '.ts', '.tsx'],
      },
    },
    react: {
      version: 'detect',
    },
  },
  env: {
    browser: true,
  },
};
