// Connect to your SignalR server
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/spinhub") // Path to your hub
    .build();

// Start the connection
connection.start()
    .catch(err => console.error(err.toString()));

// Listen for spin events
connection.on("Spin", function () {
    spin();
});

const canvas = document.getElementById("wheelcanvas");
const context = canvas.getContext("2d");

const wheel = {
  radius: 400,
  segments: [],
  rotation: 0,
  speed: 0,
  colors: [], // Colors for the segments
};

function draw() {
  context.clearRect(0, 0, canvas.width, canvas.height);

  for (let i = 0; i < wheel.segments.length; i++) {
    context.beginPath();
    context.moveTo(canvas.width / 2, canvas.height / 2);
    context.arc(
      canvas.width / 2,
      canvas.height / 2,
      wheel.radius,
      ((Math.PI * 2) / wheel.segments.length) * i + wheel.rotation,
      ((Math.PI * 2) / wheel.segments.length) * (i + 1) + wheel.rotation
    );
    context.lineTo(canvas.width / 2, canvas.height / 2);
    context.closePath();
    context.fillStyle = wheel.colors[i % wheel.colors.length]; // Use color
    context.fill();
    context.stroke();
    context.fillStyle = "#ffffff"; // Text color
    context.fillText(
      wheel.segments[i],
      canvas.width / 2 + Math.cos(((Math.PI * 2) / wheel.segments.length) * i + (Math.PI * 2) / wheel.segments.length / 2 + wheel.rotation) * (wheel.radius / 1.2),
      canvas.height / 2 + Math.sin(((Math.PI * 2) / wheel.segments.length) * i + (Math.PI * 2) / wheel.segments.length / 2 + wheel.rotation) * (wheel.radius / 1.2)
    );

  }
}

let isSpinning = false;

function spin(local) {

  if (isSpinning) {
    return; // Ignore if the wheel is already spinning
  }

  isSpinning = true; // Set spinning to true

  if (local) {
    var speed = Math.random()*0.5+0.1;
    wheel.speed = speed;
    connection.invoke("Spin",speed).catch(err => console.error(err));
  }

  function animate() {
    wheel.rotation += wheel.speed;
    wheel.speed *= 0.99; // Gradual slowdown

    if (wheel.speed > 0.001) {
      requestAnimationFrame(animate);
    } else {
      wheel.speed = 0; // Stop completely when slow enough
      isSpinning = false; // Set spinning to false
    }
    draw();
  }

  animate();
}

// Listen for spin events
connection.on("Spin", function (clients,speed) {
  wheel.segments=clients
  wheel.speed=speed
  spin(false); // This is a remote spin, not a local one
});

draw();

// Listen for clients updated events
connection.on("ClientsUpdated", function (clients) {
  // Update wheel segments with client names
  wheel.segments = clients.map(client => client.nombreCompleto); // Assuming 'nombreCompleto' is a property of your client objects

  // Optionally, update colors here
  wheel.colors = clients.map(() => getRandomColor());

  draw(); // Redraw the wheel
});

// When the page loads
window.onload = function() {
    // Get clients from server
    fetch('/Sorteos/GetClients')
    .then(response => response.json())
    .then(clients => {
        wheel.segments = clients.map(client => client.nombreCompleto); // Or client.name, or however you want to represent the client
        wheel.colors = clients.map(() => getRandomColor()); // You need to implement getRandomColor
        draw();
    })
    .catch(error => console.error('Error:', error));
};

function getRandomColor(){
  var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

document.addEventListener('DOMContentLoaded', function () {

  const formularioContacto = document.getElementById("formulario-sorteos");

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
    enviarFormularioSorteo(new FormData(formularioContacto)).then((respuesta) => {
      // Suponemos que la función enviarFormulario() resuelve la promesa con un objeto que tiene una propiedad "exitoso" si el envío fue exitoso
      if (respuesta.exitoso) {
        Swal.fire(
          'Enviado',
          'Tu mensaje ha sido enviado exitosamente',
          'success'
        )
      }else {
        Swal.fire(
          'Error',
          'No encontramos tu dni',
          'error'
        );
      }
    });
  });

  function enviarFormularioSorteo(formData) {
    // Este es solo un ejemplo y no funcionará en un entorno real.
    return fetch("/Sorteos/ProcessDni", {
      method: "POST",
      body: formData,
    })
      .then((response) => response.json())
      .catch((error) => {
        console.error("Error:", error);
      });
  }
});