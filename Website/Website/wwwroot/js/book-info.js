/* =========================star-rating============================= */

let star = document.querySelectorAll('input');
let showValue = document.querySelector('#rating-value');

for (let i = 0; i < star.length; i++){
  star[i].addEventListener('click', function(){
    i = this.value;

    showValue.innerHTML = i + " out of 5"
  })
}

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
