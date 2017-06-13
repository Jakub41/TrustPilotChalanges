// THE REVIEWS DATA
getReviews(processReviews);
var reviews;
// Tells js where is the widget
var widget = $('#widget');

function processReviews(reviews_list) {
    // Reviews list
    reviews = reviews_list;
    // total number of reviewers
    var total = reviews.length;
    // initialize reviewSum variable to add all review ratings in it
    var reviewSum = 0;
    var carousalHtml = '';
    // loop through each review
    $(reviews).each(function(index, value) {
        // add current review's rating in global reviewSum variable
        reviewSum = reviewSum + parseInt(value.starRating);
        // review Body if text is bigger then 200 characters then add more functionality
        var reviewBody = value.reviewBody.length > 180 ? value.reviewBody.substr(0, 180) + '... <span class="showFullReview" onclick="javascript: showFullReviewBody($(this) ,' + index + ');">more</span>' : value.reviewBody;
        // create class for Bad, Middle and good review for styling purposes
        var reviewStarsClass = '';
        var reviewSmile = '';
        if (parseInt(value.starRating) > 2 && parseInt(value.starRating) <= 3) {
            reviewStarsClass = 'middle';
        } else if (parseInt(value.starRating) >= 0 && parseInt(value.starRating) <= 2) {
            reviewStarsClass = 'bad';
        } else {
            reviewStarsClass = 'good';
        }
        // Create HTML for carousal
        carousalHtml += '<div class="testimonial"><div class="top"><span class="name">' + value.fullName + '</span><div class="star-rating ' + reviewStarsClass + '"><span style="width:' + 20 * parseInt(value.starRating) + '%' + '" id="starRatingPresenter"><strong>' + value.starRating + '</strong> out of 5</span></div></div><div class="content"><span class="firstQuote fa fa-quote-left"></span><span class="main">' + reviewBody + '</span><span class="lastQuote fa fa-quote-right"></span></div><div class="footer">' + value.location + '</div></div>';
    });
    // calculate average rating
    var averageRating = reviewSum / total;
    // Calculate happiness percentage
    var averageRatePercentage = 20 * averageRating;
    // calculate average rating's class for styling purposes
    var reviewStarsClass = '';
    if (parseInt(averageRating) > 2 && parseInt(averageRating) <= 3) {
        reviewStarsClass = 'middle';
    } else if (parseInt(averageRating) >= 0 && parseInt(averageRating) <= 2) {
        reviewStarsClass = 'bad';
    } else {
        reviewStarsClass = 'good';
    }
    // Set dynamic text for banner
    var bannerText = '';
    var reviewSmile = '';
    var siteTrust = '';
    if (reviewStarsClass == 'good') {
        bannerText = 'We\'ve got our customers happy !';
        reviewSmile = '<i class="fa fa-smile-o fa-4x" aria-hidden="true"></i>';
        siteTrust = 'Trusted';
    } else if (reviewStarsClass == 'middle') {
        bannerText = 'Sometimes we\'re unable to fullfil needs of our customers';
        reviewSmile = '<i class="fa fa-meh-o fa-4x" aria-hidden="true"></i>';
        siteTrust = 'Keeps your eyes open';
    } else {
        bannerText = 'Customers blame us for everything';
        reviewSmile = '<i class="fa fa-frown-o fa-4x" aria-hidden="true"></i>';
        siteTrust = 'Poor';
    }
    // Create HTML for widget
    var html = '<div class="half first"><span class="widgetTitle ' + reviewStarsClass + '">' + reviewSmile + '<h3>' + bannerText + '</h3></span><div class="averageRatingInfo"><div class="star-rating ' + reviewStarsClass + '"><span style="width:' + averageRatePercentage + '%' + '" id="starRatingPresenter"><strong>' + averageRating + '</strong> out of 5</span></div><br><div class="averageRatingWrap"><span class="averageRating">' + averageRating + ' <span class="light">Trust Score</span></span></div><div class="reviewersWrap"><span class="reviewers">' + total + ' <span class="light">Reviewers</span></span></div><div class="clear"></div><hr /><div class="reviewFooterIndicator"><span class="fa fa-star ' + reviewStarsClass + '"></span>' + siteTrust + '</div></div></div><div class="half second"><div class="owl-carousel testimonials">' + carousalHtml + '</div></div>';
    // Apply widget html
    widget.html(html);
    // Initialize Carousel
    $('.owl-carousel').owlCarousel({
        loop: true,
        margin: 20,
        autoplay: true,
        autoplayTimeout: 6000,
        nav: true,
        navText: [
            '<i class="fa fa-chevron-left"></i>',
            '<i class="fa fa-chevron-right"></i>'
        ],
        autoplayHoverPause: true,
        responsive: {
            0: {
                items: 1
            },
            480: {
                items: 1
            },
            600: {
                items: 2
            },
            1000: {
                items: 2
            }
        }
    });
}

// Function to show all review text when clicked on more button
function showFullReviewBody(e, id) {
    console.log(id);
    var contentPlace = e.closest('span.main');
    var fullText = reviews[id].reviewBody + ' <span class=\'showFullReview\' onclick=\'javascript: showLessReviewBody($(this),' + id + ');\'\'>less</span>';
    contentPlace.html(fullText);
}

function showLessReviewBody(e, id) {
    console.log(id);
    var contentPlace = e.closest('span.main');
    var reviewBody = reviews[id].reviewBody.substr(0, 180) + '... <span class="showFullReview" onclick="javascript: showFullReviewBody($(this) ,' + id + ');">more</span>';
    contentPlace.html(reviewBody);
}
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
        url: 'API/reviews.json',
        //url: 'API/badreviews.json',
        //url: 'API/goodreviews.json',
        success: function(data) {
            reviews = data;
        }

    }).done(re);
}