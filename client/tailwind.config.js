/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}"
  ],
  theme: {
    extend: {
      fontFamily: {
        'sans': ['ui-sans-serif', 'system-ui','sans-serif', ],
        'istok-web': ['"Istok Web"', 'sans-serif'],
      },
      lineHeight: {
        'relaxed': '1.5'
      },
      fontWeight: {
        'normal': '400'
      },
      colors: { //defining theme colors for use throughout code. to use, in className, use text-<color>. ex) text-dark-grey
        //main colors
        "lbsu-yellow": "#ECAA00",
        "lbsu-blue": "#006494",
        "lbsu-dark-blue": "#003554",

        //hover/secondary colors
        "light-gold": "#EBC464",
        "dark-gold": "#BC8700",
        "light-blue": "#0097DD",
        "dark-blue": "#003554",

        //greys
        "off-white": "#eaeaea",
        "input-white": "#FDF8F8",
        "light-grey-1": "#F8F8F8",
        "light-grey-1-hover": "#CAC9CC",
        "light-grey-2": "#E5E3E8",
        "med-grey": "#898989",
        "med-grey-2": "#7C8386",
        
        //alert colors
        "alert-red": "#EB2929",
        "alert-green": "#8EF060",
      },
    },
  },
  plugins: [],
}

