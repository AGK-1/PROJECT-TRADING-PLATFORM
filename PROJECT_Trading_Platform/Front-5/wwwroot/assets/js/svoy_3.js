var swiper = new Swiper(".mySwiper", {
    effect: "cube",
    grabCursor: true,
    cubeEffect: {
        shadow: true,
        slideShadows: true,
        shadowOffset: 20,
        shadowScale: 0.94,
    },
    pagination: {
        el: ".swiper-pagination",
    },
    autoplay: {
        delay: 400, // Delay between transitions in milliseconds (2.5 seconds in this case)
        disableOnInteraction: false,
    },
});


