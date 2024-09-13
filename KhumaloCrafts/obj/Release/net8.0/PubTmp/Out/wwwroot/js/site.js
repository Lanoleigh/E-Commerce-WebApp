let slideIndex = 0;
slideShow();
/* https://www.w3schools.com/howto/howto_js_slideshow.asp */
function slideShow() {
    let x;
    let slides = document.getElementsByClassName("Slides");
    for (x = 0; x < slides.length; x++) {
        slides[x].style.display = "none";
    }
    slideIndex++;
    if (slideIndex > slides.length) { slideIndex = 1 }
    slides[slideIndex - 1].style.display = "block";
    slides[slideIndex - 1].style.transition = "ease-in-out";
    setTimeout(slideShow, 4000); // Changes picture every 4 seconds
}