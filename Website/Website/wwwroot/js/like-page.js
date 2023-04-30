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

for (let i = 0; i < star.length; i++) {
    star[i].addEventListener('click', function () {
        r = event.target.value;
        itemId = event.target.parentElement.id.replace("rating_", "");
        setRating(itemId, r);
        let showValue = document.querySelector('#rating-value_' + itemId.toString());
        showValue.innerHTML = r + " out of 5"
    })
}

/*
let star = document.querySelectorAll('input');
let showValue = document.querySelector('#rating-value');

for (let i = 0; i < star.length; i++){
  star[i].addEventListener('click', function(){
    i = this.value;

    showValue.innerHTML = i + " out of 5"
  })
}


let star2 = document.querySelectorAll('input');
let showValue2 = document.querySelector('#rating-value2');

for (let i = 0; i < star2.length; i++){
  star2[i].addEventListener('click', function(){
    i = this.value;

    showValue2.innerHTML = i + " out of 5"
  })
}
let star3 = document.querySelectorAll('input');
let showValue3 = document.querySelector('#rating-value3');

for (let i = 0; i < star3.length; i++){
  star3[i].addEventListener('click', function(){
    i = this.value;

    showValue3.innerHTML = i + " out of 5"
  })
}
let star4 = document.querySelectorAll('input');
let showValue4 = document.querySelector('#rating-value4');

for (let i = 0; i < star4.length; i++){
  star4[i].addEventListener('click', function(){
    i = this.value;

    showValue4.innerHTML = i + " out of 5"
  })
}
let star5 = document.querySelectorAll('input');
let showValue5 = document.querySelector('#rating-value5');

for (let i = 0; i < star5.length; i++){
  star5[i].addEventListener('click', function(){
    i = this.value;

    showValue5.innerHTML = i + " out of 5"
  })
}
*/

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
