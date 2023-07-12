document.addEventListener('DOMContentLoaded', function () {

  var swiper = new Swiper('.swiper-container', {
      slidesPerView: 1,
      spaceBetween: 10,
      // Responsive breakpoints
      breakpoints: {
        // when window width is >= 768px
        768: {
          slidesPerView: 4,
          spaceBetween: 40
        }
      },
      pagination: {
        el: '.swiper-pagination',
        clickable: true
      },
      navigation: {
        nextEl: '.swiper-button-next-unique',
        prevEl: '.swiper-button-prev-unique',
      },
      loop: true
    });

  const formularioContacto = document.getElementById("formulario-contacto");

  formularioContacto.addEventListener("submit", function (e) {
    e.preventDefault(); // Detiene el envío normal del formulario
    Swal.fire({
      title: 'Enviando',
      text: 'Tu mensaje está siendo enviado...',
      showConfirmButton: false,
      onBeforeOpen: () => {
        Swal.showLoading()
      }
    });
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

function sendWsp(texto){
  var encodedText = encodeURIComponent(texto); // codificar el texto para que pueda ser parte de una URL
  var url = "https://wa.me/+51923854963?text=" + encodedText;
  window.open(url, '_blank'); // abrir la URL en una nueva pestaña
}

$(document).ready(function () {
  $("#porque-link").click(function (e) {
    e.preventDefault();
    $("#beneficio-texto").html(`En JD Telecomunicaciones, contamos con una red de Fibra Óptica que te permite disfrutar de una conexión a internet segura y de
          acuerdo a tus necesidades. <br><br> No estás amarrado a contratos esclavizantes. <br><br> Te brindamos
          soporte técnico con soluciones eficientes. <br><br> Siepmre estamos a la vanguardia e innovando para brindarte
          el mejor servicio. <br><br> Además de proomciones exclusivas si realizas tus pagos a tiempo.`);
  });
  $("#cerca-link").click(function (e) {
    e.preventDefault();
    $("#beneficio-texto").html(`En JD Telecomunicaciones, estamos comprometidos con acercar la tecnología a ti. Como una de las principales empresas de internet por fibra óptica en la región, entendemos la importancia de tener una conexión rápida y fiable en todo momento. <br><br>Estamos cerca, siempre disponibles para asegurar que tu experiencia en línea sea la mejor. Nuestra proximidad nos permite entender y satisfacer tus necesidades de manera rápida y eficaz.`);

  });
});

$(document).ready(function () {
  $(".nav-link").click(function (e) {
    e.preventDefault();
    $(".nav-link").removeClass("active");
    $(this).addClass("active");
    const position = $(this).position();
    const width = $(this).outerWidth();
    $(".nav-underline").css({ left: position.left, width: width });
  });
  $("#porque-link").click();
});

function carouselSlide(direction) {
  const carousel = document.getElementById('plan-carousel');
  const scrollAmount = 200; // Adjust this value as needed

  if (direction === 'left') {
    carousel.scrollLeft -= scrollAmount;
  } else if (direction === 'right') {
    carousel.scrollLeft += scrollAmount;
  }
}

$(document).ready(function() {
  var $navbar = $('#navbar');
  var $cloneContainer = $('#cloneContainer');

  $navbar.find('a').each(function() {
    var $originalLink = $(this);
    var $cloneLink = $originalLink.clone();

    // Copia la posición y el tamaño del enlace original al enlace clonado
    var offset = $originalLink.offset();

    $cloneLink.css({
      position: 'absolute',
      top: offset.top,
      left: offset.left,
      width: $originalLink.outerWidth()+1,
      height: $originalLink.outerHeight()
    });
    // Agrega el enlace clonado al contenedor de clones
    $cloneContainer.append($cloneLink);
  });
  
});