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