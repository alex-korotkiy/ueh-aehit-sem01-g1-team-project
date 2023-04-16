/* =========================like-scroll============================= */

window.addEventListener("scroll", function(){
  var header = document.querySelector("header");
  header.classList.toggle("sticky", window.scrollY > 0)
})

/* =========================like-input============================= */
function showicon () {
  const inputContent = document.querySelector(".content_input").value;
  const inputClose = document.querySelector(".input_close");

  if(inputContent.length <= 0) document.body.classList.remove("active");
  else document.body.classList.add("active");

  inputClose.addEventListener("click", () =>{
    document.querySelector(".content_input").value = "";
    document.body.classList.remove("active")
  })
}
/* =========================like-star--rating============================= */

let star = document.querySelectorAll('input');
let showValue = document.querySelector('#rating-value');

for (let i = 0; i < star.length; i++){
  star[i].addEventListener('click', function(){
    i = this.value;

    showValue.innerHTML = i + " out of 5"
  })
}

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

  