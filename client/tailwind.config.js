/** @type {import('tailwindcss').Config} */
module.exports = {
	content: ["./src/**/*.{js,jsx,ts,tsx}"],
	theme: {
		extend: {
			fontFamily: {
				sans: ["Roboto", "ui-sans-serif", "system-ui"],
			},
			colors: {
				'wf-blue': '#bfdbfe', // from-blue-200
				'wf-cyan': '#a5f3fc', // via-cyan-200
				'wf-purple': '#ddd6fe', // to-purple-200
			},
			backgroundImage: {
				'wf': 'linear-gradient(to right, #b3e6ff, #c1d7ff, #d4d4fc)',
			},
		},
	}
};
