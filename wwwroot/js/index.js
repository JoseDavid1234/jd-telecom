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

  const formularioContacto = document.getElementById("formulario-contacto");

  formularioContacto.addEventListener("submit", function (e) {
    e.preventDefault(); // Detiene el envío normal del formulario

    // Supongamos que tienes una función enviarFormulario() que envía los datos del formulario y devuelve una promesa
    enviarFormulario(new FormData(formularioContacto)).then((respuesta) => {
      // Suponemos que la función enviarFormulario() resuelve la promesa con un objeto que tiene una propiedad "exitoso" si el envío fue exitoso
      if (respuesta.exitoso) {
        Swal.fire(
          'Enviado',
          'Tu mensaje ha sido enviado exitosamente',
          'success'
        )
      }
    });
  });

  function enviarFormulario(formData) {
    // Aquí deberías implementar el envío de los datos del formulario.
    console.log("Hola");
    // Este es solo un ejemplo y no funcionará en un entorno real.
    return fetch("/Home/EnviarFormulario", {
      method: "POST",
      body: formData,
    })
      .then((response) => response.json())
      .catch((error) => {
        console.error("Error:", error);
      });
  }
});
//cards
var x;
var $cards = $(".card");
var $style = $(".hover");

$cards
  .on("mousemove touchmove", function(e) { 
    // normalise touch/mouse
    var pos = [e.offsetX,e.offsetY];
    e.preventDefault();
    if ( e.type === "touchmove" ) {
      pos = [ e.touches[0].clientX, e.touches[0].clientY ];
    }
    var $card = $(this);
    // math for mouse position
    var l = pos[0];
    var t = pos[1];
    var h = $card.height();
    var w = $card.width();
    var px = Math.abs(Math.floor(100 / w * l)-100);
    var py = Math.abs(Math.floor(100 / h * t)-100);
    var pa = (50-px)+(50-py);
    // math for gradient / background positions
    var lp = (50+(px - 50)/1.5);
    var tp = (50+(py - 50)/1.5);
    var px_spark = (50+(px - 50)/7);
    var py_spark = (50+(py - 50)/7);
    var p_opc = 20+(Math.abs(pa)*1.5);
    var ty = ((tp - 50)/2) * -1;
    var tx = ((lp - 50)/1.5) * .5;
    // css to apply for active card
    var grad_pos = `background-position: ${lp}% ${tp}%;`
    var sprk_pos = `background-position: ${px_spark}% ${py_spark}%;`
    var opc = `opacity: ${p_opc/100};`
    var tf = `transform: rotateX(${ty}deg) rotateY(${tx}deg)`
    // need to use a <style> tag for psuedo elements
    var style = `
      .card:hover:before { ${grad_pos} }  /* gradient */
      .card:hover:after { ${sprk_pos} ${opc} }   /* sparkles */ 
    `
    console.log(grad_pos);
    // set / apply css class and style
    $cards.removeClass("active");
    $card.removeClass("animated");
    $card.attr( "style", tf );
    $style.html(style);
    if ( e.type === "touchmove" ) {
      return false; 
    }
    clearTimeout(x);
  }).on("mouseout touchend touchcancel", function() {
    // remove css, apply custom animation on end
    var $card = $(this);
    $style.html("");
    $card.removeAttr("style");
    x = setTimeout(function() {
      $card.addClass("animated");
    },2500);
  });


  function sendWsp(texto){
    var encodedText = encodeURIComponent(texto); // codificar el texto para que pueda ser parte de una URL
    var url = "https://wa.me/+51991471172?text=" + encodedText;
    window.open(url, '_blank'); // abrir la URL en una nueva pestaña
}