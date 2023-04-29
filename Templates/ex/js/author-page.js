/* =========================author-scroll============================= */

window.addEventListener("scroll", function(){
  var header = document.querySelector("header");
  header.classList.toggle("sticky", window.scrollY > 0)
})

/* =========================author-slider============================= */

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

/* =========================author-popup============================= */

  document.getElementById("about_read-more").addEventListener("click", function(){
    document.querySelector(".about_popup").style.display = "flex";
  })

  document.querySelector(".close_icon").addEventListener("click", function(){
    document.querySelector(".about_popup").style.display = "none";
  })