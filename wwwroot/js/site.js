
    document.addEventListener('DOMContentLoaded', function () {
        var swiper = new Swiper('.swiper', {
        loop: true, // Enable looping of slides
    autoplay: {
        delay: 2000, // Autoplay delay in milliseconds
    disableOnInteraction: false, // Keep autoplay running after user interactions
            },
    pagination: {
        el: '.swiper-pagination',
    clickable: true, // Allow clicking on pagination bullets
            },
    navigation: {
        nextEl: '.swiper-button-next', // Selector for the next button
    prevEl: '.swiper-button-prev', // Selector for the previous button
            },
    scrollbar: {
        el: '.swiper-scrollbar', // Selector for the scrollbar
    draggable: true, // Allow dragging of the scrollbar
            },
        });
    });

