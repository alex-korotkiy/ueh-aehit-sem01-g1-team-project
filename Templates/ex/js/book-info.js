/* =========================star-rating============================= */
const allStars = document.querySelectorAll('.star');
let current_rating = document.querySelector('.current_rating');

allStars.forEach((star, i)=> {
  star.onclick = function(){
    let current_star_level = i + 1;
    current_rating.innerText = `${current_star_level} of 5`; 

    allStars.forEach((star, j)=> {
      if(current_star_level >= j +1){
        star.innerHTML= "&#9733";
      } else {
        star.innerHTML= "&#9734";
      }
    })
  }
})

/* =========================heading-scroll============================= */
window.addEventListener("scroll", function(){
  var header = document.querySelector("header");
  header.classList.toggle("sticky", window.scrollY > 0)
})

/* =========================like-slider============================= */

$(function(){

$('.like_inner').slick({
arrows:false,
dots: false,
slidesToShow: 7,
draggable: false,
waitForAnimate: true,
})

$('.slider-prev').on('click', function(e){
  e.preventDefault()
  $('.like_inner').slick('slickPrev')
})

$('.slider-next').on('click', function(e){
  e.preventDefault()
  $('.like_inner').slick('slickNext')
})


})
