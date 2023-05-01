document.addEventListener('DOMContentLoaded', function () {
  var swiper = new Swiper('.swiper-container', {
    slidesPerView: 1,
    spaceBetween: 10,
    loop: true, // Habilita el loop
    autoplay: {
      delay: 3000, // Cambia el slide cada 3 segundos
      disableOnInteraction: false, // Continúa el autoplay después de interactuar con el carrusel
    },
    navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
    },
    pagination: {
      el: '.swiper-pagination',
      clickable: true,
    },
  });
});