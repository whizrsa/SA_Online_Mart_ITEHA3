//Main carousel
$('.carousel').carousel({
    interval: 3000
})


// Latest Products swiper
var swiper = new Swiper(".mySwiper", {
    slidesPerView: 3,
    spaceBetween: 20,
    loop: false,
    grabCursor: true,
    freeMode: true,
    breakpoints: {
        320: { slidesPerView: 1.2 },
        576: { slidesPerView: 2 },
        768: { slidesPerView: 2.5 },
        992: { slidesPerView: 3 },
    },
});