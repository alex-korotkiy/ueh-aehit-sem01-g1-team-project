/* =========================main-scroll============================= */

window.addEventListener("scroll", function(){
  var header = document.querySelector("header");
  header.classList.toggle("sticky", window.scrollY > 0)
})

/* =========================main-input============================= */
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
/* =========================main-sort by============================= */

function showList() {
  var showList = document.getElementById('show_list');

  if(showList.style.display === "none") {
    showList.style.display = "block"
  } else {
    showList.style.display = "none";
  }
}

const btnActive = document.querySelectorAll('.btn_show-list');

btnActive.forEach(btnActive => {
  btnActive.addEventListener('click', function(){
    document.querySelector('.active')?.classList.remove('active')
    btnActive.classList.add('active');
  })
})

/* =========================main-slider============================= */

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

  
