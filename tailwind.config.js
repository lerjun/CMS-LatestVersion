/** @type {import('tailwindcss').Config} */
module.exports = {

    content: [
      "./Pages/**/*.cshtml",
      "./Views/**/*.cshtml"
    ],

  theme: {
    fontSize: {
      xsm:['7px','10px'],
      sm: ['14px', '20px'],
      base: ['16px', '24px'],
      lg: ['20px', '28px'],
      xl: ['24px', '32px'],
  },
  extend: {
      colors: {
          clifford: '#da373d',
      },
      backgroundImage: {
        'hero-pattern': "url('/image/ODECCI.png')",
      }
  },
  screens: {
    '2xsm':'50px',
      xsm:'320px',
      sm: '480px',
      md: '768px',
      lg: '976px',
      xl: '1440px',
  },
  },
  plugins: [],
}
