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
  segments: ["* Cliente 1", "* Cliente 2", "* Cliente 3", "* Cliente 4", "* Cliente 5", "* Cliente 6", "* Cliente 7"],
  rotation: 0,
  speed: 0,
  colors: ["#FF0000", "#FF7F00", "#FFFF00", "#00FF00", "#0000FF", "#4B0082", "#8B00FF"], // Colors for the segments
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

function spin() {
  wheel.speed = Math.random() * 0.5 + 0.1; // Random initial speed

  function animate() {
    wheel.rotation += wheel.speed;
    wheel.speed *= 0.99; // Gradual slowdown

    if (wheel.speed > 0.001) {
      requestAnimationFrame(animate);
    } else {
      wheel.speed = 0; // Stop completely when slow enough
    }

    draw();
  }

  animate();

  // Emit spin event to server
    connection.invoke("Spin").catch(err => console.error(err));
}

draw();
