document.addEventListener('DOMContentLoaded', function () {
  new Splide('#splide', {
    type: 'loop',
    perPage: 1,
    arrows: true,
    autoplay: true, // Habilitar reproducción automática
    interval: 3000, // Intervalo de tiempo en milisegundos entre cada transición
    transition: 'fade'
  }).mount();
});