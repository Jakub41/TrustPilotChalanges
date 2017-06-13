# TrustPilot - FrontEnd Challenge
## Intro
This is my Frontend solution to the Reviews Widget Challenge

### Requirements

It's internally driven by [Gulp](http://gulpjs.com).

[Node](https://nodejs.org) needs to be installed in your system.

[Bower](https://bower.io/) the package manager

[Sass/Scss](http://sass-lang.com/) is used as a preprocessor and compiled into CSS  


### Run the solution
Follow the next step for run the project:

Execute commands from terminal/console

1. (sudo) npm install
2. gulp 
3. gulp serve   -> Development
4. gulp build   -> Production


### How it works
As first when Gulp is running, it is inizializing the project and compiles the SCSS file into a CSS file, readable for the browser. It is also optimizing the CSS and JS file for performance with minification.

The index.html loads the required assets and markdown needed for javascript to operate and create the widget. 

The main.scss provides styles and the main.js creates the widget. 

The main.js gets the reviews in an array (provided by TrustPilot). Then, loops through it and creates HTML for the widget and then pushes it on the HTML. 

All just operates in a second without any Loading problems. It is a runtime application and does everything when page is loaded.

Last, the banner is changing when the average of the reviews change. As an example if the reviews are positive in average so the banner becomes green.
Please test it out changing this part of the code inside the main.js:

```javascript
/**
 * == Function to get reviews ==
 * to see the banner change based on the average of the reviews,
 * un-comment the desired reviews .json file
 * Good reviews -> goodreviews.json
 * Mid  reviews -> reviews.json
 * Bad  reviews -> badreviews.json
 **/
function getReviews(re) {
    $.ajax({
        //url: 'API/reviews.json'
        //url: 'API/badreviews.json'
        url: 'API/goodreviews.json',
        success: function(data) {
            reviews = data;
        }

    }).done(re);
}
```

### Extra Bonus
Check the console while running the project

&#128007;&#128007;&#128007;&#128007;&#128048;&#128048;&#128048;&#128048;
 
