module.exports = {
  schema: 'http://localhost:5095/graphql',
  documents: ['./src/hotchocolate/**/*.tsx'],
  overwrite: true,
  generates: {
    './src/hc-generated/graphql.tsx': {
      plugins: [
        'typescript',
        'typescript-operations',
        'typescript-react-apollo',
      ],
      config: {
        skipTypename: false,
        withHooks: true,
        withHOC: false,
        withComponent: false,
      },
    },
  },
};
