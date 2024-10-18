/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: 'class',
  content: [
    "./src/**/*.{vue,js,ts,jsx,tsx}",
    "./node_modules/flowbite/**/*.js",
    "./index.html",
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require('flowbite/plugin')({
        datatables: true,
    }),
  ]
}
